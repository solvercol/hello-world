using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface ILogReclamoView : IView
    {
        #region Events

        event EventHandler FilterEvent;

        #endregion

        #region Members

        void LogsList(List<TBL_ModuloReclamos_LogReclamos> items);

        string IdReclamo { get; }

        bool IsLoadedControl { get; set; }

        #endregion

    }
}
