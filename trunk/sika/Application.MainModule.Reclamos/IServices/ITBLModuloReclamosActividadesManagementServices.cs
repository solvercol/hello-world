using System.Collections.Generic;
using Domain.Core;
using Domain.MainModules.Entities;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_ActividadesManagementServices : IGenericServices<TBL_ModuloReclamos_Actividades>
    {
        TBL_ModuloReclamos_Actividades GetById(decimal id);
        List<TBL_ModuloReclamos_Actividades> GetByIdReclamo(decimal idReclamo);
    }
}