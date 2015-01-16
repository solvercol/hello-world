using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IEditCategoriaReclamosView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        #endregion

        #region Members
        void GetResponsables(IList<TBL_Admin_Usuarios> items);
        void GetTipoReclamos(IList<TBL_ModuloReclamos_TipoReclamo> items);
        string IdCategoriaReclamo { get; }
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
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get;}
        string MensajeMultivalor { set; }
        #endregion
    }
}
