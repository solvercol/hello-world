using System;
using System.Globalization;
using System.Web;
using System.Web.Security;
using Applications.MainModule.Admin.IServices;
using Infraestructure.CrossCutting.Security.Security;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;
using System.Configuration;

namespace ASP.NETCLIENTE.HTTPModules
{
    public class AuthenticationModule : IHttpModule
    {
        #region Members

        string AutenticationType = ConfigurationManager.AppSettings.Get("tipoAutenticacion");

        #endregion

        private ITraceManager _traceManager;
        private ISfTBL_Admin_UsuariosManagementServices _iAutentication;

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += ContextAuthenticateRequest;
            //context.AuthorizeRequest += OnAuthorizeRequest;
            _iAutentication = IoC.Resolve<ISfTBL_Admin_UsuariosManagementServices>();
            _traceManager = IoC.Resolve<ITraceManager>();
        }

        //void OnAuthorizeRequest(object sender, EventArgs args)
        //{
        //    var app = (HttpApplication)sender;
        //    var context = app.Context;
        //    if (context.SkipAuthorization)
        //        return;

        //    if (!context.Request.FilePath.EndsWith(".aspx") )
        //        return;

        //    if (context.Request.HttpMethod == "GET")
        //    {
        //        if (HttpContext.Current.User != null)
        //        {
        //            //Si el usuario esta Autenticado 
        //            if (HttpContext.Current.User.Identity.IsAuthenticated)
        //            {
        //                if (HttpContext.Current.User is SolutionFrameworkPrincipal)
        //                {
        //                    //Se verifica si el Perfil del usuario tiene autorizaci�n para acceder a la p�gina 
        //                    if (!_iAutentication.ValidarAutorizacion(
        //                            HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath))
        //                        HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=401");
        //                }
        //            }
        //        }
        //    }
        //}


        private void ContextAuthenticateRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
            {
                if (AutenticationType == "1")
                {
                    //_traceManager.LogInfo("ContextAuthenticateRequest : " + app.Context.User.Identity.Name,
                    //                        LogType.Notify);
                    if (string.IsNullOrEmpty(app.Context.User.Identity.Name)) return;
                    if (IsNumeric(app.Context.User.Identity.Name))
                    {
                        var userId = app.Context.User.Identity.Name;
                        //var userId = app.Context.User.Identity.Name;
                        var solutionFrameworkUser = _iAutentication.FindById(Convert.ToInt32(userId));
                        if (solutionFrameworkUser != null)
                        {
                            solutionFrameworkUser.IsAuthenticated = true;
                            app.Context.User = new SolutionFrameworkPrincipal(solutionFrameworkUser);
                        }
                        else
                        {
                            FormsAuthentication.SignOut();
                            HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                        }
                    }
                }
            }

            if (AutenticationType == "0")
            {
                var userWc = ConfigurationManager.AppSettings.Get("UsuarioAplicacion");
                var userApplication = "";
                if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains(@"\"))
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                }
                else
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                }

                AuthenticateUser(userApplication);
            }
        }

        private static bool IsNumeric(object val)
        {
            try
            {
                Convert.ToInt32(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AuthenticateUser(string username, string password)
        {

            try
            {
                var user = _iAutentication.GetUserByCredential(username.Trim(), password.Trim());
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
                                        "El usuario {0} intento ingresar estando inactivo.",
                                        username),
                                        LogType.Notify);
                        HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                    }
                    user.IsAuthenticated = true;
                    var currentIp = HttpContext.Current.Request.UserHostAddress;
                    user.lastlogin = DateTime.Now;
                    user.lastip = currentIp;
                    // Save login date and IP
                    _iAutentication.Modify(user);
                    // Create the authentication ticket
                    HttpContext.Current.User = new SolutionFrameworkPrincipal(user);                    
                    if (HttpContext.Current.Session != null)
                        HttpContext.Current.Session["Main_AuthenticatedUser"] = user;
                    FormsAuthentication.SetAuthCookie(user.IdUser.ToString(), true);

                    return true;
                }
                _traceManager.LogInfo(String.Format("Nombre de Usuario no v�lido: {0}.", username), LogType.Notify);
                HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                return false;
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(String.Format("Error T�cnico Modulo de Autenticaci�n:  '{0}': " + ex.Message), LogType.Notify);
                return false;
            }

           
        }

        public bool AuthenticateUser(string codigo)
        {

            try
            {
                var user = _iAutentication.GetUserByCredential(codigo);
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
                                        "El usuario {0} intento ingresar estando inactivo.",
                                        user.Nombres),
                                        LogType.Notify);
                        HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                    }
                    user.IsAuthenticated = true;
                    var currentIp = HttpContext.Current.Request.UserHostAddress;
                    user.lastlogin = DateTime.Now;
                    user.lastip = currentIp;
                    // Save login date and IP
                    _iAutentication.Modify(user);
                    // Create the authentication ticket
                    HttpContext.Current.User = new SolutionFrameworkPrincipal(user);
                    if (HttpContext.Current.Session != null)
                        HttpContext.Current.Session["Main_AuthenticatedUser"] = user;
                    FormsAuthentication.SetAuthCookie(user.IdUser.ToString(), true);

                    return true;
                }
                _traceManager.LogInfo(String.Format("C�digo de Usuario no v�lido: {0}.", codigo), LogType.Notify);
                HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                return false;
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(String.Format("Error T�cnico Modulo de Autenticaci�n:  '{0}': " + ex.Message), LogType.Notify);
                return false;
            }

            return false;
        }

        /// <summary>
        /// Log out the current user.
        /// </summary>
        public void Logout()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }

        public void Dispose()
        {
           
        }
    }
}