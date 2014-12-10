using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using System.Linq;

namespace Modules.Admin.Catalogos
{
    public partial class FrmViewUser : ViewPage<DetailUserPresenter, IDetailUserView>, IDetailUserView
    {

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {

            ImprimirTituloVentana("Detalle Usuario");
            btnEdit.Visible = !string.IsNullOrEmpty(IdUser);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmViewUserControlsevent;
        }

        void FrmViewUserControlsevent(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
        }

        #endregion

        #region Events

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminUsers.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditUser.aspx{0}&UserId={1}", GetBaseQueryString(), IdUser));
        }

        public void RptRolesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var role = e.Item.DataItem as TBL_Admin_Roles;
            if (role == null) return;
            ViewState[e.Item.UniqueID] = role.IdRol;
        }

        #endregion

        #region Members

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
            set { lblUserCode.Text = value; }
        }

        public string Names
        {
            set { lblNames.Text = value; }
        }

        public string IncomeDate
        {
            set { lblIncomeDate.Text = value; }
        }

        public string UserName
        {
            set { lblUserName.Text = value; }
        }

        public string Email
        {
            set { lblEmail.Text = value; }
        }

        public bool Activo
        {
            set { chkActive.Checked = value; }
        }

        public string CreateBy
        {
            set { lblCreateBy.Text = value; }
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