IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SolicitudesAPC_Vistas_Actividades') AND type in (N'P', N'PC'))
DROP PROCEDURE SolicitudesAPC_Vistas_Actividades
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE SolicitudesAPC_Vistas_Actividades
(
	@ServerHostPath varchar(512)
	,@FromView varchar(50)
	,@ModuleId varchar(4)
	,@IdUsuario int
	,@dateFrom datetime
	,@dateEnd datetime
	,@NoAccion varchar(50)
	,@Tipo varchar(50)
	,@Area int
	,@Proceso varchar(512)
	,@Estado varchar(50)
)
AS
/*************************** Variables De Prueba **********************/
--declare @ServerHostPath varchar(512),@FromView varchar(50),@ModuleId varchar(4), @IdUsuario int,@dateFrom datetime,@dateEnd datetime, @NoAccion varchar(50),@Tipo varchar(50),@Area int,@Proceso varchar(512),@Estado varchar(50)

--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '23'
--set @IdUsuario = 1
--set @dateFrom = '2014-01-01'
--set @dateEnd = '2014-12-31'
--set @FromView = 'misalternativas'
--set @NoAccion = ''
--set @Tipo = ''
--set @Area = 0
--set @Proceso = ''
--set @Estado = 'Programada'
/***********************************************************************/
-- Definiendo variables de trabajo
declare	@TblReport table
		(
			IdActividad				decimal(18,0)
			
			,Descripcion			varchar(512)
			,EstadoActividad		varchar(50)
			,FechaActividad			datetime
			,IdMes					int
			,Mes					varchar(50)	
			,ResponsableEjecucion	varchar(512)		
			,ResponsableSeguimiento	varchar(512)
			,CreadoPor				varchar(512)
			,FechaCreacion			datetime
			,IdSolicitudAPC			decimal(18,0)
			,CodigoSolicitudAPC		varchar(50)
			,UrlActividad			varchar(2048)
		)

insert	into
		@TblReport
		(
			IdActividad
			,Descripcion
			,EstadoActividad
			,FechaActividad
			,IdMes
			,Mes
			,ResponsableEjecucion
			,ResponsableSeguimiento			
			,CreadoPor
			,FechaCreacion
			,IdSolicitudAPC
			,CodigoSolicitudAPC
			,UrlActividad
		)
select	distinct
		actividad.IdActividad								as IdActividad
		,actividad.Descripcion								as Descripcion
		,actividad.EstadoActividad							as EstadoActividad
		,actividad.FechaActividad							as FechaActividad
		,month(actividad.FechaActividad)					as IdMes
		,case month(actividad.FechaActividad)
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
			end												as Mes
		,responsableEjecucion.Nombres						as ResponsableEjecucion
		,responsableSeguimiento.Nombres						as ResponsableSeguimiento
		,creadoPor.Nombres									as CreadoPor
		,actividad.CreateOn									as FechaCreacion
		,actividad.IdSolicitudAPC							as IdSolicitudAPC
		,solicitud.Codigo									as CodigoSolicitudAPC
		,@ServerHostPath + '/Pages/Modules/AccionesPC/Admin/FrmAdminActividadSolicitud.aspx?ModuleId=' + @ModuleId + '&IdActividad=' + cast(actividad.IdActividad as varchar(18)) + '&IdSolicitud=' + cast(actividad.IdSolicitudAPC as varchar(18)) + '&from=' + @FromView as UrlActividad
from	TBL_ModuloAPC_Actividades actividad with(nolock)
		inner join TBL_Admin_Usuarios responsableEjecucion with(nolock)
			on actividad.IdResponsableEjecucion = responsableEjecucion.IdUser
		inner join TBL_Admin_Usuarios responsableSeguimiento with(nolock)
			on actividad.IdResponsableSeguimiento = responsableSeguimiento.IdUser
		inner join TBL_Admin_Usuarios creadoPor with(nolock)
			on actividad.CreateBy = creadoPor.IdUser
		inner join TBL_ModuloAPC_Solicitud solicitud with(nolock)
			on actividad.IdSolicitudAPC = solicitud.IdSolucitudAPC
where	actividad.FechaActividad >= @dateFrom
		and actividad.FechaActividad <= @dateEnd
		and actividad.EstadoActividad =  case @Estado when '' then actividad.EstadoActividad else @Estado end
		and actividad.IdResponsableEjecucion = case @IdUsuario when 0 then actividad.IdResponsableEjecucion else @IdUsuario end
		and isnull(solicitud.Codigo,'') like '%' + case @NoAccion when '' then isnull(solicitud.Codigo,'') else @NoAccion end + '%'
		and isnull(solicitud.TipoAccion,'') like '%' + case @Tipo when '' then isnull(solicitud.TipoAccion,'') else @Tipo end + '%'
		and isnull(solicitud.IdAreaAccion,'') = case @Area when 0 then isnull(solicitud.IdAreaAccion,'') else @Area end
		and isnull(solicitud.Proceso,'') like '%' +  case @Proceso when '' then isnull(solicitud.Proceso,'') else @Proceso end + '%'
		
		
select	IdActividad
		,Descripcion
		,EstadoActividad
		,FechaActividad
		,IdMes
		,Mes
		,ResponsableSeguimiento
		,ResponsableEjecucion
		,CreadoPor
		,FechaCreacion
		,IdSolicitudAPC
		,CodigoSolicitudAPC
		,UrlActividad
from	@TblReport tbl