using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminActividadesReclamoView : IView
    {
        // Admin Actividad
        string IdReclamo { get; }
        string IdActividadReclamo { get; set; }
        string Estado { get; set; }
        string Descripcion { get; set; }
        DateTime FechaActividad { get; set; }
        string IdUsuarioAsignacion { get; set; }
        string Observaciones { get; set; }

        string IdSelectedActividad { get; set; }
        bool IsNewActividad { get; set; }

        //Methods
        void ShowAdminActividadWindow(bool visible);
        void LoadActividadesReclamo(List<TBL_ModuloReclamos_Actividades> items);
        void LoadUsuarioAsignacion(List<TBL_Admin_Usuarios> items);
        void LoadActividadesAdmin(List<TBL_ModuloReclamos_ActividadesReclamo> items);
    }
}