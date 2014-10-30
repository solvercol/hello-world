using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.AccionesPC.IServices
{
    public interface ISfTBL_ModuloAPC_CausasManagementServices : IGenericServices<TBL_ModuloAPC_Causas>
    {
        TBL_ModuloAPC_Causas GetById(decimal id);
        List<TBL_ModuloAPC_Causas> GetByIdSolicitud(decimal idSolicitud);
    }
}