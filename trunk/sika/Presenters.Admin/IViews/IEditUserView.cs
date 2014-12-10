using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Admin.IViews
{
    public interface IEditUserView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        #endregion

        #region Members
        void GetAllRoles(IList<TBL_Admin_Roles> items);
        void RolesAsigandos(IList<TBL_Admin_Roles> items);
        string IdUser { get; }
        string UserCode { get; set; }
        string Names { get; set; }
        DateTime IncomeDate { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        ArrayList GetSelectdRole();
        #endregion
    }
}
