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
			,NumeroReclamo			varchar(50)
			,Alternativa			varchar(max)
			,FechaAlternativa		datetime
			,Estado					varchar(50)
			,Seguimiento			varchar(max)			
			,UrlAlternativa			varchar(max)
		)

insert	into
		@TblReport
		(
			IdAlternativa
			,IdReclamo
			,NumeroReclamo
			,Alternativa
			,FechaAlternativa
			,Estado
			,Seguimiento
			,UrlAlternativa
		)
select	distinct
		alternativa.IdAlternativa
		,alternativa.IdReclamo
		,reclamo.NumeroReclamo
		,alternativa.Alternativa
		,alternativa.FechaAlternativa
		,alternativa.Estado
		,Alternativa.Seguimiento
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmAdminAlternativaReclamo.aspx?ModuleId=' + @ModuleId + '&IdAlternativa=' + cast(alternativa.IdAlternativa as varchar(18))
from	TBL_ModuloReclamos_Alternativas alternativa with(nolock)
		inner join TBL_ModuloReclamos_Reclamo reclamo with(nolock)
			on alternativa.IdReclamo = reclamo.IdReclamo
where	alternativa.IdResponsable = @IdResponsable


select	IdAlternativa
		,IdReclamo
		,NumeroReclamo
		,Alternativa
		,FechaAlternativa
		,Estado
		,Seguimiento
		,UrlAlternativa
from	@TblReport