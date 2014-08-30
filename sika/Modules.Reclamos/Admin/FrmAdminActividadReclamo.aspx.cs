using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Core;
using Applications.MainModule.WorkFlow.DTO;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Reclamos.UserControls;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;
using System.Web.UI;

namespace Modules.Reclamos.Admin
{
    public partial class FrmAdminActividadReclamo : ViewPage<AdminActividadReclamoPresentador, IAdminActividadReclamoView>, IAdminActividadReclamoView
    {
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

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Actividad - Reclamo de {0} No. {1}", TipoReclamo, NumeroReclamo));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {

        }

        protected void BtnViewReclamo_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from=admactividad&idfrom={2}", ModuleId, IdReclamo, IdActividad));
        }

        protected void BtnEditActividadClick(object sender, EventArgs e)
        {
            EnableEdit(true);
            Presenter.LoadArhchivosAdjuntos();
        }

        protected void BtnCancelActividadClick(object sender, EventArgs e)
        {
            EnableEdit(false);
            Presenter.LoadArhchivosAdjuntos();
        }

        protected void BtnSaveActividadClick(object sender, EventArgs e)
        {
            Presenter.UpdateActividadReclamo();
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
            ddlEstado.Visible = enabled;

            fupAnexoArchivo.Visible = enabled;
            btnAddArchivoAdjunto.Visible = enabled;

            lblObservaciones.Visible = !enabled;
            lblEstado.Visible = !enabled;

            btnCancel.Visible = enabled;
            btnSave.Visible = enabled;
            btnEdit.Visible = !enabled;
        }

        public void LoadUsuariosCopia(List<DTO_ValueKey> items)
        {
            lstUsuariosCopia.DataSource = items;
            lstUsuariosCopia.DataValueField = "Id";
            lstUsuariosCopia.DataTextField = "Value";
            lstUsuariosCopia.DataBind();
        }

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntos.DataSource = items;
            rptArchivosAdjuntos.DataBind();
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

        public string IdActividad
        {
            get
            {
                return Request.QueryString["IdActividad"];
            }
        }

        public string IdReclamo
        {
            get
            {
                return ViewState["AdminActividad_IdReclamo"] == null ? string.Empty : ViewState["AdminActividad_IdReclamo"].ToString();
            }
            set
            {
                ViewState["AdminActividad_IdReclamo"] = value;
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

        public string TotalCostoReclamo
        {
            get
            {
                return lblTotalCostoReclamo.Text;
            }
            set
            {
                lblTotalCostoReclamo.Text = value;
            }
        }

        public string UsuarioAsignacion
        {
            get
            {
                return lblAsignado.Text;
            }
            set
            {
                lblAsignado.Text = value;
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

        public string Actividad
        {
            get
            {
                return lblActividad.Text;
            }
            set
            {
                lblActividad.Text = value;
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

        #endregion

        #endregion    
    }
}