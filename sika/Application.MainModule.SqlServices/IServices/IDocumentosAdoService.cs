using System.Data;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IDocumentosAdoService
    {
        #region Vistas Y Reportes

        DataTable GetVistaDocumentos(int idUser, int idEstado, string searchText, string fromview, string serverHost, string moduleId);

        #endregion
    }
}