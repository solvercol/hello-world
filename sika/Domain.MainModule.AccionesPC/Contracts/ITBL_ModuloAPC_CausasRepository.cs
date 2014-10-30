using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.AccionesPC.Contracts
{
    public interface ITBL_ModuloAPC_CausasRepository : IRepository<TBL_ModuloAPC_Causas>
    {
        TBL_ModuloAPC_Causas GetCompleteEntity(ISpecification<TBL_ModuloAPC_Causas> specification);
        List<TBL_ModuloAPC_Causas> GetCompleteEntityList(ISpecification<TBL_ModuloAPC_Causas> specification);
    }
}