using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCReadingReclamoProductoDetalle : ViewUserControl<ReadingReclamoProductoDetallePresenter, IReadingReclamoProductoDetalleView>, IReadingReclamoProductoDetalleView, IReclamoWebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region IReclamoWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

        public event Action RiseFatherPostback;

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        public string Asesor
        {
            get
            {
                return lblAsesor.Text;
            }
            set
            {
                lblAsesor.Text = value;
            }
        }

        public string AtendidoPor
        {
            get
            {
                return lblAtendidoPor.Text;
            }
            set
            {
                lblAtendidoPor.Text = value;
            }
        }

        public int CantidadVendidaUnidad
        {
            get
            {
                return Convert.ToInt32(lblCantidadUnidadVendida.Text);
            }
            set
            {
                lblCantidadUnidadVendida.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public int CantidadReclamadaUnidad
        {
            get
            {
                return Convert.ToInt32(lblCantidadReclamadaUnidad.Text);
            }
            set
            {
                lblCantidadReclamadaUnidad.Text = string.Format("{0:0,0.0}", value);
            }
        }

        public bool Aplicado
        {
            get
            {
                return true;
            }
            set
            {
                lblAplicado.Text = value ? "SI" : "NO";
            }
        }

        public bool Solucionado
        {
            get
            {
                return true;
            }
            set
            {
                lblSolucionado.Text = value ? "SI" : "NO";
            }
        }

        public DateTime FechaVenta
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                lblFechaVenta.Text = string.Format("{0:dd/MM/yyyy}", value);
            }
        }        

        public string TipoContacto
        {
            get
            {
                return lblTipoContacto.Text;
            }
            set
            {
                lblTipoContacto.Text = value;
            }
        }

        public string Planta
        {
            get
            {
                return lblPlanta.Text;
            }
            set
            {
                lblPlanta.Text = value;
            }
        }

        public string NombreCliente
        {
            get
            {
                return lblNombreCliente.Text;
            }
            set
            {
                lblNombreCliente.Text = value;
            }
        }

        public string NombreContacto
        {
            get
            {
                return lblNombreContacto.Text;
            }
            set
            {
                lblNombreContacto.Text = value;
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

        public string EmailContacto
        {
            get
            {
                return lblEmailContacto.Text;
            }
            set
            {
                lblEmailContacto.Text = value;
            }
        }

        public string NombreObra
        {
            get
            {
                return lblNombreObra.Text;
            }
            set
            {
                lblNombreObra.Text = value;
            }
        }

        public string AplicadoPor
        {
            get
            {
                return lblAplicadoPor.Text;
            }
            set
            {
                lblAplicadoPor.Text = value;
            }
        }

        public string PropietarioObra
        {
            get
            {
                return lblPropietarioObra.Text;
            }
            set
            {
                lblPropietarioObra.Text = value;
            }
        }

        public string EmailQuienAplica
        {
            get
            {
                return lblEmailQuienAplica.Text;
            }
            set
            {
                lblEmailQuienAplica.Text = value;
            }
        }

        public string EmailPropietario
        {
            get
            {
                return lblEmailPropietario.Text;
            }
            set
            {
                lblEmailPropietario.Text = value;
            }
        }

        public string AspectoExteriorEnvase
        {
            get
            {
                return lblAspectoExteriorEnvase.Text;
            }
            set
            {
                lblAspectoExteriorEnvase.Text = value;
            }
        }

        public string AspectoProducto
        {
            get
            {
                return lblAspectoProducto.Text;
            }
            set
            {
                lblAspectoProducto.Text = value;
            }
        }

        public string DescripcionAspectoEnvase
        {
            get
            {
                return lblDescripcionAspectoEnvase.Text;
            }
            set
            {
                lblDescripcionAspectoEnvase.Text = value;
            }
        }

        public string DescripcionAspectoProducto
        {
            get
            {
                return lblDescripcionAspectoProducto.Text;
            }
            set
            {
                lblDescripcionAspectoProducto.Text = value;
            }
        }

        public string NumeroLote
        {
            get
            {
                return lblNumeroLote.Text;
            }
            set
            {
                lblNumeroLote.Text = value;
            }
        }

        public string NumeroLote2
        {
            get
            {
                return lblNumeroLote2.Text;
            }
            set
            {
                lblNumeroLote2.Text = value;
            }
        }

        public string NumeroLote3
        {
            get
            {
                return lblNumeroLote3.Text;
            }
            set
            {
                lblNumeroLote3.Text = value;
            }
        }

        public bool MuestraDisponible
        {
            get
            {
                return true;
            }
            set
            {
                lblMuestraDisponible.Text = value ? "SI" : "NO";
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

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string NombreProducto
        {
            get
            {
                return litNombreProducto.Text;
            }
            set
            {
                litNombreProducto.Text = value;
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
    }
}