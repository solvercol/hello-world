using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCReadingReclamoServicioT4Detalle : ViewUserControl<ReadingReclamoServicioT4DetallePresenter, IReadingReclamoServicioT4DetalleView>, IReadingReclamoServicioT4DetalleView, IReclamoWebUserControl
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

        public string SubCategoriaReclamo
        {
            get
            {
                return lblSubCategoria.Text;
            }
            set
            {
                lblSubCategoria.Text = value;
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