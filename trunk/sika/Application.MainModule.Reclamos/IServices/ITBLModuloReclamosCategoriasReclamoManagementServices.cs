using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices : IGenericServices<TBL_ModuloReclamos_CategoriasReclamo>
    {
        int CountByPaged(string search);
        List<TBL_ModuloReclamos_CategoriasReclamo> GetByTipoReclamo(int idTipoReclamo);
        List<TBL_ModuloReclamos_CategoriasReclamo> FindPaged(int pageIndex, int pageCount, string search);
    }
}