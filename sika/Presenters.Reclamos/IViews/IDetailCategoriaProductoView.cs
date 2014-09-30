using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IDetailCategoriaProductoView : IView
    {
        #region Members
        string IdCategoriaProducto { get; }
        string Nombre { set; }
        string Descripcion { set; }
        string IngResponsables { get; set; }
        bool Activo { set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }
        #endregion
    }
}
