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
    public partial class FrmViewCategoriaProducto : ViewPage<DetailCategoriaProductoPresenter, IDetailCategoriaProductoView>, IDetailCategoriaProductoView
    {
        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {

            ImprimirTituloVentana("Detalle Categoría producto");
            btnEdit.Visible = !string.IsNullOrEmpty(IdCategoriaProducto);
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
            Response.Redirect(string.Format("FrmAdminCategoriasProductos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditCategoriaProducto.aspx{0}&CategoriaProductoId={1}", GetBaseQueryString(), IdCategoriaProducto));
        }
        #endregion

        #region Members


        public string Nombre
        {
            set { txtNombre.Text = value; }
        }

        public string Descripcion
        {
            set { txtDescripcion.Text = value; }
        }

        public string IngResponsables
        {
            set { txtResponsables.Text = value; }
            get { return txtResponsables.Text; }
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

        public string IdCategoriaProducto
        {
            get { return Request.QueryString["CategoriaProductoId"]; }
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