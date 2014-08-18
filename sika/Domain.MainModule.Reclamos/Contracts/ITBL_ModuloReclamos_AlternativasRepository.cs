using System.Collections.Generic;
using Domain.Core;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_AlternativasRepository : IRepository<TBL_ModuloReclamos_Alternativas>
    {
        TBL_ModuloReclamos_Alternativas GetCompleteEntityBySpec(ISpecification<TBL_ModuloReclamos_Alternativas> specification);
        List<TBL_ModuloReclamos_Alternativas> GetCompleteListBySpec(ISpecification<TBL_ModuloReclamos_Alternativas> specification);
    }
}