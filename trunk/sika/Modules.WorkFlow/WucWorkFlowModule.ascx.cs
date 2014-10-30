using System;
using System.Reflection;
using System.Web.UI.WebControls;
using Application.Core;
using Applications.MainModule.WorkFlow.DTO;
using ASP.NETCLIENTE.UI;
using Domain.MainModule.WorkFlow.Enums;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Infrastructure.CrossCutting.NetFramework.Structures;

namespace Modules.WorkFlow
{
    public partial class WucWorkFlowModule : BaseUserControl
    {
        private WorkFlowModule _module;


        protected void Page_Load(object sender, EventArgs e)
        {
            _module = Module as WorkFlowModule;

            ResponseeventHandler += WucWorkFlowModule_ResponseeventHandler;

            btnAceptar.Attributes.Add("Onclick", "javascript:InhabilitarControl('" + DivMomento.ClientID + "','" + divContinuar.ClientID + "')");

            if (_module != null)
            {

                if (ViewState["control"] == null)
                {
                    CargarWorkFlow();
                }
                else
                {
                    RenderButtomControl((RenderTypeControlButtonDto)ViewState["control"]);
                }
            }
        }



        void WucWorkFlowModule_ResponseeventHandler(object sender, ViewResulteventArgs e)
        {
            if (e.MessageView == null) return;

            var input = (InputParameter)e.MessageView;

            if (e.Sender.ToString() == "Refresh")
            {
                CargarWorkFlow();
                return;
            }

            var oDocument = (RenderTypeControlButtonDto)ViewState["control"];

            oDocument.Parameters = input.Inputs;

            switch (e.Sender.ToString())
            {
                case "IngenieroResponsable":
                    _module.ActualizarIngenieroResponsable(oDocument);
                    InvokeActualizarEvent(new ViewResulteventArgs("UpdatePanel"));
                    break;

                case "CategorizacionReclamo":
                    var res = _module.CategorizarReclamo(oDocument);
                    InvokeActualizarEvent(res.Processestaus == "Ok"
                                              ? new ViewResulteventArgs("UpdatePanel")
                                              : new ViewResulteventArgs(res));
                    break;

                case "Devolucion":
                    res = _module.DevolverReclamo(oDocument);
                    InvokeActualizarEvent(res.Processestaus == "Ok"
                                              ? new ViewResulteventArgs("UpdatePanel")
                                              : new ViewResulteventArgs(res));
                    break;

                case "Rechazo":
                    res = _module.CancelarReclamo(oDocument);
                    InvokeActualizarEvent(res.Processestaus == "Ok"
                                              ? new ViewResulteventArgs("UpdatePanel")
                                              : new ViewResulteventArgs(res));
                    break;

                case "CambiarIngeniero":
                    res = _module.CambiarIngeniero(oDocument);
                    InvokeActualizarEvent(res.Processestaus == "Ok"
                                              ? new ViewResulteventArgs("UpdatePanel")
                                              : new ViewResulteventArgs(res));
                    break;

                case "ResponsableSolicitud":
                    res = _module.AsignarresponsableSolicitud(oDocument);
                    InvokeActualizarEvent(res.Processestaus == "Ok"
                                              ? new ViewResulteventArgs("UpdatePanel")
                                              : new ViewResulteventArgs(res));
                    break;
            }

            CargarWorkFlow();
        }


        private void CargarWorkFlow()
        {
            try
            {
                
                if (Module == null)return;
                if(Module.Seccion == null)return;
                if( Module.Seccion.TBL_Admin_Modulos == null)return;
                var module = Module.Seccion.TBL_Admin_Modulos.NombreModulo == "Reclamos" ? ModulosAplicacion.Reclamos : ModulosAplicacion.AccionesPc;
                var idDocumento = module == ModulosAplicacion.Reclamos ? Request.QueryString["IdReclamo"] : Request.QueryString["IdSolicitud"];

                if (string.IsNullOrEmpty(idDocumento)) return;
                var oControl = _module.CargarWorkFlow(idDocumento, module);
                RenderButtomControl(oControl);
                ViewState["control"] = oControl;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void RenderButtomControl(RenderTypeControlButtonDto oControl)
        {
            plHolder.Controls.Clear();
            if (oControl == null)
            {
                return;
            }
           
            var btn = new Button
            {
                ID = "btnAprove",
                Text = oControl.TextControl,
                Visible = !string.IsNullOrEmpty(oControl.TextControl),
                CausesValidation = false
            };
            if (!string.IsNullOrEmpty(oControl.TextControl))
            {
                if (string.IsNullOrEmpty(oControl.IdCurrentResponsibe))
                    btn.Visible = false;
                else
                {
                    if (oControl.IsCurrentGroupResponsible)
                        btn.Visible = AuthenticatedUser.IsInRoleId(Convert.ToInt32(oControl.IdCurrentResponsibe));
                    else
                        btn.Visible = oControl.IdCurrentResponsibe == AuthenticatedUser.IdUser.ToString();
                }
            }

            btn.Click += BtnClick;
            plHolder.Controls.Add(btn);
        }

        void BtnClick(object sender, EventArgs e)
        {
            mpeSearch.Show();
        }

        protected void BtnAceptarClick(object sender, EventArgs e)
        {
            try
            {
                var oDocument = (RenderTypeControlButtonDto)ViewState["control"];
                if (oDocument == null) return;

                
                
                var doc = Module.Seccion.TBL_Admin_Modulos.NombreModulo == "Reclamos" ? 
                    _module.EjecutarWorkFlow(oDocument) :
                    _module.EjecutarWorkFlowModuloSolicitudes(oDocument);

                //todo: pendiente por implementar logica cuando el objeto es null
                if (doc == null) return;

                switch (doc.Processestaus)
                {
                    case "Ok":
                        CargarWorkFlow();
                        InvokeActualizarEvent(new ViewResulteventArgs("UpdatePanel"));
                        break;

                    default:
                        InvokeActualizarEvent(new ViewResulteventArgs(oDocument));
                        break;
                }

            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected void BtnCancelarClick(object sender, EventArgs e)
        {
            mpeSearch.Hide();
        }

    }
}