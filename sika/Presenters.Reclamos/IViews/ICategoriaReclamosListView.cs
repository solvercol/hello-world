using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface ICategoriaReclamosListView : IView
    {
        event EventHandler FilterEvent;
        event EventHandler PagerEvent;

        void GetCategoriaReclamos(List<TBL_ModuloReclamos_CategoriasReclamo> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string IdModule { get;}

        string Search { get; }
    }
}
