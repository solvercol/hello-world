using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IAddActividadReclamosView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        void GetTipoReclamos(IList<TBL_ModuloReclamos_TipoReclamo> items);
        string Nombre { get; set; }
        string Descripcion { get; set; }
        int IdTipoReclamo { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string IdModule { get; }
        #endregion
    }
}
