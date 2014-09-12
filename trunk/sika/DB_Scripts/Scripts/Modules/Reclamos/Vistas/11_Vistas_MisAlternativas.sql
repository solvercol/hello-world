IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_MisAlternativas') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_MisAlternativas
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_MisAlternativas
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
)
AS

/*************************** Variables De Prueba **********************/
--declare @ServerHostPath varchar(512),@ModuleId varchar(4), @UsuarioCreacion int,@dateFrom datetime,@dateEnd datetime,@NoReclamo varchar(50),@Cliente varchar(512),@Producto varchar(512),@Servicio varchar(512)

--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '23'
--set @UsuarioCreacion = 1
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
			IdAlternativa			decimal(18,0)
			,IdReclamo				decimal(18,0)
			,TipoReclamo			varchar(50)
			,NumeroReclamo			varchar(50)
			,Alternativa			varchar(max)
			,Producto				varchar(1024)
			,Cliente				varchar(1024)
			,Categoria				varchar(1024)
			,FechaAlternativa		datetime
			,IdMes					int
			,Mes					varchar(50)
			,DescripcionProducto	varchar(1024)
			,DescripcionServicio	varchar(1024)
			,DescripcionReclamo		varchar(1024)
			,Estado					varchar(50)
			,Seguimiento			varchar(max)			
			,UrlAlternativa			varchar(max)
			,UrlAlternativaEstado	varchar(max)
		)

insert	into
		@TblReport
		(
			IdAlternativa
			,IdReclamo
			,TipoReclamo
			,NumeroReclamo
			,Alternativa
			,Producto
			,Cliente
			,Categoria
			,FechaAlternativa
			,IdMes
			,Mes
			,Estado
			,Seguimiento
			,DescripcionProducto
			,DescripcionServicio
			,UrlAlternativa
			,UrlAlternativaEstado
		)
select	distinct
		alternativa.IdAlternativa
		,alternativa.IdReclamo
		,tipoReclamo.Nombre
		,reclamo.NumeroReclamo
		,alternativa.Alternativa
		,isnull(producto.PRODUCTO,'')
		,isnull(cliente.CLIENTE, '')
		,isnull(categorias.Nombre,'')
		,alternativa.FechaAlternativa
		,month(alternativa.FechaAlternativa)
		,case month(alternativa.FechaAlternativa)
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
		,alternativa.Estado
		,Alternativa.Seguimiento
		,producto.PRODUCTO + ' (' + producto.CATEGORIA + ') - Cliente: ' + cliente.CLIENTE		as DescripcionProducto
		,categorias.Nombre + ' - Area: ' + categorias.Area										as DescripcionArea
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmAdminAlternativaReclamo.aspx?ModuleId=' + @ModuleId + '&IdAlternativa=' + cast(alternativa.IdAlternativa as varchar(18)) + '&from=misalt'
		,@ServerHostPath + '/Resources/images/' + case 
													when alternativa.Estado = 'Asignada'
														then 'scheduled.png'
													when alternativa.Estado = 'Realizada'
														then 'complete.png'
													when alternativa.Estado = 'Cancelada'
														then 'uncomplete.png'
													else
														'scheduled.png'
													end
from	TBL_ModuloReclamos_Alternativas alternativa with(nolock)
		inner join TBL_ModuloReclamos_Reclamo reclamo with(nolock)
			on alternativa.IdReclamo = reclamo.IdReclamo
		inner join TBL_ModuloReclamos_TipoReclamo tipoReclamo with(nolock)
			on reclamo.IdTipoReclamo = tipoReclamo.IdTipoReclamo
		left join Sika_SolverNET..Productos producto with(nolock)
			on reclamo.CodigoProducto = producto.CODIGOPRODUCTO
		left join Sika_SolverNET..Clientes cliente with(nolock)
			on reclamo.CodigoCliente = cliente.CODIGOCLIENTE
		left join TBL_ModuloReclamos_CategoriasReclamo categorias with(nolock)
			on reclamo.IdCategoriaReclamo = categorias.IdCategoriaReclamo
where	alternativa.CreateBy = @UsuarioCreacion
		and alternativa.FechaAlternativa between @dateFrom and @dateEnd
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


select	IdAlternativa
		,IdReclamo
		,TipoReclamo
		,NumeroReclamo
		,Alternativa
		,Producto
		,Cliente
		,Categoria
		,FechaAlternativa
		,IdMes
		,Mes
		,DescripcionProducto
		,DescripcionServicio
		,DescripcionReclamo	
		,Estado
		,Seguimiento
		,UrlAlternativa
		,UrlAlternativaEstado
from	@TblReport