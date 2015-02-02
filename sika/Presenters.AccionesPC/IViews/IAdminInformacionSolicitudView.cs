using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminInformacionSolicitudView : IView
    {
        event EventHandler FilterEvent;

        // Properties
        string IdSolicitud { get; }

        // Seccion Info Solicitud
        string ProcesoAsociado { get; set; }
        string DescripcionAccion { get; set; }
        string Observaciones { get; set; }
        string ResultadoCierre { get; set; }
        string ObservacionesCierre { get; set; }
        bool ConformidadEliminada { set; }
        bool ShowInfoCierre { get; set; }
        string ReclamosRelacionados { set; }
    }
}