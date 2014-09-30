using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Reclamos.IViews
{
    public interface IAddUnidadZonaView : IView
    {

        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members
        void GetGerentes(IList<TBL_Admin_Usuarios> items);
        void GetUnidades(IList<TBL_ModuloReclamos_Unidad> items);
        void GetZonas(IList<TBL_ModuloReclamos_Zona> items);
        string Descripcion { get; set; }
        decimal TarifasFletes { get; set; }
        int IdGerente { get; set; }
        int IdUnidad { get; set; }
        int IdZona { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string IdModule { get; }

        #endregion
    }
}
