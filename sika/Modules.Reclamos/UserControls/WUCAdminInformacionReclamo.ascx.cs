using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminInformacionReclamo : ViewUserControl<AdminInformacionReclamoPresenter, IAdminInformacionReclamoView>, IAdminInformacionReclamoView, IReclamoWebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInitReclamoControl();
        }

        #region IReclamoWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

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
            get
            {
                return Request.QueryString["IdReclamo"];
            }
        }

        public string IdCategoriaReclamo
        {
            get
            {
                return ViewState["AdminInformacionReclamo_IdCategoriaReclamo"] == null ? string.Empty : ViewState["AdminInformacionReclamo_IdCategoriaReclamo"].ToString();
            }
            set
            {
                ViewState["AdminInformacionReclamo_IdCategoriaReclamo"] = value;
            }
        }

        public string IdGrupoInformacion
        {
            get
            {
                return ViewState["AdminInformacionReclamo_IdGrupoInformacion"] == null ? string.Empty : ViewState["AdminInformacionReclamo_IdGrupoInformacion"].ToString();
            }
            set
            {
                ViewState["AdminInformacionReclamo_IdGrupoInformacion"] = value;
            }
        }

        public string TipoReclamo
        {
            get
            {
                return ViewState["AdminInformacionReclamo_TipoReclamo"] == null ? string.Empty : ViewState["AdminInformacionReclamo_TipoReclamo"].ToString();
            }
            set
            {
                ViewState["AdminInformacionReclamo_TipoReclamo"] = value;
            }
        }

        public void LoadInitReclamoControl()
        {
            if (string.IsNullOrEmpty(TipoReclamo)) return;

            var controlPath = "";
            var idUc = "";

            if (TipoReclamo == "Producto")
            {
                controlPath = string.Format("WUCReadingReclamoProductoDetalle.ascx");
                idUc = "WUCReadingReclamoProductoDetalle";
            }
            else
            {
                controlPath = string.Format("WUCReadingReclamoServicioT{0}Detalle.ascx", IdGrupoInformacion);

                idUc = "WUCReadingReclamoServicioDetalle";
            }

            phlInfoReclamoContainer.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = idUc;
            phlInfoReclamoContainer.Controls.Add(uc);

            if (uc is IReclamoWebUserControl)
                ((IReclamoWebUserControl)uc).LoadControlData();
        }
    }
}