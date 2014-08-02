using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.DefaultPresenter;

namespace ASP.NETCLIENTE.Pages.Globals
{
    public partial class FrmEditUsuarios : Page//ViewPage<UserPresenter, IUserView>, IUserView
    {

        #region Delegates

        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        private bool _isQueryString;

        #endregion

        #region Load
        protected override void OnInit(EventArgs e)
        {
            rptRoles.ItemDataBound += RptRolesItemDataBound;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInitialize();
        }

        private void LoadInitialize()
        {
            btnDelete.Attributes.Add("onclick", "return confirm(\"¿Confirma que desea continuar?\")");
            //ImprimirTituloVentana(string.IsNullOrEmpty(Request.QueryString["search"])
            //                          ? "Nuevo Usuario."
            //                          : "Editar Usuario.");
            if (Request.QueryString["search"] == null)
            {
                btnDelete.Enabled = false;
            }

            _isQueryString = Request.QueryString.HasKeys();
            //Presenter.MessageBox += PresenterMessageBox;
           
        }

        #endregion

        #region Members

        public void GetAllRoles(IEnumerable<TBL_Maestra_Roles> items)
        {
            rptRoles.DataSource = items;
            rptRoles.DataBind();
        }

        public string UserId
        {
            get { return Request.QueryString["search"]; }
        }

        public string UserName
        {
            get { return txtNombreUsuario.Text; }
            set { txtNombreUsuario.Text = value; }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        
        public string FirstName
        {
            get { return txtNombres.Text; }
            set { txtNombres.Text = value; }
        }

        public string LastName
        {
            get { return txtApellidos.Text; }
            set { txtApellidos.Text = value; }
        }

        public string Email
        {
            get { return txtMail.Text; }
            set { txtMail.Text = value; }
        }

        public int TimeZone { get; set; }

        public bool IsActive
        {
            get { return chkActivo.Checked; }
            set { chkActivo.Checked = value; }
        }

        public bool IsQueryString
        {
            get { return _isQueryString; }
        }

        public void InhabiltarDelete(bool op)
        {
            btnDelete.Enabled = op;
        }

        public void InhabiltarTodos(bool op)
        {
            btnDelete.Enabled = op;
            btnSave.Enabled = op;
            rptRoles.Visible = op;
        }

        public void RolesAsigandos(IEnumerable<TBL_Maestra_Roles> items)
        {
            foreach (RepeaterItem ri in rptRoles.Items)
            {
                if(ViewState[ri.UniqueID] == null)continue;
                var roleId = (int)ViewState[ri.UniqueID];
                var chk = (CheckBox)ri.FindControl("chkRole");
                chk.Checked = items.Any(r => r.IdRol == roleId);
            }
        }

        #endregion

        #region Eventos

        void RptRolesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var role = e.Item.DataItem as TBL_Maestra_Roles;
            if (role == null) return;
            ViewState[e.Item.UniqueID] = role.IdRol;
        }

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

        //void PresenterMessageBox(object sender, MessageBoxEventArgs e)
        //{
        //    switch (e.Tipo)
        //    {
        //        case TypeError.Ok:
        //            ShowMessageOk(e.Message);
        //            break;
        //        default:
        //            ShowError(e.Message);
        //            break;
        //    }
        //}

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmUsuarios.aspx"));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent == null) return;
            SaveEvent(string.IsNullOrEmpty(UserId) ? "Save" : "Update", EventArgs.Empty);
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }

        #endregion

    }
}