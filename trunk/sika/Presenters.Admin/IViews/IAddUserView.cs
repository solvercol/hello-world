using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Admin.IViews
{
    public interface IAddUserView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        void GetAllRoles(IList<TBL_Admin_Roles> items);
        string UserCode { get; set; }
        string Names { get; set; }
        DateTime IncomeDate { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        bool Activo { get; set; }
        ArrayList GetSelectdRole();
        #endregion
    }
}
