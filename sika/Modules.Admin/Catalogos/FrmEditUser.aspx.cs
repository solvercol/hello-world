using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using System.Collections;
using System.Linq;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditUser : ViewPage<EditUserPresenter,IEditUserView>,IEditUserView
    {
        #region Delegates

        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Editar Usuario");

            btnEliminar.Visible = !string.IsNullOrEmpty(IdUser);
            btnSave.Visible = !string.IsNullOrEmpty(IdUser);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditUserControlsevent;
        }

        void FrmEditUserControlsevent(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnEliminar.Visible = false;
        }

        #endregion

        #region events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
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

        public void RolesAsigandos(IList<TBL_Admin_Roles> items)
        {
            foreach (RepeaterItem ri in rptRoles.Items)
            {
                var roleId = (int)ViewState[ri.UniqueID];
                var chk = (CheckBox)ri.FindControl("chkRole");
                chk.Checked = items.Any(r => r.IdRol == roleId);
            }
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

        public string CreateBy
        {
            set { lblCreateBy.Text= value; }
        }

        public string CreateOn
        {
            set { lblCreateOn.Text = value; }
        }

        public string ModifiedBy
        {
            set { lblModifiedBy.Text = value; }
        }

        public string ModifiedOn
        {
            set { lblModifiedOn.Text = value; }
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