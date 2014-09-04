using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_UnidadesZonasManagementServices : IGenericServices<TBL_ModuloReclamos_UnidadesZonas>
    {
        TBL_ModuloReclamos_UnidadesZonas GetByUnidadZona(string unidad, string zona);
    }
}