using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IAddCategoriasReclamosView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        void GetResponsables(IList<TBL_Admin_Usuarios> items);
        void GetTipoReclamos(IList<TBL_ModuloReclamos_TipoReclamo> items);
       // string IdCategoriaReclamo { set; }
        string Nombre { get; set; }
        string SubCategoria { get; set; }
        string Descripcion { get; set; }
        string Area { get; set; }
        int GrupoInformacion { get; set; }
        int IdResponsable { get; set; }
        int IdTipoReclamo { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string IdModule { get; }
        #endregion
    }
}
