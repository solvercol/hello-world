using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;
using Modules.AccionesPC.UI;

namespace Modules.AccionesPC.UserControls
{
    public partial class WUCAdminInformacionSolicitud : ViewUserControl<AdminInformacionSolicitudPresenter, IAdminInformacionSolicitudView>, IAdminInformacionSolicitudView, ISolicitudWebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string IdSolicitud
        {
            get { return Request.QueryString.Get("IdSolicitud"); }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string ProcesoAsociado
        {
            get
            {
                return lblProcesoAsociado.Text;
            }
            set
            {
                lblProcesoAsociado.Text = value;
            }
        }

        public string DescripcionAccion
        {
            get
            {
                return lblDescripcionAccion.Text;
            }
            set
            {
                lblDescripcionAccion.Text = value;
            }
        }

        public string ResultadoCierre
        {
            get
            {
                return rblResultadoCierreSolicitud.SelectedValue;
            }
            set
            {
                rblResultadoCierreSolicitud.SelectedValue = value;
            }
        }

        public string ObservacionesCierre
        {
            get
            {
                return lblObservacionesCierre.Text;
            }
            set
            {
                lblObservacionesCierre.Text = value;
            }
        }

        public bool ShowInfoCierre
        {
            get
            {
                return trCierreInfo.Visible;
            }
            set
            {
                trCierreInfo.Visible = value;
                trCierreTitle.Visible = value;
            }
        }

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        public event Action RiseFatherPostback;
    }
}