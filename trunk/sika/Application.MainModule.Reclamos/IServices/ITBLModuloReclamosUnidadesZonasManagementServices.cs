using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_UnidadesZonasManagementServices : IGenericServices<TBL_ModuloReclamos_UnidadesZonas>
    {
        TBL_ModuloReclamos_UnidadesZonas GetByUnidadZona(string unidad, string zona);
        int CountByPaged(string search);
        List<TBL_ModuloReclamos_UnidadesZonas> FindPaged(int pageIndex, int pageCount, string search);
        TBL_ModuloReclamos_UnidadesZonas FindById(int IdUnidad, int IdZona, int IdGerente);
    }
}