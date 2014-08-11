using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Documentos.IViews
{
    public interface IVerHistDocumentoView
        : IView
    {
        #region eventos

        event EventHandler DescargarArchivoEvent;

        #endregion

        #region Miembros

        int IdHistDocumento { get; }
        decimal IdDocumento { get; set; }
        string Titulo { set; }
        string Version { set; }
        string Observaciones { set; }
        //byte[] Archivo { set; }
        string Categoria { set; }
        string SubCategoria { set; }
        string TipoDocumento { set; }
        string UsuarioResponsable { set; }
        bool Activo { set; }
        void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjuntoHistorial histDocumento);
        void Adjuntos(IEnumerable<TBL_ModuloDocumentos_DocumentoAdjuntoHistorial> adjuntosHist);
        #endregion
    }
}
