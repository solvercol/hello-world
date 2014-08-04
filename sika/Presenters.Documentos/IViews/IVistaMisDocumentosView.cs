using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Documentos.IViews
{
    public interface IVistaMisDocumentosView
        :IView
    {

        #region Propiedades

        List<TBL_ModuloDocumentos_Documento> ListaDocumentos { get; set; }

        string FiltroNombre { get; set; }

        int FiltroIdEstado { get; set; }

        #endregion
        
        #region Eventos

        event EventHandler FilterEvent;
        event EventHandler ClearFilterEvent;

        #endregion

        #region Members

        void ArbolDocumentos();

        void Estados(IEnumerable<TBL_ModuloDocumentos_Estados> estados);

        #endregion
    }
}
