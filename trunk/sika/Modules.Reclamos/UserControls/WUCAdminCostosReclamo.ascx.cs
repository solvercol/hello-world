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

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminCostosReclamo : ViewUserControl<AdminCostosReclamoPresenter, IAdminCostosReclamoView>, IAdminCostosReclamoView, IReclamoWebUserControl
    {
        #region Members

        private decimal _totalCostosProductoReclamo = 0;
        private decimal _totalCostosTransporte = 0;
        private decimal _totalCostosDisposicion = 0;

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

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ucFilterProduct.PostBackEvent += WucProductPostBackEvent;
            ucFilterProduct.SelectProductoEvent += WucProductoSelectEvent;
        }

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

        #region Buttons

        protected void BtnAddCosto_Click(object sender, EventArgs e)
        {
            InitAdminProducto();
            ShowAdminProductoWindow(true);
        }

        protected void BtnSaveCosto_Click(object sender, EventArgs e)
        {
            Presenter.AddCostosReclamo();
        }

        protected void BtnRemoveCosto_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var idCosto = Convert.ToDecimal(btn.CommandArgument);

            Presenter.RemoveCostosReclamo(idCosto);
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
                if (imgDeleteCosto != null) imgDeleteCosto.CommandArgument = string.Format("{0}", item.IdCostoProducto);

                _totalCostosProductoReclamo += item.CostoProducto;
                _totalCostosTransporte += item.Kilos; // Falta los Fletes
                _totalCostosDisposicion += item.CostoDisponible;
            }
        }

        #endregion

        #region TextBoxes

        protected void TxtUnidadesProductoTextChanged(object sender, EventArgs e)
        {
            CostoProducto = (Convert.ToDecimal(PrecioListaProducto) * UnidadesProducto) * PorcentajeDescuento;
            KilosProducto = (Convert.ToDecimal(PesoNetoProducto) * UnidadesProducto);
            CostoDisponibleProducto = (Convert.ToDecimal(PrecioListaProducto) * UnidadesProducto) * ValorDisposicion;

            ShowAdminProductoWindow(true);
        }

        protected void TxtTotalCostosTextChanged(object sender, EventArgs e)
        {
            CheckTotales();
        }

        #endregion

        #endregion

        #region Methods

        void WucProductPostBackEvent()
        {
            mpeAdminCosto.Show();
        }

        void WucProductoSelectEvent(Dto_Producto producto)
        {
            PesoNetoProducto = string.Format("{0:0,0.0}", producto.PesoNeto);
            PrecioListaProducto = string.Format("{0:0,0.0}", producto.PrecioLista);
        }

        void InitAdminProducto()
        {
            PesoNetoProducto = string.Format("{0:0,0.0}", 0);
            PrecioListaProducto = string.Format("{0:0,0.0}", 0);
            UnidadesProducto = 0;
            UnidadesDisponerProducto = 0;
            CostoProducto = 0;
            CostoDisponibleProducto = 0;
            KilosProducto = 0;
            ucFilterProduct.InitControl();
        }

        void CheckTotales()
        {
            TotalCostosReclamo = CostoProductoReclamo + CostoTransporte + CostoDisposicion + CostoPruebasCampo +
                                    CostoManoObra + OtrosCostos + CostosAsistenciaTecnica + CostosAsistenciaRegional +
                                    CostoViajePersonas + CostoEquiposHerramientas;            
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminProductoWindow(bool visible)
        {
            if (visible)
                mpeAdminCosto.Show();
            else
                mpeAdminCosto.Hide();
        }

        public void LoadCostoProductos(List<TBL_ModuloReclamos_CostosProducto> items)
        {
            rptCostosList.DataSource = items;
            rptCostosList.DataBind();

            if (!items.Any())
            {
                _totalCostosProductoReclamo = 0;
                _totalCostosTransporte = 0;
                _totalCostosDisposicion = 0;
            }

            CostoProductoReclamo = _totalCostosProductoReclamo;
            CostoTransporte = _totalCostosTransporte;
            CostoDisposicion = _totalCostosDisposicion;

            CheckTotales();
        }

        #endregion

        #region Methods

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
                return ucFilterProduct.SelectedProduct;
            }
            set
            {
                ucFilterProduct.SelectedProduct = value;
            }
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

        #endregion

        #endregion    
    }
}