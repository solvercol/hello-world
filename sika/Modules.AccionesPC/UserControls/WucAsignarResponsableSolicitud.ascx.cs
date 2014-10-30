using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;

namespace Modules.AccionesPC.UserControls
{
    public partial class WucAsignarResponsableSolicitud : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetListadoUsuarios();
        }

        private void GetListadoUsuarios()
        {

            var oAcciones = new AccionesPCModule();
            var dt = oAcciones.ListadoUsuarios();
            ddlResponsable.DataSource = dt;
            ddlResponsable.DataValueField = "IdUser";
            ddlResponsable.DataTextField = "Nombres";
            ddlResponsable.DataBind();

            var li = new ListItem("--Selecione--", string.Empty);
            ddlResponsable.Items.Insert(0,li);
        }


        public string IngenieroSeleccionado
        {
            get { return ddlResponsable.SelectedValue; }
        }

        public string NombreIngenieroSeleccionado
        {
            get { return ddlResponsable.SelectedItem.Text; }
        }
    }
}