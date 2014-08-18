using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_CostosProductoManagementServices : IGenericServices<TBL_ModuloReclamos_CostosProducto>
    {
        List<TBL_ModuloReclamos_CostosProducto> GetCostosByReclamo(decimal idReclamo);
        TBL_ModuloReclamos_CostosProducto GetCostosById(decimal idCosto);
    }
}