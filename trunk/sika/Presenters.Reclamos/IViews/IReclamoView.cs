using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IReclamoView : IView
    {

        event EventHandler FilterEvent;

        // Properties
        string IdReclamo { get; }
        string TipoReclamo { get; set; }
        string NumeroReclamo { get; set; }

        string IdCategoriaReclamo { get; set; }
        string IdGrupoInformacion { get; set; }
        string MonedaLocal { get; set; }

        // Seccion Info Reclamo
        string TitleReclamo { get; set; }
        string TitleReclamoFrom { get; set; }
        string Unidad { get; set; }
        string Categoria { get; set; }
        string FechaReclamo { get; set; }
        string Responsable { get; set; }
        string TotalCostoReclamo { get; set; }
        string IdCategoria { get; set; }
        bool VerCrearAccion { set; }
        bool VerBotonEdicion { set; }

        // Load
        void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones);
        void LoadInitReclamoControl();
    }
}