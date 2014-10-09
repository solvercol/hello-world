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
using System.Web.UI.HtmlControls;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminComentariosRespuestaReclamo : ViewUserControl<AdminComentariosRespuestaReclamoPresenter, IAdminComentariosRespuestaReclamoView>, IAdminComentariosRespuestaReclamoView, IReclamoWebUserControl
    {
        #region Members

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        public string IdFrom
        {
            get
            {
                return Request.QueryString["idfrom"];
            }
        }

        #endregion

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
            trInfoContacto.Visible = false;
            btnGuardarCliente.Visible = false;
            trInfoDestinatario.Visible = true;
            btnGuardar.Visible = true;
            btnGuardarCliente.Visible = false;
            ShowAdminComentarioWindow(true);
        }

        protected void BtnAddRespuestaCliente_Click(object sender, EventArgs e)
        {
            InitAdminComentario();
            trInfoDestinatario.Visible = false;
            btnGuardar.Visible = false;
            trInfoContacto.Visible = true;
            btnGuardarCliente.Visible = true;
            btnGuardar.Visible = false;
            MailContacto = MailContactoTmp;
            ShowAdminComentarioWindow(true);
        }

        protected void BtnSaveComentario_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Asunto))
                messages.Add("Es necesario ingresar un asunto.");

            if (string.IsNullOrEmpty(Comentario))
                messages.Add("Es necesario ingresar un mensaje/observaciones.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ShowAdminComentarioWindow(true);
                return;
            }

            if (IsNewComentario)
                Presenter.AddComentarioReclamo();
            else
                Presenter.UpdateComentarioReclamo();
        }

        protected void BtnSaveComentarioCliente_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Asunto))
                messages.Add("Es necesario ingresar un asunto.");

            if (string.IsNullOrEmpty(Comentario))
                messages.Add("Es necesario ingresar un mensaje/observaciones.");

            if (string.IsNullOrEmpty(MailContacto))
                messages.Add("Es necesario ingresar un mail de contacto.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ShowAdminComentarioWindow(true);
                return;
            }

            Presenter.AddComentarioReclamoCliente();
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

            Response.Redirect(string.Format("../Admin/FrmAdminComentarioRespuestaReclamo.aspx?ModuleId={0}&IdComentario={1}&from=reclamo&IdReclamo={2}&fromaux={3}&idfromaux={4}", IdModule, IdSelectedComentario, IdReclamo, FromPage, IdFrom));

            //Presenter.LoadComentarioReclamo();
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

        protected void BtnAddUsuarioCopia_Click(object sender, EventArgs e)
        {
            var usuarioCopia = new DTO_ValueKey() { Id = IdUsuarioCopia, Value = wddUsuarioCopia.SelectedItem.Text };
            if (!ExistsInCopia(usuarioCopia))
                UsuariosCopia.Add(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);

            cpeCopiarUsuarios.Collapsed = false;
            ShowAdminComentarioWindow(true);
        }

        protected void BtnRemoveUsuarioCopia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdUsuarioCopiaSelected)) return;

            var usuarioCopia = UsuariosCopia.Where(x => x.Id == IdUsuarioCopiaSelected).SingleOrDefault();

            if (usuarioCopia != null)
                UsuariosCopia.Remove(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);

            cpeCopiarUsuarios.Collapsed = false;
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

                bool hasChild = false;

                var hddIdComentario = e.Item.FindControl("hddIdComentario") as HiddenField;
                if (hddIdComentario != null) hddIdComentario.Value = string.Format("{0}", item.IdComentario);

                var lblAsunto = e.Item.FindControl("lblAsunto") as Label;
                if (lblAsunto != null) lblAsunto.Text = string.Format("{0}", item.Asunto);

                var lblMensaje = e.Item.FindControl("lblMensaje") as Label;
                if (lblMensaje != null) lblMensaje.Text = string.Format("{0}", item.Comentario);

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgSelectComentario = e.Item.FindControl("imgSelectComentario") as ImageButton;
                if (imgSelectComentario != null)
                {
                    imgSelectComentario.CommandArgument = string.Format("{0}", item.IdComentario);
                    if (item.EsComentarioCliente)
                    {
                        imgSelectComentario.ImageUrl = "~/Resources/Images/emailcliente.png";
                    }
                }

                if (item.ComentariosAsociados != null && item.ComentariosAsociados.Any())
                {
                    var rptChild = e.Item.FindControl("rptChildComentarios") as Repeater;
                    if (rptChild != null)
                    {
                        rptChild.DataSource = item.ComentariosAsociados;
                        rptChild.DataBind();
                    }

                    hasChild = true;
                }

                // Adicionando comportamiento de colapse para hijos
                var trParent = e.Item.FindControl("rowParent") as HtmlTableRow;
                if (trParent != null)
                {
                    var trChild = e.Item.FindControl("rowChild") as HtmlTableRow;
                    trChild.Visible = hasChild;
                } 
            }
        }

        protected void RptComentariosAsociadosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_ComentariosRespuesta)(e.Item.DataItem);
                // Bindind data
                var lblAsunto = e.Item.FindControl("lblAsunto") as Label;
                if (lblAsunto != null) lblAsunto.Text = string.Format("{0}", item.Asunto);

                var lblMensjae = e.Item.FindControl("lblMensjae") as Label;
                if (lblMensjae != null) lblMensjae.Text = string.Format("{0}", item.Comentario);

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);
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
                    imgDeleteAnexo.Visible = IsNewComentario;
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
                    custVal.ValidationGroup = "vsComentarios";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        void InitAdminComentario()
        {
            Asunto = string.Empty;
            Comentario = string.Empty;
            MailContacto = string.Empty;
            IdUsuarioDestino = UserSession.IdUser.ToString();
            ArchivosAdjuntos = new List<DTO_ValueKey>();
            UsuariosCopia = new List<DTO_ValueKey>();
            LoadUsuariosCopia(UsuariosCopia);
            LoadArchivosAdjuntos(ArchivosAdjuntos);
            IsNewComentario = true;
            EnableEdit(true);
        }

        bool ExistsInCopia(DTO_ValueKey item)
        {
            foreach (var itm in UsuariosCopia)
            {
                if (itm.Id == item.Id)
                    return true;
            }

            return false;
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

        public void LoadUsuarioCopia(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddUsuarioCopia.DataSource = items;
            wddUsuarioCopia.TextField = "Nombres";
            wddUsuarioCopia.ValueField = "IdUser";
            wddUsuarioCopia.DataBind();

            wddUsuarioCopia.SelectedItemIndex = 0;
        }

        public void LoadUsuariosCopia(List<DTO_ValueKey> items)
        {
            lstUsuariosCopia.DataSource = items;
            lstUsuariosCopia.DataValueField = "Id";
            lstUsuariosCopia.DataTextField = "Value";
            lstUsuariosCopia.DataBind();
        }

        public void EnableEdit(bool enable)
        {
            txtAsunto.Visible = enable;
            txtObservaciones.Visible = enable;
            wddDestinatarios.Visible = enable;
            btnGuardar.Visible = enable;

            lblAsunto.Visible = !enable;
            lblObservaciones.Visible = !enable;
            lblDestinatarios.Visible = !enable;

            fupAnexoArchivo.Visible = enable;
            btnAddArchivoAdjunto.Visible = enable;
        }

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();
        }

        public void RefreshUserControl()
        {
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        #endregion

        #region Properties

        public event Action RiseFatherPostback;

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public event EventHandler FilterEvent;

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
                lblAsunto.Text = value;
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
                lblObservaciones.Text = value;
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
                lblDestinatarios.Text = wddDestinatarios.SelectedItem.Text;
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

        public List<DTO_ValueKey> UsuariosCopia
        {
            get
            {
                if (Session["AdminComentarios_UsuarioCopia"] == null)
                    Session["AdminComentarios_UsuarioCopia"] = new List<DTO_ValueKey>();

                return Session["AdminComentarios_UsuarioCopia"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminComentarios_UsuarioCopia"] = value;
            }
        }

        public string IdUsuarioCopia
        {
            get
            {
                return wddUsuarioCopia.SelectedValue;
            }
            set
            {
                wddUsuarioCopia.SelectedValue = value;
            }
        }

        public string IdUsuarioCopiaSelected
        {
            get
            {
                return lstUsuariosCopia.SelectedValue;
            }
            set
            {
                lstUsuariosCopia.SelectedValue = value;
            }
        }

        public string MailContacto
        {
            get
            {
                return txtMailContacto.Text;
            }
            set
            {
                txtMailContacto.Text = value;
            }
        }

        public string MailContactoTmp
        {
            get
            {
                if (ViewState["AdminComentarios_MailContactoTmp"] == null)
                    ViewState["AdminComentarios_MailContactoTmp"] = string.Empty;

                return ViewState["AdminComentarios_MailContactoTmp"].ToString(); ;
            }
            set
            {
                ViewState["AdminComentarios_MailContactoTmp"] = value;
            }
        }

        public bool CanSendMailToCLient
        {
            get
            {
                return btnNuevoRespuestaCliente.Visible;
            }
            set
            {
                btnNuevoRespuestaCliente.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}