﻿using System;
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
using Infragistics.Web.UI.ListControls;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminRecServicioT1 : ViewUserControl<AdminRecServicioT1Presenter, IAdminRecServicioT1View>, IAdminRecServicioT1View
    {
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
                Response.Redirect(string.Format("../Admin/FrmListaGeneralReclamos.aspx?ModuleId={0}", ModuleId));
            else
                Response.Redirect(string.Format("../Admin/FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}", ModuleId, IdReclamo));
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (SelectedCliente.CodigoCliente == null)
                messages.Add("Es necesario seleccionar un cliente para la creación del reclamo");

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
                return;
            }

            if (string.IsNullOrEmpty(IdReclamo))
                Presenter.SaveReclamo();
            else
                Presenter.UpdateReclamo();
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

        public int NoRecordatorios
        {
            get
            {
                return txtNoRecordatorios.ValueInt;
            }
            set
            {
                txtNoRecordatorios.ValueInt = value;
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

        public bool RespuestaInmediata
        {
            get
            {
                return Convert.ToBoolean(rblRespuestaInmediata.SelectedValue);
            }
            set
            {
                rblRespuestaInmediata.SelectedValue = value.ToString();
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

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        #endregion

        #endregion
    }
}