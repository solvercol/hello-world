using System.Collections.Generic;
using Domain.Core;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_ActividadesRepository : IRepository<TBL_ModuloReclamos_Actividades>
    {
        TBL_ModuloReclamos_Actividades GetCompleteEntityBySpec(ISpecification<TBL_ModuloReclamos_Actividades> specification);
        List<TBL_ModuloReclamos_Actividades> GetCompleteListBySpec(ISpecification<TBL_ModuloReclamos_Actividades> specification);
        bool VerificarActividadesPorEstadoPorreclamo(string estado, decimal idReclamo);
    }
}