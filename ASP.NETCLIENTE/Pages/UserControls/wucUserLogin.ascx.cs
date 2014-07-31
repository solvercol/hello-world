using System;
using ASP.NETCLIENTE.HTTPModules;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IView;
using Presenters.Admin.Presenters;


namespace ASP.NETCLIENTE.Pages.UserControls
{
    public partial class wucUserLogin : ViewUserControl<LogoutPresenter, ILogoutView>, ILogoutView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

       
        
        public string User
        {
            set { litUser.Text = value; }
        }

        public string Role
        {
            set { imgRol.ToolTip = value; }
        }

        protected void LoginStatusOnLoggedOut(object sender, EventArgs e)
        {
            var am = (AuthenticationModule)Context.ApplicationInstance.Modules["AuthenticationModule"];
            am.Logout();
            Context.Response.Redirect(Context.Request.RawUrl);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }
    }
}