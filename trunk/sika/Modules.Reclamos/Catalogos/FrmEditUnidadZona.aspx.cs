using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using System.Collections;
using System.Linq;


namespace Modules.Reclamos.Catalogos
{
    public partial class FrmEditUnidadZona : ViewPage<EditUnidadZonaPresenter, IEditUnidadZonaView>, IEditUnidadZonaView
    {
        #region Delegates

        public event EventHandler DeleteEvent;

        public event EventHandler SaveEvent;

        public event EventHandler SaveEventZona;

        public event EventHandler SaveEventUnidad;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {

            ImprimirTituloVentana("Editar Unidad Zona");
            btnSave.Visible = !((string.IsNullOrEmpty(IdUnidad)) && (string.IsNullOrEmpty(IdZona)) && (string.IsNullOrEmpty(IdGerente)));
            btnSaveUnidad.Visible = true;
            btnSaveZona.Visible = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmViewUserControlsevent;
        }

        void FrmViewUserControlsevent(object sender, EventArgs e)
        {
            btnEliminar.Visible = false;
        }

        #endregion

        #region Events

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminUnidadesZonas.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            rfvDescripcion.Validate();
            rfvTarifa.Validate();
            if (rfvDescripcion.IsValid &&  rfvTarifa.IsValid)
             {
                 if (SaveEvent != null)
                 {
                     SaveEvent(null, EventArgs.Empty);
                     Response.Redirect(string.Format("FrmViewUnidadZona.aspx{0}&UnidadId={1}&ZonaId={2}&GerenteId={3}", GetBaseQueryString(), this.IdUnidad, this.IdZona, this.IdGerente));
                 }
             }
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }

        protected void BtnSaveZonaClick(object sender, EventArgs e)
        {

            rfvNombreZona.Validate();
            if (rfvNombreZona.IsValid)
            {
                if (SaveEventZona != null)
                    SaveEventZona(null, EventArgs.Empty);
            }
            else
            {
                rfvNombreZona.Focus();
                mpeZonas.Show();
            }
        }

        protected void BtnSaveUnidadClick(object sender, EventArgs e)
        {

            rfvNombreUnidad.Validate();
            if (rfvNombreUnidad.IsValid)
            {
                if (SaveEventUnidad != null)
                    SaveEventUnidad(null, EventArgs.Empty);
            }
            else
            {
                rfvNombreUnidad.Focus();
                mpeUnidades.Show();
            }
        }

        protected void BtnAddUnidad_Click(object sender, EventArgs e)
        {
            this.NombreUnidad = string.Empty;
            this.ActivoUnidad = true;
            ShowSelectUnidadWindow(true);
        }

        public void ShowSelectUnidadWindow(bool visible)
        {
            if (visible)
                mpeUnidades.Show();
            else
                mpeUnidades.Hide();
        }

        protected void BtnAddZona_Click(object sender, EventArgs e)
        {
            this.DescripcionZona = string.Empty;
            this.ActivoZona = true;
            ShowSelectZonaWindow(true);
        }

        public void ShowSelectZonaWindow(bool visible)
        {
            if (visible)
                mpeZonas.Show();
            else
                mpeZonas.Hide();
        }
        #endregion

        #region Members

        public string IdUnidad
        {
            get { return Request.QueryString["UnidadId"]; }
        }

        public string IdZona
        {
            get { return Request.QueryString["ZonaId"]; }
        }

        public string IdGerente
        {
            get { return Request.QueryString["GerenteId"]; }
        }


        public string Unidad
        {
            set { txtUnidad.Text = value; }
        }
        public string Zona
        {
            set { txtZona.Text = value; }
        }

        public string Gerente
        {
            set { txtGerente.Text = value; }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public decimal TarifasFletes
        {
            get { return decimal.Parse(txtTarifa.Text); }
            set { txtTarifa.Text = value.ToString(); }
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

        public string ModifiedBy
        {
            set { lblModifiedBy.Text = value; }
        }

        public string ModifiedOn
        {
            set { lblModifiedOn.Text = value; }
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

        #region Members Unidad

        public string NombreUnidad
        {
            get { return txtNombreUnidad.Text; }
            set { txtNombreUnidad.Text = value; }
        }

        public bool ActivoUnidad
        {
            get { return chbActiveUnidad.Checked; }
            set { chbActiveUnidad.Checked = value; }
        }

        public string CreateByUnidad
        {
            set { lblCreateByUnidad.Text = value; }
        }

        public string CreateOnUnidad
        {
            set { lblCreateOnUnidad.Text = value; }
        }


        #endregion

        #region Members  Zonas

        public string DescripcionZona
        {
            get { return txtNombreZona.Text; }
            set { txtNombreZona.Text = value; }
        }

        public bool ActivoZona
        {
            get { return chbActiveZona.Checked; }
            set { chbActiveZona.Checked = value; }
        }

        public string CreateByZona
        {
            set { lblCreateByZona.Text = value; }
        }

        public string CreateOnZona
        {
            set { lblCreateOnZona.Text = value; }
        }


        #endregion
    }
}