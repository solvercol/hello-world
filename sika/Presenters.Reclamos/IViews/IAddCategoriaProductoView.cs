using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IAddCategoriaProductoView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        void GetIngenieros(IList<TBL_Admin_Usuarios> items);
        string IdCategoriaProducto { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        bool Activo { get; set; }
        List<DTO_ValueKey> UsuariosCopia { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string IdModule { get; }
        void LoadUsuariosCopia(List<DTO_ValueKey> items);
        #endregion
    }
}
