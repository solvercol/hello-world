using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAddAreasView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        void GetGerentes(IList<TBL_Admin_Usuarios> items);
        string IdArea { get; set; }
        string Nombre { get; set; }
        string Proceso { get; set; }
        string IdGerente { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string IdModule { get; }
        #endregion
    }
}