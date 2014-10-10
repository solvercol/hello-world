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
    public partial class FrmEditCategoriaReclamo : ViewPage<EditCategoriaReclamosPresenter, IEditCategoriaReclamosView>, IEditCategoriaReclamosView
    {
        #region Delegates

        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Editar tipo Reclamo");

           // btnEliminar.Visible = !string.IsNullOrEmpty(IdCategoriaReclamo);
            btnSave.Visible = !string.IsNullOrEmpty(IdCategoriaReclamo);
            wddReclamo_SelectedIndexChanged(sender, e);
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
            {
                SaveEvent(null, EventArgs.Empty);
                Response.Redirect(string.Format("FrmViewCategoriaReclamo.aspx{0}&CategoriaReclamoId={1}", GetBaseQueryString(), this.IdCategoriaReclamo));
            }
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminCategoriasReclamos.aspx{0}", GetBaseQueryString()));
        }

        protected void wddReclamo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.wddReclamo.SelectedItem.ToString().ToUpper().Contains("SERVICIO"))
            {
                trGrupoInformacion.Visible = true;
            }
            else
            {
                trGrupoInformacion.Visible = false;
            }
        }
        #endregion

        #region Members



        public void GetResponsables(IList<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddResponsables.DataSource = items;
            wddResponsables.DataTextField = "Nombres";
            wddResponsables.DataValueField = "IdUser";
            wddResponsables.DataBind();
        }

        public void GetTipoReclamos(IList<TBL_ModuloReclamos_TipoReclamo> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombre).ToList();
            }

            wddReclamo.DataSource = items;
            wddReclamo.DataTextField = "Nombre";
            wddReclamo.DataValueField = "IdTipoReclamo";
            wddReclamo.DataBind();
        }

        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string SubCategoria
        {
            get { return txtSubcategoria.Text; }
            set { txtSubcategoria.Text = value; }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public string Area
        {
            get { return txtArea.Text; }
            set { txtArea.Text = value; }
        }

        public int GrupoInformacion
        {
            get { return WddGrupoInformacion.SelectedValue != string.Empty ? int.Parse(WddGrupoInformacion.SelectedValue) : 0; }
            set { WddGrupoInformacion.SelectedValue = value.ToString(); }
        }


        public int IdResponsable
        {
            get { return int.Parse(wddResponsables.SelectedValue); }
            set { wddResponsables.SelectedValue = value.ToString(); }
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

        public string IdCategoriaReclamo
        {
            get { return Request.QueryString["CategoriaReclamoId"]; }
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