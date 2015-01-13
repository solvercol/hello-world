using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IEditAreasViewn : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler DeleteEvent;

        #endregion

        #region Members
        void GetGerentes(IList<TBL_Admin_Usuarios> items);
        string IdGerente { get; set; }
        string IdArea { get; }
        string Nombre { get; set; }
        string Procesos { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }
        #endregion
    }
}