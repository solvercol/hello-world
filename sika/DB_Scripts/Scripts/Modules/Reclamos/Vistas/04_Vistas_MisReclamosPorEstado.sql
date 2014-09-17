IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_MisReclamosPorEstado') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_MisReclamosPorEstado
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_MisReclamosPorEstado
(
	@dateFrom datetime
	,@dateEnd datetime
	,@ServerHostPath varchar(512)
	,@ModuleId varchar(4)
	,@IdCreador int
	,@NoReclamo varchar(50)
	,@Cliente varchar(512)
	,@Producto varchar(512)
	,@Servicio varchar(512)
)
AS

/*************************** Variables De Prueba **********************/
--declare @dateFrom datetime,@dateEnd datetime, @ServerHostPath varchar(512),@ModuleId varchar(4), @IdCreador int,@NoReclamo varchar(50),@Cliente varchar(512),@Producto varchar(512),@Servicio varchar(512)
--set @dateFrom = '2014-01-01'
--set @dateEnd = '2014-09-01'
--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '23'
--set @IdCreador = 1
--set @NoReclamo = ''
--set @Cliente = ''
--set @Producto = ''
--set @Servicio = ''
/***********************************************************************/

-- Definiendo variables de trabajo
declare	@TblReport table
		(
			IdReclamo				decimal(18,0)
			,Asesor					varchar(512)
			,TipoReclamo			varchar(50)
			,IdMes					int
			,Mes					varchar(50)
			,NumeroReclamo			varchar(50)
			,Estado					varchar(50)
			,TotalCostos			decimal(18,2)
			,FechaReclamo			datetime
			,DescripcionProducto	varchar(1024)
			,DescripcionServicio	varchar(1024)
			,DescripcionReclamo		varchar(1024)
			,UrlReclamo				varchar(2048)
		)

-- Seleccionado Datos
insert	into
		@TblReport
		(
			Asesor
			,IdReclamo			
			,TipoReclamo
			,IdMes
			,Mes
			,NumeroReclamo
			,Estado
			,TotalCostos
			,FechaReclamo
			,DescripcionProducto
			,DescripcionServicio
			,UrlReclamo
		)
select	distinct
		asesor.Nombres																			as Asesor
		,reclamo.IdReclamo																		as IdReclamo
		,tipoReclamo.Nombre																		as TipoReclamo
		,month(reclamo.CreateOn)																as IdMes
		,case month(reclamo.CreateOn)
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
		,reclamo.NumeroReclamo																	as NumeroReclamo
		,estado.Descripcion																		as Estado
		,reclamo.CostoTotal																		as TotalCostos
		,reclamo.CreateOn																		as FechaReclamo
		,producto.PRODUCTO + ' (' + producto.CATEGORIA + ') - Cliente: ' + cliente.CLIENTE		as DescripcionProducto
		,categorias.Nombre + ' - Area: ' + categorias.Area										as DescripcionArea
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmReclamo.aspx?ModuleId=' + @ModuleId + '&IdReclamo=' + cast(reclamo.IdReclamo as varchar(18)) + '&from=misestado'
from	TBL_ModuloReclamos_Reclamo reclamo with(nolock)
		inner join TBL_Admin_EstadosProceso estado with(nolock)
			on reclamo.IdEstado = estado.IdEstado
		inner join TBL_Admin_Usuarios asesor with(nolock)
			on reclamo.IdAsesoradoPor = asesor.IdUser
		inner join TBL_ModuloReclamos_TipoReclamo tipoReclamo with(nolock)
			on reclamo.IdTipoReclamo = tipoReclamo.IdTipoReclamo
		left join Sika_SolverNET..Productos producto with(nolock)
			on reclamo.CodigoProducto = producto.CODIGOPRODUCTO
		left join Sika_SolverNET..Clientes cliente with(nolock)
			on reclamo.CodigoCliente = cliente.CODIGOCLIENTE
		left join TBL_ModuloReclamos_CategoriasReclamo categorias with(nolock)
			on reclamo.IdCategoriaReclamo = categorias.IdCategoriaReclamo
where	year(reclamo.FechaReclamo) >= year(@dateFrom)
		and year(reclamo.FechaReclamo) <= year(@dateEnd)
		and month(reclamo.FechaReclamo) >= month(@dateFrom)
		and month(reclamo.FechaReclamo) <= month(@dateEnd)
		and reclamo.CreateBy = @IdCreador
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

-- Seleccionando Datos
select	IdReclamo
		,Asesor
		,TipoReclamo
		,IdMes
		,Mes
		,NumeroReclamo
		,Estado
		,TotalCostos
		,FechaReclamo
		,DescripcionProducto
		,DescripcionServicio
		,DescripcionReclamo
		,UrlReclamo
from	@TblReport
order	by
		FechaReclamo asc