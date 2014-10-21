using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.AccionesPC.UI;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;
using Application.Core;
using System.Web.UI;

namespace Modules.AccionesPC.UserControls
{
    public partial class WUCAdminActividadesSolicitudes : ViewUserControl<AdminActividadesSolicitudPresenter, IAdminActividadesSolicitudView>, IAdminActividadesSolicitudView, ISolicitudWebUserControl
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





        protected void BtnSaveActividad_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Descripcion))
                messages.Add("Es necesario ingresar una descripción.");

            if (string.IsNullOrEmpty(wddUsuarioSeguimiento.SelectedValue))
            {
                messages.Add("Seleccione nuevamente el usuario de seguimiento.");

            }

            if (string.IsNullOrEmpty(wddUsuarioEjecucion.SelectedValue))
            {
                messages.Add("Seleccione nuevamente el usuario de ejecución.");

            }

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ShowAdminActividadWindow(true);
                return;
            }



            if (IsNewActividad)
                Presenter.AddActividadSolicitud();
            else
                Presenter.UpdateActividadSolicitud();
        }

        protected void BtnRemoveActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedActividad = btn.CommandArgument;

            Presenter.RemoveActividadSolicitud();
        }

        protected void BtnSelectActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedActividad = btn.CommandArgument;

            Response.Redirect(string.Format("../Admin/FrmAdminActividadSolicitud.aspx?ModuleId={0}&IdActividad={1}&from=solicitud&IdSolicitud={2}&fromaux={3}&idfromaux={4}", IdModule, IdSelectedActividad, IdSolicitud, FromPage, IdFrom));

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
                var item = (TBL_ModuloAPC_Actividades)(e.Item.DataItem);
                // Bindind data

                var hddIdActividad = e.Item.FindControl("hddIdActividad") as HiddenField;
                if (hddIdActividad != null) hddIdActividad.Value = string.Format("{0}", item.IdActividad);

                var lblFechaActividad = e.Item.FindControl("lblFechaActividad") as Label;
                if (lblFechaActividad != null) lblFechaActividad.Text = item.FechaActividad.ToShortDateString();

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = item.Descripcion;

                var lblSeguimiento = e.Item.FindControl("lblSeguimiento") as Label;
                if (lblSeguimiento != null) lblSeguimiento.Text = string.Format("{0}", item.TBL_Admin_Usuarios1.Nombres);

                var lblAsignado = e.Item.FindControl("lblEjecucion") as Label;
                if (lblAsignado != null) lblAsignado.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios2.Nombres);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item.EstadoActividad);

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
            wddUsuarioSeguimiento.SelectedIndex = 0;
            wddUsuarioEjecucion.SelectedIndex = 0;
            IdUsuarioSeguimiento = UserSession.IdUser.ToString();
            IdUsuarioEjecucion = UserSession.IdUser.ToString();
            Descripcion = string.Empty;
            FechaActividad = DateTime.Now;
            ArchivosAdjuntos = new List<DTO_ValueKey>();
            LoadArchivosAdjuntos(ArchivosAdjuntos);
            IsNewActividad = true;
            EnableEdit(true);
        }

     

        #endregion

        #region ISolicitudWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminActividadWindow(bool visible)
        {
            if (visible)
                mpeAdminActividad.Show();
            else
                mpeAdminActividad.Hide();
        }

        public void LoadActividadesSolicitud(List<TBL_ModuloAPC_Actividades> items)
        {
            rptActividadesList.DataSource = items;
            rptActividadesList.DataBind();
        }

        public void LoadUsuarioSeguimiento(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddUsuarioSeguimiento.DataSource = items;
            wddUsuarioSeguimiento.DataTextField = "Nombres";
            wddUsuarioSeguimiento.DataValueField = "IdUser";
            wddUsuarioSeguimiento.DataBind();
        }

        public void LoadUsuarioEjecucion(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddUsuarioEjecucion.DataSource = items;
            wddUsuarioEjecucion.DataTextField = "Nombres";
            wddUsuarioEjecucion.DataValueField = "IdUser";
            wddUsuarioEjecucion.DataBind();
        }

        public void EnableEdit(bool enable)
        {
            
            wddUsuarioEjecucion.Visible = enable;
            wddUsuarioSeguimiento.Visible = enable;
            txtDescripcion.Visible = enable;
            txtFechaActividad.Visible = enable;
            btnGuardar.Visible = enable;
            lblUsuarioSeguimiento.Visible = !enable;
            lblUsuarioEjecucion.Visible = !enable;
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

        public string IdSolicitud
        {
            get { return Request.QueryString.Get("IdSolicitud"); }
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

        public string IdUsuarioSeguimiento
        {
            get
            {
                return wddUsuarioSeguimiento.SelectedValue;
            }
            set
            {
                wddUsuarioSeguimiento.SelectedValue = value;
                wddUsuarioSeguimiento.Text = wddUsuarioSeguimiento.SelectedValue.ToString();
            }
        }

        public string IdUsuarioEjecucion
        {
            get
            {
                return wddUsuarioEjecucion.SelectedValue;
            }
            set
            {
                wddUsuarioEjecucion.SelectedValue = value;
                wddUsuarioEjecucion.Text = wddUsuarioEjecucion.SelectedValue.ToString();
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

        public bool CanAddActividades
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