using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class LogReclamoViewPresenter : Presenter<ILogReclamoView>
    {
        private readonly ISfTBL_ModuloReclamos_LogReclamosManagementServices _log;

        public LogReclamoViewPresenter(ISfTBL_ModuloReclamos_LogReclamosManagementServices log)
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
            if (string.IsNullOrEmpty(View.IdReclamo)) return;
            try
            {
                //var total = _log.GetTotalLogReciboByReciboId(Convert.ToDecimal(View.IdReciboCaja));
                //View.TotalRegistrosPaginador = total == 0 ? 1 : total;
                var lista = _log.GetByIdReclamo(Convert.ToDecimal(View.IdReclamo)).OrderByDescending(x=> x.CreateOn).ToList();
                View.LogsList(lista);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}