using System;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminActividadSolicitudView : IView
    {

        #region Events

        event EventHandler CancelEvent;
        event EventHandler CloseEvent;

        #endregion

        // Seccion Info Reclamo
        string IdReclamo { get; }
        string TipoReclamo { get; set; }
        string NumeroReclamo { get; set; }
        string TitleReclamo { get; set; }
        string TitleReclamoFrom { get; set; }
        string Unidad { get; set; }
        string FechaReclamo { get; set; }
        string Asesor { get; set; }
        bool ShowInfoReclamo { get; set; }


        // Admin Actividad
        string IdActividad { get; }
        string Estado { get; set; }
        string Descripcion { get; set; }
        DateTime FechaActividad { get; set; }
        string UsuarioSeguimiento { get; set; }
        string UsuarioEjecucion { get; set; }
        string Observaciones { get; set; }
    

        string ObservacionesCierre { get; set; }
        string ObservacionesCancelacion { get; set; }

        // Seccion Info Solicitud
        string IdSolicitud{ get;}
        string CodSolicitud { get; set; }
        string TipoAccion { get; set; }
        string Area { get; set; }
        string GerenteArea { get; set; }
        string ResponsableAccion { get; set; }
        string FechaInicio { get; set; }
        string FechaFinal { get; set; }
        string LogInfoMessage { set; }

        // Methods
        void EnableEdit(bool enabled);

        // Archivos Adjuntos        
        byte[] ArchivoAdjunto { get; }
        string NombreArchivoAdjunto { get; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void DescargarArchivo(DTO_ValueKey archivo);

        // Register
        bool CanRegister { get; set; }
        void ShowObservaciones(bool visible);
    }
}
