using Domain.MainModules.Entities;

namespace Application.MainModule.Reclamos.Util
{
    public interface ISendEmail
    {
        bool EnviarCorreoelectronicoAutorReclamo(decimal idComment, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoRespuestaCliente(decimal idComment, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoAlternativaSolucion(decimal idAlternativa, TBL_Admin_Usuarios userSession);

        bool EnviarCorreoelectronicoActividades(decimal idActividad, TBL_Admin_Usuarios userSession);
    }
}