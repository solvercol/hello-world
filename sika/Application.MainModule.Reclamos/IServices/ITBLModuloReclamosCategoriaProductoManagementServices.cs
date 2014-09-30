using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_CategoriaProductoManagementServices : IGenericServices<TBL_ModuloReclamos_CategoriaProducto>
    {
        TBL_ModuloReclamos_CategoriaProducto GetByNombre(string name);
        int CountByPaged(string search);
        List<TBL_ModuloReclamos_CategoriaProducto> FindPaged(int pageIndex, int pageCount, string search);
    }
}   