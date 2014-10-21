using System;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminComentarioRespuestaAPCView : IView
    {
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

        // Admin Comentario Respuesta
        string IdComentario { get; }
        string Asunto { get; set; }
        string Mensaje { get; set; }
        string Destinatario { get; set; }
        DateTime FechaComentario { get; set; }
        string NuevoComentario { get; set; }
        string IdUsuarioDestino { get; set; }
        void LoadUsuariosCopia(List<DTO_ValueKey> items);

        // Seccion Info Solicitud
        string IdSolicitud { get;}
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
        void LoadDestinatarios(List<TBL_Admin_Usuarios> items);
        void LoadComentariosRelacionados(List<TBL_ModuloAPC_ComentariosRespuesta> items);

        // Archivos Adjuntos        
        byte[] ArchivoAdjunto { get; }
        string NombreArchivoAdjunto { get; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void DescargarArchivo(DTO_ValueKey archivo);

        // Info Respuesta Edit CLiente
        bool CanEditRespuestaCliente { get; set; }
    }
}
