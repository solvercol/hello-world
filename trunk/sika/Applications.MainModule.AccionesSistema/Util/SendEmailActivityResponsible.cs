using System.Collections.Generic;
using System.IO;
using Domain.MainModule.Contracts;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Infrastructure.CrossCutting.NetFramework.Services.Email;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Applications.MainModule.AccionesSistema.Util
{
    public class SendEmailActivityResponsible 
    {
        private readonly IEmailService _iEmailService;
        private readonly ITBL_Admin_PlantillasRepository _plantillasrepository;
        private readonly ITBL_Admin_OptionListRepository _optionsRepository;

        public SendEmailActivityResponsible(IEmailService iEmailService, ITBL_Admin_PlantillasRepository plantillasrepository, ITBL_Admin_OptionListRepository optionsRepository)
        {
            _iEmailService = iEmailService;
            _optionsRepository = optionsRepository;
            _plantillasrepository = plantillasrepository;
        }

        public bool EnviarCorreoElectronicoNotificacionActividades(string  cliente, string ordenCompra, Dictionary<string,string> responsables)
        {
            if (responsables.Count == 0) return false;

            var plantilla = ObtenerPlantilla("Programada", "Colombia");

            if (string.IsNullOrEmpty(plantilla)) return false;



            var subjectParams = new Dictionary<string, string>
                                    {
                                        { "$Cliente",string.IsNullOrEmpty(cliente) ? string.Empty : cliente},
                                        { "$Estado","Programada"}
                                    };

            var strFrom = "Sistema de Gestión de Pedidos" + "<" + GetFromEmail() + ">";


            var bodyParams = new Dictionary<string, string>
                                 {
                                     {
                                         "$OrdenCompra",
                                         string.IsNullOrEmpty(ordenCompra)
                                             ? string.Empty
                                             : ordenCompra.ToUpper()
                                         },
                                     
                                     {"$Url", UrlUtil.GetUrlPreViewDocumentforEmail()}  //todo: OJO!! Aqui va la URL que lo debe llevar a la vista de Mis Actividades Pendientes.
                                 };

            foreach (var parameter in responsables)
            {
                bodyParams.Add("$Actividad", parameter.Value);
                bodyParams.Add("$Responsable", parameter.Key.Split(':')[0]);
                _iEmailService.ProcessEmail(strFrom, parameter.Key.Split(':')[1], plantilla, subjectParams, bodyParams, null, null);

                bodyParams.Remove("$Actividad");
                bodyParams.Remove("$Responsable");
            }

            return true;
        }

        private string ObtenerPlantilla(string codigoPlantilla, string pais)
        {
            const int idModule = (int) ModulosAplicacion.Reclamos;
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

        private string  GetFromEmail()
        {
            const int moduleId = (int) ModulosAplicacion.Admin;
            var option = _optionsRepository.GetOptionByKey("FromMail", moduleId);
            return option == null ? "info@empacor.com" : option.Value;
        }
    }
}