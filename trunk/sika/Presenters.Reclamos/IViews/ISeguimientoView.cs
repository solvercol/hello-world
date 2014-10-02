using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface ISeguimientoView : IViewUc
    {
        event EventHandler RefreshEvent;
        string IdReclamo { get; }
        bool IsLoadUserControl { get; set; }
        void ListadoSeguimiento(List<TBL_ModuloReclamos_Tracking> itemS);
    }
}