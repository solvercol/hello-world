using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;
using System.Data;

namespace Presenters.Documentos.IViews
{
    public interface IVistaMisDocumentosView
        :IView
    {

        #region Propiedades

        string FiltroNombre { get; set; }

        int FiltroIdEstado { get; set; }

        string ServerHostPath { get; }

        #endregion
        
        #region Eventos

        event EventHandler FilterEvent;
        event EventHandler ClearFilterEvent;

        #endregion

        #region Members

        void Estados(IEnumerable<TBL_ModuloDocumentos_Estados> estados);

        void LoadView(DataTable dt);

        #endregion
    }
}
