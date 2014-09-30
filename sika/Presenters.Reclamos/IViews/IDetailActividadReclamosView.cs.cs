using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IDetailActividadReclamosView : IView
    {
        #region Members
        string IdActividadReclamo { get; }
        string Nombre { set; }
        string Descripcion { set; }
        string IdTipoReclamo { get; set; }
        bool Activo { set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }
        #endregion
    }
}
