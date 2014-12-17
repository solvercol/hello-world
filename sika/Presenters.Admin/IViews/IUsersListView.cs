using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IUsersListView : IView
    {
        event EventHandler FilterEvent;

        void GetUsers(List<TBL_Admin_Usuarios> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string ModuleSetupId { get; set; }

        string SearchText { get; set; }
    }
}
