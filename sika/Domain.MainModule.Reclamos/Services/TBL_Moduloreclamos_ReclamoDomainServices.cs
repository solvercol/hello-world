using System;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Domain.MainModule.Reclamos.Services
{
    public class TBL_Moduloreclamos_ReclamoDomainServices : ITBL_Moduloreclamos_ReclamoDomainServices
    {

       
        public TBL_ModuloReclamos_Tracking GenerarObjetoTrack(
            TBL_ModuloReclamos_Tracking oTrack, 
            string accion, 
            string estadoAnterior, 
            int idReclamo, 
            string nuevoestado, 
            string nuevoResponsable,
            TBL_Admin_Usuarios autenticationService)
        {
            oTrack.Accion = accion;
            oTrack.Autor = autenticationService.Nombres;
            oTrack.CreateBy = autenticationService.Nombres;
            oTrack.CreateOn = DateTime.Now;
            oTrack.EstadoAnterior = estadoAnterior;
            oTrack.IdReclamo = idReclamo;
            oTrack.IsActive = true;
            oTrack.ModifiedBy = autenticationService.Nombres;
            oTrack.ModifiedOn = DateTime.Now;
            oTrack.Nuevoestado = nuevoestado;
            oTrack.NuevoResponsable = nuevoResponsable;

            return oTrack;
        }

        public TBL_ModuloReclamos_LogReclamos GenerarObjetoLog(
            TBL_ModuloReclamos_LogReclamos oLog, 
            string accion, 
            int idReclamo, 
            TBL_Admin_Usuarios autenticationService)
        {
            oLog.CreateBy = autenticationService.IdUser;
            oLog.CreateOn = DateTime.Now;
            oLog.Descripcion = string.Format("El Usuario [{0}] cambió el estado del Reclamo a [{1}] a las {2} ",
                                             autenticationService.Nombres, accion,
                                             DateTime.Now.ToShortTimeString());

            oLog.IdReclamo = idReclamo;
            oLog.IsActive = true;
            oLog.IdLog = Guid.NewGuid();
            return oLog;
        }

        public TBL_ModuloReclamos_LogReclamos GenerarObjetoLog(
            TBL_ModuloReclamos_LogReclamos oLog,
            string accion,
            int idReclamo,
            TBL_Admin_Usuarios autenticationService,
            string comentario)
        {
            oLog.CreateBy = autenticationService.IdUser;
            oLog.CreateOn = DateTime.Now;
            oLog.Descripcion = string.Format("El Usuario [{0}] cambió el estado del Reclamo a [{1}] a las {2}. Comentario: {3} ",
                                             autenticationService.Nombres, accion,
                                             DateTime.Now.ToShortTimeString(),
                                             comentario);

            oLog.IdReclamo = idReclamo;
            oLog.IsActive = true;
            oLog.IdLog = Guid.NewGuid();
            return oLog;
        }


        public TBL_ModuloReclamos_LogReclamos GenerarObjetoLog(
           TBL_ModuloReclamos_LogReclamos oLog,
           decimal idReclamo,
           TBL_Admin_Usuarios autenticationService,
           string mensaje)
        {
            oLog.CreateBy = autenticationService.IdUser;
            oLog.CreateOn = DateTime.Now;
            oLog.Descripcion = mensaje;
            oLog.IdReclamo = idReclamo;
            oLog.IsActive = true;
            oLog.IdLog = Guid.NewGuid();
            return oLog;
        }

        public TBL_Admin_SistemaNotificaciones GenerarEntradaNotificadorSistema(
            TBL_Admin_SistemaNotificaciones oNotifier, 
            int idCurrentResponsible, 
            byte[] contenido, 
             TBL_Admin_Usuarios autenticationService)
        {
            oNotifier.Contenido = contenido;
            oNotifier.CreateBy = autenticationService.IdUser.ToString();
            oNotifier.CreateOn = DateTime.Now;
            oNotifier.Fecha = DateTime.Now;
            oNotifier.IdUser = idCurrentResponsible;
            oNotifier.IsActive = true;
            oNotifier.ModifiedBy = autenticationService.IdUser.ToString();
            oNotifier.ModifiedOn = DateTime.Now;
            oNotifier.Modulo = ModulosAplicacion.Reclamos.ToString();
            oNotifier.Subject = string.Format("Sistema de Gestión de Reclamos").ToUpper();
            oNotifier.Url = UrlHelper.GetUrlPreViewForNotificationWindow();
            return oNotifier;
        }
    }
}