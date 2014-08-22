using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Reclamos.UI;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Application.Core;
using System.Web.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminComentariosRespuestaReclamo : ViewUserControl<AdminComentariosRespuestaReclamoPresenter, IAdminComentariosRespuestaReclamoView>, IAdminComentariosRespuestaReclamoView, IReclamoWebUserControl
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddComentario_Click(object sender, EventArgs e)
        {
            InitAdminComentario();
            ShowAdminComentarioWindow(true);
        }

        protected void BtnSaveComentario_Click(object sender, EventArgs e)
        {
            if (IsNewComentario)
                Presenter.AddComentarioReclamo();
            else
                Presenter.UpdateComentarioReclamo();
        }

        protected void BtnRemoveActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedComentario = btn.CommandArgument;

            Presenter.RemoveComentarioReclamo();
        }

        protected void BtnSelectComentario_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedComentario = btn.CommandArgument;

            Presenter.LoadComentarioReclamo();
        }

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivo.HasFile)
            {
                ShowAdminComentarioWindow(true);
                return;
            }

            var archivoAdjunto = new DTO_ValueKey();
            archivoAdjunto.Id = (ArchivosAdjuntos.Count + 1).ToString();
            archivoAdjunto.Value = fupAnexoArchivo.FileName;
            archivoAdjunto.ComplexValue = fupAnexoArchivo.FileBytes;

            ArchivosAdjuntos.Add(archivoAdjunto);
            LoadArchivosAdjuntos(ArchivosAdjuntos);

            ShowAdminComentarioWindow(true);
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

            ShowAdminComentarioWindow(true);
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = ArchivosAdjuntos.Where(x => x.Id == IdArchivo).SingleOrDefault();

            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");

            ShowAdminComentarioWindow(true);
        }

        #endregion

        #region Repeaters

        protected void RptComentariosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_ComentariosRespuesta)(e.Item.DataItem);
                // Bindind data

                var hddIdComentario = e.Item.FindControl("hddIdComentario") as HiddenField;
                if (hddIdComentario != null) hddIdComentario.Value = string.Format("{0}", item.IdComentario);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Comentario);

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgSelectComentario = e.Item.FindControl("imgSelectComentario") as ImageButton;
                if (imgSelectComentario != null) imgSelectComentario.CommandArgument = string.Format("{0}", item.IdComentario);
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
                    lnkNombreArchivo.Enabled = !IsNewComentario;
                    lnkNombreArchivo.CommandArgument = string.Format("{0}", item.Id);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(lnkNombreArchivo);
                }

                var imgDeleteAnexo = e.Item.FindControl("imgDeleteAnexo") as ImageButton;
                if (imgDeleteAnexo != null)
                {
                    imgDeleteAnexo.CommandArgument = string.Format("{0}", item.Id);
                    imgDeleteAnexo.Enabled = IsNewComentario;
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        void InitAdminComentario()
        {
            Asunto = string.Empty;
            Comentario = string.Empty;
            IdUsuarioDestino = UserSession.IdUser.ToString();
            ArchivosAdjuntos = new List<DTO_ValueKey>();
            LoadArchivosAdjuntos(ArchivosAdjuntos);
            IsNewComentario = true;
            EnableEdit(true);
        }

        #endregion

        #region IReclamoWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminComentarioWindow(bool visible)
        {
            if (visible)
                mpeAdminSolucion.Show();
            else
                mpeAdminSolucion.Hide();
        }

        public void LoadComentariosReclamo(List<TBL_ModuloReclamos_ComentariosRespuesta> items)
        {
            rptComentariosList.DataSource = items;
            rptComentariosList.DataBind();
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

        public void EnableEdit(bool enable)
        {
            txtAsunto.Enabled = enable;
            txtObservaciones.Enabled = enable;
            wddDestinatarios.Enabled = enable;
            btnGuardar.Visible = enable;
        }

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();
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
            get { return Request.QueryString.Get("IdReclamo"); }
        }
        
        public string Asunto
        {
            get
            {
                return txtAsunto.Text;
            }
            set
            {
                txtAsunto.Text = value;
            }
        }

        public string Comentario
        {
            get
            {
                return txtObservaciones.Text;
            }
            set
            {
                txtObservaciones.Text = value;
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

        public string IdSelectedComentario
        {
            get
            {
                return ViewState["AdminComentario_IdComentarioSelect"] as string;
            }
            set
            {
                ViewState["AdminComentario_IdComentarioSelect"] = value;
            }
        }

        public bool IsNewComentario
        {
            get
            {
                if (ViewState["AdminComentario_IsNewComentario"] == null)
                    ViewState["AdminComentario_IsNewComentario"] = false;

                return Convert.ToBoolean(ViewState["AdminComentario_IsNewComentario"]);
            }
            set
            {
                ViewState["AdminComentario_IsNewComentario"] = value;
            }
        }

        public List<DTO_ValueKey> ArchivosAdjuntos
        {
            get
            {
                if (Session["AdminComentario_ArchivosAdjuntos"] == null)
                    Session["AdminComentario_ArchivosAdjuntos"] = new List<DTO_ValueKey>();

                return Session["AdminComentario_ArchivosAdjuntos"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminComentario_ArchivosAdjuntos"] = value;
            }
        }

        #endregion

        #endregion
    }
}