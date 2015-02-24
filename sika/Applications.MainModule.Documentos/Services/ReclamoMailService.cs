using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.MainModule.Documentos.IServices;
using Infrastructure.CrossCutting.Logging;
using Infraestructure.CrossCutting.NetCommunication;
using Domain.MainModule.Contracts;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Configuration;
using Infrastructure.CrossCutting;

namespace Application.MainModule.Documentos.Services
{
    public class ReclamoMailService : IReclamoMailService
    {
        #region Members

        readonly ITraceManager _iTraceManager;
        readonly IMailHelper _iMailHelper;
        readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices _documentoService;
        readonly ITBL_Admin_OptionListRepository _optionListRepository;

        #endregion

        #region Builders

        public ReclamoMailService(ITraceManager iTraceManager, IMailHelper iMailHelper,
                                   ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoService, ITBL_Admin_OptionListRepository optionListRepository)
        {
            _iTraceManager = iTraceManager;
            _iMailHelper = iMailHelper;
            _documentoService = documentoService;
            _optionListRepository = optionListRepository;
        }

        #endregion

        #region Methos

        TBL_Admin_OptionList ObtenerOpcionBykey(string key)
        {
            Specification<TBL_Admin_OptionList> specification = new DirectSpecification<TBL_Admin_OptionList>(u => u.Key.Equals(key));

            return _optionListRepository.GetEntityBySpec(specification);

        }
        
        #endregion

        #region IReclamoMailService Members

        public void SendDocumentoPublicacionMailNotification(object parameters)
        {
            var arrayParameters = (Array)parameters;
            int idDocumento = (int)arrayParameters.GetValue(0);
            int idModule = (int)arrayParameters.GetValue(1);
            string baseUrl = (string)arrayParameters.GetValue(2);

            var documento = _documentoService.GetDocumentoByIdWithCategories(idDocumento);

            if (documento == null)
                return;

            var opBody = ObtenerOpcionBykey("BodyMailPublicacionDocumento");
            var opSubject = ObtenerOpcionBykey("AsuntoMailPublicacionDocumento");
            var opMailTo = ObtenerOpcionBykey("MailPublicacionDocumento");

            var body = opBody.Value;

            body = body.Replace("[#Titulo]", documento.Titulo)
                       .Replace("[#Categoria]", documento.TBL_ModuloDocumentos_Categorias.Nombre)
                       .Replace("[#SubCategoria]", documento.TBL_ModuloDocumentos_Categorias1.Nombre)
                       .Replace("[#TipoDocumento]", documento.TBL_ModuloDocumentos_Categorias2.Nombre)
                       .Replace("[#Responsable]", documento.CargoResponsable)
                       .Replace("[#UrlBase]", string.Format("{0}/Pages/Modules/Documentos/Consulta/FrmVerDocumento.aspx?ModuleId={1}&IdDocumento={2}&from=docspub",
                                                            baseUrl, idModule, documento.IdDocumento));


            _iMailHelper.SMTP_Host = ConfigurationManager.AppSettings.Get("host");
            _iMailHelper.SMTP_EnableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("enableSsl"));
            _iMailHelper.SMTP_User = ConfigurationManager.AppSettings.Get("smtpUsername");
            _iMailHelper.SMTP_Password = ConfigurationManager.AppSettings.Get("smtpPassword");
            _iMailHelper.SMTP_Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("port"));
            _iMailHelper.SMTP_From = ConfigurationManager.AppSettings.Get("mailFrom");
            _iMailHelper.SMTP_Subject = string.Format("{0}", opSubject.Value);
            _iMailHelper.SMTP_Body = body;
            _iMailHelper.SMTP_To = opMailTo.Value.Split('|');

            try
            {
                _iMailHelper.SendMail();
            }
            catch (Exception ex)
            {
                _iTraceManager.LogInfo(string.Format("Error al enviar mail de notificación de publicacion de documento de calidad.Cls:ReclamoMailService,Mtd:SendDocumentoPublicacionMailNotification, Error: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message), LogType.Notify);
            }
        }

        #endregion
    }
}
