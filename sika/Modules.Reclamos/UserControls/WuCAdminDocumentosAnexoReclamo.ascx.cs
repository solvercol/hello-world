﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Reclamos.UI;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Application.Core;
using System.Web.UI;
using Infragistics.Web.UI.LayoutControls;

namespace Modules.Reclamos.UserControls
{
    public partial class WuCAdminDocumentosAnexoReclamo : ViewUserControl<AdminDocumentosAnexoReclamoPresenter, IAdminDocumentosAnexoReclamoView>, IAdminDocumentosAnexoReclamoView, IReclamoWebUserControl
    {
        #region Members

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnCancelarAnexo_Click(object sender, EventArgs e)
        {
            ShowAdminDoc(false);
        }

        protected void BtnAddAnexo_Click(object sender, EventArgs e)
        {
            InitDocumento();
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(btnSaveAnexo);
            ShowAdminDoc(true);
        }

        protected void BtnSaveDocumento_Click(object sender, EventArgs e)
        {
            var msg = new List<string>();

            var messages = new List<string>();

            if (string.IsNullOrEmpty(Titulo))
                messages.Add("Es necesario ingresar un titulo para el documento.");
            
            if (!fupAnexoArchivo.HasFile && IsNew)
            {
                messages.Add("Es necesario ingresar un archivo para cargar.");
            }

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(btnSaveAnexo);
                return;
            }

            if (IsNew)
                Presenter.SaveDocumento();
            else
                Presenter.UpdateDocumento();
        }

        protected void BtnDownLoadAnexo_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = Presenter.GetAnexoDoumento(Guid.Parse(IdArchivo));

            DownloadDocument(archivo.Archivo, archivo.NombreArchivo, "application/octet-stream");
        }

        protected void BtnImgeAnexoReclamo_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdDocumentoSelected = btn.CommandArgument;

            Presenter.LoadAnexoDocumento();

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(btnSaveAnexo);
        }

        #endregion

        #region DropDownList

        protected void DdlCategoria_IndexChanged(Object sender, EventArgs e)
        {
            Presenter.LoadAnexos();
        }

        #endregion

        #region Repeaters

        protected void RptAnexosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_DocumentosAnexoReclamo)(e.Item.DataItem);
                // Bindind data

                var lblTitulo = e.Item.FindControl("lblTitulo") as Label;
                if (lblTitulo != null) lblTitulo.Text = string.Format("{0}", item.Titulo);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Descripcion);

                var lblCategoria = e.Item.FindControl("lblCategoria") as Label;
                if (lblCategoria != null) lblCategoria.Text = string.Format("{0}", item.Categoria);

                var lblCreadoPor = e.Item.FindControl("lblCreadoPor") as Label;
                if (lblCreadoPor != null) lblCreadoPor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var lblFecha = e.Item.FindControl("lblFecha") as Label;
                if (lblFecha != null) lblFecha.Text = string.Format("{0:dd/MM/yyyy}", item.CreateOn);

                var btnDownLoadFile = e.Item.FindControl("btnDownLoadFile") as LinkButton;
                if (btnDownLoadFile != null)
                {
                    btnDownLoadFile.Text = string.Format("{0}", TruncateString(item.NombreArchivo, 25));
                    btnDownLoadFile.ToolTip = string.Format("{0}", item.NombreArchivo);
                    btnDownLoadFile.CommandArgument = string.Format("{0}", item.IdDocumentoReclamo);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(btnDownLoadFile);
                }

                var btnImgeAnexoReclamo = e.Item.FindControl("btnImgeAnexoReclamo") as ImageButton;
                if (btnImgeAnexoReclamo != null)
                {
                    btnImgeAnexoReclamo.ToolTip = string.Format("{0}", item.NombreArchivo);
                    btnImgeAnexoReclamo.ImageUrl = IconoPlantilla(item.NombreArchivo);
                    btnImgeAnexoReclamo.CommandArgument = string.Format("{0}", item.IdDocumentoReclamo);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(btnImgeAnexoReclamo);
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        public void AddErrorMessages(List<string> messages)
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
                    custVal.ValidationGroup = "vsAnexos";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        void InitDocumento()
        {
            Titulo = string.Empty;
            Descripcion = string.Empty;
            ddlCategoriaDocumento.SelectedIndex = 0;
            IsNew = true;
        }

        public void LoadControlData()
        {
            Presenter.LoadInit();
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminDoc(bool visible)
        {
            if (visible)
                wdwAdminDocumentoAnexoReclamo.WindowState = DialogWindowState.Normal;
            else
                wdwAdminDocumentoAnexoReclamo.WindowState = DialogWindowState.Hidden;
        }

        public void LoadCategorias(List<DTO_ValueKey> items)
        {
            ddlCategoria.DataSource = items;
            ddlCategoria.DataTextField = "Value";
            ddlCategoria.DataValueField = "Value";
            ddlCategoria.DataBind();

            ddlCategoria.Items.Insert(0, new ListItem("Todas las categorías", ""));

            ddlCategoriaDocumento.DataSource = items;
            ddlCategoriaDocumento.DataTextField = "Value";
            ddlCategoriaDocumento.DataValueField = "Value";
            ddlCategoriaDocumento.DataBind();
        }

        public void LoadAnexos(List<TBL_ModuloReclamos_DocumentosAnexoReclamo> items)
        {
            rptAnexosList.DataSource = items;
            rptAnexosList.DataBind();
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

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        public string Categoria
        {
            get
            {
                return ddlCategoria.SelectedValue;
            }
            set
            {
                ddlCategoria.SelectedValue = value;
            }
        }

        public string CategoriaDocumento
        {
            get
            {
                return ddlCategoriaDocumento.SelectedValue;
            }
            set
            {
                ddlCategoriaDocumento.SelectedValue = value;
            }
        }

        public string Titulo
        {
            get
            {
                return txtTitulo.Text;
            }
            set
            {
                txtTitulo.Text = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return txtDescripcion.Text;
            }
            set
            {
                txtDescripcion.Text = value;
            }
        }

        public string NombreArchivo
        {
            get
            {
                return fupAnexoArchivo.FileName;
            }
        }

        public byte[] ArchivoAnexo
        {
            get
            {
                return fupAnexoArchivo.FileBytes;
            }
        }

        public bool IsNew
        {
            get
            {
                if (ViewState["AdminDocumentoAnexoReclamo_IsNew"] == null)
                    ViewState["AdminDocumentoAnexoReclamo_IsNew"] = true;

                return Convert.ToBoolean(ViewState["AdminDocumentoAnexoReclamo_IsNew"]);
            }
            set
            {
                ViewState["AdminDocumentoAnexoReclamo_IsNew"] = value;
            }
        }

        public string IdDocumentoSelected
        {
            get
            {
                return string.Format("{0}", ViewState["AdminDocumentoAnexoReclamo_IdDocumentoSelected"]);
            }
            set
            {
                ViewState["AdminDocumentoAnexoReclamo_IdDocumentoSelected"] = value;
            }
        }       

        #endregion

        #endregion
    }
}