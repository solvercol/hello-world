using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.DocumentLibrary.IViews
{
    public interface INewFolderView : IViewUc
    {

        event EventHandler SaveEvent;

        string IdParent { get; }

        string Idcategoria { get; }

        string NombreFolder { get; }

        string IdContrato { get; }

        void Listadocategorias(List<TBL_ModuloDocumentosAnexos_Categorias> items);
    }
}