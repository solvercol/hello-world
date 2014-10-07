using System;
using System.Collections.Generic;
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
        int IdUsuarioResponsable { get; set; }
        bool Activo { get; set; }
        string LogInfo { set; }
        string Estado { set; }
        int IdNivelCategoria { get; set; }
        string NuevaCategoria { get; set; }

        void Responsables(IEnumerable<TBL_Admin_Usuarios> responsables);
        void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjunto documento);

        void LoadCategorias(List<TBL_ModuloDocumentos_Categorias> items);
        void LoadSubCategorias(List<TBL_ModuloDocumentos_Categorias> items);
        void LoadTiposDocumento(List<TBL_ModuloDocumentos_Categorias> items);
        
        // Archivos Adjuntos
        List<DTO_ValueKey> ArchivosAdjuntos { get; set; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);

        void GoToViewDocument(int idDocumento);

        #endregion

    }
}
