using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.UserControls
{
    public partial class WucSeguimiento : ViewUserControl<SeguimientoPresenter, ISeguimientoView>, ISeguimientoView
    {
        public event EventHandler RefreshEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RptListadoItemDatBound(object sender, RepeaterItemEventArgs e)
        {
            var track = e.Item.DataItem as TBL_ModuloReclamos_Tracking;
            if (track == null) return;
            var litDate = e.Item.FindControl("litDate") as Literal;
            if (litDate == null) return;

            litDate.Text = string.Format("{0} - {1}", track.CreateOn.GetValueOrDefault().ToShortDateString(), track.CreateOn.GetValueOrDefault().ToShortTimeString());
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        
        public string IdReclamo
        {
            get { return Request.QueryString["IdReclamo"]; }
        }

        public void ListadoSeguimiento(List<TBL_ModuloReclamos_Tracking> itemS)
        {
            rptListado.DataSource = itemS;
            rptListado.DataBind();
        }

        public void Actualizarlistado()
        {
            if (RefreshEvent != null)
                RefreshEvent(null, EventArgs.Empty);
        }
    }
}