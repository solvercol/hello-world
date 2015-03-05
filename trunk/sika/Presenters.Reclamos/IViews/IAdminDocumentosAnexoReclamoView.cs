using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminDocumentosAnexoReclamoView : IView
    {
        string IdReclamo { get; }

        bool IsNew { get; set; }

        string IdDocumentoSelected { get; set; }

        string Categoria { get; set; }
        string CategoriaDocumento { get; set; }
        string Titulo { get; set; }
        string Descripcion { get; set; }
        string NombreArchivo { get; }
        byte[] ArchivoAnexo { get; }
        void LoadCategorias(List<DTO_ValueKey> items);
        void LoadAnexos(List<TBL_ModuloReclamos_DocumentosAnexoReclamo> items);
        void ShowAdminDoc(bool visible);
    }
}
