using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.DefaultPresenter;
using ServerControls;

namespace ASP.NETCLIENTE.Pages.Globals
{
    public partial class FrmRoles : ViewPage<RolePresenter, IRoleView>, IRoleView
    {
        public event EventHandler FilterVEvent;

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInitialize();
            if (IsPostBack) return;
            ImprimirTituloVentana("Administración De Roles.");
        }

        private void LoadInitialize()
        {
            //Presenter.MessageBox += PresenterMessageBox;
            rptListado.ItemCommand += RptListadoItemCommand;
        }

        #endregion

        #region Eventos

        protected void PgrListadoPageChanged(object sender, PageChangedEventArgs e)
        {
            if (FilterVEvent == null) return;
            FilterVEvent(null, EventArgs.Empty);
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var role = e.Item.DataItem as TBL_Maestra_Roles;
            if (role == null) return;
            var chk = e.Item.FindControl("ckhGlobal") as CheckBox;
            if (chk != null)
            {
                chk.Checked = role.Activo;
                chk.Enabled = false;
            }

            var label = e.Item.FindControl("lblDate") as Label;
            if (label != null)
            {
                label.Text = role.CreateOn.GetValueOrDefault().ToLongDateString();
            }

            var hplEdit = (LinkButton)e.Item.FindControl("lnkEdit");
            hplEdit.CommandName = "Edit";
            hplEdit.CommandArgument = role.IdRol.ToString();

            //if (role.name == "Administrator")
            hplEdit.Visible = false;
        }

        void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                Response.Redirect(String.Format("~/Pages/Globals/FrmEditRoles.aspx?search={0}", e.CommandArgument));
            }
        }

        
        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditRoles.aspx"));
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            if (FilterVEvent == null) return;
            FilterVEvent(null, EventArgs.Empty);
        }

        #endregion

        #region Members

        public void GetAll(IEnumerable<TBL_Maestra_Roles> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        public string Role
        {
            get { return txtFiltroNombres.Text; }
            set { txtFiltroNombres.Text = value; }
        }

        #endregion

    }
}