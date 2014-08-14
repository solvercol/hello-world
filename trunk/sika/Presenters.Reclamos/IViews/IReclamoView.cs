using Application.Core;
using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IReclamoView : IView
    {
        // Properties
        string IdReclamo { get; }
        string TipoReclamo { get; set; }
        string Solicitante { get; set; }
        string NumeroReclamo { get; set; }
        string DescripcionProblema { get; set; }

        // Load
        void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones);
    }
}