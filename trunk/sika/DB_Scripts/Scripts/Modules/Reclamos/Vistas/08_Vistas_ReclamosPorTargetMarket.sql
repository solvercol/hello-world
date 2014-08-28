IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_ReclamosPorTargetMarket') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_ReclamosPorTargetMarket
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_ReclamosPorTargetMarket
(
	@dateFrom datetime
	,@dateEnd datetime
	,@ServerHostPath varchar(512)
	,@ModuleId varchar(4)
	,@NoReclamo varchar(50)
	,@Cliente varchar(512)
	,@Producto varchar(512)
)
AS

/*************************** Variables De Prueba **********************/
--declare @dateFrom datetime,@dateEnd datetime, @ServerHostPath varchar(512),@ModuleId varchar(4), @NoReclamo varchar(50),@Cliente varchar(512),@Producto varchar(512)
--set @dateFrom = '2014-01-01'
--set @dateEnd = '2014-09-01'
--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '23'
--set @NoReclamo = ''
--set @Cliente = ''
--set @Producto = ''
/***********************************************************************/

-- Definiendo variables de trabajo
declare	@TblReport table
		(
			IdReclamo				decimal(18,0)			
			,Producto				varchar(512)
			,TargetMarket			varchar(512)
			,CampoAplicacion		varchar(512)
			,SubCampoAplicacion		varchar(512)
			,NumeroReclamo			varchar(50)
			,Cliente				varchar(512)
			,UnidadZona				varchar(512)
			,Estado					varchar(50)
			,TotalCostos			decimal(18,2)
			,FechaReclamo			datetime
			,Asesor					varchar(512)
			,ResponsableActual		varchar(512)
			,UrlReclamo				varchar(2048)
		)

-- Seleccionado Datos
insert	into
		@TblReport
		(
			IdReclamo
			,Producto
			,TargetMarket
			,CampoAplicacion
			,SubCampoAplicacion
			,NumeroReclamo
			,Cliente
			,UnidadZona
			,Estado
			,TotalCostos
			,FechaReclamo
			,Asesor
			,ResponsableActual
			,UrlReclamo
		)
select	distinct
		reclamo.IdReclamo																		as IdReclamo
		,isnull(producto.PRODUCTO,'')															as Producto
		,isnull(producto.GRUPOCOMPRADORES,'')													as TargetMarket
		,isnull(producto.CAMPOAPL,'')															as CampoAplicacion
		,isnull(producto.CATEGORIA,'')															as SubCampoAplicacion
		,reclamo.NumeroReclamo																	as NumeroReclamo
		,isnull(cliente.CLIENTE,'')																as Cliente
		,isnull(cliente.UNIDAD,'') + ' - ' + isnull(cliente.ZONA,'')							as Cliente
		,estado.Descripcion																		as Estado		
		,reclamo.CostoTotal																		as TotalCostos
		,reclamo.CreateOn																		as FechaReclamo
		,asesor.Nombres																			as Asesor
		,responsable.Nombres																	as Responsable
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmReclamo.aspx?ModuleId=' + @ModuleId + '&IdReclamo=' + cast(reclamo.IdReclamo as varchar(18))
from	TBL_ModuloReclamos_Reclamo reclamo with(nolock)
		inner join TBL_Admin_EstadosProceso estado with(nolock)
			on reclamo.IdEstado = estado.IdEstado
		inner join TBL_Admin_Usuarios asesor with(nolock)
			on reclamo.IdAsesoradoPor = asesor.IdUser
		inner join TBL_Admin_Usuarios responsable with(nolock)
			on reclamo.IdResponsableActual = responsable.IdUser
		inner join TBL_ModuloReclamos_TipoReclamo tipoReclamo with(nolock)
			on reclamo.IdTipoReclamo = tipoReclamo.IdTipoReclamo
		left join Sika_SolverNET..Productos producto with(nolock)
			on reclamo.CodigoProducto = producto.CODIGOPRODUCTO
		left join Sika_SolverNET..Clientes cliente with(nolock)
			on reclamo.CodigoCliente = cliente.CODIGOCLIENTE
where	reclamo.CreateOn between @dateFrom and @dateEnd
		and reclamo.NumeroReclamo like '%' + case @NoReclamo when '' then reclamo.NumeroReclamo else @NoReclamo end + '%'
		and reclamo.IdTipoReclamo = 1
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

-- Seleccionando Datos
select	IdReclamo
		,Producto
		,TargetMarket
		,CampoAplicacion
		,SubCampoAplicacion
		,NumeroReclamo
		,Cliente
		,UnidadZona
		,Estado
		,TotalCostos
		,FechaReclamo
		,Asesor
		,ResponsableActual
		,UrlReclamo
from	@TblReport
order	by
		FechaReclamo asc