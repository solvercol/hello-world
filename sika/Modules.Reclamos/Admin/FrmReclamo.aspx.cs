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

namespace Modules.Reclamos.Admin
{
    public partial class FrmReclamo : ViewPage<ReclamoPresenter, IReclamoView>, IReclamoView
    {
        #region Members

        public const string ROOTUC = "../UserControls/";

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
            ImprimirTituloVentana(string.Format("Reclamo de {0} No. {1}", TipoReclamo, NumeroReclamo));
            LoadUserControl();
        }

        protected override void OnInit(EventArgs e)
        {
            SystemActionsEvent += ReclamoSystemActionsEvent;
            base.OnInit(e);
        }

        void ReclamoSystemActionsEvent(object sender, ViewResulteventArgs e)
        {
            if (e.MessageView == null) return;


            if (e.MessageView.ToString() == "UpdatePanel")
            {
                //todo: Bloque de código que se encargará de actuaizar el panel de resumen cuando el WF termine el paso..
                WucPanelEstado1.ActualizarPanelResumen();
                //if (RefreshEvent != null)
                //    RefreshEvent(null, EventArgs.Empty);
            }
            else
            {
                var oDocument = (RenderTypeControlButtonDto)e.MessageView;
                if (oDocument.MessagesError != null)
                {
                    if (oDocument.MessagesError.Count > 0)
                    {
                        LastLoadedControlMessages = string.Format("{0}WucRenderMessagesError.ascx", ROOTUC);
                        LoadUserControlVentanaMensajes(oDocument.MessagesError);
                        pnlVentanaEmergente.Width = 550;
                        pnlVentanaEmergente.Height = 200;
                        litTitulo.Text = @"Resultado del proceso de validación.";
                        mpeVentanaEmergente.Show();
                    }
                }
                else if (oDocument.OutputParameters.Count > 0)
                {
                    //todo: se ´puede invocar un WUC para  recibir parámetros y ejecutar en un segundo paso el WF

                    //var ventana = oDocument.OutputParameters[0];
                    //switch (ventana)
                    //{
                    //    case "FechaEntrega":
                    //        litTitulo.Text = @"Ingrese la Fecha de Entrega.";
                    //        LoadUserControlVentanaMensajes(null);
                    //        mpeVentanaEmergente.Show();
                    //        break;
                    //}
                }
            }
            
        }
        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FromPage))
            {
                Response.Redirect(string.Format("../Views/FrmMisPendientes.aspx?ModuleId={0}", ModuleId));
            }

            switch (FromPage)
            {
                case "pendientes":
                    Response.Redirect(string.Format("../Views/FrmMisPendientes.aspx?ModuleId={0}", ModuleId));
                    break;
                case "misfecha":
                    Response.Redirect(string.Format("../Views/FrmMisReclamosPorFecha.aspx?ModuleId={0}", ModuleId));
                    break;
                case "misestado":
                    Response.Redirect(string.Format("../Views/FrmMisReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
                    break;
                case "rectipo":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorTipo.aspx?ModuleId={0}", ModuleId));
                    break;
                case "recestado":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
                    break;
                case "recnumero":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorNumero.aspx?ModuleId={0}", ModuleId));
                    break;
                case "rectargetmarket":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorTargetMarket.aspx?ModuleId={0}", ModuleId));
                    break;
                case "admactividad":
                    Response.Redirect(string.Format("FrmAdminActividadReclamo.aspx?ModuleId={0}&IdActividad={1}", ModuleId, IdFrom));
                    break;
                case "admalternativa":
                    Response.Redirect(string.Format("FrmAdminAlternativaReclamo.aspx?ModuleId={0}&IdAlternativa={1}", ModuleId, IdFrom));
                    break;
            }
        }

        protected void BtnEditReclamoClick(object sender, EventArgs e)
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

        private void LoadUserControlVentanaMensajes(IEnumerable<string> items)
        {
            var controlPath = LastLoadedControlMessages;

            if (string.IsNullOrEmpty(controlPath))
            {
                //controlPath = "WucFechaEntrega.ascx";
                return;
            }
            if (string.IsNullOrEmpty(controlPath)) return;
            phlVentanaMensajes.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = "UcRender";
            phlVentanaMensajes.Controls.Add(uc);

            if (items != null)
            {
                if (controlPath.Contains("WucRenderMessagesError"))
                {
                    var bl = this.GetUserControl<WucRenderMessagesError>("UcRender", "phlVentanaMensajes");
                   if (bl != null)
                   {
                      bl.RenderMessages(items);
                   }
                }
            }
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

            //phInfoReclamo.Controls.Clear();
            //var uc = LoadControl(controlPath);
            //uc.ID = idUc;
            //phInfoReclamo.Controls.Add(uc);
        }

        #endregion

        #region Properties

        private string LastLoadedControlMessages
        {
            get
            {
                return ViewState["LastLoadedControlMessages"] as string;
            }
            set
            {
                ViewState["LastLoadedControlMessages"] = value;
            }
        }

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

        #endregion

        #endregion
    }
}