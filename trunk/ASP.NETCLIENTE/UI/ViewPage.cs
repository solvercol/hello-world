using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Core;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Microsoft.Practices.Unity;
using Infrastructure.CrossCutting.NetFramework.Util;
using Domain.MainModules.Entities;

namespace ASP.NETCLIENTE.UI
{
    public class ViewPage<TPresenter, TView> : SolutionFrameworkPage
        where TPresenter : Presenter<TView>
        where TView : class
    {

        #region Properties

        public event EventHandler HideControlsevent;

        public void InvokeHideControlsevent(EventArgs e)
        {
            var handler = HideControlsevent;
            if (handler != null) handler(this, e);
        }

        private string ModuleId
        {
            get { return Request.QueryString["ModuleId"]; }
        }

        #endregion

        #region Constructor

        protected ViewPage()
        {
            var globalizacion = ConfigurationManager.AppSettings.Get("Globalizacion");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(string.IsNullOrEmpty(globalizacion) ? "es-CO" : globalizacion);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(string.IsNullOrEmpty(globalizacion) ? "es-CO" : globalizacion);
            Presenter = Container.Resolve<TPresenter>();
            Presenter.View = this as TView;
            Presenter.SubscribeViewToEvents();
            Presenter.MessageBox += PresenterMessageBox;
            Presenter.LogProcesamientoEvent += PresenterLogProcesamientoEvent;
        }

        void PresenterLogProcesamientoEvent(object sender, LogProcesamientoEventArgs e)
        {
            if (e.TipoLog == Logtype.Archivo)
                LogError(e.NombreMetodo, AuthenticatedUser.Nombres, new Uri(GetUrl, UriKind.RelativeOrAbsolute), e.Error);
            else
            {
                if (string.IsNullOrEmpty(e.IdDocumento) || string.IsNullOrEmpty(e.IdHistorial)) return;
                LogError(Convert.ToInt32(e.IdDocumento), Convert.ToInt32(e.IdHistorial), AuthenticatedUser.Nombres, e.LogAccion);
            }
        }


        void PresenterMessageBox(object sender, MessageBoxEventArgs e)
        {
            switch (e.Tipo)
            {
                case TypeError.Ok:
                    ShowMessageOk(e.Message);
                    InvokeHideControlsevent(EventArgs.Empty);
                    break;
                default:
                    ShowError(e.Message);
                    break;
            }
        }

        #endregion

        #region Members

        protected string IsEdit
        {
            get { return Request.QueryString["IsEdit"]; }
        }

        protected string FormRequest
        {
            get { return Request.QueryString["Form"]; }
        }

        protected TBL_Admin_Usuarios AuthenticatedUser
        {
            get { return ((TBL_Admin_Usuarios)HttpContext.Current.User.Identity); }
        }

        protected string GetBaseQueryString()
        {
            return String.Format("?ModuleId={0}", ModuleId);
        }

        protected static TPresenter Presenter { get; set; }

        private Literal TitulosVentana
        {
            get { return Master != null ? Master.FindControl("PageTitleLabel") as Literal : null; }
        }

        /// <summary>
        /// The messagebox.
        /// </summary>
        private HtmlGenericControl MessageBox
        {
            get { return Master != null ? Master.FindControl("MessageBox") as HtmlGenericControl : null; }
        }

       
        #endregion

        #region Metodos


        /// <summary>
        /// Try to find the MessageBox control, insert the errortext and set visibility to true.
        /// </summary>
        /// <param name="errorText"></param>
        protected void ShowError(string errorText)
        {
            if (MessageBox == null)
            {
                throw new Exception(errorText);
            }
            
            MessageBox.InnerHtml = errorText;
            MessageBox.Attributes["class"] = "errorbox";
            MessageBox.Visible = true;

        }

        /// <summary>
        /// Try to find the MessageBox control, insert the message and set visibility to true.
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessageOk(string message)
        {

            if (MessageBox == null) return;
            MessageBox.InnerHtml = message;
            MessageBox.Attributes["class"] = "messagebox";
            MessageBox.Visible = true;

        }

        /// <summary>
        /// Show the message of the exception, and the messages of the inner exceptions.
        /// </summary>
        /// <param name="exception"></param>
        protected void ShowException(Exception exception)
        {
            var exceptionMessage = "<p>" + exception.Message + "</p>";
            var innerException = exception.InnerException;
            while (innerException != null)
            {
                exceptionMessage += "<p>" + innerException.Message + "</p>";
                innerException = innerException.InnerException;
            }
            ShowError(exceptionMessage);
        }



       

        /// <summary>
        /// Try to find the MessageBox control, insert the message and set visibility to true.
        /// </summary>

        public void LimpiarMensajes()
        {
            if (MessageBox == null) return;
            MessageBox.InnerHtml = "";
            MessageBox.Visible = false;
        }

        /// <summary>
        /// The messagebox.
        /// </summary>
        public void ImprimirTituloVentana(string strTitulo)
        {

            if (TitulosVentana == null)
            {
                // Throw an Exception and hope it will be handled by the global application exception handler.
                //throw new Exception(strTitulo);
                return;
            }
            TitulosVentana.Text = strTitulo;
            //   TitulosVentana.Attributes["class"] = "TituloVentana";
            TitulosVentana.Visible = true;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        protected void RegistrarControlScriptManager(Control ctrl)
        {
            if (Master == null) return;
            var sm = Master.FindControl("smGeneral") as ScriptManager;
            if (sm == null) return;
            sm.RegisterPostBackControl(ctrl);
        }

        protected void RegistrarControlScriptManagerThisForm(Control ctrl)
        {
            var sm = FindControl("smGeneral") as ScriptManager;
            if (sm == null) return;
            sm.RegisterPostBackControl(ctrl);
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileDataStream"></param>
        /// <param name="fileLength"></param>
        /// <returns></returns>
        public static byte[] Serializar(Stream fileDataStream, int fileLength)
        {
            byte[] buffer2;
            new MemoryStream();
            try
            {
                var buffer = new byte[fileLength];
                fileDataStream.Read(buffer, 0, fileLength);
                buffer2 = buffer;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return buffer2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        protected static string ContenType(string extension)
        {
            var result = "";
            switch (extension)
            {
                case ".doc":
                    result = "application/ms-word";
                    break;
                case ".docx":
                    result = "application/ms-word";
                    break;
                case ".rtf":
                    result = "text/richtext";
                    break;
                case ".xls":
                    result = "application/vnd.xls";
                    break;
                case ".xlsx":
                    result = "application/vnd.xlsx";
                    break;
                case ".csv":
                    result = "application/CSV";
                    break;
                case ".pdf":
                    result = "application/pdf";
                    break;
                case ".ppt":
                    result = "application/vnd.ms-powerpoint";
                    break;
                case ".xml":
                    result = "text/xml";
                    break;
                case ".zip":
                    result = "application/x-zip-compressed";
                    break;
                case ".txt":
                    result = "text/plain";
                    break;
                case ".jpg":
                    result = "image/jpeg";
                    break;
                case ".tiff":
                    result = "image/tiff";
                    break;
                case ".png":
                    result = "image/png";
                    break;
                case ".html":
                    result = "Application/HTML";
                    break;
                default:
                    result = "text/plain";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Metodo que permite cifrar una cadena
        /// </summary>
        /// <param name="strCrypt"></param>
        /// <returns></returns>
        protected string Crypt(string strCrypt)
        {
            return string.IsNullOrEmpty(strCrypt) ? "" : Cryptography.Encrypt(strCrypt);
        }

        /// <summary>
        /// Metodo que permite descifrar una cadena
        /// </summary>
        /// <param name="strCrypt"></param>
        /// <returns></returns>
        protected string Decrypt(string strCrypt)
        {
            return string.IsNullOrEmpty(strCrypt) ? "" : Cryptography.Decrypt(strCrypt.Replace(" ", "+"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombrePlantilla"></param>
        /// <returns></returns>
        protected static string IconoPlantilla(string nombrePlantilla)
        {
            if (string.IsNullOrEmpty(nombrePlantilla)) return string.Empty;
            var fi = new FileInfo(nombrePlantilla);
            string rutaIcono;
            switch (fi.Extension.ToLower())
            {
                case ".doc":
                    rutaIcono = "~/Resources/images/doc.gif";
                    break;
                case ".docx":
                    rutaIcono = "~/Resources/images/doc.gif";
                    break;
                case ".rtf":
                    rutaIcono = "~/Resources/images/doc.gif";
                    break;
                case ".xls":
                    rutaIcono = "~/Resources/images/xls.gif";
                    break;
                case ".xlsx":
                    rutaIcono = "~/Resources/images/xls.gif";
                    break;
                case ".csv":
                    rutaIcono = "~/Resources/images/xls.gif";
                    break;
                case ".pdf":
                    rutaIcono = "~/Resources/images/pdf.gif";
                    break;
                case ".ppt":
                    rutaIcono = "~/Resources/images/ppt.gif";
                    break;
                case ".xml":
                    rutaIcono = "~/Resources/images/xml.gif";
                    break;
                case ".zip":
                    rutaIcono = "~/Resources/images/zip.gif";
                    break;
                case ".txt":
                    rutaIcono = "~/Resources/images/txt.gif";
                    break;
                default:
                    rutaIcono = "~/Resources/images/file.gif";
                    break;
            }
            return rutaIcono;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="fileName"></param>
        /// <param name="strType"></param>
        protected static void DownloadDocument(byte[] documento, string fileName, string strType)
        {

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = strType;
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(documento);
            HttpContext.Current.Response.End();


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool WriteByteArrayToFile(byte[] buff, string fileName)
        {
            var response = false;

            try
            {
                var fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                var bw = new BinaryWriter(fs);
                bw.Write(buff);
                bw.Close();
                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

      
        protected string TruncateString(string strCad, int num)
        {
            if (string.IsNullOrEmpty(strCad)) return string.Empty;
            if (strCad.Length <= num) return strCad;
            return strCad.Substring(0, num) + "...";
        }

        public  string GetUrl
        {
            get { return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath; }
    
        }

        #endregion


    }


    public static class PlaceExtensions
    {
        public static T GetUserControl<T>(this Page page, string name) where T : Control
        {
            var ph = page.GetType().GetField("phlContent", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
            if (ph == null) return null;
            var newph = (PlaceHolder)ph.GetValue(page);
            if (newph == null) return null;
            var field = newph.FindControl(name);
            if (field != null)
                return (T)field;

            return null;
        }
    }
}