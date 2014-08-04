using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IPlantillasCorreoListView : IView
    {
        event EventHandler FilterEvent;

        void GetTemplates(List<TBL_Admin_Plantillas> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string ModuleSetupId { get; set; }
    }
}