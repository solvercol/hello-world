using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.AccionesPC.Contracts
{
    public interface ITBL_ModuloAPC_AreasRepository : IRepository<TBL_ModuloAPC_Areas>
    {
        TBL_ModuloAPC_Areas GetCompleteEntity(ISpecification<TBL_ModuloAPC_Areas> specification);
        List<TBL_ModuloAPC_Areas> GetCompleteEntityList(ISpecification<TBL_ModuloAPC_Areas> specification);

        TBL_ModuloAPC_Areas GetEntityWithGerente(ISpecification<TBL_ModuloAPC_Areas> specification);
        List<TBL_ModuloAPC_Areas> GetEntityListWithGerente(ISpecification<TBL_ModuloAPC_Areas> specification);
    }
}