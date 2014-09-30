using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices : IGenericServices<TBL_ModuloReclamos_ActividadesReclamo>
    {
        List<TBL_ModuloReclamos_ActividadesReclamo> GetByTypoReclamo(int idTipoReclamo);
        int CountByPaged(string search);
        List<TBL_ModuloReclamos_ActividadesReclamo> FindPaged(int pageIndex, int pageCount, string search);
    }
}