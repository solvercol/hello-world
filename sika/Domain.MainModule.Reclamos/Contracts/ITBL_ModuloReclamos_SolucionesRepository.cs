using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_SolucionesRepository : IRepository<TBL_ModuloReclamos_Soluciones>
    {
        TBL_ModuloReclamos_Soluciones GetCompleteEntityBySpec(ISpecification<TBL_ModuloReclamos_Soluciones> specification);

        List<TBL_ModuloReclamos_Soluciones> GetCompleteListBySpec(ISpecification<TBL_ModuloReclamos_Soluciones> specification);
    }
}    