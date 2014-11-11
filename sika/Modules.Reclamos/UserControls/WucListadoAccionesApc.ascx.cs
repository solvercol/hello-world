using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;

namespace Modules.Reclamos.UserControls
{
    public partial class WucListadoAccionesApc : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            GetListadoSolicitudesApc();
        }

        protected void RptListadoItemDatBound(object sender, RepeaterItemEventArgs e)
        {
            var oSolicitud = e.Item.DataItem as TBL_ModuloAPC_Solicitud;
            if(oSolicitud == null)return;

            var imgSelect = e.Item.FindControl("ImgSelect") as ImageButton;
            if(imgSelect != null)
            {
                imgSelect.CommandArgument = oSolicitud.IdSolucitudAPC.ToString();
            }
        }

        protected void BtnSelectClick(object sender, ImageClickEventArgs e)
        {
            var imgButton = (ImageButton)sender;

            var idSolicitud = imgButton.CommandArgument;

            if(string.IsNullOrEmpty(idSolicitud))return;

            var idReclamo = Request.QueryString["IdReclamo"];

            if(string.IsNullOrEmpty(idReclamo))return;

            var oReclamo = new ReclamosModule();

            oReclamo.AsociarReclamoConSolicitudApc(Convert.ToDecimal(idSolicitud), Convert.ToDecimal(idReclamo),
                                                   AuthenticatedUser.IdUser);


            InvokeActualizarEvent(new ViewResulteventArgs("UpdatePanel"));
        }


        private void GetListadoSolicitudesApc()
        {
            var oReclamo = new ReclamosModule();
            rptListado.DataSource = oReclamo.ListadoSolicitudesApc();
            rptListado.DataBind();
        }

    }
}