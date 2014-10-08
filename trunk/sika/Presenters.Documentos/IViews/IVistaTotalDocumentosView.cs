using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;
using System.Data;

namespace Presenters.Documentos.IViews
{
    public interface IVistaTotalDocumentosView
        :IView
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

        void Responsables(IEnumerable<TBL_Admin_Usuarios> responsables);

        void LoadView(DataTable dt);

        #endregion

    }
}
