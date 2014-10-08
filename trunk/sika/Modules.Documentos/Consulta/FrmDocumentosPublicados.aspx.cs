using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.MainModule.Documentos.IServices;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Presenters.Documentos.IViews;
using Presenters.Documentos.Presenters;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace Modules.Documentos.Consulta
{
    public partial class FrmDocumentosPublicados
        : ViewPage<VistaDocumentosPublicadosPresenter, IVistaDocumentosPublicadosView>, IVistaDocumentosPublicadosView
    {
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

        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Documentos Publicados");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
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