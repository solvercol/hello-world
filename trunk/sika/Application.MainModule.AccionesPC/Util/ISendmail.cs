using Domain.MainModules.Entities;

namespace Application.MainModule.AccionesPC.Util
{
    public interface ISendEmail
    {
        bool EnviarCorreoelectronicoComentarios(decimal idComment, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoActividadApcRealizada(decimal idActividad, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoActividadApcCancelada(decimal idActividad, TBL_Admin_Usuarios userSession,
                                                          string motivo);
    }
}