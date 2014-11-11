using System;
using System.Collections.Generic;
using System.IO;
using Domain.MainModule.AccionesPC.Contracts;
using Domain.MainModule.Contracts;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Infrastructure.CrossCutting.NetFramework.Services.Email;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Application.MainModule.AccionesPC.Util
{
    public class SendEmail : ISendEmail
    {
        private readonly IEmailService _iEmailService;
        private readonly ITBL_Admin_PlantillasRepository _plantillasrepository;
        private readonly ITBL_Admin_OptionListRepository _optionsRepository;
        private readonly ITBL_ModuloAPC_ComentariosRespuestaRepository _comentariosRepository;
        private readonly ITBL_ModuloAPC_ActividadesRepository _actividadesRepository;
        private readonly ITBL_ModuloAPC_SolicitudRepository _solicitudesApcRepository;
        readonly ITraceManager _traceManager;

        public SendEmail(
            IEmailService iEmailService, 
            ITBL_Admin_PlantillasRepository plantillasrepository, 
            ITBL_Admin_OptionListRepository optionsRepository,
            ITBL_ModuloAPC_ComentariosRespuestaRepository comentariosRepository, 
            ITBL_ModuloAPC_ActividadesRepository actividadesRepository, 
            ITraceManager traceManager, 
            ITBL_ModuloAPC_SolicitudRepository solicitudesApcRepository)
        {
            _iEmailService = iEmailService;
            _solicitudesApcRepository = solicitudesApcRepository;
            _traceManager = traceManager;
            _actividadesRepository = actividadesRepository;
            _comentariosRepository = comentariosRepository;
            _optionsRepository = optionsRepository;
            _plantillasrepository = plantillasrepository;
        }



        public bool EnviarCorreoelectronicoComentarios(decimal idComment, TBL_Admin_Usuarios userSession)
        {

            var oComment = _comentariosRepository.GetComentarioById(idComment);

            if (oComment == null)
            {
                _traceManager.LogInfo(string.Format("El objeto comentario es null. Clase:{0} ", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var template = ObtenerPlantilla("ComentariosApc", "Colombia");

            if (string.IsNullOrEmpty(template))
            {
                _traceManager.LogInfo(string.Format("Error al obtener el objeto Template desde la BD. Código:[{0}]  - Clase:[{1}] ", "ComentariosApc", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Titulo",oComment.IdComentarioRelacionado.HasValue ? "Respuesta Comentario Acciones Preventivas Correctivas" : "Comentario Acciones Preventivas Correctivas"},
                                        {"$Codigo", oComment.TBL_ModuloAPC_Solicitud.Codigo},
                                        {"$Asunto", oComment.Asunto}
                                    };

            var strFrom = string.Format("Gestión de Calidad. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());


            var bodyParams = new Dictionary<string, string>
                                 {
                                     {"$Contenido",oComment.Comentario},
                                     {"$Url",UrlHelper.GetUrlPreViewCommentApc(oComment.IdComentario.ToString())}
                                 };

            var cc = new string[oComment.TBL_Admin_Usuarios3.Count];
            var index = 0;
            if (oComment.TBL_Admin_Usuarios3.Count > 0)
            {
                foreach (var usu in oComment.TBL_Admin_Usuarios3)
                {
                    cc[index] = usu.Email;
                    index++;
                }
            }

            var adjuntos = new Stream[oComment.TBL_ModuloAPC_AnexosComentarioRespuesta.Count];
            if (oComment.TBL_ModuloAPC_AnexosComentarioRespuesta.Count > 0)
            {
                index = 0;
                foreach (var anexo in oComment.TBL_ModuloAPC_AnexosComentarioRespuesta)
                {
                    var ms = new MemoryStream(anexo.Archivo);
                    adjuntos[index] = ms;
                    index++;
                }
            }

            bool result;
            try
            {
                _iEmailService.ProcessEmail(strFrom, oComment.TBL_Admin_Usuarios.Email, template, subjectParams, bodyParams, cc, adjuntos);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }



        public bool EnviarCorreoelectronicoActividadApcRealizada(decimal idActividad, TBL_Admin_Usuarios userSession)
        {

            var oActividad = _actividadesRepository.GetActividadById(idActividad);

            if (oActividad == null)
            {
                _traceManager.LogInfo(string.Format("El objeto Actividad es null. Clase:{0} ", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            //Si el usuario de sesion es el autor de la actividad, no se envie el Email.
            if (oActividad.CreateBy == userSession.IdUser) return true;

            var template = ObtenerPlantilla("ActividadRealizadaApc", "Colombia");

            if (string.IsNullOrEmpty(template))
            {
                _traceManager.LogInfo(string.Format("Error al obtener el objeto Template desde la BD. Código:[{0}]  - Clase:[{1}] ", "ActividadRealizadaApc", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Consecutivo",oActividad.TBL_ModuloAPC_Solicitud.Codigo}
                                    };

            var strFrom = string.Format("Gestión de Calidad. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            var bodyParams = new Dictionary<string, string>
                                 {
                                     {"$Solicitante",oActividad.TBL_Admin_Usuarios.Nombres},
                                     {"$Url",UrlHelper.GetUrlPreViewActividadSolicitudforEmail(oActividad.IdActividad.ToString(),oActividad.IdSolicitudAPC.ToString())}
                                 };

            var cc = new string[1];
            if (oActividad.TBL_Admin_Usuarios3 != null)
            {
                cc[0] = oActividad.TBL_Admin_Usuarios3.Email;
            }
            bool result;
            try
            {
                _iEmailService.ProcessEmail(strFrom, oActividad.TBL_Admin_Usuarios.Email, template, subjectParams, bodyParams, cc, null);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }


        public bool EnviarCorreoelectronicoActividadApcCancelada(decimal idActividad, TBL_Admin_Usuarios userSession, string motivo)
        {

            var oActividad = _actividadesRepository.GetActividadById(idActividad);

            if (oActividad == null)
            {
                _traceManager.LogInfo(string.Format("El objeto Actividad es null. Clase:{0} ", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            //Si el usuario de sesion es el autor de la actividad, no se envie el Email.
            if (oActividad.CreateBy == userSession.IdUser) return true;

            var template = ObtenerPlantilla("ActividadCanceladaApc", "Colombia");

            if (string.IsNullOrEmpty(template))
            {
                _traceManager.LogInfo(string.Format("Error al obtener el objeto Template desde la BD. Código:[{0}]  - Clase:[{1}] ", "ActividadCanceladaApc", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var subjectParams = new Dictionary<string, string>
                                    {
                                        {"$Consecutivo",oActividad.TBL_ModuloAPC_Solicitud.Codigo},
                                        {"$Actividad",oActividad.Descripcion}
                                    };

            var strFrom = string.Format("Gestión de Calidad. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());

            var bodyParams = new Dictionary<string, string>
                                 {
                                     {"$Solicitante",oActividad.TBL_Admin_Usuarios.Nombres},
                                     {"$Motivo",motivo},
                                     {"$Url",UrlHelper.GetUrlPreViewActividadSolicitudforEmail(oActividad.IdActividad.ToString(),oActividad.IdSolicitudAPC.ToString())}
                                 };

            var cc = new string[1];
            if (oActividad.TBL_Admin_Usuarios3 != null)
            {
                cc[0] = oActividad.TBL_Admin_Usuarios3.Email;
            }
            bool result;
            try
            {
                _iEmailService.ProcessEmail(strFrom, oActividad.TBL_Admin_Usuarios.Email, template, subjectParams, bodyParams, cc, null);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoPlantilla"></param>
        /// <param name="pais"></param>
        /// <returns></returns>
        private string ObtenerPlantilla(string codigoPlantilla, string pais)
        {
            const int idModule = (int) ModulosAplicacion.AccionesPc;
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
        private string GetFromEmail()
        {
            const int moduleId = (int)ModulosAplicacion.Admin;
            var option = _optionsRepository.GetOptionByKey("FromMail", moduleId);
            return option == null ? "velasquez.ricardo@co.sika.com" : option.Value;
        }
    }
}