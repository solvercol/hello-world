﻿using System;
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
    public partial class WUCAdminActividadesReclamo : ViewUserControl<AdminActividadesReclamoPresenter, IAdminActividadesReclamoView>, IAdminActividadesReclamoView, IReclamoWebUserControl
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

        protected void BtnAddActividad_Click(object sender, EventArgs e)
        {
            InitAdminActividad();
            ShowAdminActividadWindow(true);
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            ShowAdminActividadWindow(false);
        }

        protected void BtnAddUsuarioCopia_Click(object sender, EventArgs e)
        {
            var usuarioCopia = new DTO_ValueKey() { Id = IdUsuarioCopia, Value = wddUsuarioCopia.SelectedItem.Text };
            if (!ExistsInCopia(usuarioCopia))
                UsuariosCopia.Add(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);

            cpeCopiarUsuarios.Collapsed = false;
            ShowAdminActividadWindow(true);
        }

        protected void BtnRemoveUsuarioCopia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdUsuarioCopiaSelected)) return;

            var usuarioCopia = UsuariosCopia.Where(x => x.Id == IdUsuarioCopiaSelected).SingleOrDefault();

            if (usuarioCopia != null)
                UsuariosCopia.Remove(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);

            cpeCopiarUsuarios.Collapsed = false;
            ShowAdminActividadWindow(true);
        }

        protected void BtnSaveActividad_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Descripcion))
                messages.Add("Es necesario ingresar una descripción.");

            if (string.IsNullOrEmpty(wddUsuarioAsignacion.SelectedValue))
            {
                messages.Add("Seleccione nuevamente el asignar a.");
            }

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ShowAdminActividadWindow(true);
                return;
            }

            if (IsNewActividad)
                Presenter.AddActividadReclamo();
            else
                Presenter.UpdateActividadReclamo();
        }

        protected void BtnRemoveActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedActividad = btn.CommandArgument;

            Presenter.RemoveActividadReclamo();
        }

        protected void BtnSelectActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedActividad = btn.CommandArgument;

            Response.Redirect(string.Format("../Admin/FrmAdminActividadReclamo.aspx?ModuleId={0}&IdActividad={1}&from=reclamo&IdReclamo={2}&fromaux={3}&idfromaux={4}", IdModule, IdSelectedActividad, IdReclamo, FromPage, IdFrom));

            //Presenter.LoadActividadReclamo();
        }

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivo.HasFile)
            {
                ShowAdminActividadWindow(true);
                return;
            }

            var archivoAdjunto = new DTO_ValueKey();
            archivoAdjunto.Id = (ArchivosAdjuntos.Count + 1).ToString();
            archivoAdjunto.Value = fupAnexoArchivo.FileName;
            archivoAdjunto.ComplexValue = fupAnexoArchivo.FileBytes;

            ArchivosAdjuntos.Add(archivoAdjunto);
            LoadArchivosAdjuntos(ArchivosAdjuntos);

            ShowAdminActividadWindow(true);
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

            ShowAdminActividadWindow(true);
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = ArchivosAdjuntos.Where(x => x.Id == IdArchivo).SingleOrDefault();

            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");

            ShowAdminActividadWindow(true);
        }

        #endregion

        #region Repeaters

        protected void RptActividadesList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_Actividades)(e.Item.DataItem);
                // Bindind data

                var hddIdActividad = e.Item.FindControl("hddIdActividad") as HiddenField;
                if (hddIdActividad != null) hddIdActividad.Value = string.Format("{0}", item.IdActividad);

                var lblActividad = e.Item.FindControl("lblActividad") as Label;
                if (lblActividad != null)
                {
                    if (item.TBL_ModuloReclamos_ActividadesReclamo != null)
                        lblActividad.Text = string.Format("{0}", item.TBL_ModuloReclamos_ActividadesReclamo.Nombre);
                }

                var lblFechaActividad = e.Item.FindControl("lblFechaActividad") as Label;
                if (lblFechaActividad != null) lblFechaActividad.Text = item.Fecha.ToShortDateString();

                var lblAsignado = e.Item.FindControl("lblAsignado") as Label;
                if (lblAsignado != null) lblAsignado.Text = string.Format("{0}", item.TBL_Admin_Usuarios2.Nombres);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item.Estado);

                var imgSelectActividad = e.Item.FindControl("imgSelectActividad") as ImageButton;
                if (imgSelectActividad != null) imgSelectActividad.CommandArgument = string.Format("{0}", item.IdActividad);

                var imgDeleteActividad = e.Item.FindControl("imgDeleteActividad") as ImageButton;
                if (imgDeleteActividad != null) imgDeleteActividad.CommandArgument = string.Format("{0}", item.IdActividad);
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
                    lnkNombreArchivo.Enabled = !IsNewActividad;
                    lnkNombreArchivo.CommandArgument = string.Format("{0}", item.Id);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(lnkNombreArchivo);
                }

                var imgDeleteAnexo = e.Item.FindControl("imgDeleteAnexo") as ImageButton;
                if (imgDeleteAnexo != null)
                {
                    imgDeleteAnexo.CommandArgument = string.Format("{0}", item.Id);
                    imgDeleteAnexo.Visible = IsNewActividad;
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
                    custVal.ValidationGroup = "vsActividades";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        void InitAdminActividad()
        {
            wddActividadesReclamo.SelectedIndex = 0;
            IdUsuarioAsignacion = UserSession.IdUser.ToString();
            Descripcion = string.Empty;
            FechaActividad = DateTime.Now;
            UsuariosCopia = new List<DTO_ValueKey>();
            LoadUsuariosCopia(UsuariosCopia);
            ArchivosAdjuntos = new List<DTO_ValueKey>();
            LoadArchivosAdjuntos(ArchivosAdjuntos);
            IsNewActividad = true;
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
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(btnAddArchivoAdjunto);

            Presenter.LoadInitData();
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminActividadWindow(bool visible)
        {
            if (visible)
                mpeAdminActividad.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
            else
                mpeAdminActividad.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
        }

        public void LoadActividadesReclamo(List<TBL_ModuloReclamos_Actividades> items)
        {
            rptActividadesList.DataSource = items;
            rptActividadesList.DataBind();
        }

        public void LoadUsuarioAsignacion(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddUsuarioAsignacion.DataSource = items;
            wddUsuarioAsignacion.DataTextField = "Nombres";
            wddUsuarioAsignacion.DataValueField = "IdUser";
            wddUsuarioAsignacion.DataBind();
        }

        public void LoadUsuarioCopia(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddUsuarioCopia.DataSource = items;
            wddUsuarioCopia.DataTextField = "Nombres";
            wddUsuarioCopia.DataValueField = "IdUser";
            wddUsuarioCopia.DataBind();
        }

        public void LoadActividadesAdmin(List<TBL_ModuloReclamos_ActividadesReclamo> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombre).ToList();
            }

            wddActividadesReclamo.DataSource = items;
            wddActividadesReclamo.DataTextField = "Nombre";
            wddActividadesReclamo.DataValueField = "IdActividad";
            wddActividadesReclamo.DataBind();
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
            wddActividadesReclamo.Visible = enable;
            wddUsuarioAsignacion.Visible = enable;
            wddUsuarioCopia.Visible = enable;
            txtDescripcion.Visible = enable;
            txtFechaActividad.Visible = enable;
            lstUsuariosCopia.Enabled = enable;
            btnGuardar.Visible = enable;
            btnAddCopia.Visible = enable;
            btnRemoveCopia.Visible = enable;

            lblActividadesReclamo.Visible = !enable;
            lblUsuarioAsignacion.Visible = !enable;            
            lblDescripcion.Visible = !enable;
            lblFechaActividad.Visible = !enable;

            fupAnexoArchivo.Visible = enable;
            btnAddArchivoAdjunto.Visible = enable;
        }

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();
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

        public string IdActividadReclamo
        {
            get
            {
                return wddActividadesReclamo.SelectedValue;
            }
            set
            {
                wddActividadesReclamo.SelectedValue = value;
                lblActividadesReclamo.Text = wddActividadesReclamo.SelectedItem.Text;
            }
        }

        public string Estado
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
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
                lblDescripcion.Text = value;
            }
        }

        public DateTime FechaActividad
        {
            get
            {
                return Convert.ToDateTime(txtFechaActividad.Text);
            }
            set
            {
                txtFechaActividad.Text = string.Format("{0:dd/MM/yyyy}", value);
                lblFechaActividad.Text = string.Format("{0:dd/MM/yyyy}", value);
            }
        }

        public string IdUsuarioAsignacion
        {
            get
            {
                return wddUsuarioAsignacion.SelectedValue;
            }
            set
            {
                wddUsuarioAsignacion.SelectedValue = value;
                lblUsuarioAsignacion.Text = wddUsuarioAsignacion.SelectedItem.Text;
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

        public string IdSelectedActividad
        {
            get
            {
                return ViewState["AdminActividad_IdActividadSelect"] as string;
            }
            set
            {
                ViewState["AdminActividad_IdActividadSelect"] = value;
            }
        }

        public bool IsNewActividad
        {
            get
            {
                if (ViewState["AdminActividad_IsNewActividad"] == null)
                    ViewState["AdminActividad_IsNewActividad"] = false;

                return Convert.ToBoolean(ViewState["AdminActividad_IsNewActividad"]);
            }
            set
            {
                ViewState["AdminActividad_IsNewActividad"] = value;
            }
        }

        public List<DTO_ValueKey> UsuariosCopia
        {
            get
            {
                if (Session["AdminActividades_UsuarioCopia"] == null)
                    Session["AdminActividades_UsuarioCopia"] = new List<DTO_ValueKey>();

                return Session["AdminActividades_UsuarioCopia"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminActividades_UsuarioCopia"] = value;
            }
        }

        public List<DTO_ValueKey> ArchivosAdjuntos
        {
            get
            {
                if (Session["AdminActividades_ArchivosAdjuntos"] == null)
                    Session["AdminActividades_ArchivosAdjuntos"] = new List<DTO_ValueKey>();

                return Session["AdminActividades_ArchivosAdjuntos"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminActividades_ArchivosAdjuntos"] = value;
            }
        }

        public bool CanEditActividades
        {
            get
            {
                return btnNuevoActividad.Visible;
            }
            set
            {
                btnNuevoActividad.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}