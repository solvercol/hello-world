using System;
using System.Collections.Generic;
using System.Data;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Microsoft.Reporting.WebForms;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Views
{
    public partial class FrmMisReclamosPorEstado : ViewPage<MisReclamosPorEstadoPresenter, IMisReclamosPorEstadoView>, IMisReclamosPorEstadoView
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Mis Reclamos Por Estado");
        }

        #endregion

        #region Buttons

        protected void BtnNuevoReclamo_Click(object sender, EventArgs e)
        {
            InitNewReclamo();
            ShowNewReclamoWindow(true);
        }

        protected void BtnConfirmNuevoReclamo_Click(object sender, EventArgs e)
        {
            if (TipoReclamo == "Producto")
            {
                Response.Redirect(string.Format("../Admin/FrmAddReclamo.aspx?ModuleId={0}&tr={1}&from=misestado", ModuleId, TipoReclamo));
            }
            else
            {
                Response.Redirect(string.Format("../Admin/FrmAddReclamo.aspx?ModuleId={0}&tr={1}&cat={2}&gruinf={3}&from=misestado",
                                                ModuleId, TipoReclamo, IdCategoriaReclamo, IdGrupoInformacion
                                                ));
            }
        }

        protected void BtnFilterReclamos_Click(object sender, EventArgs e)
        {
            Presenter.LoadReport();
        }

        #endregion

        #region Radio Button

        protected void RblReclamoType_Changed(Object sender, EventArgs e)
        {
            ShowCategoriaReclamoList(rblReclamoType.SelectedValue != "Producto");
            ShowNewReclamoWindow(true);
        }

        #endregion

        #endregion

        #region Methods
        #endregion

        #region View Members

        #region Members

        public void ShowCategoriaReclamoList(bool visible)
        {
            trCategoriaReclamo.Visible = visible;
        }

        public void ShowNewReclamoWindow(bool visible)
        {
            if (visible)
                mpeNewReclamo.Show();
            else
                mpeNewReclamo.Hide();
        }

        public void LoadCategoriasReclamo(List<TBL_ModuloReclamos_CategoriasReclamo> list)
        {
            ddlCategoriaReclamo.DataSource = list;
            ddlCategoriaReclamo.DataTextField = "Nombre";
            ddlCategoriaReclamo.DataValueField = "IdCategoriaGrupoInformacion";
            ddlCategoriaReclamo.DataBind();
        }

        public void InitNewReclamo()
        {
            rblReclamoType.SelectedValue = "Producto";
            ShowCategoriaReclamoList(false);
        }

        public void LoadViewReclamos(DataTable dt)
        {
            rptReclamos.Reset();
            rptReclamos.LocalReport.DataSources.Clear();
            rptReclamos.ProcessingMode = ProcessingMode.Local;
            rptReclamos.LocalReport.ReportPath = Server.MapPath(@"~/Pages/Modules/Reclamos/Resources/ReportViewer/RptVistaMisReclamosPorEstado.rdlc");
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

        public string TipoReclamo
        {
            get
            {
                return rblReclamoType.SelectedValue;
            }
            set
            {
                rblReclamoType.SelectedValue = value;
            }
        }

        public string IdCategoriaReclamo
        {
            get
            {
                return ddlCategoriaReclamo.SelectedValue.Split('-')[0];
            }
        }

        public string IdGrupoInformacion
        {
            get
            {
                return ddlCategoriaReclamo.SelectedValue.Split('-')[1];
            }
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

        public bool CanRegister
        {
            get
            {
                return btnNuevo.Visible;
            }
            set
            {
                btnNuevo.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}