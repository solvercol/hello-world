using System;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminActividadReclamoView : IView
    {
        // Admin Actividad
        string IdActividad { get; }
        string Estado { get; set; }
        string Descripcion { get; set; }
        string Actividad { get; set; }
        DateTime FechaActividad { get; set; }
        string UsuarioAsignacion { get; set; }
        string Observaciones { get; set; }
        string LogCierre { get; set; }
        void LoadUsuariosCopia(List<DTO_ValueKey> items);

        string ObservacionesCierre { get; set; }
        string ObservacionesCancelacion { get; set; }

        // Seccion Info Reclamo
        string IdReclamo { get; set; }
        string TipoReclamo { get; set; }
        string NumeroReclamo { get; set; }
        string MonedaLocal { get; set; }
        string TitleReclamo { get; set; }
        string TitleReclamoFrom { get; set; }
        string Unidad { get; set; }
        string FechaReclamo { get; set; }
        string Asesor { get; set; }

        // Methods
        void EnableEdit(bool enabled);

        // Archivos Adjuntos        
        byte[] ArchivoAdjunto { get; }
        string NombreArchivoAdjunto { get; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void DescargarArchivo(DTO_ValueKey archivo);

        // Register
        bool CanRegister { get; set; }
        void ShoeObservaciones(bool visible);
    }
}