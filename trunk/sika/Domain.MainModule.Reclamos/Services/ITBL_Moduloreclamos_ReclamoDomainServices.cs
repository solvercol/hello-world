using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Services
{
    public interface ITBL_Moduloreclamos_ReclamoDomainServices
    {
        TBL_ModuloReclamos_Tracking GenerarObjetoTrack(
            TBL_ModuloReclamos_Tracking oTrack,
            string accion,
            string estadoAnterior,
            int idReclamo,
            string nuevoestado,
            string nuevoResponsable,
            TBL_Admin_Usuarios autenticationService);

        TBL_ModuloReclamos_LogReclamos GenerarObjetoLog(
            TBL_ModuloReclamos_LogReclamos oLog, 
            string accion, 
            int idReclamo, 
            TBL_Admin_Usuarios autenticationService);

        TBL_Admin_SistemaNotificaciones GenerarEntradaNotificadorSistema(
            TBL_Admin_SistemaNotificaciones oNotifier,
            int idCurrentResponsible,
            byte[] contenido,
             TBL_Admin_Usuarios autenticationService);
    }
}