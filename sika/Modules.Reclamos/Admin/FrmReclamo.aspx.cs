using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;

namespace Modules.Reclamos.Admin
{
    public partial class FrmReclamo : ViewPage<ReclamoPresenter, IReclamoView>, IReclamoView
    {
        #region Members

        public const string ROOTUC = "../UserControls/";

        private string LastLoadedControl
        {
            get
            {
                return ViewState["LastLoaded"] as string;
            }
            set
            {
                ViewState["LastLoaded"] = value;
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("RECLAMO No. {0}", NumeroReclamo));
            LoadUserControl();
        }

        #endregion

        #region Buttons

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmListaGeneralReclamos.aspx?ModuleId={0}", ModuleId));
        }

        protected void BtnEditReclamo_Click(object sender, EventArgs e)
        {
            if (TipoReclamo == "Producto")
            {
                Response.Redirect(string.Format("FrmAddReclamo.aspx?ModuleId={0}&tr={1}&IdReclamo={2}", ModuleId, TipoReclamo, IdReclamo));
            }
            else
            {
                Response.Redirect(string.Format("FrmAddReclamo.aspx?ModuleId={0}&tr={1}&cat={2}&gruinf={3}&IdReclamo={4}",
                                                ModuleId, TipoReclamo, IdCategoriaReclamo, IdGrupoInformacion, IdReclamo
                                                ));
            }
        }

        #endregion

        #region Menu

        protected void MnuItemClick(object sender, MenuEventArgs e)
        {
            LastLoadedControl = e.Item.Value;
            LoadUserControl();
        }

        #endregion

        #endregion

        #region Methods

        private void LoadUserControl()
        {
            var controlPath = LastLoadedControl;
            var idUc = "";

            if (string.IsNullOrEmpty(controlPath))
            {
                controlPath = "WUCAdminInformacionReclamo.ascx";
                if (mnuSecciones.Items.Count > 0)
                    mnuSecciones.Items[0].Selected = true;
            }
            if (string.IsNullOrEmpty(controlPath)) return;

            idUc = controlPath.Split('.')[0];
            controlPath = string.Format("{0}{1}", ROOTUC, controlPath);

            phlContent.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = idUc;
            phlContent.Controls.Add(uc);

            if (uc is IReclamoWebUserControl)
                ((IReclamoWebUserControl)uc).LoadControlData();
        }

        #endregion

        #region View Members

        #region Methods

        public void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones)
        {
            mnuSecciones.Items.Clear();
            foreach (var seccione in from tab in secciones select tab)
            {
                var opcion = new MenuItem
                {
                    Text = seccione.Titulo,
                    Value =
                        (string.IsNullOrEmpty(IsEdit))
                            ? seccione.PathEdit
                            : seccione.PathPreview
                };
                mnuSecciones.Items.Add(opcion);
            }
            mnuSecciones.Items[0].Selected = true;
        }

        public void LoadInitReclamoControl()
        {
            var controlPath = "";
            var idUc = "";

            if (TipoReclamo == "Producto")
            {
                controlPath = string.Format("{0}WUCReadingReclamoProducto.ascx", ROOTUC);
                idUc = "WUCReadingReclamoProducto";
            }
            else
            {
                controlPath = string.Format("{0}WUCReadingReclamoServicio.ascx", ROOTUC);

                idUc = "WUCReadingReclamoServicio";
            }

            phInfoReclamo.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = idUc;
            phInfoReclamo.Controls.Add(uc);
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
            get
            {
                return Request.QueryString["IdReclamo"];
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

        public string IdCategoriaReclamo
        {
            get
            {
                return ViewState["Reclamo_IdCategoriaReclamo"] == null ? string.Empty : ViewState["Reclamo_IdCategoriaReclamo"].ToString();
            }
            set
            {
                ViewState["Reclamo_IdCategoriaReclamo"] = value;
            }
        }

        public string IdGrupoInformacion
        {
            get
            {
                return ViewState["Reclamo_IdGrupoInformacion"] == null ? string.Empty : ViewState["Reclamo_IdGrupoInformacion"].ToString();
            }
            set
            {
                ViewState["Reclamo_IdGrupoInformacion"] = value;
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

        #endregion

        #endregion
    }
}