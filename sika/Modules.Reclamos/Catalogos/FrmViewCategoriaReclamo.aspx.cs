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
    public partial class FrmViewCategoriaReclamo : ViewPage<DetailCategoriaReclamosPresenter, IDetailCategoriaReclamosView>, IDetailCategoriaReclamosView
    {
        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {

            ImprimirTituloVentana("Detalle Categoría Reclamos");
            btnEdit.Visible = !string.IsNullOrEmpty(IdCategoriaReclamo);
            if (this.IdTipoReclamo.ToUpper().Contains("SERVICIO"))
            {
                trGrupoInformacion.Visible = true;
            }
            else
            {
                trGrupoInformacion.Visible = false;
            }
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
            Response.Redirect(string.Format("FrmAdminCategoriasReclamos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditCategoriaReclamo.aspx{0}&CategoriaReclamoId={1}", GetBaseQueryString(), IdCategoriaReclamo));
        }
        #endregion

        #region Members


        public string Nombre
        {
            set { txtNombre.Text = value; }
        }

        public string SubCategoria
        {
            set { txtSubcategoria.Text = value; }
        }

        public string Descripcion
        {
            set { txtDescripcion.Text = value; }
        }

        public string Area
        {
            set { txtArea.Text = value; }
        }

        public int GrupoInformacion
        {
            set { txtGrupoInformacion.Text = value.ToString(); }
        }


        public string IdResponsable
        {
            set { txtResponsables.Text = value; }
        }

        public string IdTipoReclamo
        {
            set { txtReclamo.Text = value; }
            get { return txtReclamo.Text; }
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