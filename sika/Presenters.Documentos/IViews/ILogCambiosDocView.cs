using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Documentos.IViews
{
    public interface ILogCambiosDocView
        : IView
    {
        #region Events

        event EventHandler FilterEvent;

        #endregion

        #region Members

        void LogsList(List<TBL_ModuloDocumentos_LogCambios> items);

        //int PageZise { get; }

        //int TotalRegistrosPaginador { set; }

        int IdDocumento { get; }

        bool IsLoadedControl { get; set; }

        #endregion

    }
}
