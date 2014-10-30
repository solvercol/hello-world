using System.Data;
using Applications.MainModule.WorkFlow.DTO;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Applications.MainModule.WorkFlow.Util
{
    public interface ISendMailNotification
    {
        bool EnviarCorreoElectronicoNotificacion(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession,ModulosAplicacion module);

        bool EnviarCorreoElectronicoNotificacionCliente(RenderTypeControlButtonDto oDocument,
                                                        TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoAsesoresJefe(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoAutorReclamo(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession);

        byte[] GetMergeTemplate(RenderTypeControlButtonDto oDocument,ModulosAplicacion module);

        bool EnviarCorreoelectronicoDevolucion(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoRechazoReclamo(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession);

        bool SendEmailActividadesSolicitud(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession, DataTable dt,
                                      ModulosAplicacion module);
    }
}