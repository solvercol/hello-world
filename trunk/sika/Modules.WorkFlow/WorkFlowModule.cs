using Application.Core;
using Applications.MainModule.Admin.IServices;
using Applications.MainModule.WorkFlow.DTO;
using Applications.MainModule.WorkFlow.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Modules.WorkFlow
{
    public class WorkFlowModule : ModuleBase
    {
        private readonly ISfTBL_ModuloWorkFlow_RutasManagementServices _rutasServices;
        private readonly ISfTBL_Admin_EstadosProcesoManagementServices _estadosService;
        private readonly ISfTBL_ModuloWorkFlow_CamposValidacionManagementServices _camposServies;

        public WorkFlowModule(ISfTBL_ModuloWorkFlow_RutasManagementServices rutasServices,
            ISfTBL_Admin_EstadosProcesoManagementServices estadosService, 
            ISfTBL_ModuloWorkFlow_CamposValidacionManagementServices camposServies)
        {
            _rutasServices = rutasServices;
            _camposServies = camposServies;
            _estadosService = estadosService;
        }

        #region Servicios

        public ISfTBL_ModuloWorkFlow_RutasManagementServices RutasWorkFlowServices
        {
            get { return _rutasServices; }
        }

        public ISfTBL_Admin_EstadosProcesoManagementServices EstadosProcesoService
        {
            get { return _estadosService; }
        }

        public ISfTBL_ModuloWorkFlow_CamposValidacionManagementServices CamposRequeridosServices
        {
            get { return _camposServies; }
        }

        #endregion

        #region Reglas de Negocio

        public RenderTypeControlButtonDto EjecutarWorkFlow(RenderTypeControlButtonDto document)
        {
           return _rutasServices.EjecutarWorkFlow(document);
        }

        public RenderTypeControlButtonDto EjecutarWorkFlowModuloSolicitudes(RenderTypeControlButtonDto document)
        {
            return _rutasServices.EjecutarWorkFlowModuloSolicitudes(document);
        }

        
        public RenderTypeControlButtonDto CargarWorkFlow(string idDocumento, ModulosAplicacion module)
        {
           return  _rutasServices.CargarWorkFlow(idDocumento, module);
        }

        public RenderTypeControlButtonDto ActualizarIngenieroResponsable(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.ActualizarIngenieroResponsable(oDocument);
        }

        public RenderTypeControlButtonDto CategorizarReclamo(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.CategorizarReclamo(oDocument);
        }

        public RenderTypeControlButtonDto DevolverReclamo(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.DevolverReclamo(oDocument);
        }

        public RenderTypeControlButtonDto CancelarReclamo(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.CancelarReclamo(oDocument);
        }

        public RenderTypeControlButtonDto CambiarIngeniero(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.CambiarIngenieroResponsable(oDocument);
        }

        #region Solicitudes APC

        public RenderTypeControlButtonDto AsignarresponsableSolicitud(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.AsignarResponsableSolicitud(oDocument);
        }

        public RenderTypeControlButtonDto EnviarActividadesSolicitud(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.EnviarActividadesSolicitud(oDocument);
        }

        public RenderTypeControlButtonDto CerrarAccion(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.CerrarAccion(oDocument);
        }

        #endregion

      
        #endregion

    }
}