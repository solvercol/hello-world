using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.AccionesPC.IServices
{
    public interface ISfTBL_ModuloAPC_AnexosSolicitudManagementServices : IGenericServices<TBL_ModuloAPC_AnexosSolicitud>
    {
        List<TBL_ModuloAPC_AnexosSolicitud> GetByIdSolicitud(decimal id);
        TBL_ModuloAPC_AnexosSolicitud GetById(decimal id);
    }
}
    