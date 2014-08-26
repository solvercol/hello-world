using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.UserControls
{
    public partial class WucPanelEstado : ViewUserControl<PanelEstadosPresenter, IPanelEstadosView>, IPanelEstadosView
    {
        

        public event EventHandler UpdateEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string IdReclamo
        {
            get { return Request.QueryString["IdReclamo"]; }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

      

        public string Estado
        {
            get { return litestado.Text; }
            set { litestado.Text = value; }
        }

        public string SolicitadoPor
        {
            get { return litSolicitadoPor.Text; }
            set { litSolicitadoPor.Text = value; }
        }

        public string FechaSolicitud
        {
            get { return litFechaSolicitud.Text; }
            set { litFechaSolicitud.Text = value; }
        }

        public string Responsable
        {
            get { return litResponsable.Text; }
            set { litResponsable.Text = value; }
        }

        public string AsignadoEn
        {
            get { return litAsignadoEn.Text; }
            set { litAsignadoEn.Text = value; }
        }

        public string NumeroDias
        {
            get { return litNumeroDias.Text; }
            set { litNumeroDias.Text = value; }
        }


        public void ActualizarPanelResumen()
        {
            if (UpdateEvent != null)
                UpdateEvent(null, EventArgs.Empty);
        }
    }
}