using System;
using System.Collections.Generic;
using System.Data;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Microsoft.Reporting.WebForms;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;
using System.Web.UI.WebControls;

namespace Modules.AccionesPC.Views
{
    public partial class FrmMisActividadesPendientes : ViewPage<MisActividadesPendientesPresenter, IMisActividadesPendientesView>, IMisActividadesPendientesView
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Mis Actividades");
        }

        #endregion

        #region Buttons

        protected void BtnFilterReclamos_Click(object sender, EventArgs e)
        {
            Presenter.LoadView();
        }

        #endregion

        #region DropDownList

        protected void DdlArea_ValueChanged(object sender, EventArgs e)
        {
            Presenter.LoadProcesos();
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
            rptReclamos.LocalReport.ReportPath = Server.MapPath(@"~/Pages/Modules/AccionesPC/Resources/ReportViewer/RptMisActividadesPendientes.rdlc");
            rptReclamos.LocalReport.EnableHyperlinks = true;
            rptReclamos.LocalReport.DataSources.Add(new ReportDataSource("DS_Report", dt));
            rptReclamos.DataBind();
            rptReclamos.LocalReport.Refresh();
        }

        public void LoadAreaAcion(List<TBL_ModuloAPC_Areas> items)
        {
            ddlArea.DataSource = items;
            ddlArea.DataTextField = "Nombre";
            ddlArea.DataValueField = "IdArea";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlArea.SelectedIndex = 0;
        }

        public void LoadProcesos(List<Application.Core.DTO_ValueKey> items)
        {
            ddlProceso.DataSource = items;
            ddlProceso.DataTextField = "Id";
            ddlProceso.DataValueField = "Value";
            ddlProceso.DataBind();
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

        public string FilterNoSolicitud
        {
            get
            {
                return txtNoSolicitud.Text;
            }
            set
            {
                txtNoSolicitud.Text = value;
            }
        }

        public string FilterTipo
        {
            get
            {
                return ddlTipo.SelectedValue;
            }
            set
            {
                ddlTipo.SelectedValue = value;
            }
        }

        public int FilterArea
        {
            get
            {
                return Convert.ToInt32(ddlArea.SelectedValue);
            }
            set
            {
                ddlArea.SelectedValue = value.ToString();
            }
        }

        public string FilterProceso
        {
            get
            {
                return ddlProceso.SelectedValue;
            }
            set
            {
                ddlProceso.SelectedValue = value;
            }
        }

        #endregion

        #endregion
    }
}