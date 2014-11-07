using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;

namespace Modules.AccionesPC.UserControls
{
    public partial class WucSeguimiento : ViewUserControl<SeguimientoPresenter, ISeguimientoView>, ISeguimientoView
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public event EventHandler RefreshEvent;

        public string IdSolicitud
        {
            get { return Request.QueryString["IdSolicitud"]; }
        }

        public void ListadoSeguimiento(List<TBL_ModuloAPC_Tracking> itemS)
        {
            rptListado.DataSource = itemS;
            rptListado.DataBind();
        }

        protected void RptListadoItemDatBound(object sender, RepeaterItemEventArgs e)
        {
            var track = e.Item.DataItem as TBL_ModuloAPC_Tracking;
            if (track == null) return;
            var litDate = e.Item.FindControl("litDate") as Literal;
            if (litDate == null) return;

            litDate.Text = string.Format("{0} - {1}", track.CreateOn.GetValueOrDefault().ToShortDateString(), track.CreateOn.GetValueOrDefault().ToShortTimeString());
        }

        public void Actualizarlistado()
        {
            if (RefreshEvent != null)
                RefreshEvent(null, EventArgs.Empty);
        }
    }
}