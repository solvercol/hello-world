using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.DefaultPresenter;
using ServerControls;

namespace ASP.NETCLIENTE.Pages.Globals
{
    public partial class FrmUsuarios :Page// ViewPage<AdminUserPresenter, IAdminUsersView>, IAdminUsersView
    {
        public event EventHandler FilterVEvent;

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInitialize();
            if (IsPostBack) return;
            //ImprimirTituloVentana("Administración De Usuarios.");
        }

        private void LoadInitialize()
        {
            //Presenter.MessageBox += PresenterMessageBox;
            rptUsers.ItemCommand += RptUsersItemCommand;
        }

        #endregion

        #region Eventos

        void PresenterMessageBox(object sender, MessageBoxEventArgs e)
        {
            //switch (e.Tipo)
            //{
            //    case TypeError.Ok:
            //        ShowMessageOk(e.Message);
            //        break;
            //    default:
            //        ShowError(e.Message);
            //        break;
            //}
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {

            if (FilterVEvent == null) return;
            FilterVEvent(null, EventArgs.Empty);
        }

        protected void RptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //var user = e.Item.DataItem as TBL_Maestra_Usuarios;
            //if (user == null) return;
            //var lblLastLogin = e.Item.FindControl("lblLastLogin") as Label;
            //if (user.ModifiedOn != null)
            //{
            //    if (lblLastLogin != null)
            //        lblLastLogin.Text = user.ModifiedOn.GetValueOrDefault().ToLongDateString();
            //}

            //var chk = e.Item.FindControl("ckhActivo") as CheckBox;
            //if (chk != null)
            //{
            //    chk.Checked = user.Activo;
            //    chk.Enabled = false;
            //}

            //var hplEdit = (LinkButton)e.Item.FindControl("lnkEdit");
            //hplEdit.CommandName = "Edit";
            //hplEdit.CommandArgument = user.IdUsuario.ToString();
            //if (user.NombreUsuario.Equals(Context.User.Identity.Name))
            //    hplEdit.Visible = false;

        }

        void RptUsersItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName.Equals("Edit"))
            {
                Response.Redirect(String.Format("~/Pages/Globals/FrmEditUsuarios.aspx?search={0}", e.CommandArgument));
            }
        }

        protected void PgrUsersPageChanged(object sender, PageChangedEventArgs e)
        {
            if (FilterVEvent == null) return;
            FilterVEvent(e.CurrentPage, EventArgs.Empty);
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditUsuarios.aspx"));
        }

        #endregion

        #region Members

        //public void GetAll(IEnumerable<TBL_Maestra_Usuarios> items)
        //{
        //    rptUsers.DataSource = items;
        //    rptUsers.DataBind();
        //}

        public string UserName
        {
            get { return txtFiltroUsuario.Text; }
            set { txtFiltroUsuario.Text = value; }
        }

        public string Nombres
        {
            get { return txtFiltroNombres.Text; }
            set { txtFiltroNombres.Text = value; }
        }

        #endregion
    }
}