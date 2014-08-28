using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModule.Reclamos.Enum;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class ReclamosPorEstadoPresenter : Presenter<IReclamosPorEstadoView>
    {
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;
        readonly IReclamosAdoService _recladoAdoService;

        public ReclamosPorEstadoPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService
                                            ,IReclamosAdoService recladoAdoService)
        {
            _categoriasReclamoService = categoriasReclamoService;
            _recladoAdoService = recladoAdoService;
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
            View.FechaFilterFrom = DateTime.Now.AddMonths(-1);
            View.FechaFilterTo = DateTime.Now.AddMonths(1);
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