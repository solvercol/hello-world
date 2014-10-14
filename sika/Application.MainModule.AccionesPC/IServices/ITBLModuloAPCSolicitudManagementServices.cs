using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.AccionesPC.IServices
{
    public interface ISfTBL_ModuloAPC_SolicitudManagementServices : IGenericServices<TBL_ModuloAPC_Solicitud>
    {
        TBL_ModuloAPC_Solicitud GetById(decimal id);
        TBL_ModuloAPC_Solicitud GetWithNavById(decimal id);
    }
}
    