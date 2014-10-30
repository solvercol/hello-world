using System.Data;

namespace Application.MainModule.SqlServices.IServices
{
    public interface ISolicitudAdoService
    {
        void InsertUsuarioCopiaComentario(string idUsuario, string idComentario);

        DataTable ListadoActividadesPorSolicitudApc(string idSolicitud);
    }
}
