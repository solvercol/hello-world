//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using Domain.MainModule.Documentos.Contracts;
using Infraestructure.Data.Core;
using Infrastructure.CrossCutting.Logging;
using Domain.MainModules.Entities;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infraestructura.Data.Documentos.Repositories
{
    public class TBL_ModuloDocumentos_LogCambiosRepository : GenericRepository<TBL_ModuloDocumentos_LogCambios>, ITBL_ModuloDocumentos_LogCambiosRepository 
    {
        public TBL_ModuloDocumentos_LogCambiosRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
    }
}
    