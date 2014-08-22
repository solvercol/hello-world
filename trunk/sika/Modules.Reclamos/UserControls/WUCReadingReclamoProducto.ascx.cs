using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCReadingReclamoProducto : ViewUserControl<ReadingReclamoProductoPresenter, IReadingReclamoProductoView>, IReadingReclamoProductoView
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #endregion

        #region Methods

        #endregion

        #region View Members

        #region Methods       

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

        public string NombreProducto
        {
            get
            {
                return lblNombreProducto.Text;
            }
            set
            {
                lblNombreProducto.Text = value;
            }
        }

        public string TargetMarket
        {
            get
            {
                return lblTargetMarket.Text;
            }
            set
            {
                lblTargetMarket.Text = value;
            }
        }

        public string CampoAplicacion
        {
            get
            {
                return lblCampoAplicacion.Text;
            }
            set
            {
                lblCampoAplicacion.Text = value;
            }
        }

        public string SubCampoAplicacion
        {
            get
            {
                return lblSubCampoAplicacion.Text;
            }
            set
            {
                lblSubCampoAplicacion.Text = value;
            }
        }

        public string Presentacion
        {
            get
            {
                return lblPresentacion.Text;
            }
            set
            {
                lblPresentacion.Text = value;
            }
        }
       
        #endregion

        #endregion
    }
}