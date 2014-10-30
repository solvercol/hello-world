using Domain.MainModules.Entities;

namespace Domain.MainModule.AccionesPC.Services
{
    public interface ISolicitudesDomainServices
    {
           TBL_ModuloAPC_Tracking GenerarObjetoTrack(
           TBL_ModuloAPC_Tracking oTrack,
           string accion,
           string estadoAnterior,
           decimal idSolicitud,
           string nuevoestado,
           string nuevoResponsable,
           TBL_Admin_Usuarios autenticationService);


           TBL_ModuloAPC_LogSolicitud GenerarObjetoLog(
               TBL_ModuloAPC_LogSolicitud oLog,
               string accion,
               decimal idsolicitud,
               TBL_Admin_Usuarios autenticationService);

           //TBL_Admin_SistemaNotificaciones GenerarEntradaNotificadorSistema(
           //    TBL_Admin_SistemaNotificaciones oNotifier,
           //    int idCurrentResponsible,
           //    byte[] contenido,
           //     TBL_Admin_Usuarios autenticationService);

           TBL_ModuloAPC_LogSolicitud GenerarObjetoLog(
               TBL_ModuloAPC_LogSolicitud oLog,
               string accion,
               decimal idsolicitud,
               TBL_Admin_Usuarios autenticationService,
               string comentario);

           TBL_ModuloAPC_LogSolicitud GenerarObjetoLog(
               TBL_ModuloAPC_LogSolicitud oLog,
               decimal idsolicitud,
               TBL_Admin_Usuarios autenticationService,
               string mensaje);



    }
}