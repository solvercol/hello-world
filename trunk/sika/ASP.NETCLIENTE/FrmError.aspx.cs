using System;
using System.Configuration;
namespace ASP.NETCLIENTE
{
    public partial class FrmError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["error"] == null) return;
            switch (Request.QueryString["error"])
            {
                case "401":
                    lblErrorCode.Text = string.Format("Error de Acceso al Recurso Solicitado.");
                    lblTituloError.Text = string.Format("Acceso no Autorizado {0}", Request.QueryString["error"]);
                    break;

                case "402":
                    var userApplication = "";
                    var userWc = ConfigurationManager.AppSettings.Get("UsuarioAplicacion");

                    if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains(@"\"))
                    {
                        userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                    }
                    else
                    {
                        userApplication = !string.IsNullOrEmpty(userWc) ? userWc : System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    }

                    lblTituloError.Text = string.Format("Acceso no Autorizado");
                    lblErrorCode.Text = string.Format("El usuario [{0}] se encuentra inactivo en del sistema.", userApplication);
                    break;
            }

        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}