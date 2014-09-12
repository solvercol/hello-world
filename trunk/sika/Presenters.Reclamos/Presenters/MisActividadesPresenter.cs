using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class MisActividadesPresenter : Presenter<IMisActividadesView>
    {
        readonly IReclamosAdoService _recladoAdoService;

        public MisActividadesPresenter(IReclamosAdoService recladoAdoService)
        {
            _recladoAdoService = recladoAdoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
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
                var dt = _recladoAdoService.GetVistaMisActividades(View.ServerHostPath, View.IdModule, View.UserSession.IdUser
                                                                    , View.FechaFilterFrom, View.FechaFilterTo, View.FilterNoReclamo, View.FilterCliente, View.FilterProducto, View.FilterServicio);
                View.LoadView(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #endregion
    }
}