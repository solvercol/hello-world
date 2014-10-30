using System;
using Domain.MainModules.Entities;

namespace Domain.MainModule.AccionesPC.Services
{
    public class SolicitudesDomainServices : ISolicitudesDomainServices
    {
        public TBL_ModuloAPC_Tracking GenerarObjetoTrack(TBL_ModuloAPC_Tracking oTrack, string accion, string estadoAnterior, decimal idSolicitud, string nuevoestado, string nuevoResponsable, TBL_Admin_Usuarios autenticationService)
        {
            oTrack.Accion = accion;
            oTrack.Autor = autenticationService.Nombres;
            oTrack.CreateBy = autenticationService.Nombres;
            oTrack.CreateOn = DateTime.Now;
            oTrack.EstadoAnterior = estadoAnterior;
            oTrack.IdSolicitud = idSolicitud;
            oTrack.IsActive = true;
            oTrack.ModifiedBy = autenticationService.Nombres;
            oTrack.ModifiedOn = DateTime.Now;
            oTrack.Nuevoestado = nuevoestado;
            oTrack.NuevoResponsable = nuevoResponsable;

            return oTrack;
        }

        public TBL_ModuloAPC_LogSolicitud GenerarObjetoLog(TBL_ModuloAPC_LogSolicitud oLog, string accion, decimal idsolicitud, TBL_Admin_Usuarios autenticationService)
        {
            oLog.CreateBy = autenticationService.IdUser;
            oLog.CreateOn = DateTime.Now;
            oLog.Descripcion = string.Format("El Usuario [{0}] cambió el estado del Reclamo a [{1}] a las {2} ",
                                             autenticationService.Nombres, accion,
                                             DateTime.Now.ToShortTimeString());
            oLog.IdSolicitud = idsolicitud;
            oLog.IsActive = true;
            return oLog;
        }

        public TBL_ModuloAPC_LogSolicitud GenerarObjetoLog(TBL_ModuloAPC_LogSolicitud oLog, string accion, decimal idsolicitud, TBL_Admin_Usuarios autenticationService, string comentario)
        {
            oLog.CreateBy = autenticationService.IdUser;
            oLog.CreateOn = DateTime.Now;
            oLog.Descripcion = string.Format("El Usuario [{0}] cambió el estado del Reclamo a [{1}] a las {2}. Comentario: {3} ",
                                             autenticationService.Nombres, accion,
                                             DateTime.Now.ToShortTimeString(),
                                             comentario);

            oLog.IdSolicitud = idsolicitud;
            oLog.IsActive = true;
            return oLog;
        }

        public TBL_ModuloAPC_LogSolicitud GenerarObjetoLog(TBL_ModuloAPC_LogSolicitud oLog, decimal idsolicitud, TBL_Admin_Usuarios autenticationService, string mensaje)
        {
            oLog.CreateBy = autenticationService.IdUser;
            oLog.CreateOn = DateTime.Now;
            oLog.Descripcion = mensaje;
            oLog.IdSolicitud = idsolicitud;
            oLog.IsActive = true;
            return oLog;
        }
    }
}