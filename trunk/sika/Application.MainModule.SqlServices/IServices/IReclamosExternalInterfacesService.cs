using System.Collections.Generic;
using Domain.MainModule.Reclamos.DTO;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IReclamosExternalInterfacesService
    {
        #region Productos

        List<Dto_Producto> GetAllProducts();
        Dto_Producto GetProductByCodigoProducto(string codigoProducto);
        List<Dto_Producto> GetAllProductsByFilter(string filter, int pagesize, int pageindex);
        int GetAllProductsByFilterCount(string filter);

        #endregion

        #region Clientes

        List<Dto_Cliente> GetAllClients();
        Dto_Cliente GetClientByCodigoCliente(string codigoCliente);
        List<Dto_Cliente> GetAllClientsByFilter(string filter, int pagesize, int pageindex);
        int GetAllClientsByFilterCount(string filter);

        #endregion
    }
}