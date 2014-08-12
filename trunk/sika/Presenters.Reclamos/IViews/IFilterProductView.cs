using System.Collections.Generic;
using Application.Core;
using Domain.MainModule.Reclamos.DTO;
using System;

namespace Presenters.Reclamos.IViews
{
    public interface IFilterProductView : IView
    {
        event EventHandler Filterevent;

        Dto_Producto SelectedProduct { get; set; }

        string FilterText { get; set; }

        void LoadSelectedProducto(Dto_Producto producto);

        void LoadProructos(List<Dto_Producto> items);

        void ShowSelectProductWindow(bool visible);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }
    }
}