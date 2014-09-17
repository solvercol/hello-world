using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminComentariosRespuestaReclamoView : IView
    {
        // Admin Comentario
        string IdReclamo { get; }
        string Asunto { get; set; }
        string Comentario { get; set; }
        string IdUsuarioDestino { get; set; }
        string MailContacto { get; set; }
        string MailContactoTmp { get; set; }

        string IdSelectedComentario { get; set; }
        bool IsNewComentario { get; set; }

        List<DTO_ValueKey> UsuariosCopia { get; set; }

        //Methods
        void ShowAdminComentarioWindow(bool visible);
        void LoadComentariosReclamo(List<TBL_ModuloReclamos_ComentariosRespuesta> items);
        void LoadDestinatarios(List<TBL_Admin_Usuarios> items);
        void LoadUsuarioCopia(List<TBL_Admin_Usuarios> items);
        void LoadUsuariosCopia(List<DTO_ValueKey> items);

        void EnableEdit(bool enable);

        // Archivos Adjuntos
        List<DTO_ValueKey> ArchivosAdjuntos { get; set; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);

        // Permisos envio cliente
        bool CanSendMailToCLient { get; set; }
    }
}