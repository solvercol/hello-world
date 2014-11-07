using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface ISeguimientoView : IViewUc
    {
        event EventHandler RefreshEvent;
        string IdSolicitud { get; }
        bool IsLoadUserControl { get; set; }
        void ListadoSeguimiento(List<TBL_ModuloAPC_Tracking> itemS);
    }
}