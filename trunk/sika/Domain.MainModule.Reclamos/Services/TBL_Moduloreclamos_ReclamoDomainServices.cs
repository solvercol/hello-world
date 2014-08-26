using System;
using Domain.MainModules.Entities;
using Infraestructure.CrossCutting.Security.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Domain.MainModule.Reclamos.Services
{
    public class TBL_Moduloreclamos_ReclamoDomainServices : ITBL_Moduloreclamos_ReclamoDomainServices
    {

        private readonly IAutentication _autenticationService;

        public TBL_Moduloreclamos_ReclamoDomainServices(IAutentication autenticationService)
        {
            _autenticationService = autenticationService;
        }

        public TBL_ModuloReclamos_Tracking GenerarObjetoTrackpedido(TBL_ModuloReclamos_Tracking oTrack, string accion, string estadoAnterior, int idReclamo, string nuevoestado, string nuevoResponsable)
        {
            oTrack.Accion = accion;
            oTrack.Autor = _autenticationService.GetUserFromSession.Nombres;
            oTrack.CreateBy = _autenticationService.GetUserFromSession.Nombres;
            oTrack.CreateOn = DateTime.Now;
            oTrack.EstadoAnterior = estadoAnterior;
            oTrack.IdReclamo = idReclamo;
            oTrack.IsActive = true;
            oTrack.ModifiedBy = _autenticationService.GetUserFromSession.Nombres;
            oTrack.ModifiedOn = DateTime.Now;
            oTrack.Nuevoestado = nuevoestado;
            oTrack.NuevoResponsable = nuevoResponsable;

            return oTrack;
        }

        public TBL_ModuloReclamos_LogReclamos GenerarObjetoLogPedido(TBL_ModuloReclamos_LogReclamos oLog, string accion, int idReclamo)
        {
            oLog.CreateBy = _autenticationService.GetUserFromSession.IdUser;
            oLog.CreateOn = DateTime.Now;
            oLog.Descripcion = string.Format("El Usuario [{0}] cambió el estado del pedido a [{1}] a las {2} ",
                                             _autenticationService.GetUserFromSession.Nombres, accion,
                                             DateTime.Now.ToShortTimeString());

            oLog.IdReclamo = idReclamo;
            oLog.IsActive = true;
            return oLog;
        }

        public TBL_Admin_SistemaNotificaciones GenerarEntradaNotificadorSistema(TBL_Admin_SistemaNotificaciones oNotifier, int idCurrentResponsible, byte[] contenido, string cliente)
        {
            oNotifier.Contenido = contenido;
            oNotifier.CreateBy = _autenticationService.GetUserFromSession.IdUser.ToString();
            oNotifier.CreateOn = DateTime.Now;
            oNotifier.Fecha = DateTime.Now;
            oNotifier.IdUser = idCurrentResponsible;
            oNotifier.IsActive = true;
            oNotifier.ModifiedBy = _autenticationService.GetUserFromSession.IdUser.ToString();
            oNotifier.ModifiedOn = DateTime.Now;
            oNotifier.Modulo = ModulosAplicacion.Reclamos.ToString();
            oNotifier.Subject = string.Format("Sistema de Gestión de Pedidos - cliente: {0}", cliente).ToUpper();
            oNotifier.Url = UrlHelper.GetUrlPreViewForNotificationWindow();
            return oNotifier;
        }
    }
}