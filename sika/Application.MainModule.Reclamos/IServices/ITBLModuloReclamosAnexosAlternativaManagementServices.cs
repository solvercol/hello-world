using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_AnexosAlternativaManagementServices : IGenericServices<TBL_ModuloReclamos_AnexosAlternativa>
    {
        List<TBL_ModuloReclamos_AnexosAlternativa> GetByIdAlternativa(decimal idAlternativa);

        TBL_ModuloReclamos_AnexosAlternativa GetById(decimal idArchivo);
    }
}
    