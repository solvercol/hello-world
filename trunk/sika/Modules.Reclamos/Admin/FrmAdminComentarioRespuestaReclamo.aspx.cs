using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Admin
{
    public partial class FrmAdminComentarioRespuestaReclamo : ViewPage<AdminComentarioRespuestaReclamoPresenter, IAdminComentarioRespuestaReclamoView>, IAdminComentarioRespuestaReclamoView
    {
        #region Members

        public bool CanEdit
        {
            get
            {
                return ViewState["AdminComentarioRespuesta_CanEdit"] == null ? false : Convert.ToBoolean(ViewState["AdminComentarioRespuesta_CanEdit"].ToString());
            }
            set
            {
                ViewState["AdminComentarioRespuesta_CanEdit"] = value;
            }
        }

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
            ImprimirTituloVentana(string.Format("Comentarios y Respuestas - Reclamo de {0} No. {1}", TipoReclamo, NumeroReclamo));

            ImgSearch.Visible = string.IsNullOrEmpty(FromPageAux);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from={2}", ModuleId, IdReclamoQS, FromPageAux));
        }

        protected void BtnViewReclamo_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from=admactividad&idfrom={2}", ModuleId, IdReclamo, IdComentario));
        }

        protected void BtnEditComentarioClick(object sender, EventArgs e)
        {
            EnableEdit(true);
            Presenter.LoadArhchivosAdjuntos();
        }

        protected void BtnCancelComentarioClick(object sender, EventArgs e)
        {
            EnableEdit(false);
            Presenter.LoadArhchivosAdjuntos();
        }

        protected void BtnSaveComentarioClick(object sender, EventArgs e)
        {
            Presenter.AddComentarioRelacionado();
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

        #endregion

        #region Repeaters

        protected void RptComentariosAsociados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_ComentariosRespuesta)(e.Item.DataItem);
                // Bindind data

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblCreadoPor = e.Item.FindControl("lblCreadoPor") as Label;
                if (lblCreadoPor != null) lblCreadoPor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var lblComentario = e.Item.FindControl("lblComentario") as Label;
                if (lblComentario != null) lblComentario.Text = string.Format("{0}", item.Comentario);
            }
        }
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

            trComentarios.Visible = enabled;
            trDestinatarios.Visible = enabled;
            txtComentario.Text = string.Empty;

            fupAnexoArchivo.Visible = enabled;
            btnAddArchivoAdjunto.Visible = enabled;

            btnCancel.Visible = enabled;
            btnSave.Visible = enabled;
            btnEdit.Visible = !enabled;
        }

        public void LoadDestinatarios(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddDestinatarios.DataSource = items;
            wddDestinatarios.TextField = "Nombres";
            wddDestinatarios.ValueField = "IdUser";
            wddDestinatarios.DataBind();
        }

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();
        }

        public void LoadComentariosRelacionados(List<TBL_ModuloReclamos_ComentariosRespuesta> items)
        {
            rptComentariosAsociados.DataSource = items;
            rptComentariosAsociados.DataBind();
        }

        public void DescargarArchivo(DTO_ValueKey archivo)
        {
            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");
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

        public string IdComentario
        {
            get
            {
                return Request.QueryString["IdComentario"];
            }
        }

        public string IdReclamo
        {
            get
            {
                return ViewState["AdminComentarioRespuesta_IdReclamo"] == null ? string.Empty : ViewState["AdminComentarioRespuesta_IdReclamo"].ToString();
            }
            set
            {
                ViewState["AdminComentarioRespuesta_IdReclamo"] = value;
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

        public string MonedaLocal
        {
            get
            {
                return ViewState["Reclamo_MonedaLocal"] == null ? string.Empty : ViewState["Reclamo_MonedaLocal"].ToString();
            }
            set
            {
                ViewState["Reclamo_MonedaLocal"] = value;
            }
        }

        public string TipoReclamo
        {
            get
            {
                return ViewState["Reclamo_TipoReclamo"] == null ? string.Empty : ViewState["Reclamo_TipoReclamo"].ToString();
            }
            set
            {
                ViewState["Reclamo_TipoReclamo"] = value;
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

        public DateTime FechaComentario
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


        public byte[] ArchivoAdjunto
        {
            get { return fupAnexoArchivo.FileBytes; }
        }

        public string NombreArchivoAdjunto
        {
            get { return fupAnexoArchivo.FileName; }
        }

        public string Asunto
        {
            get
            {
                return lblAsunto.Text;
            }
            set
            {
                lblAsunto.Text = value;
            }
        }

        public string Mensaje
        {
            get
            {
                return lblMensaje.Text;
            }
            set
            {
                lblMensaje.Text = value;
            }
        }

        public string Destinatario
        {
            get
            {
                return lblDestinatario.Text;
            }
            set
            {
                lblDestinatario.Text = value;
            }
        }

        public string NuevoComentario
        {
            get
            {
                return txtComentario.Text;
            }
            set
            {
                txtComentario.Text = value;
            }
        }

        public string IdUsuarioDestino
        {
            get
            {
                return wddDestinatarios.SelectedValue;
            }
            set
            {
                wddDestinatarios.SelectedValue = value;
            }
        }

        #endregion

        #endregion
    }
}