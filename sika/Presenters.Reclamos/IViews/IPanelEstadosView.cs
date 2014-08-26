using System;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IPanelEstadosView : IViewUc
    {
        string Estado { get; set; }
        string SolicitadoPor { get; set; }
        string FechaSolicitud { get; set; }
        string Responsable { get; set; }
        string AsignadoEn { get; set; }
        string NumeroDias { get; set; }
        string IdReclamo { get;  }

        event EventHandler UpdateEvent;
    }
}