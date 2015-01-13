using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.AccionesPC.IServices
{
    public interface ISfTBL_ModuloAPC_AreasManagementServices : IGenericServices<TBL_ModuloAPC_Areas>
    {
        TBL_ModuloAPC_Areas GetById(int id);
        List<TBL_ModuloAPC_Areas> GetEntitiesWithGerente();
        List<TBL_ModuloAPC_Areas> GetAreasConSolicitudes();
        List<TBL_ModuloAPC_Areas> FindPaged(int pageIndex, int pageCount, string search);
        int CountByPaged(string search);
    }
}
    