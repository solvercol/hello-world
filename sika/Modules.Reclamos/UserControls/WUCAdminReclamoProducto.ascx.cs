﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModule.Reclamos.Enum;
using Domain.MainModules.Entities;
using Infragistics.Web.UI.ListControls;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminReclamoProducto : ViewUserControl<AdminReclamoProductoPresenter, IAdminReclamoProductoView>, IAdminReclamoProductoView
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
            IdTipoReclamo = TipoReclamo.Producto;
            ucFilterClient.PostBackEvent += WucPostBackEvent;
            ucFilterClient.SelectClient += WucClientSelectEvent;
        }

        #endregion

        #region DropDownList

        protected void WddAsesor_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedCliente.CodigoCliente))
                Presenter.LoadUnidadZonaAsesor();
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
            
            if (SelectedProduct.CodigoProducto == null)
                messages.Add("Es necesario seleccionar un producto.");
            
            if (SelectedCliente.CodigoCliente == null)
                messages.Add("Es necesario seleccionar un cliente.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            if (string.IsNullOrEmpty(Planta))
                messages.Add("Es necesario ingresar una planta.");

            if (string.IsNullOrEmpty(UnidadZona))
                messages.Add("La unidad y zona del cliente son necesarios.");

            if (string.IsNullOrEmpty(NombreContacto))
                messages.Add("El nombre del contacto es obligatorio.");

            if (string.IsNullOrEmpty(EmailContacto))
                messages.Add("El email del contacto es obligatorio.");

            if (!string.IsNullOrEmpty(EmailContacto) && !IsValidEmail(EmailContacto))
                messages.Add("El mail de contacto ingresado no se encuentra con una estructura correcta.");

            if (!string.IsNullOrEmpty(EmailQuienAplica) && !IsValidEmail(EmailQuienAplica))
                messages.Add("El mail de quien aplica ingresado no se encuentra con una estructura correcta.");

            if (!string.IsNullOrEmpty(EmailPropietario) && !IsValidEmail(EmailPropietario))
                messages.Add("El mail de quien aplica ingresado no se encuentra con una estructura correcta.");

            if (string.IsNullOrEmpty(NumeroLote) && string.IsNullOrEmpty(NumeroLote2) && string.IsNullOrEmpty(NumeroLote3))
                messages.Add("Es necesario ingresar por lo menos un numero de lote.");

            if (string.IsNullOrEmpty(Diagnostico))
                messages.Add("El diagnostico previo del problema es requerido.");

            if (string.IsNullOrEmpty(DescripcionProblema))
                messages.Add("La descripción del problema es requerida.");

            if (AspectoExteriorEnvase == "Malo" && string.IsNullOrEmpty(DescripcionAspectoEnvase))
                messages.Add("Es necesario ingresar una descripción del aspecto exterior del envase.");

            if (AspectoProducto == "Anormal" && string.IsNullOrEmpty(DescripcionAspectoProducto))
                messages.Add("Es necesario ingresar una descripción del aspecto del producto.");

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
            wddAsesor.DataTextField = "Asesor";
            wddAsesor.DataValueField = "IdUser";
            wddAsesor.DataBind();
        }

        public void LoadAtendidoPor(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddReclamoAtentidoPor.DataSource = items;
            wddReclamoAtentidoPor.DataTextField = "Nombres";
            wddReclamoAtentidoPor.DataValueField = "IdUser";
            wddReclamoAtentidoPor.DataBind();            
        }

        public void LoadPlantas(List<DTO_ValueKey> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Value).ToList();
            }
            wddPlanta.DataSource = items;
            wddPlanta.DataTextField = "Id";
            wddPlanta.DataValueField = "Value";
            wddPlanta.DataBind();

            wddPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" ", ""));
        }

        public void SetSelectedProduct(Dto_Producto producto)
        {
            ucFilterProduct.SelectedProduct = producto;
            ucFilterProduct.LoadSelectedProducto(producto);
        }

        public void SetSelectedClient(Dto_Cliente cliente)
        {
            ucFilterClient.SelectedClient = cliente;
            ucFilterClient.NombreCliente = cliente.NombreCliente;
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

        public int CantidadVendidaUnidad
        {
            get
            {
                return txtCantidadVendidaUnidad.ValueInt;
            }
            set
            {
                txtCantidadVendidaUnidad.ValueInt = value;
            }
        }

        public int CantidadReclamadaUnidad
        {
            get
            {
                return txtCantidadReclamadaUnidad.ValueInt;
            }
            set
            {
                txtCantidadReclamadaUnidad.ValueInt = value;
            }
        }

        public bool Aplicado
        {
            get
            {
                return Convert.ToBoolean(rblAplicado.SelectedValue);
            }
            set
            {
                rblAplicado.SelectedValue = value.ToString();
            }
        }

        public DateTime FechaVenta
        {
            get
            {
                if (string.IsNullOrEmpty(txtFechaVenta.Text))
                    return DateTime.Now;

                return Convert.ToDateTime(txtFechaVenta.Text);
            }
            set
            {
                txtFechaVenta.Text = value.ToString("dd/MM/yyyy");
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

        public string NombreObra
        {
            get
            {
                return txtNombreObra.Text;
            }
            set
            {
                txtNombreObra.Text = value;
            }
        }

        public string AplicadoPor
        {
            get
            {
                return txtAplicadoPor.Text;
            }
            set
            {
                txtAplicadoPor.Text = value;
            }
        }

        public string PropietarioObra
        {
            get
            {
                return txtPropietarioObra.Text;
            }
            set
            {
                txtPropietarioObra.Text = value;
            }
        }

        public string EmailQuienAplica
        {
            get
            {
                return txtEmailQuienAplica.Text;
            }
            set
            {
                txtEmailQuienAplica.Text = value;
            }
        }

        public string EmailPropietario
        {
            get
            {
                return txtEmailPropietario.Text;
            }
            set
            {
                txtEmailPropietario.Text = value;
            }
        }

        public string AspectoExteriorEnvase
        {
            get
            {
                return rblAspectoExteriorEnvase.SelectedValue;
            }
            set
            {
                rblAspectoExteriorEnvase.SelectedValue = value;
            }
        }

        public string AspectoProducto
        {
            get
            {
                return rblAspectoProducto.SelectedValue;
            }
            set
            {
                rblAspectoProducto.SelectedValue = value;
            }
        }

        public string DescripcionAspectoEnvase
        {
            get
            {
                return txtDescripcionAspectoEnvase.Text;
            }
            set
            {
                txtDescripcionAspectoEnvase.Text = value;
            }
        }

        public string DescripcionAspectoProducto
        {
            get
            {
                return txtDescripcionAspectoProducto.Text;
            }
            set
            {
                txtDescripcionAspectoProducto.Text = value;
            }
        }

        public string NumeroLote
        {
            get
            {
                return txtNumeroLote.Text;
            }
            set
            {
                txtNumeroLote.Text = value;
            }
        }

        public string NumeroLote2
        {
            get
            {
                return txtNumeroLote2.Text;
            }
            set
            {
                txtNumeroLote2.Text = value;
            }
        }

        public string NumeroLote3
        {
            get
            {
                return txtNumeroLote3.Text;
            }
            set
            {
                txtNumeroLote3.Text = value;
            }
        }

        public bool MuestraDisponible
        {
            get
            {
                return Convert.ToBoolean(rblMuestraDisponible.SelectedValue);
            }
            set
            {
                rblMuestraDisponible.SelectedValue = value.ToString();
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

        public string Diagnostico
        {
            get
            {
                return txtDiagnostico.Text;
            }
            set
            {
                txtDiagnostico.Text = value;
            }
        }

        public string ConclusionesPrevias
        {
            get
            {
                return txtConclusionesPrevias.Text;
            }
            set
            {
                txtConclusionesPrevias.Text = value;
            }
        }

        public string Solucion
        {
            get
            {
                return txtObservacionesSolucion.Text;
            }
            set
            {
                txtObservacionesSolucion.Text = value;
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

        public Dto_Producto SelectedProduct
        {
            get { return ucFilterProduct.SelectedProduct; }
        }

        public Dto_Cliente SelectedCliente
        {
            get { return ucFilterClient.SelectedClient; }
        }

        public string ConsecutivoReclamo
        {
            get
            {
                if (ViewState["AdminReclamo_ConsecutivoReclamo"] == null)
                    ViewState["AdminReclamo_ConsecutivoReclamo"] = string.Empty;

                return ViewState["AdminReclamo_ConsecutivoReclamo"].ToString();
            }
            set
            {
                ViewState["AdminReclamo_ConsecutivoReclamo"] = value;
            }
        }

        public int IdTipoReclamo
        {
            get
            {
                if (ViewState["AdminReclamo_IdTipoReclamo"] == null)
                    ViewState["AdminReclamo_IdTipoReclamo"] = TipoReclamo.Producto;

                return Convert.ToInt32(ViewState["AdminReclamo_IdTipoReclamo"]);
            }
            set
            {
                ViewState["AdminReclamo_IdTipoReclamo"] = value;
            }
        }

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        public bool ProblemaSolucionado
        {
            get
            {
                return Convert.ToBoolean(rblSolucionado.SelectedValue);
            }
            set
            {
                rblSolucionado.SelectedValue = value.ToString();
            }
        }

        #endregion

        #endregion
    }
}