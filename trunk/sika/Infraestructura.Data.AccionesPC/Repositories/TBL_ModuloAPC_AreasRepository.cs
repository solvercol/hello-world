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
using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infrastructure.Data.MainModule.AccionesPC.Repositories
{
    public class TBL_ModuloAPC_AreasRepository : GenericRepository<TBL_ModuloAPC_Areas>, ITBL_ModuloAPC_AreasRepository 
    {
        public TBL_ModuloAPC_AreasRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
    }
}
    