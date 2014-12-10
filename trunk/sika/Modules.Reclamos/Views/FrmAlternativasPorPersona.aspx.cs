using System;
using System.Data;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Microsoft.Reporting.WebForms;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Views
{
    public partial class FrmAlternativasPorPersona : ViewPage<AlternativasPorPersonaPresenter, IAlternativasPorPersonaView>, IAlternativasPorPersonaView
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Alternativas Por Persona");
        }

        #endregion

        #region Buttons

        protected void BtnFilterReclamos_Click(object sender, EventArgs e)
        {
            Presenter.LoadReport();
        }

        #endregion

        #endregion

        #region Methods
        #endregion

        #region View Members

        #region Members

        public void LoadView(DataTable dt)
        {
            rptReclamos.Reset();
            rptReclamos.LocalReport.DataSources.Clear();
            rptReclamos.ProcessingMode = ProcessingMode.Local;
            rptReclamos.LocalReport.ReportPath = Server.MapPath(@"~/Pages/Modules/Reclamos/Resources/ReportViewer/RptVistaAlternativasPorPersona.rdlc");
            rptReclamos.LocalReport.EnableHyperlinks = true;
            rptReclamos.LocalReport.EnableExternalImages = true;
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

        public string FilterNoReclamo
        {
            get
            {
                return txtNoReclamo.Text;
            }
            set
            {
                txtNoReclamo.Text = value;
            }
        }

        public string FilterCliente
        {
            get
            {
                return txtCliente.Text;
            }
            set
            {
                txtCliente.Text = value;
            }
        }

        public string FilterProducto
        {
            get
            {
                return txtProducto.Text;
            }
            set
            {
                txtProducto.Text = value;
            }
        }

        public string FilterServicio
        {
            get
            {
                return txtServicio.Text;
            }
            set
            {
                txtServicio.Text = value;
            }
        }

        public DateTime FechaFilterFrom
        {
            get
            {
                var sDate = wdpFiltroDateFrom.Text.Split('-');
                return new DateTime(Convert.ToInt32(sDate[0]), Convert.ToInt32(sDate[1]), 1);
            }
            set
            {
                wdpFiltroDateFrom.Text = value.ToString("yyyy-MM");
            }
        }

        public DateTime FechaFilterTo
        {
            get
            {
                var sDate = wdpFiltroDateTo.Text.Split('-');
                return new DateTime(Convert.ToInt32(sDate[0]), Convert.ToInt32(sDate[1]), 1).AddMonths(1).AddDays(-1);
            }
            set
            {
                wdpFiltroDateTo.Text = value.ToString("yyyy-MM");
            }
        }

        #endregion

        #endregion
    }
}