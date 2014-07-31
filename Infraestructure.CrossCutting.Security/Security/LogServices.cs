using System;
using Application.MainModule.Documentos.IServices;
using Infraestructure.CrossCutting.Security.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Infraestructure.CrossCutting.Security.Security
{
  

    public class LogServices : ILogServices
    {
        private readonly ISfTBL_ModuloDocumentos_LogCambiosManagementServices _logServices;

        public LogServices(ISfTBL_ModuloDocumentos_LogCambiosManagementServices logServices)
        {
            _logServices = logServices;
        }

        public void CrearEntradaLogPedidos(int idDocumento, int idHostorial, string userName, Acciones accion)
        {
            var oLog = _logServices.NewEntity();
            oLog.IdDocumento = idDocumento;
            oLog.IdHistorial = idHostorial;
            oLog.CreateBy = userName;
            oLog.CreateOn = DateTime.Now;
            oLog.IsActive = true;
            oLog.Descripcion = string.Format(Message.EntradaLog,"Documento", GetAccion(accion), userName, DateTime.Now.ToShortTimeString());
            _logServices.Add(oLog);
        }

      
        private static string GetAccion(Acciones accion)
        {
            switch (accion)
            {
                case Acciones.Actualizar:
                    return Message.Actualizado;
                case Acciones.Aprobar:
                    return Message.Aprobado;
                case Acciones.Crear:
                    return Message.Creado;
            }
            return "Procesado";
        }
    }
}