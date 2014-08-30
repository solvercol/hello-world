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
			,NumeroReclamo			varchar(50)
			,Descripcion			varchar(max)
			,Fecha					datetime
			,Estado					varchar(50)
			,Autor					varchar(max)			
			,UrlActividad			varchar(max)
		)

insert	into
		@TblReport
		(
			IdActividad
			,IdReclamo
			,NumeroReclamo
			,Descripcion
			,Fecha
			,Estado
			,Autor
			,UrlActividad
		)
select	distinct
		actividad.IdActividad
		,actividad.IdReclamo
		,reclamo.NumeroReclamo
		,actividad.Descripcion
		,actividad.Fecha
		,actividad.Estado
		,autor.Nombres
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmAdminActividadReclamo.aspx?ModuleId=' + @ModuleId + '&IdActividad=' + cast(actividad.IdActividad as varchar(18))
from	TBL_ModuloReclamos_Actividades actividad with(nolock)
		inner join TBL_ModuloReclamos_Reclamo reclamo with(nolock)
			on actividad.IdReclamo = reclamo.IdReclamo
		inner join TBL_Admin_Usuarios autor with(nolock)
			on actividad.CreateBy = autor.IdUser
where	actividad.IdUsuarioAsignacion = @IdResponsable

select	IdActividad
		,IdReclamo
		,NumeroReclamo
		,Descripcion
		,Fecha
		,Estado
		,Autor
		,UrlActividad
from	@TblReport