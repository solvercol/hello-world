using System;
using System.Collections.Generic;
using System.Data;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Documentos.IViews
{
    public interface IDocumentosTableView : IView
    {
        #region Propiedades

        string ServerHostPath { get; }

        string FiltroNombre { get; set; }

        int FiltroIdEstado { get; set; }

        int FiltroIdResponsable { get; set; }

        #endregion

        #region Eventos

        event EventHandler FilterEvent;
        event EventHandler ClearFilterEvent;
        event EventHandler DeleteEvent;

        #endregion

        #region Members

        void Estados(IEnumerable<TBL_ModuloDocumentos_Estados> estados);

        void Responsables(DataTable items);

        void LoadView(DataTable dt);

        #endregion

    }
}