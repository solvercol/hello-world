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
    public partial class FrmViewAsesor : ViewPage<DetailAsesorPresenter, IDetailAsesorView>, IDetailAsesorView
    {

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {

            ImprimirTituloVentana("Detalle Asesor");
            btnEdit.Visible = !(string.IsNullOrEmpty(IdUser));
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
            Response.Redirect(string.Format("FrmAdminAsesores.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditAsesor.aspx{0}&UserId={1}", GetBaseQueryString(), IdUser));
        }

        #endregion

        #region Members

        public string IdUser
        {
            get { return Request.QueryString["UserId"]; }
        }

        public string IdUnidad
        {
            set { txtUnidad.Text = value; }
        }
        public string IdZona
        {
            set { txtZona.Text = value; }
        }

        public string AsesorName
        {
            set { txtAsesor.Text = value; }
        }

        public string JefesInmediatos
        {
            set { txtJefe.Text = value; }
            get { return txtJefe.Text; }
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