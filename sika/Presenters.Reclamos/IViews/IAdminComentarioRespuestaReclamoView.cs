using System;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminComentarioRespuestaReclamoView : IView
    {
        // Admin Comentario Respuesta
        string IdComentario { get; }
        string Asunto { get; set; }
        string Mensaje { get; set; }
        string Destinatario { get; set; }
        DateTime FechaComentario { get; set; }
        string NuevoComentario { get; set; }
        string IdUsuarioDestino { get; set; }
        void LoadUsuariosCopia(List<DTO_ValueKey> items);

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
        void LoadDestinatarios(List<TBL_Admin_Usuarios> items);
        void LoadComentariosRelacionados(List<TBL_ModuloReclamos_ComentariosRespuesta> items);

        // Archivos Adjuntos        
        byte[] ArchivoAdjunto { get; }
        string NombreArchivoAdjunto { get; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void DescargarArchivo(DTO_ValueKey archivo);

        // Info Respuesta Edit CLiente
        bool CanEditRespuestaCliente { get; set; }

    }
}