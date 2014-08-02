using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.PlanAccion.IViews;
using Presenters.PlanAccion.Presenters;

namespace Modules.PlanAccion.Admin
{
    public partial class FrmEditarCategoria : ViewPage<EditarCategoriaPresenter, IEditarCategoriaView>, IEditarCategoriaView
    {

        public event EventHandler GuardarEvent;
        public event EventHandler EliminarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdCategoria) ? "Nueva Categoría" : "Editar Categoría");
            btnCancelar.Attributes.Add("onclick","return confirm('¿Confirma que desea eliminar el registro actual?');");
            btnCancelar.Visible = !string.IsNullOrEmpty(IdCategoria);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditarCategoriaHideControlsevent;
        }

        void FrmEditarCategoriaHideControlsevent(object sender, EventArgs e)
        {
            btnCancelar.Visible = false;
            btnGuardar.Visible = false;
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmVistaCategorias.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {
            if (GuardarEvent != null)
                GuardarEvent(null, EventArgs.Empty);
        }

        protected void BtnCancelarClick(object sender, EventArgs e)
        {
            if (EliminarEvent != null)
                EliminarEvent(null, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string IdCategoria
        {
            get { return Request.QueryString["Idcategoria"]; }
        }

        public int Secuencia
        {
            get { return txtSecuencia.ValueInt; }
            set { txtSecuencia.ValueInt = value; }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public int Numeroactividades
        {
            get { return txtNumeroMinimo.ValueInt; }
            set { txtNumeroMinimo.ValueInt = value; }
        }

        public bool Activo
        {
            get { return chkActiva.Checked; }
            set { chkActiva.Checked = value; }
        }
    }
}