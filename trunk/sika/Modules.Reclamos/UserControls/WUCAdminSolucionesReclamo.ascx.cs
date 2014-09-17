using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Reclamos.UI;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using System.Web.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminSolucionesReclamo : ViewUserControl<AdminSolucionesReclamoPresenter, IAdminSolucionesReclamoView>, IAdminSolucionesReclamoView, IReclamoWebUserControl
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddSolucion_Click(object sender, EventArgs e)
        {
            InitAdminSolucion();
            ShowAdminSolucionWindow(true);
        }

        protected void BtnSaveSolucion_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Observaciones))
                messages.Add("Es necesario ingresar una descripción de solución.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ShowAdminSolucionWindow(true);
                return;
            }

            if (IsNewSolucion)
                Presenter.AddSolucionReclamo();
            else
                Presenter.UpdateSolucionReclamo();
        }

        protected void BtnRemoveSolucion_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedSolucion = btn.CommandArgument;

            Presenter.RemoveSolucionReclamo();
        }

        protected void BtnSelectSolucion_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedSolucion = btn.CommandArgument;

            Presenter.LoadSolucionReclamo();
        }

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivo.HasFile)
            {
                ShowAdminSolucionWindow(true);
                return;
            }

            var archivoAdjunto = new DTO_ValueKey();
            archivoAdjunto.Id = (ArchivosAdjuntos.Count + 1).ToString();
            archivoAdjunto.Value = fupAnexoArchivo.FileName;
            archivoAdjunto.ComplexValue = fupAnexoArchivo.FileBytes;

            ArchivosAdjuntos.Add(archivoAdjunto);
            LoadArchivosAdjuntos(ArchivosAdjuntos);

            ShowAdminSolucionWindow(true);
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

            ShowAdminSolucionWindow(true);
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = btn.CommandArgument;

            var archivo = ArchivosAdjuntos.Where(x => x.Id == IdArchivo).SingleOrDefault();

            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");
            
            ShowAdminSolucionWindow(true);
        }

        #endregion

        #region Repeaters

        protected void RptSolucionList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_Soluciones)(e.Item.DataItem);
                // Bindind data

                var hddIdSolucion = e.Item.FindControl("hddIdSolucion") as HiddenField;
                if (hddIdSolucion != null) hddIdSolucion.Value = string.Format("{0}", item.IdSolucion);

                var lblFechaSolucion = e.Item.FindControl("lblFechaSolucion") as Label;
                if (lblFechaSolucion != null) lblFechaSolucion.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblDescripcionSolucion = e.Item.FindControl("lblDescripcionSolucion") as Label;
                if (lblDescripcionSolucion != null) lblDescripcionSolucion.Text = string.Format("{0}", item.Observaciones);

                var lblDepartamentoSolucion = e.Item.FindControl("lblDepartamentoSolucion") as Label;
                if (lblDepartamentoSolucion != null) lblDepartamentoSolucion.Text = string.Format("{0}", item.Departamento);

                var lblCreateBy = e.Item.FindControl("lblCreateBy") as Label;
                if (lblCreateBy != null) lblCreateBy.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgDeleteSolucion = e.Item.FindControl("imgDeleteSolucion") as ImageButton;
                if (imgDeleteSolucion != null) imgDeleteSolucion.CommandArgument = string.Format("{0}", item.IdSolucion);

                var imgSelectSolucion = e.Item.FindControl("imgSelectSolucion") as ImageButton;
                if (imgSelectSolucion != null) imgSelectSolucion.CommandArgument = string.Format("{0}", item.IdSolucion);
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
                    lnkNombreArchivo.Enabled = !IsNewSolucion;
                    lnkNombreArchivo.CommandArgument = string.Format("{0}", item.Id);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(lnkNombreArchivo); 
                }

                var imgDeleteAnexo = e.Item.FindControl("imgDeleteAnexo") as ImageButton;
                if (imgDeleteAnexo != null)
                {
                    imgDeleteAnexo.CommandArgument = string.Format("{0}", item.Id);
                    imgDeleteAnexo.Enabled = IsNewSolucion;
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
                    custVal.ValidationGroup = "vsSoluciones";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        void InitAdminSolucion()
        {
            wddDepartamento.SelectedItemIndex = 0;
            Observaciones = string.Empty;
            ArchivosAdjuntos = new List<DTO_ValueKey>();
            LoadArchivosAdjuntos(ArchivosAdjuntos);
            IsNewSolucion = true;
            IdSelectedSolucion = string.Empty;
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

        public void ShowAdminSolucionWindow(bool visible)
        {
            if (visible)
                mpeAdminSolucion.Show();
            else
                mpeAdminSolucion.Hide();
        }

        public void LoadSolucionesReclamo(List<TBL_ModuloReclamos_Soluciones> items)
        {
            rptSolucionList.DataSource = items;
            rptSolucionList.DataBind();
        }

        public void LoadDepartamentos(List<DTO_ValueKey> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Value).ToList();
            }

            wddDepartamento.DataSource = items;
            wddDepartamento.TextField = "Value";
            wddDepartamento.ValueField = "Id";
            wddDepartamento.DataBind();

            wddDepartamento.SelectedItemIndex = 0;
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

        public string Departamento
        {
            get
            {
                return wddDepartamento.SelectedValue;
            }
            set
            {
                wddDepartamento.SelectedValue = value;
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
                txtObservaciones.Text = value;
            }
        }

        public string IdSelectedSolucion
        {
            get
            {
                return ViewState["AdminSolucion_IdSolucionSelect"] as string;
            }
            set
            {
                ViewState["AdminSolucion_IdSolucionSelect"] = value;
            }
        }

        public bool IsNewSolucion
        {
            get
            {
                if (ViewState["AdminSolucion_IsNewSolucion"] == null)
                    ViewState["AdminSolucion_IsNewSolucion"] = false;

                return Convert.ToBoolean(ViewState["AdminSolucion_IsNewSolucion"]);
            }
            set
            {
                ViewState["AdminSolucion_IsNewSolucion"] = value;
            }
        }

        public List<DTO_ValueKey> ArchivosAdjuntos
        {
            get
            {
                if (Session["AdminSolucion_ArchivosAdjuntos"] == null)
                    Session["AdminSolucion_ArchivosAdjuntos"] = new List<DTO_ValueKey>();

                return Session["AdminSolucion_ArchivosAdjuntos"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminSolucion_ArchivosAdjuntos"] = value;
            }
        }

        public bool CanEditSoluciones
        {
            get
            {
                return btnNuevoSolucion.Visible;
            }
            set
            {
                btnNuevoSolucion.Visible = value;
            }
        }

        #endregion        

        #endregion
    }
}