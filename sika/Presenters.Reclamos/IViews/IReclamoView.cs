using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IReclamoView : IView
    {
        // Properties
        string IdReclamo { get; }
        string TipoReclamo { get; set; }
        string NumeroReclamo { get; set; }

        string IdCategoriaReclamo { get; set; }
        string IdGrupoInformacion { get; set; }

        // Load
        void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones);
        void LoadInitReclamoControl();
    }
}