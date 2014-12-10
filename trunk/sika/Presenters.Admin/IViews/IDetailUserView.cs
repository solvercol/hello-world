using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Admin.IViews
{
    public interface IDetailUserView : IView
    {
        #region Events

        #endregion

        #region Members
        void GetAllRoles(IList<TBL_Admin_Roles> items);
        void RolesAsigandos(IList<TBL_Admin_Roles> items);
        string IdUser { get; }
        string UserCode { set; }
        string Names { set; }
        string IncomeDate { set; }
        string UserName { set; }
        string Email { set; }
        bool Activo { set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }

        #endregion
    }
}
