using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_AsesoresRepository : IRepository<TBL_ModuloReclamos_Asesores>
    {
        List<TBL_Admin_Usuarios> GetUsuariosBySpec(ISpecification<TBL_ModuloReclamos_Asesores> specification);
    }
}