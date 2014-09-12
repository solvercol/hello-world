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
using Domain.MainModule.Reclamos.Enum;
using Application.Core;
using Infragistics.Web.UI.ListControls;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminRecServicioT2 : ViewUserControl<AdminRecServicioT2Presenter, IAdminRecServicioT2View>, IAdminRecServicioT2View
    {
        #region Members

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            IdTipoReclamo = TipoReclamo.Servicio;
            ucFilterClient.PostBackEvent += WucPostBackEvent;
            ucFilterClient.SelectClient += WucClientSelectEvent;
        }

        #endregion

        #region Buttons

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdReclamo))
            {
                switch (FromPage)
                {
                    case "pendientes":
                        Response.Redirect(string.Format("../Views/FrmMisPendientes.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "misfecha":
                        Response.Redirect(string.Format("../Views/FrmMisReclamosPorFecha.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "misestado":
                        Response.Redirect(string.Format("../Views/FrmMisReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "rectipo":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorTipo.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "recestado":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "recnumero":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorNumero.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "rectargetmarket":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorTargetMarket.aspx?ModuleId={0}", ModuleId));
                        break;
                }
            }
            else
                Response.Redirect(string.Format("../Admin/FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}", ModuleId, IdReclamo));
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (SelectedCliente.CodigoCliente == null)
                messages.Add("Es necesario seleccionar un cliente.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            if (string.IsNullOrEmpty(UnidadZona))
                messages.Add("La unidad y zona del cliente son necesarios.");

            if (string.IsNullOrEmpty(NombreContacto))
                messages.Add("El nombre del contacto es obligatorio.");

            if (string.IsNullOrEmpty(EmailContacto))
                messages.Add("El mail del contacto es obligatorio.");

            if (!string.IsNullOrEmpty(EmailContacto) && !IsValidEmail(EmailContacto))
                messages.Add("El mail de contacto ingresado no se encuentra con una estructura correcta.");

            if (string.IsNullOrEmpty(DescripcionProblema))
                messages.Add("La descripción del problema es requerida.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            if (string.IsNullOrEmpty(IdReclamo))
                Presenter.SaveReclamo();
            else
                Presenter.UpdateReclamo();
        }

        #endregion

        #region TextBoxes

        protected void TxtFechaPedido_ValueChanged(object sender, EventArgs e)
        {
            if (FechaCompromiso != null && FechaRealEntrega != null)
            {
                if (FechaRealEntrega.Value > FechaCompromiso.Value)
                    DiasIncumplimiento = (int)(FechaRealEntrega.Value - FechaCompromiso.Value).TotalDays;
                else
                    DiasIncumplimiento = 0;
            }

            Secciones.SelectedIndex = 1;
        }

        #endregion

        #region DropDownList

        protected void WddAsesor_ValueChanged(object sender, DropDownValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedCliente.CodigoCliente))
                Presenter.LoadUnidadZonaAsesor();
        }

        #endregion

        #endregion

        #region Methods

        void AddErrorMessages(List<string> messages)
        {
            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    var custVal = new CustomValidator();
                    custVal.IsValid = false;
                    custVal.ErrorMessage = msg;
                    custVal.EnableClientScript = false;
                    custVal.Display = ValidatorDisplay.None;
                    custVal.ValidationGroup = "vgGeneral";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        void WucPostBackEvent()
        {
            Secciones.SelectedIndex = 1;
        }

        void WucClientSelectEvent(Dto_Cliente cliente)
        {
            UnidadZona = string.Format("{0}-{1}", cliente.Unidad, cliente.Zona);
            NombreContacto = cliente.Contacto;
            EmailContacto = cliente.Email;
        }

        #endregion

        #region View Members

        #region Methods

        public void GoToReclamoView(string idReclamo)
        {
            Response.Redirect(string.Format("../Admin/FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}", IdModule, idReclamo));
        }

        public void LoadAsesores(List<Dto_Asesor> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Asesor).ToList();
            }
            wddAsesor.DataSource = items;
            wddAsesor.TextField = "Asesor";
            wddAsesor.ValueField = "IdUser";
            wddAsesor.DataBind();

            wddAsesor.SelectedItemIndex = 0;
        }

        public void LoadAtendidoPor(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddReclamoAtentidoPor.DataSource = items;
            wddReclamoAtentidoPor.TextField = "Nombres";
            wddReclamoAtentidoPor.ValueField = "IdUser";
            wddReclamoAtentidoPor.DataBind();
        }

        public void LoadPlantas(List<DTO_ValueKey> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Value).ToList();
            }
            wddPlanta.DataSource = items;
            wddPlanta.TextField = "Id";
            wddPlanta.ValueField = "Value";
            wddPlanta.DataBind();

            wddPlanta.SelectedItemIndex = 0;
        }

        public void SetSelectedClient(Dto_Cliente cliente)
        {
            ucFilterClient.SelectedClient = cliente;
            ucFilterClient.NombreCliente = cliente.NombreCliente;
        }

        #endregion

        #region Members

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string IdAsesor
        {
            get
            {
                return wddAsesor.SelectedValue;
            }
            set
            {
                wddAsesor.SelectedValue = value;
            }
        }

        public string IdAtendidoPor
        {
            get
            {
                return wddReclamoAtentidoPor.SelectedValue;
            }
            set
            {
                wddReclamoAtentidoPor.SelectedValue = value;
            }
        }

        public string TipoContacto
        {
            get
            {
                return rblTipoContacto.SelectedValue;
            }
            set
            {
                rblTipoContacto.SelectedValue = value;
            }
        }

        public string UnidadZona
        {
            get
            {
                return lblUnidadZona.Text;
            }
            set
            {
                lblUnidadZona.Text = value;
            }
        }

        public string NombreContacto
        {
            get
            {
                return txtNombreContacto.Text;
            }
            set
            {
                txtNombreContacto.Text = value;
            }
        }

        public string EmailContacto
        {
            get
            {
                return txtEmailContacto.Text;
            }
            set
            {
                txtEmailContacto.Text = value;
            }
        }

        public string DescripcionProblema
        {
            get
            {
                return txtDescripcionProblema.Text;
            }
            set
            {
                txtDescripcionProblema.Text = value;
            }
        }        

        public string MensajeDescripcionProblema
        {
            get
            {
                return lblMensajeDescripcionProblema.Text;
            }
            set
            {
                lblMensajeDescripcionProblema.Text = value;
            }
        }

        public Dto_Cliente SelectedCliente
        {
            get { return ucFilterClient.SelectedClient; }
        }

        public string ConsecutivoReclamo
        {
            get
            {
                if (ViewState["AdminRecServicio_ConsecutivoReclamo"] == null)
                    ViewState["AdminRecServicio_ConsecutivoReclamo"] = string.Empty;

                return ViewState["AdminRecServicio_ConsecutivoReclamo"].ToString();
            }
            set
            {
                ViewState["AdminRecServicio_ConsecutivoReclamo"] = value;
            }
        }

        public int IdTipoReclamo
        {
            get
            {
                if (ViewState["AdminRecServicio_IdTipoReclamo"] == null)
                    ViewState["AdminRecServicio_IdTipoReclamo"] = TipoReclamo.Servicio;

                return Convert.ToInt32(ViewState["AdminRecServicio_IdTipoReclamo"]);
            }
            set
            {
                ViewState["AdminRecServicio_IdTipoReclamo"] = value;
            }
        }

        public string IdCategoriaReclamo
        {
            get
            {
                if (ViewState["AdminRecServicio_IdCategoriaReclamo"] == null)
                    ViewState["AdminRecServicio_IdCategoriaReclamo"] = Request.QueryString.Get("cat");

                return ViewState["AdminRecServicio_IdCategoriaReclamo"].ToString();
            }
            set
            {
                ViewState["AdminRecServicio_IdCategoriaReclamo"] = value;
            }
        }

        public string CategoriaReclamo
        {
            get
            {
                return lblCategoriaReclamo.Text;
            }
            set
            {
                lblCategoriaReclamo.Text = value;
            }
        }

        public string Area
        {
            get
            {
                return lblArea.Text;
            }
            set
            {
                lblArea.Text = value;
            }
        }

        public string PedidoRemisionFactura
        {
            get
            {
                return txtPedidoFacturaRemision.Text;
            }
            set
            {
                txtPedidoFacturaRemision.Text = value;
            }
        }

        public int DiarioInventario
        {
            get
            {
                return txtDiarioInventario.ValueInt;
            }
            set
            {
                txtDiarioInventario.ValueInt = value;
            }
        }

        public string Planta
        {
            get
            {
                return wddPlanta.SelectedValue;
            }
            set
            {
                wddPlanta.SelectedValue = value;
            }
        }

        public DateTime? FechaPedido
        {
            get
            {
                var oReturn = new DateTime();

                if (string.IsNullOrEmpty(txtFechaPedido.Text))
                    return null;
                
                if (!DateTime.TryParse(txtFechaPedido.Text, out oReturn))
                    return null;

                return Convert.ToDateTime(txtFechaPedido.Text);
            }
            set
            {
                if (value.HasValue && !value.Value.Equals(DateTime.MinValue))
                    txtFechaPedido.Text = value.GetValueOrDefault().ToString("dd/MM/yyyy");
                else
                    txtFechaPedido.Text = string.Empty;
            }
        }

        public DateTime? FechaCompromiso
        {
            get
            {
                var oReturn = new DateTime();

                if (string.IsNullOrEmpty(txtFechaCompromiso.Text))
                    return null;

                if (!DateTime.TryParse(txtFechaCompromiso.Text, out oReturn))
                    return null;

                return Convert.ToDateTime(txtFechaCompromiso.Text);
            }
            set
            {
                if (value.HasValue && !value.Value.Equals(DateTime.MinValue))
                    txtFechaCompromiso.Text = value.GetValueOrDefault().ToString("dd/MM/yyyy");
                else
                    txtFechaCompromiso.Text = string.Empty;                
            }
        }

        public DateTime? FechaRealEntrega
        {
            get
            {
                var oReturn = new DateTime();

                if (string.IsNullOrEmpty(txtFechaRealEntrega.Text))
                    return null;

                if (!DateTime.TryParse(txtFechaRealEntrega.Text, out oReturn))
                    return null;

                return Convert.ToDateTime(txtFechaRealEntrega.Text);
            }
            set
            {
                if (value.HasValue && !value.Value.Equals(DateTime.MinValue))
                    txtFechaRealEntrega.Text = value.GetValueOrDefault().ToString("dd/MM/yyyy");
                else
                    txtFechaRealEntrega.Text = string.Empty;                
            }
        }

        public int DiasIncumplimiento
        {
            get
            {
                if (string.IsNullOrEmpty(lblDiasIncumplimiento.Text))
                    return 0;

                return Convert.ToInt32(lblDiasIncumplimiento.Text);
            }
            set
            {
                lblDiasIncumplimiento.Text = string.Format("{0}", value);
            }
        }

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        public int IdResponsableCategoriaReclamo
        {
            get
            {
                if (ViewState["AdminServicio2_IdResponsableCategoriaReclamo"] == null)
                    ViewState["AdminServicio2_IdResponsableCategoriaReclamo"] = 0;

                return Convert.ToInt32(ViewState["AdminServicio2_IdResponsableCategoriaReclamo"]);
            }
            set
            {
                ViewState["AdminServicio2_IdResponsableCategoriaReclamo"] = value;
            }
        }

        #endregion

        #endregion
    }
}