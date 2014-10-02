using System;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class SeguimientoPresenter : Presenter<ISeguimientoView>
    {
        private readonly ISfTBL_ModuloReclamos_TrackingManagementServices _trackingServices;

        public SeguimientoPresenter(ISfTBL_ModuloReclamos_TrackingManagementServices trackingServices)
        {
            _trackingServices = trackingServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.RefreshEvent += ViewRefreshEvent;
        }

        void ViewRefreshEvent(object sender, EventArgs e)
        {
            GetAll();
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsLoadUserControl) return;
            GetAll();
            View.IsLoadUserControl = true;
        }

        private void GetAll()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdReclamo)) return;
                var list = _trackingServices.ListadotrackingByIdreclamo(Convert.ToInt32(View.IdReclamo));
                View.ListadoSeguimiento(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}