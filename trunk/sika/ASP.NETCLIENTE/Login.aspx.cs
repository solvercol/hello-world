using System;
using System.Web.Security;
using System.Web.UI;
using ASP.NETCLIENTE.HTTPModules;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;
using System.Configuration;

namespace ASP.NETCLIENTE
{
    public partial class Login : Page
    {
       
        private ITraceManager _traceManager;

        protected override void OnInit(EventArgs e)
        {
            _traceManager = IoC.Resolve<ITraceManager>();
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Test();
        }

        private void AutenticarUsuario()
        {
            try
            {
                var userWc = ConfigurationManager.AppSettings.Get("UsuarioAplicacion");
                var am = (AuthenticationModule)Context.ApplicationInstance.Modules["AuthenticationModule"];
                if (am == null)
                {
                    lblError.Text = @"Error de lectura del módulo de autenticación.";
                    return;
                }

                if (am.AuthenticateUser(txtUsername.Text,txtPassword.Text))
               // if (am.AuthenticateUser(userWc))
                {
                    Context.Response.Redirect(FormsAuthentication.GetRedirectUrl(User.Identity.Name, false));                    
                }
                else
                {
                    lblError.Text = @"Nombre de usuario o contraseña incorrecto.";
                    lblError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblError.Text = @"Error Inesperado.";
                _traceManager.LogInfo(ex.Message,LogType.Notify);
            }
           
        }

        void Test()
        {
            var userApplication = "";
            var userWc = ConfigurationManager.AppSettings.Get("UsuarioAplicacion");
            var tipoAutenticacion = ConfigurationManager.AppSettings.Get("tipoAutenticacion");
            if (tipoAutenticacion.Equals("1"))
            {
                if (Context.User.Identity.Name.Contains(@"\"))
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : Context.User.Identity.Name.Split('\\')[1];
                }
                else
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : Context.User.Identity.Name;
                }
            }
            else
            {
                if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains(@"\"))
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                }
                else
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                }
            }

            Response.Write(userApplication);
        }

        private void AutenticarUsuarioWinIDentity()
        {

            var am = (AuthenticationModule)Context.ApplicationInstance.Modules["AuthenticationModule"];
            var userApplication = "";

            var userWc = ConfigurationManager.AppSettings.Get("UsuarioAplicacion");
            var tipoAutenticacion = ConfigurationManager.AppSettings.Get("tipoAutenticacion");
            if (tipoAutenticacion.Equals("1"))
            {
                if (Context.User.Identity.Name.Contains(@"\"))
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : Context.User.Identity.Name.Split('\\')[1];
                }
                else
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : Context.User.Identity.Name;
                }
            }
            else
            {
                if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains(@"\"))
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                }
                else
                {
                    userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                }
            }

            Response.Write(userApplication);

            if (string.IsNullOrEmpty(userApplication))
            {
                lblError.Text = @"Error de lectura del usuario de windows. El parametro es nulo.";
                lblError.Visible = true;
                return;
            }

            if (am.AuthenticateUser(userApplication))
            {
                Context.Response.Redirect(FormsAuthentication.GetRedirectUrl(User.Identity.Name, false));
            }
            else
            {
                lblError.Text = @"Nombre de usuario incorrecto.";
                lblError.Visible = true;
            }

        }

        protected void BtnLoginClick(object sender, EventArgs e)
        {
            AutenticarUsuario();
        }
    }
}