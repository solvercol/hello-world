using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditEmailTemplate : ViewPage<EditEmailTemplatePresenter, IEditEmailTemplateView>, IEditEmailTemplateView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdPlantilla) ? "Nueva Plantilla" : "Editar Plantilla");

            btnEliminar.Visible = !string.IsNullOrEmpty(IdPlantilla);

            txtCodigo.Enabled = string.IsNullOrEmpty(IdPlantilla);
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditEmailTemplateHideControlsevent;
        }

        void FrmEditEmailTemplateHideControlsevent(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnEliminar.Visible = false;
        }

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
            Response.Redirect(string.Format("FrmAdminEmailTemplate.aspx{0}", GetBaseQueryString()));
        }

        public void ListadoPaises(List<TBL_Admin_Paises> items)
        {
            ddlPais.DataSource = items;
            ddlPais.DataValueField = "IdPais";
            ddlPais.DataTextField = "Nombre";
            ddlPais.DataBind();

            var li = new ListItem("--Selected--", "");
            ddlPais.Items.Insert(0, li);
        }

        public string NombrePlantilla
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string Encabezado
        {
            get { return __txtEncabezado.Text; }
            set { __txtEncabezado.Text = value; }
        }

        public string Cuerpo
        {
            get { return __txtContenido.Text; }
            set { __txtContenido.Text = value; }
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public string IdPlantilla
        {
            get { return Request.QueryString["TemplateId"]; }
        }

        public string IdPais
        {
            get { return ddlPais.SelectedValue; }
            set { ddlPais.SelectedValue = value; }
        }

        public string CodeTemplate
        {
            get { return txtCodigo.Text; }
            set { txtCodigo.Text = value; }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}