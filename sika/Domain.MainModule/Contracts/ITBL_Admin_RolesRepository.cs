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

namespace Domain.MainModule.Contracts
{
    public interface ITBL_Admin_RolesRepository : IRepository<TBL_Admin_Roles>
    {
        TBL_Admin_Roles GetURoleById(int id);

        TBL_Admin_Roles GetURoleByName(string name);
    }
}
    