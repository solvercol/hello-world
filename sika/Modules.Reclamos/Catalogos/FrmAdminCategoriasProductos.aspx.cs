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
    public partial class FrmAdminCategoriasProductos : ViewPage<CategoriasProductosListPresenter, ICategoriaProductoListView>, ICategoriaProductoListView
    {
        #region Delegates

        public event EventHandler FilterEvent;
        public event EventHandler PagerEvent;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de Categorías de Productos");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddCategoriaProducto.aspx{0}", GetBaseQueryString()));
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

        public void GetCategoriaProductos(List<TBL_ModuloReclamos_CategoriaProducto> items)
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
            Response.Redirect(string.Format("FrmViewCategoriaProducto.aspx{0}&CategoriaProductoId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var Categoria = e.Item.DataItem as TBL_ModuloReclamos_CategoriaProducto;

            if (Categoria == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = Categoria.IdCategoria.ToString();
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = Categoria.IsActive;
            }

            var litCategoria = e.Item.FindControl("litCategoria") as Literal;
            if (litCategoria != null)
            {
                litCategoria.Text = Categoria == null ? string.Empty : Categoria.Nombre;
            }

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null)
            {
                litDescripcion.Text = Categoria == null ? string.Empty : Categoria.Descripcion;
            }
            if (Categoria.TBL_Admin_Usuarios2 != null)
            {
                var litIngenieros = e.Item.FindControl("litIngenieros") as Literal;
                if (litIngenieros != null)
                {
                    foreach (var ing in Categoria.TBL_Admin_Usuarios2)
                    {
                        if (ing.Nombres != string.Empty)
                        {
                            if(litIngenieros.Text != string.Empty)
                            {
                                litIngenieros.Text = litIngenieros.Text + "<br/>";
                            }
                            litIngenieros.Text = litIngenieros.Text + ing.Nombres;
                        }
                    }
                    
                }
              
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