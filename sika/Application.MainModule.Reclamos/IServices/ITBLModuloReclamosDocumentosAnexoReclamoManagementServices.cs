using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;
using System;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_DocumentosAnexoReclamoManagementServices : IGenericServices<TBL_ModuloReclamos_DocumentosAnexoReclamo>
    {
        TBL_ModuloReclamos_DocumentosAnexoReclamo GetById(Guid id);
        List<TBL_ModuloReclamos_DocumentosAnexoReclamo> GetAnexosByReclamoCategoria(int idReclamo, string categoria);
    }
}
    