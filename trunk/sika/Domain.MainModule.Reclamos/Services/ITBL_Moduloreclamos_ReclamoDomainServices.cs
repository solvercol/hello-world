using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Services
{
    public interface ITBL_Moduloreclamos_ReclamoDomainServices
    {
        TBL_ModuloReclamos_Tracking GenerarObjetoTrackpedido(
                                   TBL_ModuloReclamos_Tracking oTrack,
                                   string accion,
                                   string estadoAnterior,
                                   int idPedido,
                                   string nuevoestado,
                                   string nuevoResponsable);

        TBL_ModuloReclamos_LogReclamos GenerarObjetoLogPedido(
                                     TBL_ModuloReclamos_LogReclamos oLog,
                                     string accion,
                                     int idReclamo);

        TBL_Admin_SistemaNotificaciones GenerarEntradaNotificadorSistema(
                                    TBL_Admin_SistemaNotificaciones oNotifier,
                                    int idCurrentResponsible,
                                    byte[] contenido,
                                    string cliente);
    }
}