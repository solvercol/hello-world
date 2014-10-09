using Applications.MainModule.WorkFlow.DTO;
using Domain.MainModules.Entities;

namespace Applications.MainModule.WorkFlow.Util
{
    public interface ISendMailNotification
    {
        bool EnviarCorreoElectronicoNotificacion(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoElectronicoNotificacionCliente(RenderTypeControlButtonDto oDocument,
                                                        TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoAsesoresJefe(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoAutorReclamo(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession);

        byte[] GetMergeTemplate(RenderTypeControlButtonDto oDocument);
    }
}