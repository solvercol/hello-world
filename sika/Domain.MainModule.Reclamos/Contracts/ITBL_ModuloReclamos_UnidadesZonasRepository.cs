//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Reclamos.Contracts
{
    public interface ITBL_ModuloReclamos_UnidadesZonasRepository : IRepository<TBL_ModuloReclamos_UnidadesZonas>
    {
        TBL_ModuloReclamos_UnidadesZonas GetUnidadZonaBySpec(ISpecification<TBL_ModuloReclamos_UnidadesZonas> specification);
    }
}
    