using System;
using Application.Core;

namespace Presenters.AccionesPC.IViews
{
    public interface IPanelResumenWfView : IViewUc
    {
        string Estado { get; set; }
        string SolicitadoPor { get; set; }
        string FechaSolicitud { get; set; }
        string Responsable { get; set; }
        string AsignadoEn { get; set; }
        string NumeroDias { get; set; }
        string IdSolicitud { get; }

        event EventHandler UpdateEvent;
    }
}