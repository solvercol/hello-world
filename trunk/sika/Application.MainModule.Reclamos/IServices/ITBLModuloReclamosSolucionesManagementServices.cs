using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_SolucionesManagementServices : IGenericServices<TBL_ModuloReclamos_Soluciones>
    {
        TBL_ModuloReclamos_Soluciones GetById(decimal id);
        List<TBL_ModuloReclamos_Soluciones> GetByIdReclamo(decimal idReclamo);
    }
}