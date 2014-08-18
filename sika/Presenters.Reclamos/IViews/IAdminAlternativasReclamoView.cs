using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminAlternativasReclamoView : IView
    {
        // Admin Alternativa
        string IdReclamo { get; }
        string Alternativa { get; set; }
        string Causas { get; set; }
        string Factores { get; set; }
        string IdResponsable { get; set; }
        DateTime FechaAlternativa { get; set; }
        string Seguimiento { get; set; }
        string Estado { get; set; }

        string IdSelectedAlternativa { get; set; }
        bool IsNewAlternativa { get; set; }

        //Methods
        void ShowAdminAlternativaWindow(bool visible);
        void LoadAlternativasReclamo(List<TBL_ModuloReclamos_Alternativas> items);
        void LoadResponsables(List<TBL_Admin_Usuarios> items);
    }
}