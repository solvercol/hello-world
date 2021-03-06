using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Applications.MainModule.WorkFlow.DTO;
using Domain.Core;
using Domain.MainModule.AccionesPC.Contracts;
using Domain.MainModule.Contracts;
using Domain.MainModule.Reclamos.Contracts;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Infrastructure.CrossCutting.NetFramework.Services.Email;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Applications.MainModule.WorkFlow.Util
{
    public class SendMailNotification : ISendMailNotification
    {
        private readonly IEmailService _iEmailService;
        private readonly ITBL_Admin_PlantillasRepository _plantillasrepository;
        private readonly ITBL_Admin_OptionListRepository _optionsRepository;
        private readonly ITBL_ModuloReclamos_ReclamoRepository _reclamosRepository;
        private readonly ITBL_ModuloReclamos_AsesoresRepository _asesoresRepository;
        private readonly ITBL_ModuloAPC_SolicitudRepository _solicitudRepository;
        private readonly ITBL_Admin_UsuariosRepository _usuariosRepository;

        public SendMailNotification(
            IEmailService iEmailService, 
            ITBL_Admin_PlantillasRepository plantillasrepository, 
            ITBL_Admin_OptionListRepository optionsRepository, 
            ITBL_ModuloReclamos_ReclamoRepository reclamosRepository, 
            ITBL_ModuloReclamos_AsesoresRepository asesoresRepository, 
            ITBL_ModuloAPC_SolicitudRepository solicitudRepository, 
            ITBL_Admin_UsuariosRepository usuariosRepository)
        {
          
            _iEmailService = iEmailService;
            _usuariosRepository = usuariosRepository;
            _solicitudRepository = solicitudRepository;
            _asesoresRepository = asesoresRepository;
            _reclamosRepository = reclamosRepository;
            _optionsRepository = optionsRepository;
            _plantillasrepository = plantillasrepository;
        }

        /// <summary>
        /// Envio notificaciones Workflow
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public bool EnviarCorreoElectronicoNotificacion(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession, ModulosAplicacion module)
        {

            var plantilla = ObtenerPlantilla(oDocument.NextStatus, "Colombia", module);
            if (string.IsNullOrEmpty(plantilla)) return true;
            return module == ModulosAplicacion.Reclamos ? 
                SendEmailReclamo(oDocument, userSession, plantilla) : 
                SendEmailSolicitud(oDocument, userSession, plantilla);
        }

        /// <summary>
        /// Envio de notificaciones desde el WF para el modulo de Reclamos
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <param name="plantilla"></param>
        /// <returns></returns>
        private bool SendEmailReclamo(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession, string plantilla)
        {
            

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;


            var bodyParams = new Dictionary<string, string>();


            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gesti�n de Reclamos."},
                                        {"$Cliente", string.IsNullOrEmpty(oReclamo.NombreCliente) ? oReclamo.CodigoCliente : oReclamo.NombreCliente},
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };

            var strFrom = string.Format("Gesti�n de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$NumeroReclamo", oReclamo.NumeroReclamo);
            bodyParams.Add("$Cliente", string.IsNullOrEmpty(oReclamo.NombreCliente) ? oReclamo.CodigoCliente : oReclamo.NombreCliente);
            bodyParams.Add("$Responsable", oReclamo.TBL_Admin_Usuarios == null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios.Nombres);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom, oDocument.EmailNextResponsibe, plantilla, subjectParams, bodyParams, null, null);
            return true;
        }

        /// <summary>
        /// Envio de notificaciones desde el WF para el modulo de Solicitudes APC
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <param name="plantilla"></param>
        /// <returns></returns>
        private bool SendEmailSolicitud(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession, string plantilla)
        {


            if (string.IsNullOrEmpty(plantilla)) return false;

            var oSolicitud = _solicitudRepository.GetSolicitudById(Convert.ToInt32(oDocument.IdDocument));
            if (oSolicitud == null) return false;


            var bodyParams = new Dictionary<string, string>();


            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gesti�n de Calidad."},
                                        {"$Consecutivo", oSolicitud.Codigo}
                                    };

            var strFrom = string.Format("Gesti�n de Calidad. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$Area", oSolicitud.TBL_ModuloAPC_Areas.Nombre);
            bodyParams.Add("$Proceso", oSolicitud.Proceso);
            bodyParams.Add("$TipoAccion", oSolicitud.TipoAccion);
            bodyParams.Add("$Solicitante", oSolicitud.TBL_Admin_Usuarios9.Nombres);
            bodyParams.Add("$Responsable", oSolicitud.TBL_Admin_Usuarios6.Nombres);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentSolicitudforEmail());

            if(!oDocument.IsNextGroupResponsible)
                _iEmailService.ProcessEmail(strFrom, oDocument.EmailNextResponsibe, plantilla, subjectParams, bodyParams, null, null);
            else
            {
                var m = DefinedRegexEvaluation.Grupo.Match(oDocument.FormulaNextresponsible);
                if(m.Success)
                {
                    bodyParams.Remove("$Responsable");
                    var usuarios = _usuariosRepository.RetornarUsuariosReponsablesAprobacion(m.Groups[1].Value);
                    var strResponsables = string.Empty;
                    var strEmails = string.Empty;
                    foreach (var usu in usuarios)
                    {
                        strResponsables += string.Format("{0}, ", usu.Nombres);
                        strEmails += string.Format("{0},", usu.Email);
                    }

                    bodyParams.Add("$Responsable", strResponsables.Substring(0,strResponsables.Length-1));
                    _iEmailService.ProcessEmail(strFrom, strEmails.Substring(0, strEmails.Length-1), plantilla, subjectParams, bodyParams, null, null);
                }
            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <param name="dt"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public bool SendEmailActividadesSolicitud(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession, DataTable dt, ModulosAplicacion module)
        {

            var plantilla = ObtenerPlantilla("ActividadesProgramadasAPC", "Colombia", module);
            if (string.IsNullOrEmpty(plantilla)) return false;


            var strFrom = string.Format("Gesti�n de Calidad. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            foreach (DataRow dr in dt.Rows)
            {
                var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gesti�n de Calidad."},
                                        {"$Consecutivo", dr["codigo"].ToString()}
                                    };

                var bodyParams = new Dictionary<string, string>
                                     {
                                         {"$Area",dr["Nombre"].ToString()},
                                         {"$FechaProgramacion", dr["FechaActividad"].ToString()},
                                         {"$TipoAccion",dr["TipoAccion"].ToString()},
                                         {"$Autor", dr["Autor"].ToString()},
                                         {"$Responsable", dr["ResponsableEjecucion"].ToString()},
                                         {"$Actividad", dr["Descripcion"].ToString()},
                                         {"$Proceso", dr["Proceso"].ToString()},
                                         {"$Url", UrlHelper.GetUrlPreViewActividadSolicitudforEmail(dr["IdActividad"].ToString(),oDocument.IdDocument)}
                                     };
                string[] cc = null;
                if (!(dr["EmailResponsableSeguimiento"] is DBNull))
                {
                    cc= new string[1];
                    cc[0] = dr["EmailResponsableSeguimiento"].ToString();
                }

                _iEmailService.ProcessEmail(strFrom, dr["EmailResponsableEjecucion"].ToString(), plantilla, subjectParams, bodyParams, cc, null);
            }

            
            return true;
        }


        public bool SendEmailSolicitudCerrada(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession, TBL_ModuloAPC_Solicitud apcSolicitud, ModulosAplicacion module)
        {

            var plantilla = ObtenerPlantilla("AccionCerrada", "Colombia", module);
            if (string.IsNullOrEmpty(plantilla)) return false;


            var strFrom = string.Format("Gesti�n de Calidad. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gesti�n de Calidad."},
                                        {"$Consecutivo", apcSolicitud.Codigo}
                                    };

            var bodyParams = new Dictionary<string, string>
                                 {
                                     {"$Solicitante", apcSolicitud.TBL_Admin_Usuarios9.Nombres},
                                     {"$Area", apcSolicitud.TBL_ModuloAPC_Areas.Nombre},
                                     {"$Proceso", apcSolicitud.Proceso},
                                     {"$TipoAccion", apcSolicitud.TipoAccion},
                                     {"$Url", UrlHelper.GetUrlPreViewDocumentSolicitudforEmail()}
                                 };


            var usuarios = _usuariosRepository.RetornarUsuariosReponsablesAprobacion("AdministradoresAPC");
            var strEmails = usuarios.Aggregate(string.Empty, (current, usu) => current + string.Format("{0},", usu.Email));
            strEmails = apcSolicitud.TBL_ModuloAPC_Actividades.Aggregate(strEmails, (current, act) => current + string.Format("{0},{1},", act.TBL_Admin_Usuarios2.Email, act.TBL_Admin_Usuarios3.Email));
            strEmails = strEmails.Substring(0, strEmails.Length - 1);
            var cc = strEmails.Split(',').Distinct().ToArray();

            _iEmailService.ProcessEmail(strFrom, apcSolicitud.TBL_Admin_Usuarios9.Email, plantilla, subjectParams,
                                        bodyParams, cc, null);

           

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoElectronicoNotificacionCliente(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession)
        {

            var plantilla = ObtenerPlantilla("EmailContacto", "Colombia",ModulosAplicacion.Reclamos);

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;
            
            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gesti�n de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$Cliente", string.IsNullOrEmpty(oReclamo.NombreCliente) ? oReclamo.CodigoCliente : oReclamo.NombreCliente);
            bodyParams.Add("$IngenieroResponsable", oReclamo.TBL_Admin_Usuarios4.Nombres);
            bodyParams.Add("$TipoReclamo", oReclamo.TipoReclamo);
            
            if(oReclamo.TipoReclamo == "Producto")
            {
                bodyParams.Add("$DetalleReclamo", string.IsNullOrEmpty(oReclamo.NombreProducto) ? oReclamo.CodigoProducto : oReclamo.NombreProducto);
            }
            else
            {
                var texto = string.Format("{0}<br/>{1}", oReclamo.TBL_ModuloReclamos_CategoriasReclamo.Descripcion,
                                          string.IsNullOrEmpty(oReclamo.NombreObra)
                                              ? string.Empty
                                              : string.Format("Nombre de la Obra: {0}", oReclamo.NombreObra));

                bodyParams.Add("$DetalleReclamo", texto);
            }


            bodyParams.Add("$Responsable", oReclamo.TBL_Admin_Usuarios == null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios.Nombres);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom, oDocument.EmailCurrentResponsibe, plantilla, subjectParams, bodyParams, null, null);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoelectronicoAsesoresJefe(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession)
        {

            var plantilla = ObtenerPlantilla("EmailJefes", "Colombia",ModulosAplicacion.Reclamos);

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;

            if (!oReclamo.IdAsesoradoPor.HasValue) return false;

            var asesores = _asesoresRepository.GetUsuariosAsesoresByIdAsesorado(oReclamo.IdAsesoradoPor.GetValueOrDefault());

            if (asesores.Count == 0) return false;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Cliente", string.IsNullOrEmpty(oReclamo.NombreCliente) ? oReclamo.CodigoCliente : oReclamo.NombreCliente}
                                    };


            var strFrom = string.Format("Gesti�n de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$TipoReclamo", oReclamo.TipoReclamo);

            if (oReclamo.TipoReclamo == "Producto")
            {
                bodyParams.Add("$DetalleReclamo", string.IsNullOrEmpty(oReclamo.NombreProducto) ? oReclamo.CodigoProducto : oReclamo.NombreProducto);
            }
            else
            {
                var texto = string.Format("{0}<br/>{1}", oReclamo.TBL_ModuloReclamos_CategoriasReclamo.Descripcion,
                                          string.IsNullOrEmpty(oReclamo.NombreObra)
                                              ? string.Empty
                                              : string.Format("Nombre de la Obra: {0}", oReclamo.NombreObra));

                bodyParams.Add("$DetalleReclamo", texto);
            }

            bodyParams.Add("$AsesoradoPor", oReclamo.AsesoradoPor.Nombres);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            var strToEmail = asesores.Aggregate(string.Empty, (current, usuariose) => current + string.Format("{0},", usuariose.Email));
            if (string.IsNullOrEmpty(strToEmail)) return false;
            _iEmailService.ProcessEmail(strFrom, strToEmail.Substring(0, strToEmail.Length-1), plantilla, subjectParams, bodyParams, null, null);
            return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoelectronicoAutorReclamo(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession)
        {

            var plantilla = ObtenerPlantilla("MailAutor", "Colombia",ModulosAplicacion.Reclamos);

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gesti�n de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$Autor", oReclamo.TBL_Admin_Usuarios2.Nombres);
            bodyParams.Add("$TipoReclamo", oReclamo.TipoReclamo);

            if (oReclamo.TipoReclamo == "Producto")
            {
                bodyParams.Add("$DetalleReclamo", string.IsNullOrEmpty(oReclamo.NombreProducto) ? oReclamo.CodigoProducto : oReclamo.NombreProducto);
            }
            else
            {
                var texto = string.Format("{0}<br/>{1}", oReclamo.TBL_ModuloReclamos_CategoriasReclamo.Descripcion,
                                          string.IsNullOrEmpty(oReclamo.NombreObra)
                                              ? string.Empty
                                              : string.Format("Nombre de la Obra: {0}", oReclamo.NombreObra));

                bodyParams.Add("$DetalleReclamo", texto);
            }

            
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom, oReclamo.TBL_Admin_Usuarios2.Email, plantilla, subjectParams, bodyParams, null, null);

            return true;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoelectronicoDevolucion(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession)
        {

            var plantilla = ObtenerPlantilla("DevolucionResponsable", "Colombia",ModulosAplicacion.Reclamos);

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gesti�n de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$Responsable", oReclamo.TBL_Admin_Usuarios == null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios.Nombres);
            bodyParams.Add("$Motivo", oDocument.Comentarios);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom, oReclamo.TBL_Admin_Usuarios2.Email, plantilla, subjectParams, bodyParams, null, null);

            return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoelectronicoRechazoReclamo(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession)
        {

            var plantilla = ObtenerPlantilla("RechazarReclamo", "Colombia",ModulosAplicacion.Reclamos);

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gesti�n de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$Responsable", oReclamo.TBL_Admin_Usuarios3 == null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios3.Nombres);
            bodyParams.Add("$Motivo", oDocument.Comentarios);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom, oReclamo.TBL_Admin_Usuarios2.Email, plantilla, subjectParams, bodyParams, null, null);

            return true;

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public byte[] GetMergeTemplate(RenderTypeControlButtonDto oDocument, ModulosAplicacion module)
        {
            
            var plantilla = ObtenerPlantilla(oDocument.NextStatus, "Colombia", module);

            if (string.IsNullOrEmpty(plantilla)) return null;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return null;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gesti�n de Reclamos."},
                                        {"$Cliente", oReclamo.CodigoCliente},
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            bodyParams.Add("$NumeroReclamo", oReclamo.NumeroReclamo);
            bodyParams.Add("$Cliente", oReclamo.CodigoCliente);
            bodyParams.Add("$Responsable",  oReclamo.TBL_Admin_Usuarios== null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios.Nombres);
            bodyParams.Add("$Url", UrlUtil.GetUrlPreViewForNotificationWindow());

            var tmp = _iEmailService.MergeTemplate(plantilla, subjectParams, bodyParams);

            var uniEncoding = new UTF8Encoding();
            var templte = string.Format("{0}\n{1}", tmp[0], tmp[1]);
            var byteEncabezado = uniEncoding.GetBytes(templte);

            byte[] oTemplate;
            using (var ms = new MemoryStream())
            {
                ms.Write(byteEncabezado, 0, byteEncabezado.Length);
                oTemplate = ms.GetBuffer();
            }

            return oTemplate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoPlantilla"></param>
        /// <param name="pais"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        private string ObtenerPlantilla(string codigoPlantilla, string pais, ModulosAplicacion module)
        {
            var idModule = (int) module;

            var plantilla = _plantillasrepository.GetPlantillaByIdPaisByCodigo(codigoPlantilla, pais, idModule.ToString());

            if (plantilla == null) return string.Empty;

            var strTemplate = string.Empty;
            if (plantilla.Contenido.Length > 0)
            {
                var memstrm = new MemoryStream(plantilla.Contenido);

                using (var memrdr = new StreamReader(memstrm))
                {
                    strTemplate = memrdr.ReadToEnd();
                }
            }
            return strTemplate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string  GetFromEmail()
        {
            const int moduleId = (int) ModulosAplicacion.Admin;
            var option = _optionsRepository.GetOptionByKey("FromMail", moduleId);
            return option == null ? "velasquez.ricardo@co.sika.com" : option.Value;
        }
    }
}