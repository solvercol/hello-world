using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Applications.MainModule.WorkFlow.DTO;
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

        public SendMailNotification(
            IEmailService iEmailService, 
            ITBL_Admin_PlantillasRepository plantillasrepository, 
            ITBL_Admin_OptionListRepository optionsRepository, 
            ITBL_ModuloReclamos_ReclamoRepository reclamosRepository, 
            ITBL_ModuloReclamos_AsesoresRepository asesoresRepository)
        {
          
            _iEmailService = iEmailService;
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
        /// <returns></returns>
        public bool EnviarCorreoElectronicoNotificacion(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession)
        {

            var plantilla = ObtenerPlantilla(oDocument.NextStatus, "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;


            var bodyParams = new Dictionary<string, string>();


            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gestión de Reclamos."},
                                        {"$Cliente", string.IsNullOrEmpty(oReclamo.NombreCliente) ? oReclamo.CodigoCliente : oReclamo.NombreCliente},
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };

            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$NumeroReclamo",oReclamo.NumeroReclamo);
            bodyParams.Add("$Cliente", string.IsNullOrEmpty(oReclamo.NombreCliente) ? oReclamo.CodigoCliente : oReclamo.NombreCliente);
            bodyParams.Add("$Responsable", oReclamo.TBL_Admin_Usuarios == null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios.Nombres);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom,oDocument.EmailCurrentResponsibe,plantilla,subjectParams,bodyParams,null,null);
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

            var plantilla = ObtenerPlantilla("EmailContacto", "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;
            
            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

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

            var plantilla = ObtenerPlantilla("EmailJefes", "Colombia");

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


            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

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

            var plantilla = ObtenerPlantilla("MailAutor", "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

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

            var plantilla = ObtenerPlantilla("DevolucionResponsable", "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            bodyParams.Add("$Responsable", oReclamo.TBL_Admin_Usuarios == null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios.Nombres);
            bodyParams.Add("$Motivo", oDocument.Comentarios);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom, oReclamo.TBL_Admin_Usuarios2.Email, plantilla, subjectParams, bodyParams, null, null);

            return true;

        }


        public bool EnviarCorreoelectronicoRechazoReclamo(RenderTypeControlButtonDto oDocument, TBL_Admin_Usuarios userSession)
        {

            var plantilla = ObtenerPlantilla("RechazarReclamo", "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };


            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

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
        /// <returns></returns>
        public byte[] GetMergeTemplate(RenderTypeControlButtonDto oDocument)
        {
            var plantilla = ObtenerPlantilla(oDocument.NextStatus,  "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return null;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return null;

            var bodyParams = new Dictionary<string, string>();

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gestión de Reclamos."},
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
        /// <returns></returns>
        private  string ObtenerPlantilla(string codigoPlantilla, string pais)
        {
            var plantilla = _plantillasrepository.GetPlantillaByIdPaisByCodigo(codigoPlantilla,pais);

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