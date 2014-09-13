using Application.Core;
using Applications.MainModule.Admin.IServices;
using Applications.MainModule.WorkFlow.DTO;
using Applications.MainModule.WorkFlow.IServices;

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

        public RenderTypeControlButtonDto CargarWorkFlow(string idDocumento)
        {
           return  _rutasServices.CargarWorkFlow(idDocumento);
        }

        public RenderTypeControlButtonDto ActualizarIngenieroResponsable(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.ActualizarIngenieroResponsable(oDocument);
        }

        public RenderTypeControlButtonDto CategorizarReclamo(RenderTypeControlButtonDto oDocument)
        {
            return _rutasServices.CategorizarReclamo(oDocument);
        }

        #endregion

    }
}