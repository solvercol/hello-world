using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface ISolicitudAPCView : IView
    {
        // Properties
        string IdSolicitud { get; }

        // Seccion Info Solicitud
        string TipoAccion { get; set; }
        string Area { get; set; }
        string GerenteArea { get; set; }
        string ResponsableAccion { get; set; }
        string FechaInicio { get; set; }
        string FechaFinal { get; set; }
        string LogInfoMessage { set; }

        // Seccion Info Reclamo
        string IdReclamo { get; }
        string TipoReclamo { get; set; }
        string NumeroReclamo { get; set; }
        string TitleReclamo { get; set; }
        string TitleReclamoFrom { get; set; }
        string Unidad { get; set; }
        string FechaReclamo { get; set; }
        string Asesor { get; set; }
        bool ShowInfoReclamo { get; set; }
        bool MostrarBotonCierreSolicitud {  set; }

        // Load
        void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones);
        void LoadInitSolicitudControl();
        string IdModuleReclamo { get; set; }
    }
}