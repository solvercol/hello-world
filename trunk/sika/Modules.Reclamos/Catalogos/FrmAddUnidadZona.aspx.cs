using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using System.Collections;
using System.Linq;
using System.Data;

namespace Modules.Reclamos.Catalogos
{
    public partial class FrmAddUnidadZona : ViewPage<AddUnidadZonaPresenter, IAddUnidadZonaView>, IAddUnidadZonaView
    {
        #region Delegates

        public event EventHandler SaveEvent;
        public event EventHandler SaveEventZona;
        public event EventHandler SaveEventUnidad;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Adicionar Unidades por Zona");
            btnSave.Visible = true;
            btnSaveUnidad.Visible = true;
            btnSaveZona.Visible = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditUserControlsevent;
        }

        void FrmEditUserControlsevent(object sender, EventArgs e)
        {
            btnSave.Visible = false;
        }

        #endregion

        #region events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            //rfvDescripcion.Validate();
            rfvgerente.Validate();
            rfvTarifa.Validate();
            rfvUnidad.Validate();
            rfvZona.Validate();
            if (rfvgerente.IsValid &&rfvTarifa.IsValid&& rfvUnidad.IsValid&&rfvZona.IsValid)
            {
                if (SaveEvent != null)
                {
                    SaveEvent(null, EventArgs.Empty);
                    Response.Redirect(string.Format("FrmViewUnidadZona.aspx{0}&UnidadId={1}&ZonaId={2}&GerenteId={3}", GetBaseQueryString(), this.IdUnidad, this.IdZona, this.IdGerente));
                }
            }
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

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminUnidadesZonas.aspx{0}", GetBaseQueryString()));
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

        public void GetGerentes(IList<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddGerente.DataSource = items;
            wddGerente.DataTextField = "Nombres";
            wddGerente.DataValueField = "IdUser";
            wddGerente.DataBind();
        }

        public void GetUnidades(IList<TBL_ModuloReclamos_Unidad> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombre).ToList();
            }

            wddUnidad.DataSource = items;
            wddUnidad.DataTextField = "Nombre";
            wddUnidad.DataValueField = "IdUnidad";
            wddUnidad.DataBind();
        }

        public void GetZonas(IList<TBL_ModuloReclamos_Zona> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Descripcion).ToList();
            }

            wddZona.DataSource = items;
            wddZona.DataTextField = "Descripcion";
            wddZona.DataValueField = "IdZona";
            wddZona.DataBind();
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

        public int IdGerente
        {
            get { return int.Parse(wddGerente.SelectedValue); }
            set { wddGerente.SelectedValue = value.ToString(); }
        }

        public int IdUnidad
        {
            get { return int.Parse(wddUnidad.SelectedValue); }
            set { wddUnidad.SelectedValue = value.ToString(); }
        }

        public int IdZona
        {
            get { return int.Parse(wddZona.SelectedValue); }
            set { wddZona.SelectedValue = value.ToString(); }
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

        #endregion

    }

}


