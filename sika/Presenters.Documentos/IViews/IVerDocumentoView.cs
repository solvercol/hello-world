﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Documentos.IViews
{
    public interface IVerDocumentoView
        : IView
    {
        #region eventos

        event EventHandler DescargarArchivoEvent;

        #endregion

        #region Miembros

        int IdDocumento { get; }
        string Titulo { set; }
        string Version { set; }
        string Observaciones { set; }
        string Categoria { set; }
        string SubCategoria { set; }
        string TipoDocumento { set; }
        string UsuarioResponsable { set; }

        void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjunto adjunto);
        void Adjuntos(IEnumerable<TBL_ModuloDocumentos_DocumentoAdjunto> adjuntos);

        #endregion
    }
}