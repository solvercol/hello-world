using System;
using System.Web.UI;
using Infrastructure.CrossCutting;
using log4net;

namespace ASP.NETCLIENTE.UI
{
    public class BaseUserControl : UserControl, IBaseUserControl
    {

       

        #region Log

        private const string ApplicationName = "Empacor";

        private static readonly ILog Logger = LogManager.GetLogger(ApplicationName);

        protected void LogError(string metodo, string user, Uri url, Exception ex)
        {
            SetOptionalParametersOnLogger(user, url);
            LogError(metodo, ex);
        }

        private static void SetOptionalParametersOnLogger(string user, Uri url)
        {
            if (user != null)
            {
                MDC.Set("user", user);
            }
            MDC.Set("url", url.ToString());
        }

        private static void LogError(string message, Exception ex)
        {
            if (ex == null) return;

            if (Logger.IsErrorEnabled)
            {
                Logger.Error(message, ex.InnerException ?? ex);
            }
        }

        public void LogInfo(string message, LogType typeLog)
        {
            ILog logger = null;
            logger = LogManager.GetLogger(typeLog == LogType.Notify
                                          ? LogType.Notify.ToString()
                                          : LogType.General.ToString());

            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }
        #endregion

        public string NumeroCotizacion
        {
            get;
            set;
        }

        //public string IdBaseCompany
        //{
        //    get;
        //    set;
        //}

        //public string IdBaseOrden
        //{
        //    get;
        //    set;
        //}

        //public string IdPartner
        //{
        //    get;
        //    set;
        //}

        //public string IdManager
        //{
        //    get;
        //    set;
        //}

        //public string DocumentName
        //{
        //    get;
        //    set;
        //}
        ///// <summary>
        ///// Valida si es necesario validar el evento SAVE de la Seccion
        ///// </summary>
        //public bool ValidaSeccion { get; set; }

        //public event EventHandler<InvoiceResultEventArgs> NextEvent;

        //public event EventHandler<InvoiceResultEventArgs> PreviusEvent;

        //public event EventHandler<InvoiceResultEventArgs> CloseWizardEvent;

        //public event EventHandler<InvoiceResultEventArgs> ReloadEvent;

        //public event EventHandler<InvoiceResultEventArgs> SaveSectionEvent;

        ///// <summary>
        ///// Invoca el evento que pasa el Id del Invoice en el asistente
        ///// </summary>
        ///// <param name="e"></param>
        //protected void ReturnInvoiceIdNext(InvoiceResultEventArgs e)
        //{
        //    var handler = NextEvent;
        //    if (handler != null) handler(this, e);
        //}

        //protected void ReturnInvoiceIdPrevius(InvoiceResultEventArgs e)
        //{
        //    var handler = PreviusEvent;
        //    if (handler != null) handler(this, e);
        //}

        //protected void CloseWizard(InvoiceResultEventArgs e)
        //{
        //    var handler = CloseWizardEvent;
        //    if (handler != null) handler(this, e);
        //}

        //protected void Reload(InvoiceResultEventArgs e)
        //{
        //    var handler = ReloadEvent;
        //    if (handler != null) handler(this, e);
        //}

        //protected void SaveSection(InvoiceResultEventArgs e)
        //{
        //    var handler = SaveSectionEvent;
        //    if (handler != null) handler(this, e);
        //}
    }
}