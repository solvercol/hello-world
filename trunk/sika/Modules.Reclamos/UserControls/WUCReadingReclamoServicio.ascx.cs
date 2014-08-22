using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCReadingReclamoServicio : ViewUserControl<ReadingReclamoServicioPresenter, IReadingReclamoServicioView>, IReadingReclamoServicioView
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

        public string Categoria
        {
            get
            {
                return lblNombreCategoria.Text;
            }
            set
            {
                lblNombreCategoria.Text = value;
            }
        }

        #endregion

        #endregion
    }
}