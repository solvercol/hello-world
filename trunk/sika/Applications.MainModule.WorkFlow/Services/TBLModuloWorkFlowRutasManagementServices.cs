//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por el motor de generacion de codigo de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  junio 18 de 2014.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.SystemActions.Service;
using Applications.MainModule.WorkFlow.DTO;
using Applications.MainModule.WorkFlow.IServices;
using Applications.MainModule.WorkFlow.Resources;
using Applications.MainModule.WorkFlow.Util;
using Domain.MainModule.Contracts;
using Domain.MainModule.Documentos.Contracts;
using Domain.MainModule.Reclamos.Contracts;
using Domain.MainModule.Reclamos.Services;
using Domain.MainModule.WorkFlow.Contracts;
using Domain.MainModule.WorkFlow.Enums;
using Domain.MainModule.WorkFlow.Services.FieldsValidatos;
using Domain.MainModule.WorkFlow.Services.WorkFlow;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Infraestructure.CrossCutting.Security.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace Applications.MainModule.WorkFlow.Services
{
    public class SfTBL_ModuloWorkFlow_RutasManagementServices : ISfTBL_ModuloWorkFlow_RutasManagementServices
    {

        #region Fields
        private readonly ITBL_ModuloWorkFlow_RutasRepository _tblModuloWorkFlowRutasRepository;
        private readonly ITBL_ModuloReclamos_ReclamoRepository _tblDocumentosRepository;
        private readonly ITBL_Admin_EstadosProcesoRepository _estadosRepository;
        private readonly ITblModuloWorkFlowRutasFieldsValidatorDomainServices _workFlowDomainFieldsValidatorServices;
        private readonly ITblModuloWorkFlowRutaDomainServices _workFlowDomainServices;
        private readonly ISystemActionsManagementServices _systemActionsServices;
        private readonly IAutentication _autenticationService;
        private readonly ITBL_Admin_UsuariosRepository _usuariosRepository;
        private readonly ITBL_Admin_SistemaNotificacionesRepository _notificacionesSistemaRepository;
        private readonly ISendMailNotification _sendMailNotificationServices;
        private readonly IReclamosAdoService _sqlReclamosServices;
        private readonly ITBL_Moduloreclamos_ReclamoDomainServices _reclamosDomainServices;
        private readonly ITBL_ModuloReclamos_TrackingRepository _trackRepository;
        private readonly ITBL_ModuloReclamos_LogReclamosRepository _logDocumentosRepository;
        #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloWorkFlow_RutasManagementServices( 
             ITBL_ModuloWorkFlow_RutasRepository tblModuloWorkFlowRutasRepository, 
             ITBL_Admin_EstadosProcesoRepository estadosRepository,
             ITblModuloWorkFlowRutasFieldsValidatorDomainServices workFlowDomainFieldsValidatorServices, 
             ITblModuloWorkFlowRutaDomainServices workFlowDomainServices, 
             ISystemActionsManagementServices systemActionsServices, 
             IAutentication autenticationService, 
             ITBL_Admin_UsuariosRepository usuariosRepository, 
             ITBL_Admin_SistemaNotificacionesRepository notificacionesSistemaRepository, 
             ISendMailNotification sendMailNotificationServices,
             ITBL_ModuloReclamos_ReclamoRepository tblDocumentosRepository, 
             IReclamosAdoService sqlReclamosServices, 
             ITBL_Moduloreclamos_ReclamoDomainServices reclamosDomainServices, 
             ITBL_ModuloReclamos_TrackingRepository trackRepository)
         {
            if (tblModuloWorkFlowRutasRepository == null)
                throw new ArgumentNullException("tblModuloWorkFlowRutasRepository");
            _tblModuloWorkFlowRutasRepository = tblModuloWorkFlowRutasRepository;
             _trackRepository = trackRepository;
             _reclamosDomainServices = reclamosDomainServices;
             _sqlReclamosServices = sqlReclamosServices;
             _tblDocumentosRepository = tblDocumentosRepository;
             _sendMailNotificationServices = sendMailNotificationServices;
             _notificacionesSistemaRepository = notificacionesSistemaRepository;
             _usuariosRepository = usuariosRepository;
             _autenticationService = autenticationService;
             _systemActionsServices = systemActionsServices;
             _workFlowDomainServices = workFlowDomainServices;
             _workFlowDomainFieldsValidatorServices = workFlowDomainFieldsValidatorServices;
             _estadosRepository = estadosRepository;

         }
         #endregion

         #region Members Work Flow
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloWorkFlow_Rutas NewEntity()
         {
            return new TBL_ModuloWorkFlow_Rutas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloWorkFlow_Rutas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloWorkFlowRutasRepository.UnitOfWork;
            _tblModuloWorkFlowRutasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloWorkFlow_Rutas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblModuloWorkFlowRutasRepository.UnitOfWork;
            _tblModuloWorkFlowRutasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloWorkFlow_Rutas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloWorkFlowRutasRepository.UnitOfWork;

            _tblModuloWorkFlowRutasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloWorkFlow_Rutas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloWorkFlow_Rutas> specification = new DirectSpecification<TBL_ModuloWorkFlow_Rutas>(u => u.IdRuta == id);

            return _tblModuloWorkFlowRutasRepository.GetEntityBySpec(specification);
           
         }
	

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloWorkFlow_Rutas> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloWorkFlow_Rutas> specification = new DirectSpecification<TBL_ModuloWorkFlow_Rutas>(u => u.IsActive == isActive);
            return _tblModuloWorkFlowRutasRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloWorkFlow_Rutas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloWorkFlow_Rutas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloWorkFlow_Rutas>(u => u.IsActive);

            return _tblModuloWorkFlowRutasRepository.GetPagedElements(pageIndex, pageCount, u => u.IdRuta, onlyEnabledSpec, true).ToList();
         }

         public IEnumerable<TBL_ModuloWorkFlow_Rutas> ListadoRutasPorIdModule(ModulosAplicacion  module)
         {
             var strModule = module.ToString();
             var specification = new DirectSpecification<TBL_ModuloWorkFlow_Rutas>(u => u.IsActive && u.TipoModulo.Equals(strModule));
             return _tblModuloWorkFlowRutasRepository.FindRutasBySpec(specification).ToList();
         }

         public List<TBL_ModuloWorkFlow_Rutas> FindBySpec(int idRuta)
         {
             Specification<TBL_ModuloWorkFlow_Rutas> specification = new DirectSpecification<TBL_ModuloWorkFlow_Rutas>(u => u.IdRuta == idRuta);
             return _tblModuloWorkFlowRutasRepository.FindRutasBySpec(specification).ToList();
         }


         public List<TBL_ModuloWorkFlow_Rutas> GetRutasByEstadoByModule(int idEstadoDocumento, ModulosAplicacion module)
         {
             var strModule = module.ToString();
             var specification =
                 new DirectSpecification<TBL_ModuloWorkFlow_Rutas>(
                     u => u.IsActive && u.IdEstado == idEstadoDocumento && u.TipoModulo.Equals(strModule));
             return _tblModuloWorkFlowRutasRepository.FindRutasBySpec(specification).OrderBy(x=> x.Secuencia).ToList();

         }


        #endregion

         #region Members RECLAMOS

        private DataTable GetDocumentWorkFlowById(string id)
        {
            return _sqlReclamosServices.GetReclamoWorkFlowById(id);
        }
         #endregion

       
        /// <summary>
        /// Carga los parametros iniciales aplicando las reglas de validaciónn definidas en las rutas.
        /// </summary>
        /// <param name="idDocument"></param>
        /// <returns></returns>
        public RenderTypeControlButtonDto CargarWorkFlow(string idDocument)
        {

            try
            {

                var estadoReclamo = _sqlReclamosServices.EstadoReclamo(idDocument);

                if (string.IsNullOrEmpty(estadoReclamo))
                    throw new Exception("Error al obtener el estado del reclamo desde la Base de Datos.");

                //Se listan los campos involucrados en el Work Flow con el fin de mapearlos y posteriormente evaluar su
                //contenido.
                var dtReclamo = GetDocumentWorkFlowById(idDocument);

                var listadoRutas = GetRutasByEstadoByModule(Convert.ToInt32(estadoReclamo), ModulosAplicacion.Reclamos);

                var oWorkflow = _workFlowDomainServices.CargarWorkFlow(listadoRutas, dtReclamo, idDocument);

                if (oWorkflow == null) return null;

                var responsable = RetornarUsuarioResponsable(oWorkflow.CurrenteResponsible, Convert.ToInt32(idDocument), dtReclamo, estadoReclamo);

                var oRender = new RenderTypeControlButtonDto
                                  {
                                      CurrentStatus = oWorkflow.CurrentStatus,
                                      IdCurrentStatus = estadoReclamo,
                                      NextStatus = oWorkflow.NextStatus,
                                      IdNextStatus = oWorkflow.IdNextStatus,
                                      TextControl = oWorkflow.TextControl,
                                      IdDocument = idDocument,
                                      CurrentResponsibe = responsable == null ? string.Empty : responsable.Nombres,
                                      IdCurrentResponsibe = responsable == null ? string.Empty : responsable.IdUser.ToString(),
                                      EmailCurrentResponsibe = responsable == null ? string.Empty : responsable.Email
                };

                return oRender;
            }
            catch (Exception ex)
            {
                throw new Exception("CargarWorkFlow", ex);
            }
        }

        /// <summary>
        /// Ejecuta el WorkFlow realizando validaciones y dependiendo del resultado de las mismas, se actualiza  o no el pedido.
        /// </summary>
        /// <param name="oDocument"></param>
        /// <returns></returns>
        public RenderTypeControlButtonDto EjecutarWorkFlow(RenderTypeControlButtonDto oDocument)
        {
            try
            {


                if (oDocument == null) return null;

                var oEstado = _estadosRepository.GetEstadoById(Convert.ToInt32(oDocument.IdCurrentStatus));

                if (oEstado == null)
                    throw new ArgumentException(string.Format("Error al recuperar el estado {0} desde la Base de Datos.", oDocument.CurrentStatus));

                var oDoc = _tblDocumentosRepository.GetReclamoById(Convert.ToInt32(oDocument.IdDocument));

                if (oDoc == null)
                    throw new ArgumentException(string.Format("Error al recuperar el reclamo {0} desde la Base de Datos.", oDocument.IdDocument));

                if (oDoc.IdEstado == Convert.ToInt32(oDocument.IdNextStatus)) return oDocument;

                var nextStatus = Convert.ToInt32(oDocument.IdNextStatus);
                var currentRule = oEstado.TBL_ModuloWorkFlow_Rutas.Where(x => x.SiguienteEstado == nextStatus).SingleOrDefault();

                if (currentRule.ValidaRequeridos)
                {
                    if (oEstado.TBL_ModuloWorkFlow_CamposValidacion.Count > 0)
                    {
                        var result = ValidacionDeCampos(oEstado, oDoc, oDocument);
                        if (!result)
                        {
                            oDocument.ProcessStatus = ProcessStatus.ValidationErrorField;
                            return oDocument;
                        }
                    }
                    else
                    {
                        var mensaje = string.Format(Messages.NotFieldValidations, currentRule.IdRuta, currentRule.IdEstado);
                        oDocument.MessagesError.Add(mensaje);
                    }
                }

                if (currentRule.TBL_ModuloWorkFlow_ValidacionesSalida.Where(x => x.Ejecutar == true).Count() > 0)
                {

                    var inputParameters = currentRule.TBL_ModuloWorkFlow_ValidacionesSalida.Where(
                            x => x.NombreMetodo.Contains("[InputParameters]"));

                    if (inputParameters.Count() > 0)
                    {
                        //Los parametros de entrada rompen el flujo de la aplicación para lanzar ventanas de captura y proceguir con el flujo 
                        //desde otro formulario.
                        ProcesarParametrosEntrada(oDocument, currentRule.TBL_ModuloWorkFlow_ValidacionesSalida);
                        oDocument.ProcessStatus = ProcessStatus.InputParameters;
                        return oDocument;
                    }

                    var listerror = EjecutarAccionSistema(currentRule.TBL_ModuloWorkFlow_ValidacionesSalida,oDoc.IdReclamo.ToString());
                    if (listerror.Count > 0)
                    {
                        foreach (var msg in listerror)
                        {
                            oDocument.MessagesError.Add(msg);
                        }
                        oDocument.ProcessStatus = ProcessStatus.ValidationErrorSystemActions;
                        return oDocument;
                    }
                }

                

                var txSettings = new TransactionOptions()
                {
                    Timeout = TransactionManager.DefaultTimeout,
                    IsolationLevel = IsolationLevel.Serializable
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var unitOfWork = _tblDocumentosRepository.UnitOfWork;

                    oDoc.IdEstado = Convert.ToInt32(oDocument.IdNextStatus);

                    oDoc.ModifiedBy = _autenticationService.GetUserFromSession.IdUser;

                    oDoc.ModifiedOn = DateTime.Now;

                    var responsable = GetResponsableDocumento(oDocument.IdDocument, oDocument.IdNextStatus);
                    if (!string.IsNullOrEmpty(responsable))
                        oDoc.IdResponsableActual = Convert.ToInt32(responsable);

                    _tblDocumentosRepository.Modify(oDoc);

                    //Crea un nuevo registro en el tracking del pedido
                    GenerarEntradatracking(oDocument);

                    //Crea un nuevo registro en el log del pedido.
                    GenerarEntradalogPedido(oDocument);

                    GenerarNotificacionSistema(oDocument);

                    unitOfWork.CommitAndRefreshChanges();

                    oDocument.ProcessStatus = ProcessStatus.Ok;

                    scope.Complete();
                }

                SendMail(oDocument);

                return oDocument;

            }
            catch (Exception ex)
            {
                throw new Exception("EjecutarWorkFlow", ex);
            }
        }

        private string GetResponsableDocumento(string idDocumento, string estado)
        {

            var dtPedido = GetDocumentWorkFlowById(idDocumento);

            var listadoRutas = GetRutasByEstadoByModule(Convert.ToInt32(estado), ModulosAplicacion.Reclamos);

            var rolResponsable = _workFlowDomainServices.GetResponsablePedidobyRuta(listadoRutas, dtPedido);

            if (!string.IsNullOrEmpty(rolResponsable))
            {
                var responsable = RetornarUsuarioResponsable(rolResponsable, Convert.ToInt32(idDocumento), dtPedido, estado);
                if (responsable != null)
                    return responsable.IdUser.ToString();
            }

            return string.Empty;
        }
        
        /// <summary>
        /// Realiza el proceso de validación de campos con base a las reglas definidas en la tabla FieldValidations.
        /// </summary>
        /// <param name="oEstado"></param>
        /// <param name="oDoc"></param>
        /// <param name="oDocument"></param>
        /// <returns></returns>
        private bool ValidacionDeCampos(TBL_Admin_EstadosProceso oEstado, TBL_ModuloReclamos_Reclamo oDoc, RenderTypeControlButtonDto oDocument)
        {
            var isValidFields = _workFlowDomainFieldsValidatorServices.IsValidField(oDoc, oEstado);
            if (!isValidFields)
            {
                var listErrors = _workFlowDomainFieldsValidatorServices.GetValidationErrorsMessages;

                var procedimientos = _workFlowDomainFieldsValidatorServices.GetStoreProceduresValidadtionFunctions;
                if (procedimientos.Count > 0)
                {
                    listErrors.AddRange(from procedimiento in procedimientos
                                        let result = EjecutarSp(procedimiento, oDoc)
                                        where !result
                                        select string.Format("SP:[{0}] - Mensaje:[{1}]", procedimiento, "La validación a travéz de la función externa no fue exitosa!!."));
                }

                oDocument.MessagesError = listErrors;
            }

            return isValidFields;
        }

        /// <summary>
        /// Función que ejecuta procedimientos almacenados con AdoNet
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="oDoc"></param>
        /// <returns></returns>
        private bool EjecutarSp(string sp,TBL_ModuloReclamos_Reclamo oDoc )
        {
            var parameters = new Dictionary<string, string> {{"IdReclamo", oDoc.IdReclamo.ToString()}};
            var result = _sqlReclamosServices.EjecutarSpToBool(sp, parameters);
            return Convert.ToBoolean(result);
          
        }

        /// <summary>
        /// El parámetro mínimo que de debe enviar al servicio de ejecución de funciones del sistema debe ser el Id del Documento
        /// Si por alguna razón es necesario algun parámetro adicional, se deberán implemetar las reglas de negocio dentro de la clase
        /// que invoca el método que realiza la validación. 
        /// 
        /// </summary>
        /// <param name="accionesSistema"></param>
        /// <param name="idDocument"></param>
        /// <returns></returns>
        private List<string> EjecutarAccionSistema(IEnumerable<TBL_ModuloWorkFlow_ValidacionesSalida> accionesSistema, string idDocument)
        {
            try
            {
                var messages = new List<string>();
                foreach (var salida in accionesSistema)
                {
                    _systemActionsServices.AssemblyQualifiedName = salida.NombreEnsamblado;
                    _systemActionsServices.MethodName = salida.NombreMetodo;
                    _systemActionsServices.Params = new object[] { idDocument };

                    var result = _systemActionsServices.Execute();

                    if (result is bool)
                    {
                        if (!Convert.ToBoolean(result))
                            messages.Add(string.Format("La Ejecución de la Acción del Sistema [{0}] retornó FALSO en el proceso de validación..", salida.NombreMetodo));
                    }
                }

                return messages;
            }
            catch (Exception ex)
            {
                throw new Exception("EjecutarAccionSistema", ex);
            }
        }

        /// <summary>
        /// Recoge en una lista el nombre de las funciones que se deben ejecutar como una ventana de mensaje desde la interface grafica.
        /// </summary>
        /// <param name="oDocument"></param>
        /// <param name="ovalidaciones"></param>
        private static void ProcesarParametrosEntrada(RenderTypeControlButtonDto oDocument, IEnumerable<TBL_ModuloWorkFlow_ValidacionesSalida> ovalidaciones)
        {
            var subjectRegex = new Regex(@"\[InputParameters\](.*)\[\/InputParameters\]", RegexOptions.Compiled | RegexOptions.Singleline);
            var inputList = new List<string>();
            foreach (var input in
                ovalidaciones.Select(val => subjectRegex.Match(val.NombreMetodo).Groups[1].Value).Where(input => !string.IsNullOrEmpty(input)))
            {
                if(input.Contains(":"))
                {
                    var nameFunctions = input.Split(':');
                    inputList.AddRange(nameFunctions);
                }
                else
                {
                    inputList.Add(input);
                }

                oDocument.OutputParameters = inputList;
            }
        }

        /// <summary>
        /// Evalua la expresión retornando el nombre del usuario responsable  como producto de la verificación
        /// </summary>
        /// <param name="role"></param>
        /// <param name="idPedido"></param>
        /// <param name="dt"></param>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        private TBL_Admin_Usuarios RetornarUsuarioResponsable(string role, int idPedido, DataTable dt, string idEstado)
        {
            if (string.IsNullOrEmpty(role)) return null;
            //1= Estado Registrado
            if (idEstado == "1")
            {
                if (!(dt.Rows[0]["CreateBy"] is DBNull))
                {
                    var user = _usuariosRepository.GetUsuarioById(Convert.ToInt32(dt.Rows[0]["CreateBy"]));
                    return user;
                }
            }

            var roleResponsable = role;
            if (role.Contains("[Fn]"))
            {
                roleResponsable = _workFlowDomainServices.MapearExpresion(role, dt);
            }

            if (roleResponsable.Contains("Autor"))
            {
                var user = _usuariosRepository.RetornarUsuarioAutordocumento(idPedido);
                if (user != null)
                {
                    return user;
                }
            }

            var userrole = _usuariosRepository.RetornarUsuarioReponsableAprobacion(roleResponsable);

            return userrole;
            
        }

        /// <summary>
        /// Crea un nuevo registro en el trackin del pedido
        /// </summary>
        /// <param name="oDocument"></param>
        private void GenerarEntradatracking(RenderTypeControlButtonDto oDocument)
        {

            var oTrack = _reclamosDomainServices.GenerarObjetoTrackpedido(_trackRepository.NewEntity(), oDocument.TextControl,
                                                             oDocument.CurrentStatus,
                                                             Convert.ToInt32(oDocument.IdDocument), oDocument.NextStatus,
                                                             oDocument.CurrentResponsibe);
            _trackRepository.Add(oTrack);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDocument"></param>
        private void GenerarEntradalogPedido(RenderTypeControlButtonDto oDocument)
        {
            var oLog = _reclamosDomainServices.GenerarObjetoLogPedido(_logDocumentosRepository.NewEntity(),
                                                                     oDocument.TextControl,
                                                                     Convert.ToInt32(oDocument.IdDocument));
            _logDocumentosRepository.Add(oLog);
        }

        /// <summary>
        /// Envía una notificación al responsable del documento al correo electronico.
        /// </summary>
        /// <param name="oDocument"></param>
        /// <returns></returns>
        private bool SendMail(RenderTypeControlButtonDto oDocument)
        {
            return _sendMailNotificationServices.EnviarCorreoElectronicoNotificacion(oDocument);
        }

        /// <summary>
        /// Crea un nuevo registro en la tabla de notificaciones para el usuario responsable del documento.
        /// </summary>
        private void GenerarNotificacionSistema(RenderTypeControlButtonDto oDocument)
        {
            var template = _sendMailNotificationServices.GetMergeTemplate(oDocument);
            if(template == null)return;

            var cliente = "";// _sqlPedidosServices.RetornarNombreClienteByIdPedido(oDocument.IdDocument);

            var oNotify = _reclamosDomainServices.GenerarEntradaNotificadorSistema(
                                       _notificacionesSistemaRepository.NewEntity(),
                                       Convert.ToInt32(oDocument.IdCurrentResponsibe),
                                       template, cliente);

            _notificacionesSistemaRepository.Add(oNotify);
        }


        #region IDisposable Members

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            //release used unit of work
            //if you have many repositories but  lifetime is per resolve only need
            //dispose one

            if (_tblModuloWorkFlowRutasRepository != null)
            {
                _tblModuloWorkFlowRutasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    