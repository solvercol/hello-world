//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using Domain.MainModule.AccionesPC.Contracts;
using Infraestructure.Data.Core;
using Infrastructure.CrossCutting.Logging;
using Domain.MainModules.Entities;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infraestructura.Data.AccionesPC.Repositories
{
    public class TBL_ModuloAPC_LogSolicitudRepository : GenericRepository<TBL_ModuloAPC_LogSolicitud>, ITBL_ModuloAPC_LogSolicitudRepository 
    {
        public TBL_ModuloAPC_LogSolicitudRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
    }
}
    