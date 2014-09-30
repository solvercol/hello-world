using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface ICategoriaProductoListView : IView
    {
        event EventHandler FilterEvent;
        event EventHandler PagerEvent;

        void GetCategoriaProductos(List<TBL_ModuloReclamos_CategoriaProducto> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string IdModule { get; }

        string Search { get; }
    }
}
