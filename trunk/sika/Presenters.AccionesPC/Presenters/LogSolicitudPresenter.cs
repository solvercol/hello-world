using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;

namespace Presenters.AccionesPC.Presenters
{
    public class LogSolicitudPresenter : Presenter<ILogSolicitudesView>
    {
        private readonly ISfTBL_ModuloAPC_LogSolicitudManagementServices _log;

        public LogSolicitudPresenter(ISfTBL_ModuloAPC_LogSolicitudManagementServices log)
        {
            _log = log;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(sender == null ? 0 : Convert.ToInt32(sender));
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAll(0);
        }

        private void GetAll(int currentePage)
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;
            try
            {
                var total = _log.CountByIdSolicitud(Convert.ToInt32(View.IdSolicitud));
                View.TotalRegistrosPaginador = total == 0 ? 1 : total;
                var lista = _log.LisadoByIdSolicitud(Convert.ToInt32(View.IdSolicitud), currentePage, View.PageSize);
                View.LogsList(lista);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}