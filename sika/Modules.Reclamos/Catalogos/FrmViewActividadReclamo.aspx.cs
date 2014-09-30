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
    public partial class FrmViewActividadReclamo : ViewPage<DetailActividadReclamosPresenter, IDetailActividadReclamosView>, IDetailActividadReclamosView
    {
        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {

            ImprimirTituloVentana("Detalle Actividad Reclamos");
            btnEdit.Visible = !string.IsNullOrEmpty(IdActividadReclamo);

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
            Response.Redirect(string.Format("FrmAdminActividadesReclamos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditActividadReclamo.aspx{0}&ActividadReclamoId={1}", GetBaseQueryString(), IdActividadReclamo));
        }

        public void RptRolesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var role = e.Item.DataItem as TBL_Admin_Roles;
            if (role == null) return;
            ViewState[e.Item.UniqueID] = role.IdRol;
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
    }
}