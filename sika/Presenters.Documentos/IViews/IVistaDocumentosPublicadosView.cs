using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;
using System.Data;

namespace Presenters.Documentos.IViews
{
    public interface IVistaDocumentosPublicadosView
        : IView
    {
        #region Propiedades

        string FiltroNombre { get; set; }

        string ServerHostPath { get; }

        #endregion

        #region Eventos

        event EventHandler FilterEvent;

        #endregion

        #region Members

        void LoadView(DataTable dt);

        #endregion

    }
}
