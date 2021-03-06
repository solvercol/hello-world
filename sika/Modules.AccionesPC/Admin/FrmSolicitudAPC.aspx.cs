﻿using System;
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

        private string InputWindow
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
            ImprimirTituloVentana(string.Format("Acciones Preventivas Correctivas - {0}", NumeroAPC));
            LoadUserControl();
            LoadUserControlVentanaMensajes(null);

        }

        protected override void OnInit(EventArgs e)
        {
            SystemActionsEvent += SolicitudSystemActionsEvent;
            base.OnInit(e);
        }

        void SolicitudSystemActionsEvent(object sender, ViewResulteventArgs e)
        {
            if (e.MessageView == null) return;


            if (e.MessageView.ToString() == "UpdatePanel")
            {
                //todo: Bloque de código que se encargará de actuaizar el panel de resumen cuando el WF termine el paso..
                WucPanelEstado1.ActualizarPanelResumen();
                ActualizarControlUsuarioActivo();
                WucLogSolicitudesApc1.RefreshInfo();

                if (FilterEvent != null)
                    FilterEvent(null, EventArgs.Empty);

                ShowMessageOk("Proceso realizado satisfactoriamente.");
            }
            else
            {
                var oDocument = (RenderTypeControlButtonDto)e.MessageView;

                if (oDocument.MessagesError.Count > 0)
                {
                    LastLoadedControlMessages = string.Format("{0}WucRenderMessagesError.ascx", ROOTUC);
                    LoadUserControlVentanaMensajes(oDocument.MessagesError);
                    pnlVentanaEmergente.Width = 550;
                    pnlVentanaEmergente.Height = 200;
                    litTitulo.Text = @"Resultado del proceso de validación.";
                    mpeVentanaEmergente.Show();
                }
                else if (oDocument.OutputParameters.Count > 0)
                {
                    var ventana = oDocument.OutputParameters[0];
                    InputWindow = ventana;

                    switch (ventana)
                    {
                        case "ResponsableSolicitud":
                            litTitulo.Text = @"Seleccione el Responsable.";
                            LastLoadedControlMessages = "../UserControls/WucAsignarResponsableSolicitud.ascx";
                            LoadUserControlVentanaMensajes(null);
                            mpeVentanaEmergente.Show();
                            break;

                        case "EnvioActividades":
                            litTitulo.Text = @"Envío de notificacíones";
                            LastLoadedControlMessages = "../UserControls/WucEnviarActividades.ascx";
                            LoadUserControlVentanaMensajes(null);
                            mpeVentanaEmergente.Show();
                            break;

                        case "CerrarSolicitud":
                            litTitulo.Text = @"Cierre de la acción";
                            pnlVentanaEmergente.Width = 600;
                            pnlVentanaEmergente.Height = 250;
                            LastLoadedControlMessages = "../UserControls/WucCierreSolicitud.ascx";
                            LoadUserControlVentanaMensajes(null);
                            mpeVentanaEmergente.Show();
                            break;
                    }
                }
            }

        }

        private void ActualizarControlUsuarioActivo()
        {
            var uc = this.GetUserControl<WucSeguimiento>("WucSeguimiento", "phlContent");
            if (uc != null)
            {
                uc.Actualizarlistado();
                return;
            }

            var ucInfo = this.GetUserControl<WUCAdminInformacionSolicitud>("WUCAdminInformacionSolicitud", "phlContent");
            if (ucInfo != null)
            {
                ucInfo.ReloadWuc();
                return;
            }

            var ucCausas = this.GetUserControl<WUCAdminCausasSolicitud>("WUCAdminCausasSolicitud", "phlContent");
            if (ucCausas != null)
            {
                ucCausas.ReloadWuc();
                return;
            }

            var ucActionPlan = this.GetUserControl<WUCAdminActividadesSolicitudes>("WUCAdminActividadesSolicitudes", "phlContent");
            if (ucActionPlan != null)
            {
                ucActionPlan.ReloadWuc();
                return;
            }

            var ucComment = this.GetUserControl<WUCAdminComentariosRespuestaSolicitud>("WUCAdminComentariosRespuestaSolicitud", "phlContent");
            if (ucComment != null)
            {
                ucComment.ReloadWuc();
                return;
            }

            var ucAnexos = this.GetUserControl<WUCAdminAnexosSolicitud>("WUCAdminAnexosSolicitud", "phlContent");
            if (ucAnexos != null)
            {
                ucAnexos.ReloadWuc();
                return;
            }

        }
        #endregion

        #region Buttons

        protected void BtnRegresarClick(object sender, EventArgs e)
        {

            if( !string.IsNullOrEmpty(Request.QueryString["IdReclamo"]))
            {
                Response.Redirect(string.Format("../../Reclamos/Admin/FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from=pendientes", IdModuleReclamo, Request.QueryString["IdReclamo"]));
            }

            switch (FromPage)
            {
                case "mispendientes":
                    Response.Redirect(string.Format("../Views/FrmMisSolicitudesPendientes.aspx?ModuleId={0}", ModuleId));
                    break;
                case "misolestados":
                    Response.Redirect(string.Format("../Views/FrmMisSolicitudesPorEstado.aspx?ModuleId={0}", ModuleId));
                    break;
                case "missolicitudes":
                    Response.Redirect(string.Format("../Views/FrmMisSolicitudes.aspx?ModuleId={0}", ModuleId));
                    break;
                case "misolfecha":
                    Response.Redirect(string.Format("../Views/FrmMisSolicitudesPorFecha.aspx?ModuleId={0}", ModuleId));
                    break;
                case "segarea":
                    Response.Redirect(string.Format("../Views/FrmSeguimientoPorArea.aspx?ModuleId={0}", ModuleId));
                    break;
                case "segautor":
                    Response.Redirect(string.Format("../Views/FrmSeguimientoPorAutor.aspx?ModuleId={0}", ModuleId));
                    break;
                case "segestado":
                    Response.Redirect(string.Format("../Views/FrmSeguimientoPorEstado.aspx?ModuleId={0}", ModuleId));
                    break;
                case "segfecha":
                    Response.Redirect(string.Format("../Views/FrmSeguimientoPorFecha.aspx?ModuleId={0}", ModuleId));
                    break;
                case "segrespact":
                    Response.Redirect(string.Format("../Views/FrmSeguimientoPorRespActual.aspx?ModuleId={0}", ModuleId));
                    break;
                case "segtipo":
                    Response.Redirect(string.Format("../Views/FrmSeguimientoPorTipo.aspx?ModuleId={0}", ModuleId));
                    break;
                case "segtotal":
                    Response.Redirect(string.Format("../Views/FrmSeguimientoTotalSolicitudes.aspx?ModuleId={0}", ModuleId));
                    break;
                default:
                    Response.Redirect(string.Format("../Views/FrmMisSolicitudesPendientes.aspx?ModuleId={0}", ModuleId));
                    break;
            }
        }

        protected void BtnEditSolicitudClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminSolicitudAPC.aspx?ModuleId={0}&IdSolicitud={1}&from=solicitud",
                                               ModuleId, IdSolicitud
                                               ));
        }

        protected void BtnViewReclamoClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("../../Reclamos/Admin/FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}&from=admactividad&idfrom={2}", IdModuleReclamo, Request.QueryString["IdReclamo"], IdSolicitud));
        }

        protected void BtnAceptarInputClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InputWindow)) return;

            switch (InputWindow)
            {
                case "ResponsableSolicitud":
                    {
                        var ucr = this.GetUserControl<WucAsignarResponsableSolicitud>("UcRender", "phlVentanaMensajes");
                        if (ucr == null) return;
                        if (string.IsNullOrEmpty(ucr.IngenieroSeleccionado)) return;

                        var parameters = new InputParameter
                        {
                            Inputs = new Dictionary<string, string>
                                                              {
                                                                  {"IdResponsable", ucr.IngenieroSeleccionado},
                                                                  {"NombreResponsable", ucr.NombreIngenieroSeleccionado}
                                                              }
                        };

                        EjecutarWorkFlow(parameters, InputWindow);

                    }
                    break;

                case "EnvioActividades":
                    {
                        var parameters = new InputParameter();
                        EjecutarWorkFlow(parameters, InputWindow);
                    }

                    break;

                case "CerrarSolicitud":
                    {
                        var uc = this.GetUserControl<WucCierreSolicitud>("UcRender", "phlVentanaMensajes");
                        if (uc == null) return;
                        if (string.IsNullOrEmpty(uc.Adecuada) && string.IsNullOrEmpty(uc.Eficaz))
                        {
                            ShowError("Debe seleccionar por lo menos una de las opciones de la ventana (Adecuada, eficáz) para continuar con el proceso de cierre..");
                            return;
                        }

                        if(string.IsNullOrEmpty(uc.ConformidadEliminada))
                        {
                            ShowError("Debe indicar si la no conformidad fue eliminada o no para continuar con el proceso de cierre..");
                            return;
                        }


                        var parameters = new InputParameter
                        {
                            Inputs = new Dictionary<string, string>
                                                              {
                                                                  {"Adecuada", uc.Adecuada},
                                                                  {"Eficaz", uc.Eficaz},
                                                                  {"ConformidadEliminada", uc.ConformidadEliminada},
                                                                  {"Observaciones", uc.Observaciones}
                                                              }
                        };

                        EjecutarWorkFlow(parameters, InputWindow);
                    }

                    break;

              
            }
        }

        protected void BtnCancelarInputClick(object sender, EventArgs e)
        {
            mpeVentanaEmergente.Hide();
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


        private void LoadUserControlVentanaMensajes(IEnumerable<string> items)
        {
            var controlPath = LastLoadedControlMessages;

            if (string.IsNullOrEmpty(controlPath))
            {
                return;
            }
            if (string.IsNullOrEmpty(controlPath)) return;
            phlVentanaMensajes.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = "UcRender";
            ConfigurarUserControl(uc);
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

        private void ConfigurarUserControl(Control oControl)
        {
            var uc = (BaseUserControl)oControl;
            if (uc == null) return;
            //uc.IdDocument = IdCategoria;
            //if (IdIngenieroResponsable > 0)
            //    uc.IdResponsable = IdIngenieroResponsable.ToString();
        }

        protected void BtnCerrarAccionClick(object sender, EventArgs e)
        {
            litTitulo.Text = @"Cierre de la acción";
            InputWindow = "CerrarSolicitud";
            pnlVentanaEmergente.Width = 600;
            pnlVentanaEmergente.Height = 250;
            LastLoadedControlMessages = "../UserControls/WucCierreSolicitud.ascx";
            LoadUserControlVentanaMensajes(null);
            mpeVentanaEmergente.Show();
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

        public string IdModuleReclamo
        {
            get { return ViewState["IdModuleReclamo"] == null ? string.Empty : ViewState["IdModuleReclamo"].ToString(); }
            set { ViewState["IdModuleReclamo"] = value; }
        }

        #endregion

        #region Properties

        public bool MostrarBotonCierreSolicitud
        {
            set { btncerrar.Visible = value; }
        }


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

        public event EventHandler FilterEvent;

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

        public string NumeroAPC
        {
            get
            {
                return string.Format("{0}", ViewState["SolicitudAPC_NumeroAPC"]);
            }
            set
            {
                ViewState["SolicitudAPC_NumeroAPC"] = value;
            }
        }

        public bool CanEdit
        {
            get
            {
                return btnEdit.Visible;
            }
            set
            {
                btnEdit.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}