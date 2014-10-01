using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IAddZonaView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        string Descripcion { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string IdModule { get; }

        #endregion
    }
}
