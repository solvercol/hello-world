using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IFrmViewAreasView : IView
    {
        event EventHandler FilterEvent;
        event EventHandler PagerEvent;

        void GetAreas(List<TBL_ModuloAPC_Areas> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string IdModule { get; }

        string Search { get; }
    }
}