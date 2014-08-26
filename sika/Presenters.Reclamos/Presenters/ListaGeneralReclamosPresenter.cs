using System;
using Application.Core;
using Presenters.Reclamos.IViews;
using Application.MainModule.Reclamos.IServices;
using System.Reflection;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Domain.MainModule.Reclamos.Enum;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Reclamos.Presenters
{
    public class ListaGeneralReclamosPresenter : Presenter<IListaGeneralReclamosView>
    {
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;
        readonly IReclamosAdoService _recladoAdoService;

        public ListaGeneralReclamosPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService
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
                var dt = _recladoAdoService.GetVistaGeneralReclamos(View.FechaFilterFrom, View.FechaFilterTo, View.ServerHostPath, View.IdModule);
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