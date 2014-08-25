using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Documentos.IViews
{
    public interface IEditarDocumentoView
        :IView
    {
        #region eventos

        event EventHandler GuardarEvent;
        event EventHandler PublicarEvent;
        event EventHandler CancelarEvent;
        event EventHandler DescargarArchivoEvent;
        event EventHandler EliminarAdjuntoEvent;
        event EventHandler GuardarAdjuntoEvent;

        #endregion

        #region Miembros

        int IdDocumento { get; }
        string Titulo { get; set; }
        string Observaciones { get; set; }
        byte[] Archivo { get; }
        string NombreArchivo { get; }
        double TamanioMaxArchivoACargar { get; }
        double TamanioArchivoActual { get; }
        int IdDocCreado { get; set; }
        int IdCategoria { get; set; }
        int IdSubCategoria { get; set; }
        int IdTipoDocumento { get; set; }
        string Categoria { get; set; }
        string SubCategoria { get; set; }
        string TipoDocumento { get; set; }
        int IdUsuarioResponsable { get; set; }
        bool Activo { get; set; }
        void Responsables(IEnumerable<TBL_Admin_Usuarios> responsables);
        void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjunto documento);

        void Adjuntos(IEnumerable<TBL_ModuloDocumentos_DocumentoAdjunto> adjuntos);

        #endregion

    }
}
