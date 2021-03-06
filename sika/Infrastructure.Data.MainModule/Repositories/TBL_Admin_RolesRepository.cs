//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System.Linq;
using Domain.MainModule.Contracts;
using Infraestructure.Data.Core;
using Infrastructure.CrossCutting.Logging;
using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infrastructure.Data.MainModule.Repositories
{
    public class TBL_Admin_RolesRepository : GenericRepository<TBL_Admin_Roles>, ITBL_Admin_RolesRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_Admin_RolesRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_Admin_Roles GetURoleById(int id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_Admin_Roles>();

                return set.Where(c => c.IdRol == id)
                          .Select(c => c)
                          .SingleOrDefault();
            }
            return null;
        }

        public TBL_Admin_Roles GetURoleByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var set = _currentUnitOfWork.CreateSet<TBL_Admin_Roles>();

                return set.Where(c => c.NombreRol == name)
                          .Select(c => c)
                          .SingleOrDefault();
            }
            return null;
        }

    }
}
    