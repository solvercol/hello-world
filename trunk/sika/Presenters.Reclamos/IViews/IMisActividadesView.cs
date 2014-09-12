using System;
using System.Data;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IMisActividadesView : IView
    {
        // Filtros
        string ServerHostPath { get; }
        string FilterNoReclamo { get; set; }
        string FilterCliente { get; set; }
        string FilterProducto { get; set; }
        string FilterServicio { get; set; }
        DateTime FechaFilterFrom { get; set; }
        DateTime FechaFilterTo { get; set; }

        void LoadView(DataTable dt);
    }
}