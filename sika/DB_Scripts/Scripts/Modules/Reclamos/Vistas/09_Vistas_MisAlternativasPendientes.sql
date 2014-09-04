IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_MisAlternativasPendientes') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_MisAlternativasPendientes
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_MisAlternativasPendientes
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
			IdAlternativa			decimal(18,0)
			,IdReclamo				decimal(18,0)
			,TipoReclamo			varchar(50)
			,NumeroReclamo			varchar(50)
			,Alternativa			varchar(max)
			,FechaAlternativa		datetime
			,DescripcionProducto	varchar(1024)
			,DescripcionServicio	varchar(1024)
			,DescripcionReclamo		varchar(1024)
			,Estado					varchar(50)
			,Seguimiento			varchar(max)			
			,UrlAlternativa			varchar(max)
		)

insert	into
		@TblReport
		(
			IdAlternativa
			,IdReclamo
			,TipoReclamo
			,NumeroReclamo
			,Alternativa
			,FechaAlternativa
			,Estado
			,Seguimiento
			,DescripcionProducto
			,DescripcionServicio
			,UrlAlternativa
		)
select	distinct
		alternativa.IdAlternativa
		,alternativa.IdReclamo
		,tipoReclamo.Nombre
		,reclamo.NumeroReclamo
		,alternativa.Alternativa
		,alternativa.FechaAlternativa
		,alternativa.Estado
		,Alternativa.Seguimiento
		,producto.PRODUCTO + ' (' + producto.CATEGORIA + ') - Cliente: ' + cliente.CLIENTE		as DescripcionProducto
		,categorias.Nombre + ' - Area: ' + categorias.Area										as DescripcionArea
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmAdminAlternativaReclamo.aspx?ModuleId=' + @ModuleId + '&IdAlternativa=' + cast(alternativa.IdAlternativa as varchar(18))
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
where	alternativa.IdResponsable = @IdResponsable
		and alternativa.Estado != 'Realizada'

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
		,FechaAlternativa
		,DescripcionProducto
		,DescripcionServicio
		,DescripcionReclamo	
		,Estado
		,Seguimiento
		,UrlAlternativa
from	@TblReport