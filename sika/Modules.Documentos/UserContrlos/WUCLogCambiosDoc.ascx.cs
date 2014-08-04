using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Documentos.IViews;
using Presenters.Documentos.Presenters;
using ServerControls;

namespace Modules.Documentos.UserContrlos
{
    public partial class WUCLogCambiosDoc
        : ViewUserControl<LogCambiosDocPresenter, ILogCambiosDocView>, ILogCambiosDocView
    {
        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RptListadoItemDatBound(object sender, RepeaterItemEventArgs e)
        {
            var log = e.Item.DataItem as TBL_ModuloDocumentos_LogCambios;
            if (log == null) return;
            var litDate = e.Item.FindControl("litDate") as Literal;
            if (litDate == null) return;

            litDate.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", log.CreateOn);
            
            var HyprLnkVerHistDoc = e.Item.FindControl("HyprLnkVerHistDoc") as HyperLink;
            if (HyprLnkVerHistDoc == null) return;
            if (log.IdHistorial != null)
                HyprLnkVerHistDoc.NavigateUrl = string.Format("~/pages/modules/documentos/Consulta/FrmVerHistDocumento.aspx?IdHistDocumento={0}", log.IdHistorial);
            else
                HyprLnkVerHistDoc.Visible = false;
        }

        protected void PgrListLogPageChanged(object sender, PageChanged e)
        {
            if (FilterEvent == null) return;
            FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        public int IdDocumento
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["IdDocumento"]))
                    id = Convert.ToInt32(Request.QueryString["IdDocumento"]);
                return id;

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


        public void LogsList(List<TBL_ModuloDocumentos_LogCambios> items)
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
    }
}