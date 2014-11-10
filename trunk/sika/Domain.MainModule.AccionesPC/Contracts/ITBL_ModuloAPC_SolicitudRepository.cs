using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.AccionesPC.Contracts
{
    public interface ITBL_ModuloAPC_SolicitudRepository : IRepository<TBL_ModuloAPC_Solicitud>
    {
        TBL_ModuloAPC_Solicitud GetCompleteEntity(ISpecification<TBL_ModuloAPC_Solicitud> specification);
        List<TBL_ModuloAPC_Solicitud> GetCompleteEntityList(ISpecification<TBL_ModuloAPC_Solicitud> specification);
        TBL_ModuloAPC_Solicitud GetSolicitudById(decimal id);
        TBL_ModuloAPC_Solicitud GetSolicitudCierreWf(decimal id);
    }
}