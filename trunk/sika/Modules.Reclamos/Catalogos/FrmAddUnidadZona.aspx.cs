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
    public partial class FrmAddUnidadZona : ViewPage<AddUnidadZonaPresenter, IAddUnidadZonaView>, IAddUnidadZonaView
    {
        #region Delegates

        public event EventHandler SaveEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Adicionar Unidades por Zona");
            btnSave.Visible = true;
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
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }



        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminUnidadesZonas.aspx{0}", GetBaseQueryString()));
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
            wddGerente.TextField = "Nombres";
            wddGerente.ValueField = "IdUser";
            wddGerente.DataBind();
        }

        public void GetUnidades(IList<TBL_ModuloReclamos_Unidad> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombre).ToList();
            }

            wddUnidad.DataSource = items;
            wddUnidad.TextField = "Nombre";
            wddUnidad.ValueField = "IdUnidad";
            wddUnidad.DataBind();
        }

        public void GetZonas(IList<TBL_ModuloReclamos_Zona> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Descripcion).ToList();
            }

            wddZona.DataSource = items;
            wddZona.TextField = "Descripcion";
            wddZona.ValueField = "IdZona";
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
    }
}