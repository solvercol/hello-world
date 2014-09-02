using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Domain.MainModule.Reclamos.DTO;
using Modules.Reclamos.UI;
using ServerControls;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminCostosReclamo : ViewUserControl<AdminCostosReclamoPresenter, IAdminCostosReclamoView>, IAdminCostosReclamoView, IReclamoWebUserControl
    {
        #region Members

        private decimal _totalCostosProductoReclamo = 0;
        private decimal _totalCostosTransporte = 0;
        private decimal _totalCostosDisposicion = 0;

        public bool CanEdit
        {
            get
            {
                if (ViewState["AdminCostos_CanEdit"] == null)
                    ViewState["AdminCostos_CanEdit"] = false;
                return Convert.ToBoolean(ViewState["AdminCostos_CanEdit"]);
            }
            set
            {
                ViewState["AdminCostos_CanEdit"] = value;
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void LoadControlData()
        {
            Presenter.LoadInitData();
            EditCostos(false);
            txtFilterProduct.Attributes.Add("onkeypress", "return clickButtonProduct(event,'" + btnFiltrar.ClientID + "')");
        }

        #endregion

        #region Buttons

        protected void BtnEditCostos_Click(object sender, EventArgs e)
        {
            EditCostos(true);
            Presenter.LoadCostosReclamo();
        }

        protected void BtnSaveCostos_Click(object sender, EventArgs e)
        {
            EditCostos(false);
            Presenter.UpdateCostosReclamo();            
        }

        protected void BtnAddCosto_Click(object sender, EventArgs e)
        {
            InitAdminProducto();
            ShowAdminProductoWindow(true);
            EditCostos(true);
        }

        protected void BtnCancelCosto_Click(object sender, EventArgs e)
        {
            EditCostos(true);
        }

        protected void BtnSaveCosto_Click(object sender, EventArgs e)
        {
            EditCostos(true);
            Presenter.AddCostosReclamo();            
        }

        protected void BtnRemoveCosto_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var idCosto = Convert.ToDecimal(btn.CommandArgument);

            EditCostos(true);
            Presenter.RemoveCostosReclamo(idCosto);            
        }

        protected void BtnSearchProduct_Click(object sender, EventArgs e)
        {
            FilterText = string.Empty;

            Presenter.LoadTotalProductos();
            Presenter.LoadProductos(0);
            
            ShowSelectProductWindow(true);

            txtFilterProduct.Focus();
        }

        protected void BtnFiltrarClick(object sender, EventArgs e)
        {
            Presenter.LoadTotalProductos();
            Presenter.LoadProductos(0);

            ShowSelectProductWindow(true);

            txtFilterProduct.Focus();
        }

        protected void BtnCancelFiltrarClick(object sender, EventArgs e)
        {
            ShowAdminProductoWindow(true);
            EditCostos(true);
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            var imgButton = (ImageButton)sender;

            var codigoProducto = imgButton.CommandArgument;

            Presenter.SelectProduct(codigoProducto);

            ShowAdminProductoWindow(true);
        }

        #endregion

        #region Repeaters

        protected void RptCostosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_CostosProducto)(e.Item.DataItem);
                // Bindind data

                var hddIdCosto = e.Item.FindControl("hddIdCosto") as HiddenField;
                if (hddIdCosto != null) hddIdCosto.Value = string.Format("{0}", item.IdCostoProducto);

                var lblNoCosto = e.Item.FindControl("lblNoCosto") as Label;
                if (lblNoCosto != null) lblNoCosto.Text = string.Format("{0}", item.NoCosto);

                var lblProducto = e.Item.FindControl("lblProducto") as Label;
                if (lblProducto != null) lblProducto.Text = string.Format("{0}", item.NombreProducto);

                var lblPesoNeto = e.Item.FindControl("lblPesoNeto") as Label;
                if (lblPesoNeto != null) lblPesoNeto.Text = string.Format("{0:0,0.0}", item.PesoNeto);

                var lblPrecioLista = e.Item.FindControl("lblPrecioLista") as Label;
                if (lblPrecioLista != null) lblPrecioLista.Text = string.Format("{0:0,0.0}", item.PrecioLista);

                var lblUnidades = e.Item.FindControl("lblUnidades") as Label;
                if (lblUnidades != null) lblUnidades.Text = string.Format("{0:0,0.0}", item.Unidades);

                var lblCostoProducto = e.Item.FindControl("lblCostoProducto") as Label;
                if (lblCostoProducto != null) lblCostoProducto.Text = string.Format("{0:0,0.0}", item.CostoProducto);

                var lblKilos = e.Item.FindControl("lblKilos") as Label;
                if (lblKilos != null) lblKilos.Text = string.Format("{0:0,0.0}", item.Kilos);

                var lblUnidadesDisponibles = e.Item.FindControl("lblUnidadesDisponibles") as Label;
                if (lblUnidadesDisponibles != null) lblUnidadesDisponibles.Text = string.Format("{0:0,0.0}", item.UnidadesDisponibles);

                var lblCostoDisponible = e.Item.FindControl("lblCostoDisponible") as Label;
                if (lblCostoDisponible != null) lblCostoDisponible.Text = string.Format("{0:0,0.0}", item.CostoDisponible);

                var imgDeleteCosto = e.Item.FindControl("imgDeleteCosto") as ImageButton;
                if (imgDeleteCosto != null)
                {
                    imgDeleteCosto.CommandArgument = string.Format("{0}", item.IdCostoProducto);
                    imgDeleteCosto.Visible = CanEdit;
                }

                _totalCostosProductoReclamo += item.CostoProducto;
                _totalCostosTransporte += item.Kilos; // Falta los Fletes
                _totalCostosDisposicion += item.CostoDisponible;
            }
        }

        protected void RptListadoProductoItemDataBound(object sender, RepeaterItemEventArgs e)
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
                litPesoNeto.Text = string.Format("{0:0,0.00}", item.PesoNeto);
            }

            var litPrecioLista = e.Item.FindControl("litPrecioLista") as Literal;
            if (litPrecioLista != null)
            {
                litPrecioLista.Text = string.Format("{0:0,0.00}", item.PrecioLista);
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

            ShowSelectProductWindow(true);
        }

        #endregion

        #region TextBoxes

        protected void TxtUnidadesProductoTextChanged(object sender, EventArgs e)
        {
            CheckCostosProductoSelect();
            ShowAdminProductoWindow(true);
        }

        protected void TxtUnidadesDisponerProductoTextChanged(object sender, EventArgs e)
        {
            CheckCostosDisponibilidadProductoSelect();
            ShowAdminProductoWindow(true);
        }

        protected void TxtTotalCostosTextChanged(object sender, EventArgs e)
        {
            CheckTotales();
            EditCostos(true);
        }

        #endregion

        #endregion

        #region Methods

        void InitAdminProducto()
        {
            PesoNetoProducto = string.Format("{0:0,0.0}", 0);
            PrecioListaProducto = string.Format("{0:0,0.0}", 0);
            UnidadesProducto = 0;
            UnidadesDisponerProducto = 0;
            CostoProducto = 0;
            CostoDisponibleProducto = 0;
            KilosProducto = 0;
            SelectedProduct = null;
            NombreProducto = string.Empty;
            FilterText = string.Empty;
        }

        void CheckTotales()
        {
            TotalCostosReclamo = CostoProductoReclamo + CostoTransporte + CostoDisposicion + CostoPruebasCampo +
                                    CostoManoObra + OtrosCostos + CostosAsistenciaTecnica + CostosAsistenciaRegional +
                                    CostoViajePersonas + CostoEquiposHerramientas;            
        }

        void EditCostos(bool enable)
        {
            CanEdit = enable;

            txtCostoPruebasCampo.Visible = enable;
            lblCostoPruebasCampo.Visible = !enable;

            txtCostoManoObra.Visible = enable;
            lblCostoManoObra.Visible = !enable;

            txtOtrosCostos.Visible = enable;
            lblOtrosCostos.Visible = !enable;

            txtCostosAsistenciaTecnica.Visible = enable;
            lblCostosAsistenciaTecnica.Visible = !enable;

            txtCostosAsistenciaRegional.Visible = enable;
            lblCostosAsistenciaRegional.Visible = !enable;

            txtCostoViajePersonas.Visible = enable;
            lblCostoViajePersonas.Visible = !enable;

            txtCostoEquiposHerramientas.Visible = enable;
            lblCostoEquiposHerramientas.Visible = !enable;

            txtCostoEquiposHerramientas.Visible = enable;
            lblCostoEquiposHerramientas.Visible = !enable;

            btnNuevoGasto.Visible = enable;
            btnSaveCostos.Visible = enable;
            btnEditar.Visible = !enable;
        }

        #endregion

        #region View Members

        #region Methods

        public void CheckCostosProductoSelect()
        {
            if (SelectedProduct == null) return;

            CostoProducto = (Convert.ToDecimal(PrecioListaProducto) * UnidadesProducto) - ((Convert.ToDecimal(PrecioListaProducto) * UnidadesProducto) * PorcentajeDescuento);
            KilosProducto = (Convert.ToDecimal(PesoNetoProducto) * UnidadesProducto);
        }

        public void CheckCostosDisponibilidadProductoSelect()
        {
            if (SelectedProduct == null) return;

            CostoDisponibleProducto = (Convert.ToDecimal(PesoNetoProducto) * UnidadesDisponerProducto) * ValorDisposicion;
        }

        public void ShowAdminProductoWindow(bool visible)
        {
            if (visible)
                mpeAdminCosto.Show();
            else
                mpeAdminCosto.Hide();
        }

        public void LoadCostoProductos(List<TBL_ModuloReclamos_CostosProducto> items)
        {
            _totalCostosProductoReclamo = 0;
            _totalCostosTransporte = 0;
            _totalCostosDisposicion = 0;

            rptCostosList.DataSource = items;
            rptCostosList.DataBind();
            
            CostoProductoReclamo = _totalCostosProductoReclamo;
            CostoTransporte = _totalCostosTransporte;
            CostoDisposicion = _totalCostosDisposicion;

            CheckTotales();
        }

        public void LoadProructos(List<Dto_Producto> items)
        {
            rptListadoProducto.DataSource = items;
            rptListadoProducto.DataBind();

            lblNoRecords.Visible = !items.Any();
        }

        public void ShowSelectProductWindow(bool visible)
        {
            if (visible)
                mpeSearchProducto.Show();
            else
                mpeSearchProducto.Hide();
        }

        #endregion

        #region Properties

        public event EventHandler Filterevent;

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        public Dto_Producto SelectedProduct
        {
            get
            {
                if (Session["AdminCostos_SelectedProducto"] == null)
                    Session["AdminCostos_SelectedProducto"] = new Dto_Producto();
                return Session["AdminCostos_SelectedProducto"] as Dto_Producto;
            }
            set
            {
                Session["AdminCostos_SelectedProducto"] = value;
            }
        }

        public string PesoNetoProducto
        {
            get { return lblPesoNetoProducto.Text; }
            set { lblPesoNetoProducto.Text = value; }
        }

        public string PrecioListaProducto
        {
            get { return lblPrecioListaProducto.Text; }
            set { lblPrecioListaProducto.Text = value; }
        }

        public int UnidadesProducto
        {
            get
            {
                return txtUnidadesProducto.ValueInt;
            }
            set
            {
                txtUnidadesProducto.ValueInt = 0;
            }
        }

        public int UnidadesDisponerProducto
        {
            get
            {
                return txtUnidadesDisponerProducto.ValueInt;
            }
            set
            {
                txtUnidadesDisponerProducto.ValueInt = value;
            }
        }

        public decimal CostoProducto
        {
            get { return Convert.ToDecimal(lblCostoProducto.Text); }
            set { lblCostoProducto.Text = string.Format("{0:0,0.0}", value); }
        }

        public decimal KilosProducto
        {
            get { return Convert.ToDecimal(lblKilosProducto.Text); }
            set { lblKilosProducto.Text = string.Format("{0:0,0.0}", value); }
        }

        public decimal CostoDisponibleProducto
        {
            get { return Convert.ToDecimal(lblCostoDisponibleProducto.Text); }
            set { lblCostoDisponibleProducto.Text = string.Format("{0:0,0.0}", value); }
        }

        public decimal PorcentajeDescuento
        {
            get
            {
                if (ViewState["AdminCosto_PorcentajeDescuento"] == null)
                    ViewState["AdminCosto_PorcentajeDescuento"] = 0;

                return Convert.ToDecimal(ViewState["AdminCosto_PorcentajeDescuento"]);

            }
            set
            {
                ViewState["AdminCosto_PorcentajeDescuento"] = value;
            }
        }

        public decimal ValorDisposicion
        {
            get
            {
                if (ViewState["AdminCosto_ValorDisposicion"] == null)
                    ViewState["AdminCosto_ValorDisposicion"] = 0;

                return Convert.ToDecimal(ViewState["AdminCosto_ValorDisposicion"]);

            }
            set
            {
                ViewState["AdminCosto_ValorDisposicion"] = value;
            }
        }

        public decimal CostoProductoReclamo
        {
            get
            {
                return Convert.ToDecimal(lblCostoProductoReclamo.Text);
            }
            set
            {
                lblCostoProductoReclamo.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostoTransporte
        {
            get
            {
                return Convert.ToDecimal(lblCostoTransporte.Text);
            }
            set
            {
                lblCostoTransporte.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostoDisposicion
        {
            get
            {
                return Convert.ToDecimal(lblCostoDisposicion.Text);
            }
            set
            {
                lblCostoDisposicion.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostoPruebasCampo
        {
            get
            {
                return txtCostoPruebasCampo.ValueDecimal;
            }
            set
            {
                txtCostoPruebasCampo.ValueDecimal = value;
                lblCostoPruebasCampo.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostoManoObra
        {
            get
            {
                return txtCostoManoObra.ValueDecimal;
            }
            set
            {
                txtCostoManoObra.ValueDecimal = value;
                lblCostoManoObra.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal OtrosCostos
        {
            get
            {
                return txtOtrosCostos.ValueDecimal;
            }
            set
            {
                txtOtrosCostos.ValueDecimal = value;
                lblOtrosCostos.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostosAsistenciaTecnica
        {
            get
            {
                return txtCostosAsistenciaTecnica.ValueDecimal;
            }
            set
            {
                txtCostosAsistenciaTecnica.ValueDecimal = value;
                lblCostosAsistenciaTecnica.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostosAsistenciaRegional
        {
            get
            {
                return txtCostosAsistenciaRegional.ValueDecimal;
            }
            set
            {
                txtCostosAsistenciaRegional.ValueDecimal = value;
                lblCostosAsistenciaRegional.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostoViajePersonas
        {
            get
            {
                return txtCostoViajePersonas.ValueDecimal;
            }
            set
            {
                txtCostoViajePersonas.ValueDecimal = value;
                lblCostoViajePersonas.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal CostoEquiposHerramientas
        {
            get
            {
                return txtCostoEquiposHerramientas.ValueDecimal;
            }
            set
            {
                txtCostoEquiposHerramientas.ValueDecimal = value;
                lblCostoEquiposHerramientas.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public decimal TotalCostosReclamo
        {
            get
            {
                return Convert.ToDecimal(lblTotalCostosReclamo.Text);
            }
            set
            {
                lblTotalCostosReclamo.Text = string.Format("{0:0,0.0}", value);
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

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        #endregion

        #endregion        
    }
}