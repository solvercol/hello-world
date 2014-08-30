using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_AnexosActividadManagementServices : IGenericServices<TBL_ModuloReclamos_AnexosActividad>
    {
        List<TBL_ModuloReclamos_AnexosActividad> GetByIdActividad(decimal idActividad);

        TBL_ModuloReclamos_AnexosActividad GetById(decimal idArchivo);
    }
}