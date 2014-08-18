using System.Collections.Generic;
using Domain.Core;
using Domain.MainModules.Entities;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_AlternativasManagementServices : IGenericServices<TBL_ModuloReclamos_Alternativas>
    {
        TBL_ModuloReclamos_Alternativas GetById(decimal id);
        List<TBL_ModuloReclamos_Alternativas> GetByIdReclamo(decimal idReclamo);
    }
}