using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using System.Linq;
using System.Collections;

namespace Modules.Admin.Catalogos
{
    public partial class FrmAddUser : ViewPage<AddUserPresenter, IAddUserView>, IAddUserView
    {
        public event EventHandler SaveEvent;

        #region load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Nuevo Usuario");
        }

        #endregion

        #region Events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminUsers.aspx{0}", GetBaseQueryString()));
        }

        public void RptRolesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var role = e.Item.DataItem as TBL_Admin_Roles;
            if (role == null) return;
            ViewState[e.Item.UniqueID] = role.IdRol;
        }

        #endregion

        #region Members

        public ArrayList GetSelectdRole()
        {
            var arrayList = new ArrayList();
            foreach (var roleId in from RepeaterItem ri in rptRoles.Items
                                   let roleId = (int)ViewState[ri.UniqueID]
                                   let chk = (CheckBox)ri.FindControl("chkRole")
                                   where chk.Checked
                                   select roleId)
            {
                arrayList.Add(roleId);
            }
            return arrayList;
        }

        public void GetAllRoles(IList<TBL_Admin_Roles> items)
        {
            rptRoles.DataSource = items;
            rptRoles.DataBind();
        }

        public string UserCode
        {
            get { return txtUserCode.Text; }
            set { txtUserCode.Text = value; }
        }

        public string Names
        {
            get { return txtNames.Text; }
            set { txtNames.Text = value; }
        }

        public DateTime IncomeDate
        {
            get { return string.IsNullOrEmpty(txtIncomeDate.Text) ? DateTime.Now : DateTime.Parse(txtIncomeDate.Text); }
            set { txtIncomeDate.Text = value.ToShortDateString(); }
        }

        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = value; }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        public string Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public string IdUser
        {
            get { return Request.QueryString["UserId"]; }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        #endregion
    }
}