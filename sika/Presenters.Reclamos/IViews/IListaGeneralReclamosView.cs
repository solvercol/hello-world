using Application.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Presenters.Reclamos.IViews
{
    public interface IListaGeneralReclamosView : IView
    {
        void ShowNewReclamoWindow(bool visible);

        void LoadCategoriasReclamo(List<TBL_ModuloReclamos_CategoriasReclamo> list);

        string TipoReclamo { get; set; }
        string IdCategoriaReclamo { get; }
        string IdGrupoInformacion { get; }
    }
}