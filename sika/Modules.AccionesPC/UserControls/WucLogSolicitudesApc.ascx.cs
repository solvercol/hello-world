using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;
using ServerControls;

namespace Modules.AccionesPC.UserControls
{
    public partial class WucLogSolicitudesApc : ViewUserControl<LogSolicitudPresenter, ILogSolicitudesView>, ILogSolicitudesView
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public event EventHandler FilterEvent;
        public void LogsList(List<TBL_ModuloAPC_LogSolicitud> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        public string IdSolicitud
        {
            get
            {
                return Request.QueryString.Get("IdSolicitud");
            }
        }

        public bool IsLoadedControl
        {
            get
            {
                if (ViewState["LoadedControl"] == null)
                    ViewState["LoadedControl"] = false;
                return (bool)ViewState["LoadedControl"];
            }
            set { ViewState["LoadedControl"] = value; }
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageSize
        {
            get { return pgrListado.PageSize; }
        }

        protected void RptListadoItemDatBound(object sender, RepeaterItemEventArgs e)
        {
            var log = e.Item.DataItem as TBL_ModuloAPC_LogSolicitud;
            if (log == null) return;

            var litDate = e.Item.FindControl("litDate") as Literal;
            if (litDate != null) litDate.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", log.CreateOn);

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null) litDescripcion.Text = string.Format("{0}", log.Descripcion);

        }

        protected void PgrListadoPageChanged(object sender, PageChanged e)
        {
            if (FilterEvent == null) return;
            FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        public void RefreshInfo()
        {
            if (FilterEvent != null)
                FilterEvent(0, EventArgs.Empty);
        }
    }
}