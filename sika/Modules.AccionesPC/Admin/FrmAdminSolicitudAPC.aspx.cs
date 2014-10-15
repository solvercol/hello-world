using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;

namespace Modules.AccionesPC.Admin
{
    public partial class FrmAdminSolicitudAPC : ViewPage<AdminSolicitudAPCPresenter, IAdminSolicitudAPCView>, IAdminSolicitudAPCView
    {
        #region Members

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        public string IdReclamoQS
        {
            get
            {
                return Request.QueryString["IdReclamo"];
            }
        }

        public string FromPageAux
        {
            get
            {
                return Request.QueryString["fromaux"];
            }
        }

        public string IdFrom
        {
            get
            {
                return Request.QueryString["idfrom"];
            }
        }

        public string IdFromAux
        {
            get
            {
                return Request.QueryString["idfromaux"];
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Solicitud Acciones Preventivas Correctivas"));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            switch (FromPage)
            {
                case "reclamo":
                    Response.Redirect(string.Format("FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from={2}", ModuleId, IdReclamoQS, FromPageAux));
                    break;
                case "solicitud":
                    Response.Redirect(string.Format("FrmSolicitudAPC.aspx?ModuleId={0}&IdSolicitud={1}", ModuleId, IdSolicitud));
                    break;
                default:
                    Response.Redirect(string.Format("~/Default.aspx?ModuleId={0}", ModuleId));
                    break;
            }
        }

        protected void BtnViewReclamo_Click(object sender, EventArgs e)
        {
            //Response.Redirect(string.Format("FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from=admactividad&idfrom={2}", ModuleId, IdReclamo, IdActividad));
        }

        protected void BtnSaveSolicitudAPCClick(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(DescripcionAccion))
                messages.Add("Es necesario ingresar una descripción del posible problema o conformidad.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            if (string.IsNullOrEmpty(IdSolicitud))
                Presenter.SaveSolicitudAPC();
            else
                Presenter.UpdateSolicitudAPC();
        }

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivo.HasFile)
            {
                return;
            }

            var archivoAdjunto = new DTO_ValueKey();
            archivoAdjunto.Id = (ArchivosAdjuntos.Count + 1).ToString();
            archivoAdjunto.Value = fupAnexoArchivo.FileName;
            archivoAdjunto.ComplexValue = fupAnexoArchivo.FileBytes;
            
            ArchivosAdjuntos.Add(archivoAdjunto);
            LoadArchivosAdjuntos(ArchivosAdjuntos);
        }

        protected void BtnRemoveArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = ArchivosAdjuntos.Where(x => x.Id == IdArchivo).SingleOrDefault();

            if (archivo != null)
            {
                ArchivosAdjuntos.Remove(archivo);
                LoadArchivosAdjuntos(ArchivosAdjuntos);
            }
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = ArchivosAdjuntos.Where(x => x.Id == IdArchivo).SingleOrDefault();

            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");
        }

        #endregion

        #region DropDownList

        protected void WddArea_ValueChanged(object sender, EventArgs e)
        {
            Presenter.LoadProcesos();
        }

        #endregion

        #region Repeaters

        protected void RptArchivosAdjuntos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (DTO_ValueKey)(e.Item.DataItem);
                // Bindind data

                var hddIdArchivo = e.Item.FindControl("hddIdArchivo") as HiddenField;
                if (hddIdArchivo != null) hddIdArchivo.Value = string.Format("{0}", item.Id);

                var lnkNombreArchivo = e.Item.FindControl("lnkNombreArchivo") as LinkButton;
                if (lnkNombreArchivo != null)
                {
                    lnkNombreArchivo.Text = string.Format("{0}", item.Value);
                    lnkNombreArchivo.CommandArgument = string.Format("{0}", item.Id);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(lnkNombreArchivo);
                }

                var imgDeleteAnexo = e.Item.FindControl("imgDeleteAnexo") as ImageButton;
                if (imgDeleteAnexo != null)
                {
                    imgDeleteAnexo.CommandArgument = string.Format("{0}", item.Id);
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        void AddErrorMessages(List<string> messages)
        {
            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    var custVal = new CustomValidator();
                    custVal.IsValid = false;
                    custVal.ErrorMessage = msg;
                    custVal.EnableClientScript = false;
                    custVal.Display = ValidatorDisplay.None;
                    custVal.ValidationGroup = "vgGeneral";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        #endregion

        #region View Members

        #region Methods
            
        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();

            trAnexos.Visible = items.Any();
        }

        public void DescargarArchivo(DTO_ValueKey archivo)
        {
            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");
        }

        public void LoadAreaAcion(List<TBL_ModuloAPC_Areas> items)
        {
            wddArea.DataSource = items;
            wddArea.DataTextField = "Nombre";
            wddArea.DataValueField = "IdArea";
            wddArea.DataBind();
        }

        public void LoadProcesos(List<DTO_ValueKey> items)
        {
            wddProceso.DataSource = items;
            wddProceso.DataTextField = "Id";
            wddProceso.DataValueField = "Value";
            wddProceso.DataBind();
        }

        public void GoToSolicitudView(string idSolicitud)
        {
            Response.Redirect(string.Format("FrmSolicitudAPC.aspx?ModuleId={0}&IdSolicitud={1}", ModuleId, idSolicitud));
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

        public string IdReclamo
        {
            get
            {
                return ViewState["AdminSolicitud_IdReclamo"] == null ? IdReclamoQS : ViewState["AdminSolicitud_IdReclamo"].ToString();
            }
            set
            {
                ViewState["AdminSolicitud_IdReclamo"] = value;
            }
        }

        public string NumeroReclamo
        {
            get
            {
                return ViewState["Reclamo_NumeroReclamo"] == null ? string.Empty : ViewState["Reclamo_NumeroReclamo"].ToString();
            }
            set
            {
                ViewState["Reclamo_NumeroReclamo"] = value;
            }
        }    

        public string TipoReclamo
        {
            get
            {
                return ViewState["AdminSolicitud_TipoReclamo"] == null ? string.Empty : ViewState["AdminSolicitud_TipoReclamo"].ToString();
            }
            set
            {
                ViewState["AdminSolicitud_TipoReclamo"] = value;
            }
        }

        public string TitleReclamo
        {
            get
            {
                return lblTitleReclamo.Text;
            }
            set
            {
                lblTitleReclamo.Text = value;
            }
        }

        public string TitleReclamoFrom
        {
            get
            {
                return lblTitleReclamoFrom.Text;
            }
            set
            {
                lblTitleReclamoFrom.Text = value;
            }
        }

        public string Unidad
        {
            get
            {
                return lblUnidad.Text;
            }
            set
            {
                lblUnidad.Text = value;
            }
        }

        public string FechaReclamo
        {
            get
            {
                return lblFechaReclamo.Text;
            }
            set
            {
                lblFechaReclamo.Text = value;
            }
        }

        public string Asesor
        {
            get
            {
                return lblAsesor.Text;
            }
            set
            {
                lblAsesor.Text = value;
            }
        }
    
        public byte[] ArchivoAdjunto
        {
            get { return fupAnexoArchivo.FileBytes; }
        }

        public string NombreArchivoAdjunto
        {
            get { return fupAnexoArchivo.FileName; }
        }

        public string ConsecutivoSolicitud
        {
            get
            {
                return ViewState["AdminSolicitud_ConsecutivoSolicitud"] == null ? string.Empty : ViewState["AdminSolicitud_ConsecutivoSolicitud"].ToString();
            }
            set
            {
                ViewState["AdminSolicitud_ConsecutivoSolicitud"] = value;
            }
        }

        public bool ShowInfoReclamo
        {
            get
            {
                return trInfoReclamo.Visible;
            }
            set
            {
                trInfoReclamo.Visible = value;
            }
        }

        public string IdSolicitud
        {
            get { return Request.QueryString.Get("IdSolicitud"); }
        }

        public string TipoAccion
        {
            get
            {
                return rblTipoAccion.SelectedValue;
            }
            set
            {
                rblTipoAccion.SelectedValue = value;
            }
        }

        public int IdAreaAccion
        {
            get
            {
                return Convert.ToInt32(wddArea.SelectedValue);
            }
            set
            {
                wddArea.SelectedValue = value.ToString();
            }
        }

        public string Proceso
        {
            get
            {
                return wddProceso.SelectedValue;
            }
            set
            {
                wddProceso.SelectedValue = value;
            }
        }

        public int IdGerente
        {
            get
            {
                return ViewState["AdminSolicitud_IdGerente"] == null ? 0 : Convert.ToInt32(ViewState["AdminSolicitud_IdGerente"]);
            }
            set
            {
                ViewState["AdminSolicitud_IdGerente"] = value;
            }
        }

        public string Gerente
        {
            get
            {
                return lblGerenteArea.Text;
            }
            set
            {
                lblGerenteArea.Text = value;
            }
        }

        public string DescripcionAccion
        {
            get
            {
                return txtDescripcionAccion.Text;
            }
            set
            {
                txtDescripcionAccion.Text = value;
            }
        }

        public DateTime FechaDesde
        {
            get
            {
                return Convert.ToDateTime(txtFechaInicio.Text);
            }
            set
            {
                txtFechaInicio.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public DateTime FechaHasta
        {
            get
            {
                return Convert.ToDateTime(txtFechaFin.Text);
            }
            set
            {
                txtFechaFin.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public string LogInfoMessage
        {
            set { lblLogInfo.Text = value; }
        }

        public List<DTO_ValueKey> ArchivosAdjuntos
        {
            get
            {
                if (Session["AdminSolicitudAPC_ArchivosAdjuntos"] == null)
                    Session["AdminSolicitudAPC_ArchivosAdjuntos"] = new List<DTO_ValueKey>();

                return Session["AdminSolicitudAPC_ArchivosAdjuntos"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminSolicitudAPC_ArchivosAdjuntos"] = value;
            }
        }

        public bool ShowArchivosAdjuntos
        {
            get
            {
                return trContainerAnexos.Visible;
            }
            set
            {
                trContainerAnexos.Visible = value;
            }
        }
           
        #endregion

        #endregion
    }
}