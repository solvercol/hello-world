using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using ServerControls;



namespace Modules.Admin.Catalogos
{
    public partial class FrmAdminUsers : ViewPage<UserListPresenter,IUsersListView> , IUsersListView
    {
        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de usuarios");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddUser.aspx{0}", GetBaseQueryString()));
        }

        public void GetUsers(List<TBL_Admin_Usuarios> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        public string ModuleSetupId
        {
            get { return ViewState["ModuleSetupId"] == null ? string.Empty : ViewState["ModuleSetupId"].ToString(); }
            set { ViewState["ModuleSetupId"] = value; }
        }

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect(string.Format("FrmViewUser.aspx{0}&UserId={1}", GetBaseQueryString(), e.CommandArgument));
        }


        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var User = e.Item.DataItem as TBL_Admin_Usuarios;

            if (User == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = User.IdUser.ToString();
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = User.IsActive;
            }

            var litUseCode = e.Item.FindControl("litUseCode") as Literal;
            if (litUseCode != null)
            {
                litUseCode.Text = User == null ? string.Empty : User.CodigoUser;
            }

            var litName = e.Item.FindControl("litName") as Literal;
            if (litName != null)
            {
                litName.Text = User == null ? string.Empty : User.Nombres;
            }

            var litUsername = e.Item.FindControl("litUsername") as Literal;
            if (litUsername != null)
            {
                litUsername.Text = User == null ? string.Empty : User.UserName;
            }

            var litEmail = e.Item.FindControl("litEmail") as Literal;
            if (litEmail != null)
            {
                litEmail.Text = User == null ? string.Empty : User.Email;
            }
        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}