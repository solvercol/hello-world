using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Applications.MainModule.WorkFlow.DTO;
using Domain.MainModule.Contracts;
using Domain.MainModule.Reclamos.Contracts;
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


        public SendMailNotification(
            IEmailService iEmailService, 
            ITBL_Admin_PlantillasRepository plantillasrepository, 
            ITBL_Admin_OptionListRepository optionsRepository, 
            ITBL_ModuloReclamos_ReclamoRepository reclamosRepository)
        {
          
            _iEmailService = iEmailService;
            _reclamosRepository = reclamosRepository;
            _optionsRepository = optionsRepository;
            _plantillasrepository = plantillasrepository;
        }

        /// <summary>
        /// Envio notificaciones Workflow
        /// </summary>
        /// <param name="oDocument"></param>
        /// <returns></returns>
        public bool EnviarCorreoElectronicoNotificacion(RenderTypeControlButtonDto oDocument)
        {

            var plantilla = ObtenerPlantilla(oDocument.NextStatus, "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return false;

            var oReclamo = _reclamosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));
            if (oReclamo == null) return false;


            var bodyParams = new Dictionary<string, string>();


            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Aplicacion", "SIka - Gestión de Reclamos."},
                                        {"$Cliente", oReclamo.CodigoCliente},
                                        {"$NumeroReclamo", oReclamo.NumeroReclamo}
                                    };

            var strFrom = "Gestión de Reclamos" + "<" + GetFromEmail() + ">";

            bodyParams.Add("$NumeroReclamo",oReclamo.NumeroReclamo);
            bodyParams.Add("$Cliente", oReclamo.CodigoCliente);
            bodyParams.Add("$Responsable", oReclamo.TBL_Admin_Usuarios == null ? oDocument.CurrentResponsibe : oReclamo.TBL_Admin_Usuarios.Nombres);
            bodyParams.Add("$Url", UrlHelper.GetUrlPreViewDocumentforEmail());

            _iEmailService.ProcessEmail(strFrom,oDocument.EmailCurrentResponsibe,plantilla,subjectParams,bodyParams,null,null);
            return true;
        }


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

        private string  GetFromEmail()
        {
            const int moduleId = (int) ModulosAplicacion.Admin;
            var option = _optionsRepository.GetOptionByKey("FromMail", moduleId);
            return option == null ? "velasquez.ricardo@co.sika.com" : option.Value;
        }
    }
}