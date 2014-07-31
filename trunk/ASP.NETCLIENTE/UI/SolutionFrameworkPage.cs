
using System;
using Infraestructure.CrossCutting.Security.IServices;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.NetFramework.Enums;
using log4net;
using Microsoft.Practices.Unity;

namespace ASP.NETCLIENTE.UI
{
    public class SolutionFrameworkPage : System.Web.UI.Page
    {
        private const string ApplicationName = "Empacor";
        private readonly ILogServices _logPedidosServices;
        private static readonly ILog Logger = LogManager.GetLogger(ApplicationName);

        private readonly IUnityContainer _container;

        protected IUnityContainer Container
        {
            get { return _container; }
        }

        protected SolutionFrameworkPage()
        {
            _container = IoC.Container;
            _logPedidosServices = IoC.Resolve<ILogServices>();
        }

        #region Log

        protected static void LogError(string metodo, string user, Uri url, Exception ex)
        {
            SetOptionalParametersOnLogger(user, url);
            LogError(metodo, ex);
        }

        protected  void LogError(int idPedido,int idHistorial, string userName, Acciones accion)
        {
            _logPedidosServices.CrearEntradaLogPedidos(idPedido, idHistorial, userName, accion);
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
    }
}