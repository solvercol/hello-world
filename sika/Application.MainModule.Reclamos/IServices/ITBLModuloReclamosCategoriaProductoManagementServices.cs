using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_CategoriaProductoManagementServices : IGenericServices<TBL_ModuloReclamos_CategoriaProducto>
    {
        TBL_ModuloReclamos_CategoriaProducto GetByNombre(string name);
    }
}   