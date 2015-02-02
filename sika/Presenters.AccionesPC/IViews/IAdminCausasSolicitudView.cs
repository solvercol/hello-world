using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminCausasSolicitudView : IView
    {

        event EventHandler FilterEvent;
        // Admin Causa
        string IdSolicitud { get; }
        string Descripcion { get; set; }
        string Comentarios { get; set; }

        bool IsNew { get; set; }
        decimal SelectedId { get; set; }

        void ShowAdminCausaWindow(bool visible);

        void LoadCausasSolicitud(List<TBL_ModuloAPC_Causas> items);

        bool CanAddCausas { get; set; }
    }
}