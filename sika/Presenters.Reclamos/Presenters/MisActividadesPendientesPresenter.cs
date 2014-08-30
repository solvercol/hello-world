using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class MisActividadesPendientesPresenter : Presenter<IMisActividadesPendientesView>
    {
        readonly IReclamosAdoService _recladoAdoService;

        public MisActividadesPendientesPresenter(IReclamosAdoService recladoAdoService)
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
           
            LoadReport();
        }

        #region Methods

        void LoadInitView()
        {
        }

        public void LoadReport()
        {
            try
            {
                var dt = _recladoAdoService.GetVistaMisActividadesPendientes(View.ServerHostPath, View.IdModule, View.UserSession.IdUser);
                View.LoadViewReclamos(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #endregion
    }
}