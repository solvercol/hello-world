using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IUnidadesZonasListView: IView
    {
        event EventHandler FilterEvent;
        event EventHandler PagerEvent;

        void GetUnidadesZonas(List<TBL_ModuloReclamos_UnidadesZonas> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string IdModule { get; }

        string Search { get; }
    }
}
