IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_MisActividadesPendientes') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_MisActividadesPendientes
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_MisActividadesPendientes
(
	 @ServerHostPath varchar(512)
	 ,@ModuleId varchar(4)
	 ,@IdResponsable int
)
AS

/*************************** Variables De Prueba **********************/
--declare @ServerHostPath varchar(512),@ModuleId varchar(4), @IdResponsable int

--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '23'
--set @IdResponsable = 1
/***********************************************************************/

-- Definiendo variables de trabajo
declare	@TblReport table
		(
			IdActividad				decimal(18,0)
			,IdReclamo				decimal(18,0)
			,TipoReclamo			varchar(50)
			,NumeroReclamo			varchar(50)
			,Descripcion			varchar(max)
			,Fecha					datetime
			,Actividad				varchar(512)
			,DescripcionProducto	varchar(1024)
			,DescripcionServicio	varchar(1024)
			,DescripcionReclamo		varchar(1024)
			,Estado					varchar(50)
			,Autor					varchar(max)			
			,UrlActividad			varchar(max)
		)

insert	into
		@TblReport
		(
			IdActividad
			,IdReclamo
			,TipoReclamo
			,NumeroReclamo
			,Descripcion
			,Actividad
			,Fecha
			,Estado
			,Autor
			,DescripcionProducto
			,DescripcionServicio
			,UrlActividad
		)
select	distinct
		actividad.IdActividad
		,actividad.IdReclamo
		,tipoReclamo.Nombre
		,reclamo.NumeroReclamo
		,actividad.Descripcion
		,actividades.Nombre
		,actividad.Fecha
		,actividad.Estado
		,autor.Nombres
		,producto.PRODUCTO + ' (' + producto.CATEGORIA + ') - Cliente: ' + cliente.CLIENTE
		,categorias.Nombre + ' - Area: ' + categorias.Area
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmAdminActividadReclamo.aspx?ModuleId=' + @ModuleId + '&IdActividad=' + cast(actividad.IdActividad as varchar(18))
from	TBL_ModuloReclamos_Actividades actividad with(nolock)
		inner join TBL_ModuloReclamos_Reclamo reclamo with(nolock)
			on actividad.IdReclamo = reclamo.IdReclamo
		inner join TBL_Admin_Usuarios autor with(nolock)
			on actividad.CreateBy = autor.IdUser
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
where	actividad.IdUsuarioAsignacion = @IdResponsable
		and actividad.Estado not in ('Realizada','Cancelada')

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
		,Descripcion
		,Fecha
		,Actividad
		,DescripcionProducto
		,DescripcionServicio
		,DescripcionReclamo	
		,Estado
		,Autor
		,UrlActividad
from	@TblReport