using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IEditAsesoresView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler DeleteEvent;

        #endregion

        #region Members
        void GetJefes(IList<TBL_Admin_Usuarios> items);
        void GetUnidades(IList<TBL_ModuloReclamos_Unidad> items);
        void GetZonas(IList<TBL_ModuloReclamos_Zona> items);
        string IdUser { get; }
        string IdUnidad { get; set; }
        string IdZona { get; set; }
        string AsesorName { set; }
        List<DTO_ValueKey> UsuariosCopia { get; set; }
        void LoadUsuariosCopia(List<DTO_ValueKey> items);
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        #endregion

    }
}
