using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using ServerControls;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCLogReclamoView : ViewUserControl<LogReclamoViewPresenter, ILogReclamoView>, ILogReclamoView
    {
        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RptListadoItemDatBound(object sender, RepeaterItemEventArgs e)
        {
            var log = e.Item.DataItem as TBL_ModuloReclamos_LogReclamos;
            if (log == null) return;

            var litDate = e.Item.FindControl("litDate") as Literal;
            if (litDate != null) litDate.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", log.CreateOn);

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null) litDescripcion.Text = string.Format("{0}", log.Descripcion);
            
        }

        protected void PgrListLogPageChanged(object sender, PageChanged e)
        {
            if (FilterEvent == null) return;
            FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        public string IdReclamo
        {
            get
            {
                return Request.QueryString.Get("IdReclamo");
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

        public void RefreshInfo()
        {
            if (FilterEvent != null)
                FilterEvent(0, EventArgs.Empty);
        }


        public void LogsList(List<TBL_ModuloReclamos_LogReclamos> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public bool IsValid
        {
            get { return true; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void CargarLog()
        {
            if (FilterEvent == null) return;
            FilterEvent(null, EventArgs.Empty);
        }
    }
}