using System;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;

namespace Presenters.AccionesPC.Presenters
{
    public class SeguimientoPresenter : Presenter<ISeguimientoView>
    {
        private readonly ISfTBL_ModuloAPC_TrackingManagementServices _seguimientoServices;

        public SeguimientoPresenter(ISfTBL_ModuloAPC_TrackingManagementServices seguimientoServices)
        {
            _seguimientoServices = seguimientoServices;
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
                if (string.IsNullOrEmpty(View.IdSolicitud)) return;
                var list = _seguimientoServices.ListadoByIdSolicitud(Convert.ToInt32(View.IdSolicitud));
                View.ListadoSeguimiento(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}