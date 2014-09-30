using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAsesoresListView : IView
    {
        event EventHandler FilterEvent;
        event EventHandler PagerEvent;

        void GetAsesores(List<TBL_ModuloReclamos_Asesores> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string IdModule { get; }

        string Search { get; }

    }
}