using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IEditActividadReclamosView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        #endregion

        #region Members
        void GetTipoReclamos(IList<TBL_ModuloReclamos_TipoReclamo> items);
        string IdActividadReclamo { get; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        int IdTipoReclamo { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }
        #endregion
    }
}
