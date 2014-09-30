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
    public partial class FrmEditActividadReclamo : ViewPage<EditActividadReclamosPresenter, IEditActividadReclamosView>, IEditActividadReclamosView
    {
        #region Delegates

        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Editar Actividad Reclamo");

            // btnEliminar.Visible = !string.IsNullOrEmpty(IdCategoriaReclamo);
            btnSave.Visible = !string.IsNullOrEmpty(IdActividadReclamo);
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
            Response.Redirect(string.Format("FrmAdminActividadesReclamos.aspx{0}", GetBaseQueryString()));
        }

        #region Members

        public void GetTipoReclamos(IList<TBL_ModuloReclamos_TipoReclamo> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombre).ToList();
            }

            wddReclamo.DataSource = items;
            wddReclamo.TextField = "Nombre";
            wddReclamo.ValueField = "IdTipoReclamo";
            wddReclamo.DataBind();
        }

        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public int IdTipoReclamo
        {
            get { return int.Parse(wddReclamo.SelectedValue); }
            set { wddReclamo.SelectedValue = value.ToString(); }
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

        public string IdActividadReclamo
        {
            get { return Request.QueryString["ActividadReclamoId"]; }
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

        #endregion
    }
}