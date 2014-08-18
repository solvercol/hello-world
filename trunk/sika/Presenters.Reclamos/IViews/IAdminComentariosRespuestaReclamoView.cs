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

        string IdSelectedComentario { get; set; }
        bool IsNewComentario { get; set; }

        //Methods
        void ShowAdminComentarioWindow(bool visible);
        void LoadComentariosReclamo(List<TBL_ModuloReclamos_ComentariosRespuesta> items);
        void LoadDestinatarios(List<TBL_Admin_Usuarios> items);
    }
}