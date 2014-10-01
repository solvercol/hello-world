using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using System.Collections;
using System.Linq;
namespace Modules.Reclamos.UserControls
{
    public partial class WUCAddZona : ViewUserControl<AddZonaPresenter, IAddZonaView>, IAddZonaView
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
            this.Descripcion = string.Empty;
            this.Activo = false;
            ShowSelectUnidadWindow(true);
        }

        public void ShowSelectUnidadWindow(bool visible)
        {
            if (visible)
                mpeSearch.Show();
            else
                mpeSearch.Hide();
        }

        #endregion

        #region events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            
            rfvDescripcion.Validate();
            if (rfvDescripcion.IsValid)
            {
                if (SaveEvent != null)
                    SaveEvent(null, EventArgs.Empty);
            }
            else
            {
                rfvDescripcion.Focus();
                mpeSearch.Show();
            }
        }

        #endregion

        #region Members

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
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