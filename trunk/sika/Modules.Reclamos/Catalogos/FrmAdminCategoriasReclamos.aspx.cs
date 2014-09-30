using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using ServerControls;

namespace Modules.Reclamos.Catalogos
{
    public partial class FrmAdminCategoriasReclamos : ViewPage<CategoriaReclamosListPresenter, ICategoriaReclamosListView>, ICategoriaReclamosListView
    {
        #region Delegates

        public event EventHandler FilterEvent;
        public event EventHandler PagerEvent;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de Categorías de Reclamos");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddCategoriaReclamo.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnFilterReclamos_Click(object sender, EventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(sender, EventArgs.Empty);
        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (PagerEvent != null)
                PagerEvent(e.CurrentPage, EventArgs.Empty);
        }

        public void GetCategoriaReclamos(List<TBL_ModuloReclamos_CategoriasReclamo> items) 
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        public string ModuleSetupId
        {
            get { return ViewState["ModuleSetupId"] == null ? string.Empty : ViewState["ModuleSetupId"].ToString(); }
            set { ViewState["ModuleSetupId"] = value; }
        }

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect(string.Format("FrmViewCategoriaReclamo.aspx{0}&CategoriaReclamoId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var Categoria = e.Item.DataItem as TBL_ModuloReclamos_CategoriasReclamo;

            if (Categoria == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = Categoria.IdCategoriaReclamo.ToString();
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = Categoria.IsActive;
            }

            var litNombre = e.Item.FindControl("litNombre") as Literal;
            if (litNombre != null)
            {
                litNombre.Text = Categoria == null ? string.Empty : Categoria.Nombre;
            }

            var litSubCategoria = e.Item.FindControl("litSubCategoria") as Literal;
            if (litSubCategoria != null)
            {
                litSubCategoria.Text = Categoria == null ? string.Empty : Categoria.SubCategoria;
            }

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null)
            {
                litDescripcion.Text = Categoria == null ? string.Empty : Categoria.Descripcion;
            }

            var litArea = e.Item.FindControl("litArea") as Literal;
            if (litArea != null)
            {
                litArea.Text = Categoria == null ? string.Empty : Categoria.Area;
            }

            var litResponsable = e.Item.FindControl("litResponsable") as Literal;
            if (litResponsable != null)
            {
                litResponsable.Text = Categoria == null ? string.Empty : Categoria.TBL_Admin_Usuarios.Nombres;
            }

            var litGrupoInformacion = e.Item.FindControl("litGrupoInformacion") as Literal;
            if (litGrupoInformacion != null)
            {
                litGrupoInformacion.Text = Categoria == null ? string.Empty : Categoria.GrupoInformacion.ToString();
            }

            var litTipoReclamo = e.Item.FindControl("litTipoReclamo") as Literal;
            if (litTipoReclamo != null)
            {
                litTipoReclamo.Text = Categoria == null ? string.Empty : Categoria.TBL_ModuloReclamos_TipoReclamo.Nombre;
            }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string Search
        {
            get { return txtSearch.Text; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}