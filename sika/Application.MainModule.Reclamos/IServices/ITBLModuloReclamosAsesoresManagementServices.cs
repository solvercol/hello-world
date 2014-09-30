using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_AsesoresManagementServices : IGenericServices<TBL_ModuloReclamos_Asesores>
    {
        List<TBL_Admin_Usuarios> GetAll();
        int CountByPaged(string search);
        List<TBL_ModuloReclamos_Asesores> FindPaged(int pageIndex, int pageCount, string search);
    
    }
}