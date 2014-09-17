IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_GestionActividades') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_GestionActividades
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_GestionActividades
(
	 @ServerHostPath varchar(512)
	 ,@ModuleId varchar(4)
	 ,@UsuarioCreacion int
	 ,@dateFrom datetime
	 ,@dateEnd datetime
	 ,@NoReclamo varchar(50)
	 ,@Cliente varchar(512)
	 ,@Producto varchar(512)
	 ,@Servicio varchar(512)
	 ,@FromView varchar(50)
)
AS

/*************************** Variables De Prueba **********************/
--declare @ServerHostPath varchar(512),@ModuleId varchar(4),@dateFrom datetime,@dateEnd datetime,@NoReclamo varchar(50),@Cliente varchar(512),@Producto varchar(512),@Servicio varchar(512)

--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '23'
--set @dateFrom = '2014-01-01'
--set @dateEnd = '2014-12-01'
--set @NoReclamo = ''
--set @Cliente = ''
--set @Producto = ''
--set @Servicio = ''
/***********************************************************************/

-- Definiendo variables de trabajo
declare	@TblReport table
		(
			IdActividad				decimal(18,0)
			,IdReclamo				decimal(18,0)
			,TipoReclamo			varchar(50)
			,NumeroReclamo			varchar(50)
			,FechaReclamo			datetime
			,Descripcion			varchar(max)
			,Fecha					datetime
			,IdMes					int
			,Mes					varchar(50)
			,Actividad				varchar(512)
			,Producto				varchar(1024)
			,Cliente				varchar(1024)
			,Categoria				varchar(1024)
			,DescripcionProducto	varchar(1024)
			,DescripcionServicio	varchar(1024)
			,DescripcionReclamo		varchar(1024)
			,Estado					varchar(50)
			,Autor					varchar(1024)			
			,UsuarioAsignacion		varchar(1024)	
			,UrlActividad			varchar(2048)
		)

insert	into
		@TblReport
		(
			IdActividad
			,IdReclamo
			,TipoReclamo
			,NumeroReclamo
			,FechaReclamo
			,Descripcion
			,Actividad
			,Producto
			,Cliente
			,Categoria
			,Fecha
			,IdMes
			,Mes
			,Estado
			,Autor
			,UsuarioAsignacion
			,DescripcionProducto
			,DescripcionServicio
			,UrlActividad
		)
select	distinct
		actividad.IdActividad
		,actividad.IdReclamo
		,tipoReclamo.Nombre
		,reclamo.NumeroReclamo
		,reclamo.FechaReclamo
		,actividad.Descripcion
		,actividades.Nombre
		,isnull(producto.PRODUCTO,'')
		,isnull(cliente.CLIENTE, '')
		,isnull(categorias.Nombre,'')
		,actividad.Fecha
		,month(actividad.Fecha)
		,case month(actividad.Fecha)
				when 1
				then 'Enero'
				when 2
				then 'Febrero'
				when 3
				then 'Marzo'
				when 4
				then 'Abril'
				when 5
				then 'Mayo'
				when 6
				then 'Junio'
				when 7
				then 'Julio'
				when 8
				then 'Agosto'
				when 9
				then 'Septiembre'
				when 10
				then 'Octubre'
				when 11
				then 'Noviembre'
				when 12
				then 'Diciembre'
			end	
		,actividad.Estado
		,autor.Nombres
		,asignado.Nombres
		,producto.PRODUCTO + ' (' + producto.CATEGORIA + ') - Cliente: ' + cliente.CLIENTE
		,categorias.Nombre + ' - Area: ' + categorias.Area
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmAdminActividadReclamo.aspx?ModuleId=' + @ModuleId + '&IdActividad=' + cast(actividad.IdActividad as varchar(18)) + '&from=' + @FromView
from	TBL_ModuloReclamos_Actividades actividad with(nolock)
		inner join TBL_ModuloReclamos_Reclamo reclamo with(nolock)
			on actividad.IdReclamo = reclamo.IdReclamo
		inner join TBL_Admin_Usuarios autor with(nolock)
			on actividad.CreateBy = autor.IdUser
		inner join TBL_Admin_Usuarios asignado with(nolock)
			on actividad.IdUsuarioAsignacion = asignado.IdUser
		inner join TBL_ModuloReclamos_TipoReclamo tipoReclamo with(nolock)
			on reclamo.IdTipoReclamo = tipoReclamo.IdTipoReclamo
		inner join TBL_ModuloReclamos_ActividadesReclamo actividades with(nolock)
			on actividad.IdActividadReclamo = actividades.IdActividad
		left join Sika_SolverNET..Productos producto with(nolock)
			on reclamo.CodigoProducto = producto.CODIGOPRODUCTO
		left join Sika_SolverNET..Clientes cliente with(nolock)
			on reclamo.CodigoCliente = cliente.CODIGOCLIENTE
		left join TBL_ModuloReclamos_CategoriasReclamo categorias with(nolock)
			on reclamo.IdCategoriaReclamo = categorias.IdCategoriaReclamo
where	year(actividad.Fecha) >= year(@dateFrom)
		and year(actividad.Fecha) <= year(@dateEnd)
		and month(actividad.Fecha) >= month(@dateFrom)
		and month(actividad.Fecha) <= month(@dateEnd)
		and reclamo.NumeroReclamo like '%' + case @NoReclamo when '' then reclamo.NumeroReclamo else @NoReclamo end + '%'
		and (
				isnull(cliente.CLIENTE,'') like '%' + case @Cliente when '' then isnull(cliente.CLIENTE,'') else @Cliente end + '%'
			or
				isnull(cliente.CODIGOCLIENTE,'') like '%' + case @Cliente when '' then isnull(cliente.CODIGOCLIENTE,'') else @Cliente end + '%'
			)
		and (
				isnull(producto.PRODUCTO,'') like '%' + case @Producto when '' then isnull(producto.PRODUCTO,'') else @Producto end + '%'
			or
				isnull(producto.CODIGOPRODUCTO,'') like '%' + case @Producto when '' then isnull(producto.CODIGOPRODUCTO,'') else @Producto end + '%'
			)
		and (
				isnull(categorias.Nombre,'') like '%' + case @Servicio when '' then isnull(categorias.Nombre,'') else @Servicio end + '%'
			or
				isnull(categorias.SubCategoria,'') like '%' + case @Servicio when '' then isnull(categorias.SubCategoria,'') else @Servicio end + '%'
			)

update	@TblReport
set		DescripcionReclamo = DescripcionProducto
where	TipoReclamo = 'Producto'

update	@TblReport
set		DescripcionProducto = ''
where	DescripcionProducto is null

update	@TblReport
set		DescripcionReclamo = DescripcionServicio
where	TipoReclamo = 'Servicio'

update	@TblReport
set		DescripcionServicio = ''
where	DescripcionServicio is null

select	IdActividad
		,IdReclamo
		,TipoReclamo
		,NumeroReclamo
		,FechaReclamo
		,Descripcion
		,Fecha
		,IdMes
		,Mes
		,Actividad
		,Producto
		,Cliente
		,Categoria
		,DescripcionProducto
		,DescripcionServicio
		,DescripcionReclamo	
		,Estado
		,Autor
		,UsuarioAsignacion
		,UrlActividad
from	@TblReport