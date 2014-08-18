using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_ComentariosRespuestaRepository : IRepository<TBL_ModuloReclamos_ComentariosRespuesta>
    {
        TBL_ModuloReclamos_ComentariosRespuesta GetCompleteEntityBySpec(ISpecification<TBL_ModuloReclamos_ComentariosRespuesta> specification);
        List<TBL_ModuloReclamos_ComentariosRespuesta> GetCompleteListBySpec(ISpecification<TBL_ModuloReclamos_ComentariosRespuesta> specification);
    }
}
   