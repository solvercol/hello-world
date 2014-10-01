using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Reclamos.UI;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Application.Core;
using System.Web.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAddUnidad : ViewUserControl<AddUnidadPresenter, IAddUnidadView>, IAddUnidadView
    {
        #region Delegates

        public event Action PostBackEvent;

        public event EventHandler SaveEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Visible = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        void FrmEditUserControlsevent(object sender, EventArgs e)
        {
            btnSave.Visible = false;
        }

        protected void BtnSearchProduct_Click(object sender, EventArgs e)
        {
            if (PostBackEvent != null)
                PostBackEvent();
            this.Nombre = string.Empty;
            this.Activo = false;
            ShowSelectZonaWindow(true);
        }

        #endregion

        #region events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            
            rfvNombre.Validate();
            if (rfvNombre.IsValid)
            {
                if (SaveEvent != null)
                    SaveEvent(null, EventArgs.Empty);
            }
            else
            {
                rfvNombre.Focus();
                mpeSearch.Show();
            }
        }

        public void ShowSelectZonaWindow(bool visible)
        {
            if (visible)
                mpeSearch.Show();
            else
                mpeSearch.Hide();
        }

        #endregion

        #region Members

        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
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