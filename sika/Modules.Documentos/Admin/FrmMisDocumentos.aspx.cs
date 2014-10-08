using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Microsoft.Reporting.WebForms;
using Presenters.Documentos.IViews;
using Presenters.Documentos.Presenters;

namespace Modules.Documentos.Admin
{
    public partial class FrmMisDocumentos
        : ViewPage<VistaMisDocumentosPresenter, IVistaMisDocumentosView>, IVistaMisDocumentosView
    {

        public string QueryStringFrom
        {
            get { return Request.QueryString.Get("from"); }
        }      

        public string FiltroNombre
        {
            get
            {
                return txtFiltroNombre.Text;
            }
            set
            {
                txtFiltroNombre.Text = value;
            }
        }

        public int FiltroIdEstado
        {
            get
            {
                return Convert.ToInt32(ddlEstado.SelectedValue);
            }
            set
            {
                ddlEstado.SelectedIndex = -1;
                ddlEstado.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        public event EventHandler FilterEvent;
        public event EventHandler ClearFilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Mis Documentos");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnNuevoClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditarDocumento.aspx{0}{1}", GetBaseQueryString(), "&from=misdocs"));
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        protected void BtnClearFilterClick(object sender, EventArgs e)
        {
            if (ClearFilterEvent != null)
                ClearFilterEvent(null, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }              

        public void Estados(IEnumerable<TBL_ModuloDocumentos_Estados> estados)
        {
            ddlEstado.Items.Clear();
            foreach (var est in estados)
                ddlEstado.Items.Add(new ListItem(est.Nombre, est.IdEstado.ToString()));
            var li = new ListItem("Todos", "0");
            ddlEstado.Items.Insert(0, li);
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string ServerHostPath
        {
            get
            {
                string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
                if (port == null || port == "80" || port == "443")
                    port = "";
                else
                    port = ":" + port;

                string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
                if (protocol == null || protocol == "0")
                    protocol = "http://";
                else
                    protocol = "https://";

                string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

                if (sOut.EndsWith("/"))
                {
                    sOut = sOut.Substring(0, sOut.Length - 1);
                }

                return sOut;
            }
        }

        public void LoadView(DataTable dt)
        {
            rptView.Reset();
            rptView.LocalReport.DataSources.Clear();
            rptView.ProcessingMode = ProcessingMode.Local;
            rptView.LocalReport.ReportPath = Server.MapPath(@"~/Pages/Modules/Documentos/Resources/ReportViewer/View_MisDocumentos.rdlc");
            rptView.LocalReport.EnableHyperlinks = true;
            rptView.LocalReport.DataSources.Add(new ReportDataSource("DS_Report", dt));
            rptView.DataBind();
            rptView.LocalReport.Refresh();
        }
    }
}