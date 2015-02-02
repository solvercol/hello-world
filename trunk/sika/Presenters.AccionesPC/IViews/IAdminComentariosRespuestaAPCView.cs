using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface IAdminComentariosRespuestaAPCView : IView
    {

        event EventHandler FilterEvent;
        #region  Admin Comentario

        string IdSolicitud { get; }
        string Asunto { get; set; }
        string Comentario { get; set; }
        string IdUsuarioDestino { get; set; }
        //string MailContacto { get; set; }
        //string MailContactoTmp { get; set; }
        string IdSelectedComentario { get; set; }
        bool IsNewComentario { get; set; }

        List<DTO_ValueKey> UsuariosCopia { get; set; }
        #endregion

        #region Methods

        void ShowAdminComentarioWindow(bool visible);
        void LoadComentariosSolicitud(List<TBL_ModuloAPC_ComentariosRespuesta> items);
        void LoadDestinatarios(List<TBL_Admin_Usuarios> items);
        void LoadUsuarioCopia(List<TBL_Admin_Usuarios> items);
        void LoadUsuariosCopia(List<DTO_ValueKey> items);
    
        void EnableEdit(bool enable);
        #endregion

        #region Archivos Adjuntos

        List<DTO_ValueKey> ArchivosAdjuntos { get; set; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        #endregion

   
    }
}
