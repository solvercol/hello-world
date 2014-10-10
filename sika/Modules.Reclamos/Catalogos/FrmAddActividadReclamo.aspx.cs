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
    public partial class FrmAddActividadCategoria : ViewPage<AddActividadReclamosPresenter, IAddActividadReclamosView>, IAddActividadReclamosView
    {
        #region Delegates

        public event EventHandler SaveEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Adicionar actividad Reclamo");
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
            {
                SaveEvent(null, EventArgs.Empty);
                Response.Redirect(string.Format("FrmViewActividadReclamo.aspx{0}&ActividadReclamoId={1}", GetBaseQueryString(), this.IdActividadReclamo));
            }
        }



        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminActividadesReclamos.aspx{0}", GetBaseQueryString()));
        }



        #endregion

        #region Members

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

        public string IdActividadReclamo
        {
            get;
            set;
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