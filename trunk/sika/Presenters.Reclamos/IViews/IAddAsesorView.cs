using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IAddAsesorView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        void GetAsesores(IList<TBL_Admin_Usuarios> items);
        void GetJefes(IList<TBL_Admin_Usuarios> items);
        void GetUnidades(IList<TBL_ModuloReclamos_Unidad> items);
        void GetZonas(IList<TBL_ModuloReclamos_Zona> items);
        string IdUser { get; set; }
        string IdUnidad { get; }
        string IdZona { get;}
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        bool InsertFlag { get; set; }
        List<DTO_ValueKey> UsuariosCopia { get; set; }
        void LoadUsuariosCopia(List<DTO_ValueKey> items);
        #endregion
    }
}
