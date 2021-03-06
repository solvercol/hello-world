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
using Modules.Reclamos.UI;
using Modules.Reclamos.UserControls;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Admin
{
    public partial class FrmReclamo : ViewPage<ReclamoPresenter, IReclamoView>, IReclamoView
    {
        #region Members

        public event EventHandler FilterEvent;

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

        public string IdCategoria
        {
            get { return ViewState["IdCategoria"] == null ? string.Empty : ViewState["IdCategoria"].ToString(); }
            set { ViewState["IdCategoria"] = value; }
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

        public bool VerCrearAccion
        {
            set { btnActualizarIndicadores.Visible = value; }
        }

        public bool VerBotonEdicion
        {
            set { btnEdit.Visible = value; }
        }

        public string TextoBotonDevolucion
        {
            set
            {
                btnDeclinar.Text = value;
                btnDeclinar.Visible = !string.IsNullOrEmpty(value);
            }
        }

        public bool VerBotonRechazarReclamo
        {
            set { btnRechazar.Visible = value; }
        }

        public bool VerBotonCambiarIngeniero
        {
            set { btnCambiarIngeniero.Visible = value; }
        }

        public int? IdIngenieroResponsable
        {
            get { return ViewState["IdIngenieroResponsable"] == null ? 0 : (int)ViewState["IdIngenieroResponsable"]; }
            set { ViewState["IdIngenieroResponsable"] = value; }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Reclamo de {0} No. {1}", TipoReclamo, NumeroReclamo));
            LoadUserControl();
            LoadUserControlVentanaMensajes(null);
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
                ActualizarControlUsuarioActivo();
                wucLogReclamo.CargarLog();
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
                        case "IngenieroResponsable":
                            litTitulo.Text = @"Seleccione el Ingeniero Responsable.";
                            LastLoadedControlMessages = "../UserControls/WucIngresarIngenieroResponsable.ascx";
                            LoadUserControlVentanaMensajes(null);
                            mpeVentanaEmergente.Show();
                            break;

                        case "CategorizacionReclamo":
                             litTitulo.Text = @"Caregorización del reclamo.";
                             LastLoadedControlMessages = "../UserControls/WucCategorizarReclamo.ascx";
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

            var rr = this.GetUserControl<WUCAdminComentariosRespuestaReclamo>("WUCAdminComentariosRespuestaReclamo", "phlContent");
            if (rr != null)
            {
                rr.RefreshUserControl();
                return;
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
                case "rectautor":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorAutor.aspx?ModuleId={0}", ModuleId));
                    break;
                case "reccliente":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorCliente.aspx?ModuleId={0}", ModuleId));
                    break;
                case "recfecha":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorFecha.aspx?ModuleId={0}", ModuleId));
                    break;
                case "recproducto":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorProducto.aspx?ModuleId={0}", ModuleId));
                    break;
                case "recresponsable":
                    Response.Redirect(string.Format("../Views/FrmReclamosPorResponsable.aspx?ModuleId={0}", ModuleId));
                    break;
                case "rectotal":
                    Response.Redirect(string.Format("../Views/FrmTotalReclamos.aspx?ModuleId={0}", ModuleId));                
                    break;
                case "recexport":
                    Response.Redirect(string.Format("../Views/FrmReclamosExport.aspx?ModuleId={0}", ModuleId));
                    break;
                case "admactividad":
                    Response.Redirect(string.Format("FrmAdminActividadReclamo.aspx?ModuleId={0}&IdActividad={1}", ModuleId, IdFrom));
                    break;
                case "admalternativa":
                    Response.Redirect(string.Format("FrmAdminAlternativaReclamo.aspx?ModuleId={0}&IdAlternativa={1}", ModuleId, IdFrom));
                    break;
            }
        }

        protected void BtnCopiarReclamoClick(object sender, EventArgs e)
        {
            CampoRelacionado = string.Empty;
            mpeCopiarReclamo.Show();
        }

        protected void BtnSaveCopiarClick(object sender, EventArgs e)
        {
            Presenter.CopiarReclamo();
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

        protected void BtnEnviarInputCick(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(InputWindow))return;


            switch (InputWindow)
            {
                case "IngenieroResponsable":
                    {
                        var ucIr = this.GetUserControl<WucIngresarIngenieroResponsable>("UcRender", "phlVentanaMensajes");
                        if (ucIr == null) return;
                        if ( string.IsNullOrEmpty(ucIr.IngenieroSeleccionado)) return;

                        var parameters = new InputParameter
                                             {
                                                 Inputs = new Dictionary<string, string>
                                                              {
                                                                  {"IdIngeniero", ucIr.IngenieroSeleccionado}
                                                              }
                                             };

                        EjecutarWorkFlow(parameters, InputWindow);
                
                    }
                    break;

                case "CategorizacionReclamo":
                    {
                        var uc = this.GetUserControl<WucCategorizarReclamo>("UcRender", "phlVentanaMensajes");
                        if (uc == null) return;
                        if (string.IsNullOrEmpty(uc.AreaSeleccionada) || string.IsNullOrEmpty(uc.CategoriaSeleccionada)) return;

                        var parameters = new InputParameter
                        {
                            Inputs = new Dictionary<string, string>
                                                              {
                                                                  {"Idcategoria", uc.CategoriaSeleccionada},
                                                                  {"Area", uc.AreaSeleccionada}
                                                              }
                        };

                        EjecutarWorkFlow(parameters, InputWindow);
                    }

                    break;

                case "Devolucion":
                    {
                        var uc = this.GetUserControl<WucComentariosDevolverReclamo>("UcRender", "phlVentanaMensajes");
                        if (uc == null) return;
                        if (string.IsNullOrEmpty(uc.RetornarComentario) ) return;

                        var parameters = new InputParameter
                        {
                            Inputs = new Dictionary<string, string>
                                                              {
                                                                  {"Comentario", uc.RetornarComentario}
                                                              }
                        };

                        EjecutarWorkFlow(parameters, InputWindow);
                    }

                    break;

                case "Rechazo":
                    {
                        var uc = this.GetUserControl<WucComentariosDevolverReclamo>("UcRender", "phlVentanaMensajes");
                        if (uc == null) return;
                        if (string.IsNullOrEmpty(uc.RetornarComentario)) return;

                        var parameters = new InputParameter
                        {
                            Inputs = new Dictionary<string, string>
                                                              {
                                                                  {"Comentario", uc.RetornarComentario}
                                                              }
                        };

                        EjecutarWorkFlow(parameters, InputWindow);
                    }
                    break;

                case "CambiarIngeniero":
                    {
                        var uc = this.GetUserControl<WucCambiarIngenieroResponsable>("UcRender", "phlVentanaMensajes");
                        if (uc == null) return;
                        if (string.IsNullOrEmpty(uc.Comentarios) || string.IsNullOrEmpty(uc.IngenieroSeleccionado)) return;

                        var parameters = new InputParameter
                        {
                            Inputs = new Dictionary<string, string>
                                                              {
                                                                  {"IdIngeniero", uc.IngenieroSeleccionado},
                                                                  {"NombreIngeniero", uc.NombreIngenieroSeleccionado},
                                                                  {"Comentario", uc.Comentarios}
                                                              }
                        };

                        EjecutarWorkFlow(parameters, InputWindow);
                    }
                    break;

            }
        }

        protected void BtnCreacionAccionesClick(object sender, EventArgs e)
        {
           Presenter.CrearAcciones();
        }


        protected void BtnDevolverReclamoClick(object sender, EventArgs e)
        {
            InputWindow = "Devolucion";
            litTitulo.Text = @"Ingrese los motivos de la devolución.";
            LastLoadedControlMessages = "../UserControls/WucComentariosDevolverReclamo.ascx";
            LoadUserControlVentanaMensajes(null);
            mpeVentanaEmergente.Show();
        }

        protected void BtnCancelarReclamoClick(object sender, EventArgs e)
        {
            InputWindow = "Rechazo";
            litTitulo.Text = @"Ingrese los motivos del Rechazo.";
            LastLoadedControlMessages = "../UserControls/WucComentariosDevolverReclamo.ascx";
            LoadUserControlVentanaMensajes(null);
            mpeVentanaEmergente.Show();
        }

        protected void BtnCambiarIngenieroClick(object sender, EventArgs e)
        {
            InputWindow = "CambiarIngeniero";
            litTitulo.Text = @"Seleccione el Ingeniero Responsable.";
            LastLoadedControlMessages = "../UserControls/WucCambiarIngenieroResponsable.ascx";
            LoadUserControlVentanaMensajes(null);
            mpeVentanaEmergente.Show();
        }

        protected void BtnRelacionarplanAccionClick(object sender, EventArgs e)
        {
            InputWindow = "RelacioarPlanAccion";
            litTitulo.Text = @"Seleccione el Plan de Acción.";
            LastLoadedControlMessages = "../UserControls/WucListadoAccionesApc.ascx";
            LoadUserControlVentanaMensajes(null);
            btnAceptarInput.Visible = false;
            pnlVentanaEmergente.Width = 700;
            pnlVentanaEmergente.Height = 300;
            mpeVentanaEmergente.Show();
        }

        protected void LnkAccionesClick(object sender, EventArgs e)
        {
            if (ViewState["IdAccionPc"] == null) return;

            Response.Redirect(string.Format("../../AccionesPC/Admin/FrmSolicitudAPC.aspx?ModuleId={0}&IdSolicitud={1}&from=misolestados&IdReclamo={2}", IdModuloApc, ViewState["IdAccionPc"], IdReclamo));
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

           
            
            if(!controlPath.Contains('/'))
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

            if (uc is IReclamoWebUserControl)
            {
                var ucReclamo = ((IReclamoWebUserControl)uc);
                ucReclamo.LoadControlData();
                ucReclamo.RiseFatherPostback += RefreshReclamoInfo;
            }                
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
            uc.IdDocument = IdCategoria;
            if (IdIngenieroResponsable > 0)
            uc.IdResponsable = IdIngenieroResponsable.ToString();
            uc.ActualizarEvent += uc_ActualizarEvent;
        }

        void uc_ActualizarEvent(object sender, ViewResulteventArgs e)
        {
            ShowMessageOk("Proceso realizado satisfactoriamente.");
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        #endregion

        #region View Members

        #region Methods

        void RefreshReclamoInfo()
        {
            Presenter.LoadReclamo();
        }

        public bool MostrarBotonAsociacinPlanAccion
        {
            set
            {
                btnAsociarPlanAccion.Visible = value;
                btnActualizarIndicadores.Visible = value;
            }
        }

        public string ConfigurarHiperlinkAcciones
        {
            set { 
                ViewState["IdAccionPc"] = value;
                trAcciones.Visible = !string.IsNullOrEmpty(value);
            }
        }

        public string TextHyperlinkAcciones
        {
            set { lnkAcciones.Text = value; }
        }

        public string IdModuloApc
        {
            get { return ViewState["IdModuleApc"] == null ? string.Empty : ViewState["IdModuleApc"].ToString(); }
            set { ViewState["IdModuleApc"] = value; }
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

        public string Categoria
        {
            get
            {
                return lblCategoria.Text;
            }
            set
            {
                lblCategoria.Text = value;
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

        public string Responsable
        {
            get
            {
                return lblResponsable.Text;
            }
            set
            {
                lblResponsable.Text = value;
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

        public string LogInfoMessage
        {
            set { lblLogInfo.Text = value; }
        }

        public string CampoRelacionado
        {
            get
            {
                return txtCampoRelacionado.Text;
            }
            set
            {
                txtCampoRelacionado.Text = value;
            }
        }

        #endregion

        #endregion       
    }
}