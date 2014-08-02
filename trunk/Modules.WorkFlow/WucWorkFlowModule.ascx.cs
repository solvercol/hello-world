using System;
using System.Reflection;
using System.Web.UI.WebControls;
using Application.Core;
using Applications.MainModule.WorkFlow.DTO;
using ASP.NETCLIENTE.UI;
using Domain.MainModule.WorkFlow.Enums;
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

            if (_module != null)
            {
                
                if (ViewState["control"] == null)
                {
                    CargarPedido();
                }
                else
                {
                    RenderButtomControl((RenderTypeControlButtonDto)ViewState["control"]);
                }
            }
        }

        void WucWorkFlowModule_ResponseeventHandler(object sender, ViewResulteventArgs e)
        {
            if(e.MessageView == null)return;

            var oDocument = (RenderTypeControlButtonDto) ViewState["control"];

            var input = (InputParameter) e.MessageView;

            oDocument.Parameters.Add(input.Key, input.Value);

            //_module.ActualizarFechaEntrega(oDocument);

            CargarPedido();

            InvokeActualizarEvent(new ViewResulteventArgs("UpdatePanel"));
        }


        private void CargarPedido()
        {
            try
            {
                var oControl = _module.CargarWorkFlow(Request.QueryString["IdPedido"]);
                RenderButtomControl(oControl);
                ViewState["control"] = oControl;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name,ex);
            }
        }

        private void RenderButtomControl(RenderTypeControlButtonDto oControl)
        {
            plHolder.Controls.Clear();
            var btn = new Button {ID = "btnAprove", Text = oControl.TextControl};
            btn.Click += BtnClick;
            plHolder.Controls.Add(btn);
        }

        void BtnClick(object sender, EventArgs e)
        {
            try
            {
                var oDocument = (RenderTypeControlButtonDto)ViewState["control"];
                if (oDocument == null) return;
                var doc = _module.EjecutarWorkFlow(oDocument);
                //todo: pendiente por implementar logica cuando el objeto es null
                if (doc == null)return;

                switch (doc.ProcessStatus)
                {
                    case ProcessStatus.Ok:
                        CargarPedido();
                        InvokeActualizarEvent(new ViewResulteventArgs("UpdatePanel"));
                        break;

                    default:
                        InvokeActualizarEvent(new ViewResulteventArgs(oDocument));
                        break;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}