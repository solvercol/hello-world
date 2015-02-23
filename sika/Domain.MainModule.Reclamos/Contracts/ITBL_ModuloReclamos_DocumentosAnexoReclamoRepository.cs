using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_DocumentosAnexoReclamoRepository : IRepository<TBL_ModuloReclamos_DocumentosAnexoReclamo>
    {
        TBL_ModuloReclamos_DocumentosAnexoReclamo GetCompleteEntity(ISpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification);
        List<TBL_ModuloReclamos_DocumentosAnexoReclamo> GetCompleteEntityList(ISpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification);
    }
}