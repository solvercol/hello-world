using System;
using System.Collections.Generic;
using System.IO;
using Domain.MainModule.Contracts;
using Domain.MainModule.Reclamos.Contracts;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Infrastructure.CrossCutting.NetFramework.Services.Email;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Application.MainModule.Reclamos.Util
{
    public class SendEmail : ISendEmail
    {
        private readonly IEmailService _iEmailService;
        private readonly ITBL_Admin_PlantillasRepository _plantillasrepository;
        private readonly ITBL_Admin_OptionListRepository _optionsRepository;
        private readonly ITBL_ModuloReclamos_ComentariosRespuestaRepository _comentariosRepository;
        private readonly ITBL_ModuloReclamos_AlternativasRepository _alternativasRepository;
        private readonly ITBL_ModuloReclamos_ActividadesRepository _actividadesRepository;
        readonly ITraceManager _traceManager;


        public SendEmail(
            IEmailService iEmailService, 
            ITBL_Admin_PlantillasRepository plantillasrepository, 
            ITBL_Admin_OptionListRepository optionsRepository, 
            ITBL_ModuloReclamos_ComentariosRespuestaRepository comentariosRepository, 
            ITraceManager traceManager, 
            ITBL_ModuloReclamos_AlternativasRepository alternativasRepository, 
            ITBL_ModuloReclamos_ActividadesRepository actividadesRepository)
        {
            _iEmailService = iEmailService;
            _actividadesRepository = actividadesRepository;
            _alternativasRepository = alternativasRepository;
            _traceManager = traceManager;
            _comentariosRepository = comentariosRepository;
            _optionsRepository = optionsRepository;
            _plantillasrepository = plantillasrepository;
        }


        public bool EnviarCorreoelectronicoAutorReclamo(decimal idComment, TBL_Admin_Usuarios userSession)
        {

            var oComment = _comentariosRepository.GetComentarioById(idComment);

            if (oComment == null)
            {
                _traceManager.LogInfo(string.Format("El objeto comentario es null. Clase:{0} ", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var template = ObtenerPlantilla("EstadoPorEnviar", "Colombia");

            if (string.IsNullOrEmpty(template))
            {
                _traceManager.LogInfo(string.Format("Error al obtener el objeto Template desde la BD. Código:[{0}]  - Clase:[{1}] ", "SendCommentPedidos", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var subjectParams = new Dictionary<string, string>
                                    {
                                        { "$Asunto",oComment.Asunto},
                                        { "$NumeroReclamo",oComment.TBL_ModuloReclamos_Reclamo.NumeroReclamo}
                                    };

            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());


            var bodyParams = new Dictionary<string, string>
                                 {
                                     {"$Contenido",oComment.Comentario},
                                     {"$Url", UrlHelper.GetUrlPreViewComment(idComment.ToString())}
                                 };

            var cc = new string[oComment.TBL_Admin_Usuarios3.Count] ;
            var index = 0;
            if (oComment.TBL_Admin_Usuarios3.Count > 0)
            {
                foreach (var usu in oComment.TBL_Admin_Usuarios3)
                {
                    cc[index] = usu.Email;
                    index++;
                }
            }

            var adjuntos = new Stream[oComment.TBL_ModuloReclamos_AnexosComentarioRespuesta.Count];
            if (oComment.TBL_ModuloReclamos_AnexosComentarioRespuesta.Count > 0)
            {

                index = 0;
                foreach (var anexo in oComment.TBL_ModuloReclamos_AnexosComentarioRespuesta)
                {
                    var ms = new MemoryStream(anexo.Archivo);
                    adjuntos[index] = ms;   
                    index++;
                }
            }

            bool result;
            try
            {
                _iEmailService.ProcessEmail(strFrom, oComment.TBL_Admin_Usuarios2.Email, template, subjectParams, bodyParams, cc, adjuntos);
                result = true;
            }
            catch 
            {
                result = false;
            }
          
            if(result)
            {
                //Actualizacion del estado del comentario.
                var unitOfWork = _comentariosRepository.UnitOfWork;
                oComment.ModifiedBy = userSession.IdUser;
                oComment.ModifiedOn = DateTime.Now;
                oComment.Estado = "Enviado";
                _comentariosRepository.Modify(oComment);
                unitOfWork.CommitAndRefreshChanges();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idComment"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoelectronicoRespuestaCliente(decimal idComment, TBL_Admin_Usuarios userSession)
        {

            var oComment = _comentariosRepository.GetComentarioById(idComment);

            if (oComment == null)
            {
                _traceManager.LogInfo(string.Format("El objeto comentario es null. Clase:{0} ", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var template = ObtenerPlantilla("EnvioRespuestaCliente", "Colombia");

            if (string.IsNullOrEmpty(template))
            {
                _traceManager.LogInfo(string.Format("Error al obtener el objeto Template desde la BD. Código:[{0}]  - Clase:[{1}] ", "SendCommentPedidos", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var subjectParams = new Dictionary<string, string>
                                    {
                                        { "$Cliente",string.IsNullOrEmpty( oComment.TBL_ModuloReclamos_Reclamo.NombreCliente) ? oComment.TBL_ModuloReclamos_Reclamo.CodigoCliente : oComment.TBL_ModuloReclamos_Reclamo.NombreCliente}
                                    };

            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());


            var bodyParams = new Dictionary<string, string>
                                 {
                                     {"$Asunto",oComment.Asunto},
                                     {"$Contenido",oComment.Comentario}
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

            var adjuntos = new Stream[oComment.TBL_ModuloReclamos_AnexosComentarioRespuesta.Count];
            if (oComment.TBL_ModuloReclamos_AnexosComentarioRespuesta.Count > 0)
            {

                index = 0;
                foreach (var anexo in oComment.TBL_ModuloReclamos_AnexosComentarioRespuesta)
                {
                    var ms = new MemoryStream(anexo.Archivo);
                    adjuntos[index] = ms;
                    index++;
                }
            }

            bool result;
            try
            {
                _iEmailService.ProcessEmail(strFrom, oComment.EmailDestinatarioCliente, template, subjectParams, bodyParams, cc, adjuntos);
                result = true;
            }
            catch
            {
                result = false;
            }

            if (result)
            {
                //Actualizacion del estado del comentario.
                var unitOfWork = _comentariosRepository.UnitOfWork;
                oComment.ModifiedBy = userSession.IdUser;
                oComment.ModifiedOn = DateTime.Now;
                oComment.Estado = "Enviado";
                _comentariosRepository.Modify(oComment);
                unitOfWork.CommitAndRefreshChanges();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAlternativa"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoelectronicoAlternativaSolucion(decimal idAlternativa, TBL_Admin_Usuarios userSession)
        {

            var oAlternativa = _alternativasRepository.GetAlternativaById(idAlternativa);

            if (oAlternativa == null)
            {
                _traceManager.LogInfo(string.Format("El objeto comentario es null. Clase:{0} ", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            if (oAlternativa.IdResponsable == oAlternativa.CreateBy)
                return true;

            var template = ObtenerPlantilla("Alternativas", "Colombia");

            if (string.IsNullOrEmpty(template))
            {
                _traceManager.LogInfo(string.Format("Error al obtener el objeto Template desde la BD. Código:[{0}]  - Clase:[{1}] ", "SendCommentPedidos", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var subjectParams = new Dictionary<string, string>
                                    {
                                        { "$NumeroReclamo",oAlternativa.TBL_ModuloReclamos_Reclamo.NumeroReclamo}
                                    };

            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());


            var bodyParams = new Dictionary<string, string>
                                 {
                                     {
                                         "$Responsable",
                                         oAlternativa.TBL_Admin_Usuarios2 == null
                                             ? string.Empty
                                             : oAlternativa.TBL_Admin_Usuarios2.Nombres
                                         },
                                     {
                                         "$NumeroReclamo",
                                         oAlternativa.TBL_ModuloReclamos_Reclamo == null
                                             ? string.Empty
                                             : oAlternativa.TBL_ModuloReclamos_Reclamo.NumeroReclamo
                                         },
                                     {"$Accion", oAlternativa.Alternativa},
                                     {
                                         "$TipoReclamo",
                                         oAlternativa.TBL_ModuloReclamos_Reclamo == null
                                             ? string.Empty
                                             : oAlternativa.TBL_ModuloReclamos_Reclamo.TipoReclamo
                                         },
                                     {"$Url", UrlHelper.GetUrlPreViewAlternativaSolucion(idAlternativa.ToString())},
                                     {
                                         "$Cliente",
                                         !string.IsNullOrEmpty( oAlternativa.TBL_ModuloReclamos_Reclamo == null ? string.Empty: oAlternativa.TBL_ModuloReclamos_Reclamo.CodigoCliente)
                                             ? oAlternativa.TBL_ModuloReclamos_Reclamo == null ? string.Empty: oAlternativa.TBL_ModuloReclamos_Reclamo.NombreCliente
                                             : string.Empty
                                         }
                                 };


            var adjuntos = new Stream[oAlternativa.TBL_ModuloReclamos_AnexosAlternativa.Count];
            if (oAlternativa.TBL_ModuloReclamos_AnexosAlternativa.Count > 0)
            {

                var index = 0;
                foreach (var anexo in oAlternativa.TBL_ModuloReclamos_AnexosAlternativa)
                {
                    var ms = new MemoryStream(anexo.Archivo);
                    adjuntos[index] = ms;
                    index++;
                }
            }

            bool result;
            try
            {
                _iEmailService.ProcessEmail(strFrom, oAlternativa.TBL_Admin_Usuarios2.Email, template, subjectParams, bodyParams, null, adjuntos);
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
        /// <param name="idActividad"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool EnviarCorreoelectronicoActividades(decimal idActividad, TBL_Admin_Usuarios userSession)
        {

            var oActividad = _actividadesRepository.GetActividadById(idActividad);

            if (oActividad == null)
            {
                _traceManager.LogInfo(string.Format("El objeto comentario es null. Clase:{0} ", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            if (oActividad.IdUsuarioAsignacion == oActividad.CreateBy)
                return true;

            var template = ObtenerPlantilla("ProgramarActividad", "Colombia");

            if (string.IsNullOrEmpty(template))
            {
                _traceManager.LogInfo(string.Format("Error al obtener el objeto Template desde la BD. Código:[{0}]  - Clase:[{1}] ", "SendCommentPedidos", System.Reflection.MethodBase.GetCurrentMethod().Name), LogType.Notify);
                return false;
            }

            var subjectParams = new Dictionary<string, string>
                                    {
                                        { "$NumeroReclamo",oActividad.TBL_ModuloReclamos_Reclamo.NumeroReclamo},
                                        { "$Accion",oActividad.TBL_ModuloReclamos_ActividadesReclamo.Nombre}
                                    };

            var strFrom = string.Format("Gestión de Reclamos. Enviado por: {0}<{1}>", userSession.Nombres, GetFromEmail());


            var bodyParams = new Dictionary<string, string>
                                 {
                                     {"$Responsable",oActividad.TBL_Admin_Usuarios2.Nombres},
                                     {"$NumeroReclamo",oActividad.TBL_ModuloReclamos_Reclamo.NumeroReclamo},
                                     {"$TipoReclamo",oActividad.TBL_ModuloReclamos_Reclamo.TipoReclamo},
                                     {"$Url", UrlHelper.GetUrlPreViewActividad(idActividad.ToString())}
                                 };

            bodyParams.Add("$Cliente",
                           !string.IsNullOrEmpty(oActividad.TBL_ModuloReclamos_Reclamo.CodigoCliente)
                               ? oActividad.TBL_ModuloReclamos_Reclamo.NombreCliente
                               : string.Empty);


            var adjuntos = new Stream[oActividad.TBL_ModuloReclamos_AnexosActividad.Count];
            if (oActividad.TBL_ModuloReclamos_AnexosActividad.Count > 0)
            {
                var index = 0;
                foreach (var anexo in oActividad.TBL_ModuloReclamos_AnexosActividad)
                {
                    var ms = new MemoryStream(anexo.Archivo);
                    adjuntos[index] = ms;
                    index++;
                }
            }


            var cc = new string[oActividad.TBL_Admin_Usuarios3.Count];
            var i = 0;
            if (oActividad.TBL_Admin_Usuarios3.Count > 0)
            {
                foreach (var usu in oActividad.TBL_Admin_Usuarios3)
                {
                    cc[i] = usu.Email;
                    i++;
                }
            }

            bool result;
            try
            {
                _iEmailService.ProcessEmail(strFrom, oActividad.TBL_Admin_Usuarios2.Email, template, subjectParams, bodyParams, null, adjuntos);
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
            var plantilla = _plantillasrepository.GetPlantillaByIdPaisByCodigo(codigoPlantilla, pais);

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