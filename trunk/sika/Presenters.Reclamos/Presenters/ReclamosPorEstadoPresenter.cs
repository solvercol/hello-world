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
    public class ReclamosPorEstadoPresenter : Presenter<IReclamosPorEstadoView>
    {
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;
        readonly IReclamosAdoService _recladoAdoService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;

        public ReclamosPorEstadoPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService
                                            ,IReclamosAdoService recladoAdoService, ISfTBL_Admin_OptionListManagementServices optionListService)
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
            LoadInitDate();
            LoadReport();
        }

        #region Methods

        void LoadInitView()
        {
            View.FechaFilterTo = new DateTime(DateTime.Now.Year, 12, 31);
            CheckRegiterReclamo();
        }

        void LoadInitDate()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("FechaInitVistas", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.FechaFilterFrom = Convert.ToDateTime(op.Value);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
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
                var dt = _recladoAdoService.GetVistaReclamosPorEstado(View.FechaFilterFrom, View.FechaFilterTo, View.ServerHostPath, View.IdModule
                                                                      , View.FilterNoReclamo, View.FilterCliente, View.FilterProducto, View.FilterServicio);
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