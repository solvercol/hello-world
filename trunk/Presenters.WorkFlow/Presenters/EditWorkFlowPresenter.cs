using System;
using System.Reflection;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.WorkFlow.IViews;

namespace Presenters.WorkFlow.Presenters
{
    public class EditWorkFlowPresenter : Presenter<IAdminWorkFlowView>
    {
        private readonly ISfTBL_Admin_EstadosProcesoManagementServices _estadosService;
        private readonly ISfTBL_Admin_SeccionesManagementServices _seccionesServices;

        public EditWorkFlowPresenter(ISfTBL_Admin_EstadosProcesoManagementServices estadosService, 
            ISfTBL_Admin_SeccionesManagementServices seccionesServices)
        {
            _estadosService = estadosService;
            _seccionesServices = seccionesServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            CargarSecciones();
           
        }

        private void CargarSecciones()
        {
            try
            {
                if(string.IsNullOrEmpty(View.IdModule))return;
                var secciones = _seccionesServices.ListadoSeccionesPorModulo(Convert.ToInt32(View.IdModule));
                View.ListadoSecciones(secciones);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}