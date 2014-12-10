IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SolicitudesAPC_Vistas_Seguimiento') AND type in (N'P', N'PC'))
DROP PROCEDURE SolicitudesAPC_Vistas_Seguimiento
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE SolicitudesAPC_Vistas_Seguimiento
(
	@dateFrom datetime
	,@dateEnd datetime
	,@ServerHostPath varchar(512)
	,@ModuleId varchar(4)
	,@NoAccion varchar(50)
	,@Tipo varchar(50)
	,@Area int
	,@Proceso varchar(512)
	,@IdResponsable int
	,@FromView varchar(50)
)
AS
/*************************** Variables De Prueba **********************/
--declare @dateFrom datetime,@dateEnd datetime, @ServerHostPath varchar(512),@ModuleId varchar(4), @NoAccion varchar(50),@Tipo varchar(50),@Area int,@Proceso varchar(512), @FromView varchar(50), @IdResponsable int
--set @dateFrom = '2014-01-01'
--set @dateEnd = '2014-12-01'
--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '26'
--set @NoAccion = ''
--set @Tipo = ''
--set @Area = 0
--set @Proceso = ''
--set @IdResponsable = 1
--set @FromView = 'misolicitudes'
/***********************************************************************/
declare	@View table
		(
			IdSolicitudAPC				decimal(18,0)
			,FechaSolicitud				datetime
			,IdMes						int
			,Mes						varchar(50)
			,Codigo						varchar(50)
			,Tipo						varchar(50)
			,Accion						varchar(512)
			,Proceso					varchar(512)
			,AreaAccion					varchar(512)
			,Estado						varchar(512)
			,Autor						varchar(512)
			,GerenteArea				varchar(512)
			,ResponsableActual			varchar(512)
			,ResponsableSeguimiento		varchar(512)
			,ResponsableEjecucion		varchar(512)
			,UrlSolicitud				varchar(2048)
		)

-- Seleccionando Datos
insert	into
		@View
		(
			IdSolicitudAPC
			,FechaSolicitud
			,IdMes
			,Mes
			,Codigo
			,Tipo
			,Accion
			,Proceso
			,AreaAccion
			,Estado
			,Autor
			,GerenteArea
			,ResponsableActual
			,ResponsableSeguimiento
			,ResponsableEjecucion
			,UrlSolicitud
		)
select	distinct
		solicitud.IdSolucitudAPC			as IdSolucitudAPC
		,solicitud.FechaSolicitud			as FechaSolicitud
		,month(solicitud.FechaSolicitud)	as IdMes
		,case month(solicitud.FechaSolicitud)
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
			end								as Mes
		,solicitud.Codigo					as Codigo
		,solicitud.TipoAccion				as Tipo
		,solicitud.DescripcionAccion		as Accion
		,solicitud.Proceso					as Proceso
		,area.Nombre						as AreaAccion
		,estado.Descripcion					as Estado
		,autor.Nombres						as Autor
		,gerenteArea.Nombres				as GerenteArea
		,responsableActual.Nombres			as ResponsableActual
		,responsableSeguimiento.Nombres		as ResponsableSeguimiento		
		,responsableEjecucion.Nombres		as ResponsableEjecucion
		,@ServerHostPath + '/Pages/Modules/AccionesPC/Admin/FrmSolicitudAPC.aspx?ModuleId=' + @ModuleId + '&IdSolicitud=' + cast(solicitud.IdSolucitudAPC as varchar(18)) + '&from=' + @FromView as UrlSolicitud
from	TBL_ModuloAPC_Solicitud solicitud with(nolock)
		left join TBL_ModuloAPC_Areas area with(nolock)
			on solicitud.IdAreaAccion = area.IdArea
		inner join TBL_Admin_EstadosProceso estado with(nolock)
			on solicitud.IdEstado = estado.IdEstado
		left join TBL_Admin_Usuarios gerenteArea with(nolock)
			on area.IdGerente = gerenteArea.IdUser
		inner join TBL_Admin_Usuarios autor with(nolock)
			on solicitud.CreateBy = autor.IdUser
		left join TBL_Admin_Usuarios responsableActual with(nolock)
			on solicitud.IdResponsableActual = responsableActual.IdUser
		left join TBL_Admin_Usuarios responsableSeguimiento with(nolock)
			on solicitud.IdResponsableSeguimiento = responsableSeguimiento.IdUser
		left join TBL_Admin_Usuarios responsableEjecucion with(nolock)
			on solicitud.IdResponsableEjecucion = responsableEjecucion.IdUser
where	solicitud.FechaSolicitud >= @dateFrom
		and solicitud.FechaSolicitud <= @dateEnd
		and isnull(solicitud.Codigo,'') like '%' + case @NoAccion when '' then isnull(solicitud.Codigo,'') else @NoAccion end + '%'
		and isnull(solicitud.TipoAccion,'') like '%' + case @Tipo when '' then isnull(solicitud.TipoAccion,'') else @Tipo end + '%'
		and isnull(solicitud.IdAreaAccion,'') = case @Area when 0 then isnull(solicitud.IdAreaAccion,'') else @Area end
		and isnull(solicitud.Proceso,'') like '%' +  case @Proceso when '' then isnull(solicitud.Proceso,'') else @Proceso end + '%'				
		
-- Result View
select	distinct
		IdSolicitudAPC
		,FechaSolicitud
		,IdMes
		,Mes
		,Codigo
		,Tipo
		,Accion
		,Proceso
		,AreaAccion
		,Estado
		,Autor
		,GerenteArea
		,ResponsableActual
		,ResponsableSeguimiento
		,ResponsableEjecucion
		,UrlSolicitud
from	@View