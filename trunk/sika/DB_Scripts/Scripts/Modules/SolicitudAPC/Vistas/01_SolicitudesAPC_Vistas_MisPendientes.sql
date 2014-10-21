IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SolicitudesAPC_Vistas_MisPendientes') AND type in (N'P', N'PC'))
DROP PROCEDURE SolicitudesAPC_Vistas_MisPendientes
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE SolicitudesAPC_Vistas_MisPendientes
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
)
AS

/*************************** Variables De Prueba **********************/
--declare @dateFrom datetime,@dateEnd datetime, @ServerHostPath varchar(512),@ModuleId varchar(4), @NoAccion varchar(50),@Tipo varchar(50),@Area int,@Proceso varchar(512)
--set @dateFrom = '2014-01-01'
--set @dateEnd = '2014-12-01'
--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '26'
--set @NoAccion = ''
--set @Tipo = ''
--set @Area = 0
--set @Proceso = ''
--set @IdResponsable = 1
/***********************************************************************/

select	distinct
		solicitud.FechaSolicitud			as FechaSolicitud
		,solicitud.Codigo					as Codigo
		,solicitud.TipoAccion				as Tipo
		,solicitud.Proceso					as Proceso
		,area.Nombre						as AreaAccion
		,estado.Descripcion					as Estado
		,autor.Nombres						as Autor
		,gerenteArea.Nombres				as GerenteArea
		,responsableActual.Nombres			as ResponsableActual
		,responsableSeguimiento.Nombres		as ResponsableSeguimiento
		,responsableEjecucion.Nombres		as ResponsableEjecucion
from	TBL_ModuloAPC_Solicitud solicitud with(nolock)
		inner join TBL_ModuloAPC_Areas area with(nolock)
			on solicitud.IdAreaAccion = area.IdArea
		inner join TBL_Admin_EstadosProceso estado with(nolock)
			on solicitud.IdEstado = estado.IdEstado
		inner join TBL_Admin_Usuarios gerenteArea with(nolock)
			on area.IdGerente = gerenteArea.IdUser
		inner join TBL_Admin_Usuarios autor with(nolock)
			on solicitud.CreateBy = autor.IdUser
		inner join TBL_Admin_Usuarios responsableActual with(nolock)
			on solicitud.IdResponsableActual = responsableActual.IdUser
		inner join TBL_Admin_Usuarios responsableSeguimiento with(nolock)
			on solicitud.IdResponsableSeguimiento = responsableSeguimiento.IdUser
		inner join TBL_Admin_Usuarios responsableEjecucion with(nolock)
			on solicitud.IdResponsableEjecucion = responsableEjecucion.IdUser
where	year(solicitud.FechaSolicitud) >= year(@dateFrom)
		and year(solicitud.FechaSolicitud) <= year(@dateEnd)
		and month(solicitud.FechaSolicitud) >= month(@dateFrom)
		and month(solicitud.FechaSolicitud) <= month(@dateEnd)
		and solicitud.Codigo like '%' + case @NoAccion when '' then solicitud.Codigo else @NoAccion end + '%'
		and solicitud.TipoAccion like '%' + case @Tipo when '' then solicitud.TipoAccion else @Tipo end + '%'
		and solicitud.IdAreaAccion = case @Area when 0 then solicitud.IdAreaAccion else @Area end
		and solicitud.Proceso like '%' +  case @Proceso when '' then solicitud.Proceso else @Proceso end + '%'
		and solicitud.IdResponsableActual = case @IdResponsable when 0 then solicitud.IdResponsableActual else @IdResponsable end