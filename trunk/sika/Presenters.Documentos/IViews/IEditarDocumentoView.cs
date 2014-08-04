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
        #region eventos y delegados

        event EventHandler GuardarEvent;
        event EventHandler PublicarEvent;
        event EventHandler CancelarEvent;
        event EventHandler DescargarArchivoEvent;

        #endregion

        #region Miembros

        int IdDocumento { get; }
        string Titulo { get; set; }
        string Observaciones { get; set; }
        byte[] Archivo { get; set; }
        string NombreArchivo { get; }
        Int32 TamanioMaxArchivoACargar { get; }
        Int32 TamanioArchivoActual { get; }
        int IdCategoria { get; set; }
        int IdSubCategoria { get; set; }
        int IdTipoDocumento { get; set; }
        string Categoria { get; set; }
        string SubCategoria { get; set; }
        string TipoDocumento { get; set; }
        int IdUsuarioResponsable { get; set; }
        bool Activo { get; set; }
        void Responsables(IEnumerable<TBL_Admin_Usuarios> responsables);
        void DescargarArchivo(TBL_ModuloDocumentos_Documento documento);
        #endregion

    }
}
