using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IEditUnidadZonaView:IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        #endregion

        #region Members

        string IdUnidad { get; }
        string IdZona { get; }
        string IdGerente { get; }
        string Unidad { set; }
        string Zona { set; }
        string Gerente { set; }
        string Descripcion { get; set; }
        decimal TarifasFletes { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }

        #endregion
    }
}
