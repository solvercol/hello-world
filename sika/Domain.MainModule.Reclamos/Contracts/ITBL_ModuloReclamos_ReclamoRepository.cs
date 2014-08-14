using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_ReclamoRepository : IRepository<TBL_ModuloReclamos_Reclamo>
    {
        TBL_ModuloReclamos_Reclamo GetCompleteEntity(ISpecification<TBL_ModuloReclamos_Reclamo> specification);

        List<TBL_ModuloReclamos_Reclamo> GetCompleteEntityList(ISpecification<TBL_ModuloReclamos_Reclamo> specification);
    }
}    