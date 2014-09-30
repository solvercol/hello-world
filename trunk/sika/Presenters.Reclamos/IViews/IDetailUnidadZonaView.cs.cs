using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IDetailUnidadZonaView : IView
    {
        #region Events

        event EventHandler DeleteEvent;

        #endregion

        #region Members
        string IdUnidad { get; }
        string IdZona { get; }
        string IdGerente { get;  }
        string Unidad { set; }
        string Zona { set; }
        string Gerente { set; }
        string Descripcion { set; }
        decimal TarifasFletes { set; }
        bool Activo { set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }
        #endregion
    }
}
