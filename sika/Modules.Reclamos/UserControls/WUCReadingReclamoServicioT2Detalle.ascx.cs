using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCReadingReclamoServicioT2Detalle : ViewUserControl<ReadingReclamoServicioT2DetallePresenter, IReadingReclamoServicioT2DetalleView>, IReadingReclamoServicioT2DetalleView, IReclamoWebUserControl
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

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
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

        public string PedidoRemisionFactura
        {
            get
            {
                return lblPedidoFacturaRemision.Text;
            }
            set
            {
                lblPedidoFacturaRemision.Text = value;
            }
        }

        public int DiarioInventario
        {
            get
            {
                return 0;
            }
            set
            {
                lblDiarioInventario.Text = string.Format("{0}", value);
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

        public string NombreCliente
        {
            get
            {
                return lblCliente.Text;
            }
            set
            {
                lblCliente.Text = value;
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

        public DateTime FechaPedido
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))
                    lblFechaPedido.Text = value.ToString("dd/MM/yyyy");
                else
                    lblFechaPedido.Text = string.Empty;
            }
        }

        public DateTime FechaCompromiso
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))
                    lblFechaCompromiso.Text = value.ToString("dd/MM/yyyy");
                else
                    lblFechaCompromiso.Text = string.Empty;                
            }
        }

        public DateTime FechaRealEntrega
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))
                    lblFechaRealEntrega.Text = value.ToString("dd/MM/yyyy");
                else
                    lblFechaRealEntrega.Text = string.Empty;                
            }
        }

        public int DiasIncumplimiento
        {
            get
            {
                return 0;
            }
            set
            {
                lblDiasIncuplimiento.Text = string.Format("{0}", value);
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
    }
}