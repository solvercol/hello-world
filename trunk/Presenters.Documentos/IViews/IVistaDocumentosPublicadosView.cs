﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Documentos.IViews
{
    public interface IVistaDocumentosPublicadosView
        : IView
    {
        #region Propiedades

        List<TBL_ModuloDocumentos_Documento> ListaDocumentos { get; set; }

        string FiltroNombre { get; set; }

        #endregion

        #region Eventos

        event EventHandler FilterEvent;

        #endregion

        #region Members

        void ArbolDocumentos();

        #endregion

    }
}
