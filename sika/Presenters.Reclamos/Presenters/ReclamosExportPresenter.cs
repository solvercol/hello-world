using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModule.Reclamos.Enum;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Applications.MainModule.Admin.IServices;

namespace Presenters.Reclamos.Presenters
{
    public class ReclamosExportPresenter : Presenter<IReclamosExportView>
    {
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;
        readonly IReclamosAdoService _recladoAdoService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;

        public ReclamosExportPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService
                                            , IReclamosAdoService recladoAdoService, ISfTBL_Admin_OptionListManagementServices optionListService)
        {
            _categoriasReclamoService = categoriasReclamoService;
            _recladoAdoService = recladoAdoService;
            _optionListService = optionListService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadCategoriasReclamo();
            LoadInitView();
            LoadReport();
        }

        #region Methods

        void LoadInitView()
        {
            View.FechaFilterFrom = new DateTime(DateTime.Now.Year, 1, 1);
            View.FechaFilterTo = new DateTime(DateTime.Now.Year, 12, 31);
            CheckRegiterReclamo();
        }

        void CheckRegiterReclamo()
        {
            try
            {
                var idRolRegistro = _optionListService.ObtenerOpcionBykeyModuleId("IdRolRegistroReclamo", Convert.ToInt32(View.IdModule));

                if (idRolRegistro != null)
                {
                    var idRol = Convert.ToInt32(idRolRegistro.Value);

                    View.CanRegister = View.UserSession.IsInRoleId(idRol);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadReport()
        {
            try
            {
                var dt = _recladoAdoService.GetVistaReclamosExport(View.ServerHostPath, View.IdModule, View.UserSession.IdUser, View.FechaFilterFrom, View.FechaFilterTo
                                                                    , View.FilterNoReclamo, View.FilterNoRelacion, View.FilterCliente, View.FilterProducto, View.FilterServicio, "recexport");
                View.LoadViewReclamos(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadCategoriasReclamo()
        {
            try
            {
                var categories = _categoriasReclamoService.GetByTipoReclamo(TipoReclamo.Servicio);
                View.LoadCategoriasReclamo(categories);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #endregion
    }
}