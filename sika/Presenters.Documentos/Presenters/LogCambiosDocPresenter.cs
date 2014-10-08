using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Application.Core;
using Application.MainModule.Documentos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Documentos.IViews;
using Applications.MainModule.Admin.IServices;

namespace Presenters.Documentos.Presenters
{
    public class LogCambiosDocPresenter
        : Presenter<ILogCambiosDocView>
    {
        private readonly ISfTBL_ModuloDocumentos_LogCambiosManagementServices _log;
        private readonly ISfTBL_Admin_OptionListManagementServices _optionListServices;

        public LogCambiosDocPresenter(ISfTBL_ModuloDocumentos_LogCambiosManagementServices log, ISfTBL_Admin_OptionListManagementServices optionListServices)
        {
            _log = log;
            _optionListServices = optionListServices;
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
            LoadOptionListConfigValues();
            GetAll(0);
        }

        void LoadOptionListConfigValues()
        {
            try
            {                
                var msg = _optionListServices.ObtenerOpcionBykeyModuleId("MensajeCopyright", Convert.ToInt32(View.IdModule));

                if (msg != null)
                {
                    View.MsgCopyright = msg.Value;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "LoadOptionListConfigValues"), TypeError.Error));
            }
        }

        private void GetAll(int currentePage)
        {
            if (View.IdDocumento == 0) return;
            try
            {
                //var total = _log.GetTotalLogReciboByReciboId(Convert.ToDecimal(View.IdReciboCaja));
                //View.TotalRegistrosPaginador = total == 0 ? 1 : total;
                var lista = _log.FindBySpec(true).FindAll(lg => lg.IdDocumento == View.IdDocumento).OrderByDescending(lg => lg.CreateOn).ToList();
                View.LogsList(lista);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}
