using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using Applications.MainModule.WorkFlow.DTO;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Structures;
using Modules.AccionesPC.UI;
using Modules.AccionesPC.UserControls;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;

namespace Modules.AccionesPC.Admin
{
    public partial class FrmSolicitudAPC : ViewPage<SolicitudAPCPresenter, ISolicitudAPCView>, ISolicitudAPCView
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

        public string InputWindow
        {
            get
            {
                return ViewState["InputWindow"] as string;
            }
            set
            {
                ViewState["InputWindow"] = value;
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Acciones Preventivas Correctivas."));
            LoadUserControl();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }     

        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(FromPage))
            //{
            //    Response.Redirect(string.Format("../Views/FrmMisPendientes.aspx?ModuleId={0}", ModuleId));
            //}

            //switch (FromPage)
            //{
            //    case "pendientes":
            //        Response.Redirect(string.Format("../Views/FrmMisPendientes.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "misfecha":
            //        Response.Redirect(string.Format("../Views/FrmMisReclamosPorFecha.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "misestado":
            //        Response.Redirect(string.Format("../Views/FrmMisReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "rectipo":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorTipo.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "recestado":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "recnumero":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorNumero.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "rectargetmarket":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorTargetMarket.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "rectautor":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorAutor.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "reccliente":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorCliente.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "recfecha":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorFecha.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "recproducto":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorProducto.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "recresponsable":
            //        Response.Redirect(string.Format("../Views/FrmReclamosPorResponsable.aspx?ModuleId={0}", ModuleId));
            //        break;
            //    case "admactividad":
            //        Response.Redirect(string.Format("FrmAdminActividadReclamo.aspx?ModuleId={0}&IdActividad={1}", ModuleId, IdFrom));
            //        break;
            //    case "admalternativa":
            //        Response.Redirect(string.Format("FrmAdminAlternativaReclamo.aspx?ModuleId={0}&IdAlternativa={1}", ModuleId, IdFrom));
            //        break;
            //}
        }

        protected void BtnEditSolicitudClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminSolicitudAPC.aspx?ModuleId={0}&IdSolicitud={1}&from=solicitud",
                                               ModuleId, IdSolicitud
                                               ));
        }

        protected void BtnViewReclamo_Click(object sender, EventArgs e)
        {
            //Response.Redirect(string.Format("FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from=admactividad&idfrom={2}", ModuleId, IdReclamo, IdActividad));
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
                controlPath = "WUCAdminInformacionSolicitud.ascx";
                if (mnuSecciones.Items.Count > 0)
                    mnuSecciones.Items[0].Selected = true;
            }
            if (string.IsNullOrEmpty(controlPath)) return;

            if (!controlPath.Contains('/'))
            {
                idUc = controlPath.Split('.')[0];
                controlPath = string.Format("{0}{1}", ROOTUC, controlPath);
            }
            else
            {
                idUc = "uc";
            }

            phlContent.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = idUc;
            phlContent.Controls.Add(uc);
            if (controlPath.Contains("WucDocumentLibrary"))
                RegistrarControlScriptManager(uc);

            if (uc is ISolicitudWebUserControl)
            {
                var ucReclamo = ((ISolicitudWebUserControl)uc);
                ucReclamo.LoadControlData();
                //ucReclamo.RiseFatherPostback += RefreshReclamoInfo;
            }   
        }       

        #endregion

        #region View Members

        #region Methods

        void RefreshSolicitudInfo()
        {
            Presenter.LoadSolicitud();
        }

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

        public void LoadInitSolicitudControl()
        {
            var controlPath = "";
            var idUc = "";

            controlPath = string.Format("{0}WUCAdminInformacionSolicitud.ascx", ROOTUC);
            idUc = "WUCAdminInformacionSolicitud";

            //phInfoReclamo.Controls.Clear();
            //var uc = LoadControl(controlPath);
            //uc.ID = idUc;
            //phInfoReclamo.Controls.Add(uc);
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



        public string IdSolicitud
        {
            get
            {
                return Request.QueryString["IdSolicitud"];
            }
        }

        public string TipoAccion
        {
            get
            {
                return lblTipoAccion.Text;
            }
            set
            {
                lblTipoAccion.Text = value;
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

        public string GerenteArea
        {
            get
            {
                return lblGerenteArea.Text;
            }
            set
            {
                lblGerenteArea.Text = value;
            }
        }

        public string ResponsableAccion
        {
            get
            {
                return lblResponsableAccion.Text;
            }
            set
            {
                lblResponsableAccion.Text = value;
            }
        }

        public string FechaInicio
        {
            get
            {
                return lblFechaInicio.Text;
            }
            set
            {
                lblFechaInicio.Text = value;
            }
        }

        public string FechaFinal
        {
            get
            {
                return lblFechaFin.Text;
            }
            set
            {
                lblFechaFin.Text = value;
            }
        }

        public string LogInfoMessage
        {
            set { lblLogInfo.Text = value; }
        }

        public string IdReclamo
        {
            get
            {
                return ViewState["SolicitudAPC_IdReclamo"] == null ? "0" : ViewState["SolicitudAPC_IdReclamo"].ToString();
            }
            set
            {
                ViewState["SolicitudAPC_IdReclamo"] = value;
            }
        }

        public string NumeroReclamo
        {
            get
            {
                return ViewState["SolicitudAPC_NumeroReclamo"] == null ? string.Empty : ViewState["SolicitudAPC_NumeroReclamo"].ToString();
            }
            set
            {
                ViewState["SolicitudAPC_NumeroReclamo"] = value;
            }
        }

        public string TipoReclamo
        {
            get
            {
                return ViewState["SolicitudAPC_TipoReclamo"] == null ? string.Empty : ViewState["SolicitudAPC_TipoReclamo"].ToString();
            }
            set
            {
                ViewState["SolicitudAPC_TipoReclamo"] = value;
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

        public bool ShowInfoReclamo
        {
            get
            {
                return trInfoReclamo.Visible;
            }
            set
            {
                trInfoReclamo.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}