using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminCausasSolicitudView : IView
    {
        // Admin Causa
        string IdSolicitud { get; }
        string Descripcion { get; set; }
        string Comentarios { get; set; }

        void LoadCausasSolicitud(List<TBL_ModuloAPC_Causas> items);
    }
}