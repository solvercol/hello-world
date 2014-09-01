using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.MainModules.Entities;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_LogReclamosManagementServices : IGenericServices<TBL_ModuloReclamos_LogReclamos>
    {

        TBL_ModuloReclamos_LogReclamos FindById(Guid id);
        List<TBL_ModuloReclamos_LogReclamos> GetByIdReclamo(decimal idReclamo);
    }
}