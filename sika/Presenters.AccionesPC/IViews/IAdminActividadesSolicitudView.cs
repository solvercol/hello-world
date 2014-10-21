using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminActividadesSolicitudView : IView
    {
        // Admin Actividad
        string IdSolicitud{ get; }
        //string IdActividadSolicitud{ get; set; }
        string Estado { get; set; }
        string Descripcion { get; set; }
        DateTime FechaActividad { get; set; }
        string IdUsuarioEjecucion { get; set; }
        string IdUsuarioSeguimiento { get; set; }

        string IdSelectedActividad { get; set; }
        bool IsNewActividad { get; set; }

        //Methods
        void ShowAdminActividadWindow(bool visible);
        void LoadActividadesSolicitud(List<TBL_ModuloAPC_Actividades> items);
        void LoadUsuarioSeguimiento(List<TBL_Admin_Usuarios> items);
        void LoadUsuarioEjecucion(List<TBL_Admin_Usuarios> items);

        void EnableEdit(bool enable);

        // Archivos Adjuntos
        List<DTO_ValueKey> ArchivosAdjuntos { get; set; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);

        // Controles Edicion
        bool CanAddActividades { get; set; }
    }
}
