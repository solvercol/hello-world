using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCReadingReclamoServicioT1Detalle : ViewUserControl<ReadingReclamoServicioT1DetallePresenter, IReadingReclamoServicioT1DetalleView>, IReadingReclamoServicioT1DetalleView, IReclamoWebUserControl
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

        public int NoRecordatorios
        {
            get
            {
                return 0;
            }
            set
            {
                lblNoRecordatorios.Text = string.Format("{0}", value);
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

        public bool RespuestaInmediata
        {
            get
            {
                return true;
            }
            set
            {
                lblRespuestaInmediata.Text = value ? "SI" : "NO";
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