using System.Collections.Generic;
using Application.Core;
using Domain.MainModule.Reclamos.DTO;
using System;

namespace Presenters.Reclamos.IViews
{
    public interface IFilterClientView : IView
    {
        event EventHandler Filterevent;

        Dto_Cliente SelectedClient { get; set; }

        string FilterText { get; set; }

        string IdReclamo { get; }

        void LoadSelectedClient(Dto_Cliente cliente);

        void LoadClientes(List<Dto_Cliente> items);

        void ShowSelectClienteWindow(bool visible);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }
    }
}