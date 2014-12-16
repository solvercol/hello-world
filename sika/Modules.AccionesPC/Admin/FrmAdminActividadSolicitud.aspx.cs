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
    public partial class FrmAdminActividadSolicitud : ViewPage<AdminActividadSolicitudPresentador, IAdminActividadSolicitudView>, IAdminActividadSolicitudView
    {
         #region Delegates

        public event EventHandler CancelEvent;
        public event EventHandler CloseEvent;

        #endregion

        #region Members

        public bool CanEdit
        {
            get
            {
                return ViewState["AdminActividad_CanEdit"] == null ? false : Convert.ToBoolean(ViewState["AdminActividad_CanEdit"].ToString());
            }
            set
            {
                ViewState["AdminActividad_CanEdit"] = value;
            }
        }

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        public string IdSolicitudQS
        {
            get
            {
                return Request.QueryString["IdSolicitud"];
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
            ImprimirTituloVentana(string.Format("Plan de Acción - {0} Solicitud No. {1}", TipoAccion, CodSolicitud));

            //ImgSearch.Visible = string.IsNullOrEmpty(FromPageAux);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FromPage))
            {
                //Response.Redirect(string.Format("../Views/FrmMisActividadesPendientes.aspx?ModuleId={0}", ModuleId));
            }
            else
            {
                switch (FromPage)
                {
                    case "solicitud":
                        Response.Redirect(string.Format("FrmSolicitudAPC.aspx?ModuleId={0}&IdSolicitud={1}&from={2}", ModuleId, IdSolicitudQS, FromPageAux));
                        break;
                    case "misactpend":
                        Response.Redirect(string.Format("../Views/FrmMisActividadesPendientes.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "actpersona":
                        Response.Redirect(string.Format("../Views/FrmActividadesPorPersona.aspx?ModuleId={0}", ModuleId));
                        break;
                }
            }
        }

        protected void BtnViewReclamo_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmSolicitudAPC.aspx?ModuleId={0}&IdSolicitud={1}&from=admactividad&idfrom={2}", ModuleId, IdSolicitudQS, IdActividad));
        }

        protected void BtnEditActividadClick(object sender, EventArgs e)
        {
            EnableEdit(true);
            Presenter.LoadArchivosAdjuntos();
        }

        protected void BtnCancelActividadClick(object sender, EventArgs e)
        {
            EnableEdit(false);
            Presenter.LoadArchivosAdjuntos();
        }

        protected void BtnSaveActividadClick(object sender, EventArgs e)
        {
            Presenter.UpdateActividadSolicitud();
        }

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivo.HasFile)
            {
                return;
            }

            Presenter.AddArchivoAdjunto();
        }

        protected void BtnRemoveArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var IdArchivo = Convert.ToDecimal(btn.CommandArgument);

            Presenter.RemoveArchivoAdjunto(IdArchivo);
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = Convert.ToDecimal(btn.CommandArgument);

            Presenter.DownloadArchivoAdjunto(IdArchivo);
        }

        protected void BtnSaveRealizadaClick(object sender, EventArgs e)
        {
            trObservacionesCierre.Visible = true;
            trObservacionesCancelacion.Visible = false;
            btnSaveRealizarActividad.Visible = true;
            btnSaveCancelarActividad.Visible = false;
            mpeCierreActividad.Show();
        }

        protected void BtnCancelarActividadClick(object sender, EventArgs e)
        {
            trObservacionesCierre.Visible = false;
            trObservacionesCancelacion.Visible = true;
            btnSaveRealizarActividad.Visible = false;
            btnSaveCancelarActividad.Visible = true;
            ObservacionesCancelacion = string.Empty;
            mpeCierreActividad.Show();
        }

        protected void BtnCancelCierreClick(object sender, EventArgs e)
        {
            mpeCierreActividad.Hide();
        }

        protected void BtnSaveRealizarActividad_Click(object sender, EventArgs e)
        {
             if (CloseEvent != null)
                CloseEvent(null, EventArgs.Empty);
        }

        protected void BtnSaveCancelarActividad_Click(object sender, EventArgs e)
        {
              if (CancelEvent != null)
                CancelEvent(null, EventArgs.Empty);
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
                    imgDeleteAnexo.Visible = CanEdit;
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        #endregion

        #region View Members

        #region Methods

        public void EnableEdit(bool enabled)
        {
            CanEdit = enabled;

            txtObservaciones.Visible = enabled;
            //ddlEstado.Visible = enabled;

            fupAnexoArchivo.Visible = enabled;
            btnAddArchivoAdjunto.Visible = enabled;

            lblObservaciones.Visible = !enabled;
            //lblEstado.Visible = !enabled;

            //btnCancel.Visible = enabled;
            btnSave.Visible = enabled;
            btnSaveRealizada.Visible = enabled;
            btnCancelActividad.Visible = enabled;
            btnEdit.Visible = !enabled;
        }


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

        public void ShowObservaciones(bool visible)
        {
            trObservaciones.Visible = visible;
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

        public string IdReclamoQS
        {
            get
            {
                return Request.QueryString["IdReclamo"];
            }
        }

        public string IdActividad
        {
            get
            {
                return Request.QueryString["IdActividad"];
            }
        }

        public string UsuarioSeguimiento
        {
            get
            {
                return lblUsuarioSeguimiento.Text;
            }
            set
            {
                lblUsuarioSeguimiento.Text = value;
            }
        }

        public string UsuarioEjecucion
        {
            get
            {
                return lblUsuarioEjecucion.Text;
            }
            set
            {
                lblUsuarioEjecucion.Text = value;
            }
        }

        public DateTime FechaActividad
        {
            get
            {
                return Convert.ToDateTime(lblFecha.Text);
            }
            set
            {
                lblFecha.Text = string.Format("{0:dd/MM/yyyy}", value);
            }
        }

        public string Estado
        {
            get
            {
                return ddlEstado.SelectedValue;
            }
            set
            {
                ddlEstado.SelectedValue = value;
                lblEstado.Text = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return lblDescripcion.Text;
            }
            set
            {
                lblDescripcion.Text = value;
            }
        }

        public string Observaciones
        {
            get
            {
                return txtObservaciones.Text;
            }
            set
            {
                lblObservaciones.Text = value;
                txtObservaciones.Text = value;
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

        public bool CanRegister
        {
            get
            {
                return btnEdit.Visible;
            }
            set
            {
                btnEdit.Visible = value;
            }
        }

        public string ObservacionesCierre
        {
            get { return txtObservacionesCierre.Text; }
            set { txtObservacionesCierre.Text = value; }
        }

        public string ObservacionesCancelacion
        {
            get { return txtObservacionesCancelacion.Text; }
            set { txtObservacionesCancelacion.Text = value; }
        }

        public string LogInfoMessage
        {
            set { lblLogInfo.Text = value; }
        }

        #endregion

        #region Properties Solicitud

        public string IdSolicitud
        {
            get
            {
                return Request.QueryString["IdSolicitud"];
            }
        }

        public string TipoAccion
        {
            get
            {
                return lblTipoAccion.Text;
            }
            set
            {
                lblTipoAccion.Text = value;
            }
        }

        public string CodSolicitud
        {
            get;
            set;
        }

        public string Area
        {
            get
            {
                return lblArea.Text;
            }
            set
            {
                lblArea.Text = value;
            }
        }

        public string GerenteArea
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

        public string ResponsableAccion
        {
            get
            {
                return lblResponsableAccion.Text;
            }
            set
            {
                lblResponsableAccion.Text = value;
            }
        }

        public string FechaInicio
        {
            get
            {
                return lblFechaInicio.Text;
            }
            set
            {
                lblFechaInicio.Text = value;
            }
        }

        public string FechaFinal
        {
            get
            {
                return lblFechaFin.Text;
            }
            set
            {
                lblFechaFin.Text = value;
            }
        }

        #endregion

        #region InfoReclamos
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
        #endregion

        #endregion
    }
}