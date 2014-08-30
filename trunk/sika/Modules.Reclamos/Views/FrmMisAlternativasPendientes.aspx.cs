using System;
using System.Data;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Microsoft.Reporting.WebForms;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Views
{
    public partial class FrmMisAlternativasPendientes : ViewPage<MisAlternativasPendientesPresenter, IMisAlternativasPendientesView>, IMisAlternativasPendientesView
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Mis Alternativas Pendientes");
        }

        #endregion

        #region Buttons

        #endregion


        #endregion

        #region Methods
        #endregion

        #region View Members

        #region Members

        public void LoadViewReclamos(DataTable dt)
        {
            rptReclamos.Reset();
            rptReclamos.LocalReport.DataSources.Clear();
            rptReclamos.ProcessingMode = ProcessingMode.Local;
            rptReclamos.LocalReport.ReportPath = Server.MapPath(@"~/Pages/Modules/Reclamos/Resources/ReportViewer/RptVistaMisAlternativasPendientes.rdlc");
            rptReclamos.LocalReport.EnableHyperlinks = true;
            rptReclamos.LocalReport.DataSources.Add(new ReportDataSource("DS_Report", dt));
            rptReclamos.DataBind();
            rptReclamos.LocalReport.Refresh();
        }

        #endregion

        #region Properties

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

        #endregion

        #endregion
    }
}