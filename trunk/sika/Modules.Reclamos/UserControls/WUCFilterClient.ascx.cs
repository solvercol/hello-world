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
    public partial class WUCFilterClient : ViewUserControl<FilterClientPresenter, IFilterClientView>, IFilterClientView
    {
        #region Members

        public event Action PostBackEvent;
        public event Action<Dto_Cliente> SelectClient;

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Repeater

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var item = e.Item.DataItem as Dto_Cliente;
            if (item == null) return;

            var litCodCliente = e.Item.FindControl("litCodCliente") as Literal;
            if (litCodCliente != null)
            {
                litCodCliente.Text = item.CodigoCliente;
            }

            var litCliente = e.Item.FindControl("litCliente") as Literal;
            if (litCliente != null)
            {
                litCliente.Text = item.Cliente;
            }

            var litContacto = e.Item.FindControl("litContacto") as Literal;
            if (litContacto != null)
            {
                litContacto.Text = item.Contacto;
            }

            var litEmail = e.Item.FindControl("litEmail") as Literal;
            if (litEmail != null)
            {
                litEmail.Text = item.Email;
            }

            var litUnidad = e.Item.FindControl("litUnidad") as Literal;
            if (litUnidad != null)
            {
                litUnidad.Text = item.Unidad;
            }

            var litZona = e.Item.FindControl("litZona") as Literal;
            if (litZona != null)
            {
                litZona.Text = item.Zona;
            }

            var imgSelect = e.Item.FindControl("ImgSelect") as ImageButton;
            if (imgSelect != null)
            {
                imgSelect.CommandArgument = string.Format("{0}", item.CodigoCliente);
            }
        }

        #endregion

        #region Pager

        protected void PgrListadoPageChanged(object sender, PageChanged e)
        {
            if (Filterevent != null)
                Filterevent(e.CurrentPage, EventArgs.Empty);
            mpeSearchClient.Show();

            if (PostBackEvent != null)
                PostBackEvent();
        }

        #endregion

        #region Buttons

        protected void BtnSearchCliente_Click(object sender, EventArgs e)
        {
            FilterText = string.Empty;
            ShowSelectClienteWindow(true);

            if (PostBackEvent != null)
                PostBackEvent();
        }

        protected void BtnFiltrarClick(object sender, EventArgs e)
        {
            Presenter.LoadTotalClientes();
            Presenter.LoadClientes(0);
            ShowSelectClienteWindow(true);
            if (PostBackEvent != null)
                PostBackEvent();
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            var imgButton = (ImageButton)sender;

            var codigo = imgButton.CommandArgument;

            Presenter.SelectCliente(codigo);

            if (PostBackEvent != null)
                PostBackEvent();
        }

        #endregion

        #endregion

        #region Methods
        #endregion

        #region View Members

        #region Methods

        public void LoadSelectedClient(Dto_Cliente cliente)
        {
            litNombreClienteSeleccionado.Text = cliente.NombreCliente;

            if (SelectClient != null)
                SelectClient(cliente);
        }

        public void LoadClientes(List<Dto_Cliente> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();

            lblNoRecords.Visible = !items.Any();
        }

        public void ShowSelectClienteWindow(bool visible)
        {
            if (visible)
                mpeSearchClient.Show();
            else
                mpeSearchClient.Hide();
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

        public Dto_Cliente SelectedClient
        {
            get
            {
                if (Session["FilterClient_SelectedClient"] == null)
                    Session["FilterClient_SelectedClient"] = new Dto_Cliente();
                return Session["FilterClient_SelectedClient"] as Dto_Cliente;
            }
            set
            {
                Session["FilterClient_SelectedClient"] = value;
            }
        }

        public string FilterText
        {
            get
            {
                return txtFilterCliente.Text;
            }
            set
            {
                txtFilterCliente.Text = value;
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