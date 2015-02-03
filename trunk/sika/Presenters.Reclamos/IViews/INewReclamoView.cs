using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface INewReclamoView : IView
    {
        void LoadCategoriasReclamo(List<TBL_ModuloReclamos_CategoriasReclamo> list);

        string TipoReclamo { get; set; }
        string IdCategoriaReclamo { get; }
        string IdGrupoInformacion { get; }        
    }
}