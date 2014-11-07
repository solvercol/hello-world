using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface ILogSolicitudesView : IView
    {
        #region Events

        event EventHandler FilterEvent;

        #endregion

        #region Members

        void LogsList(List<TBL_ModuloAPC_LogSolicitud> items);

        string IdSolicitud { get; }

        bool IsLoadedControl { get; set; }

        int TotalRegistrosPaginador { set; }

        int PageSize { get; }

        #endregion

    }
}