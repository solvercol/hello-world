using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Domain.MainModule.Reclamos.DTO;
using System.Web.UI.WebControls;
using ServerControls;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCFilterProduct : ViewUserControl<FilterProductPresenter, IFilterProductView>, IFilterProductView
    {
        #region Members

        public event Action PostBackEvent;
        public event Action<Dto_Producto> SelectProductoEvent;

        public bool ShowProductTable
        {
            get
            {
                if (string.IsNullOrEmpty(ViewState["FilterProduct_ShowProductTable"].ToString()))
                    ViewState["FilterProduct_ShowProductTable"] = true;

                return Convert.ToBoolean(ViewState["FilterProduct_ShowProductTable"]);
            }
            set
            {
                ViewState["FilterProduct_ShowProductTable"] = value;
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            txtFilterProduct.Attributes.Add("onkeypress", "return clickButtonProduct(event,'" + btnFiltrar.ClientID + "')");
        }

        #endregion

        #region Repeater

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var item = e.Item.DataItem as Dto_Producto;
            if (item == null) return;

            var litCodProducto = e.Item.FindControl("litCodProducto") as Literal;
            if (litCodProducto != null)
            {
                litCodProducto.Text = item.CodigoProducto;
            }

            var litProducto = e.Item.FindControl("litProducto") as Literal;
            if (litProducto != null)
            {
                litProducto.Text = item.Producto;
            }

            var litUnidad = e.Item.FindControl("litUnidad") as Literal;
            if (litUnidad != null)
            {
                litUnidad.Text = item.Unidad;
            }

            var litPesoNeto = e.Item.FindControl("litPesoNeto") as Literal;
            if (litPesoNeto != null)
            {
                litPesoNeto.Text = string.Format("{0:0,0.0}", item.PesoNeto);
            }

            var litPrecioLista = e.Item.FindControl("litPrecioLista") as Literal;
            if (litPrecioLista != null)
            {
                litPrecioLista.Text = string.Format("{0:0,0.0}", item.PrecioLista);
            }

            var litTargetMarket = e.Item.FindControl("litTargetMarket") as Literal;
            if (litTargetMarket != null)
            {
                litTargetMarket.Text = item.GrupoCompradores;
            }

            var litCampoAplicacion = e.Item.FindControl("litCampoAplicacion") as Literal;
            if (litCampoAplicacion != null)
            {
                litCampoAplicacion.Text = item.CampoApl;
            }

            var litSubCampoAplicacion = e.Item.FindControl("litSubCampoAplicacion") as Literal;
            if (litSubCampoAplicacion != null)
            {
                litSubCampoAplicacion.Text = item.Categoria;
            }

            var imgSelect = e.Item.FindControl("ImgSelect") as ImageButton;
            if (imgSelect != null)
            {
                imgSelect.CommandArgument = string.Format("{0}", item.CodigoProducto);
            }
        }

        #endregion

        #region Pager

        protected void PgrListadoPageChanged(object sender, PageChanged e)
        {
            if (Filterevent != null)
                Filterevent(e.CurrentPage, EventArgs.Empty);            

            if (PostBackEvent != null)
                PostBackEvent();

            ShowSelectProductWindow(true);
        }

        #endregion

        #region Buttons

        protected void BtnSearchProduct_Click(object sender, EventArgs e)
        {
            FilterText = string.Empty;

            Presenter.LoadTotalProductos();
            Presenter.LoadProductos(0);

            if (PostBackEvent != null)
                PostBackEvent();

            ShowSelectProductWindow(true);
            txtFilterProduct.Focus();
        }

        protected void BtnFiltrarClick(object sender, EventArgs e)
        {
            Presenter.LoadTotalProductos();
            Presenter.LoadProductos(0);            

            if (PostBackEvent != null)
                PostBackEvent();

            ShowSelectProductWindow(true);

            txtFilterProduct.Focus();
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            var imgButton = (ImageButton)sender;

            var codigoProducto = imgButton.CommandArgument;

            Presenter.SelectProduct(codigoProducto);

            if (PostBackEvent != null)
                PostBackEvent();
        }

        #endregion

        #endregion

        #region Methods

        public void InitControl()
        {
            SelectedProduct = null;
            litNombreProductoSeleccionado.Text = string.Empty;
        }

        #endregion

        #region View Members

        #region Methods

        public void LoadSelectedProducto(Dto_Producto producto)
        {
            NombreProducto = producto.NombreProducto;
            PresentacionProducto = producto.Unidad;
            TargetMarketProducto = producto.GrupoCompradores;
            CampoAplicacionProducto = producto.CampoApl;
            SubCampoAplicacionProducto = producto.Categoria;

            trInfoProducto.Visible = true && ShowProductTable;

            if (SelectProductoEvent != null)
                SelectProductoEvent(producto);
        }

        public void LoadProructos(List<Dto_Producto> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();

            lblNoRecords.Visible = !items.Any();
        }

        public void ShowSelectProductWindow(bool visible)
        {
            if (visible)
                mpeSearch.Show();
            else
                mpeSearch.Hide();
        }

        #endregion

        #region Properties

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public event EventHandler Filterevent;

        public Dto_Producto SelectedProduct
        {
            get
            {
                if (Session["FilterProducto_SelectedProducto"] == null)
                    Session["FilterProducto_SelectedProducto"] = new Dto_Producto();
                return Session["FilterProducto_SelectedProducto"] as Dto_Producto;
            }
            set
            {
                Session["FilterProducto_SelectedProducto"] = value;
            }
        }

        public string FilterText
        {
            get
            {
                return txtFilterProduct.Text;
            }
            set
            {
                txtFilterProduct.Text = value;
            }
        }

        public string NombreProducto
        {
            get
            {
                return litNombreProductoSeleccionado.Text;
            }
            set
            {
                litNombreProductoSeleccionado.Text = value;
            }
        }

        public string PresentacionProducto
        {
            get
            {
                return lblPresentacionProducto.Text;
            }
            set
            {
                lblPresentacionProducto.Text = value;
            }
        }

        public string CampoAplicacionProducto
        {
            get
            {
                return lblCampoAplicacionProducto.Text;
            }
            set
            {
                lblCampoAplicacionProducto.Text = value;
            }
        }

        public string SubCampoAplicacionProducto
        {
            get
            {
                return lblSubCampoAplicacionProducto.Text;
            }
            set
            {
                lblSubCampoAplicacionProducto.Text = value;
            }
        }

        public string TargetMarketProducto
        {
            get
            {
                return lblTargetMarketProducto.Text;
            }
            set
            {
                lblTargetMarketProducto.Text = value;
            }
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        #endregion

        #endregion
    }
}