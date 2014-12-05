using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminSolicitudAPCView : IView
    {
        // Global
        string ConsecutivoSolicitud { get; set; }

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

        // Informacion de Solicitud
        string IdSolicitud { get; }
        string TipoAccion { get; set; }
        int IdAreaAccion { get; set; }
        string Proceso { get; set; }
        int IdGerente { get; set; }
        string Gerente { get; set; }
        string DescripcionAccion { get; set; }
        string Observaciones { get; set; }
        DateTime FechaDesde { get; set; }
        DateTime FechaHasta { get; set; }
        string LogInfoMessage { set; }
        bool ShowArchivosAdjuntos { get; set; }

        // Carga
        void LoadAreaAcion(List<TBL_ModuloAPC_Areas> items);
        void LoadProcesos(List<DTO_ValueKey> items);

        // Archivos Adjuntos        
        byte[] ArchivoAdjunto { get; }
        string NombreArchivoAdjunto { get; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void DescargarArchivo(DTO_ValueKey archivo);
        List<DTO_ValueKey> ArchivosAdjuntos { get; set; }

        // Vista
        void GoToSolicitudView(string idSolicitud);
    }
}