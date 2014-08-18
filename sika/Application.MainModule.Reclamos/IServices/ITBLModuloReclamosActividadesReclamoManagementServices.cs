using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices : IGenericServices<TBL_ModuloReclamos_ActividadesReclamo>
    {
        List<TBL_ModuloReclamos_ActividadesReclamo> GetByTypoReclamo(int idTipoReclamo);
    }
}