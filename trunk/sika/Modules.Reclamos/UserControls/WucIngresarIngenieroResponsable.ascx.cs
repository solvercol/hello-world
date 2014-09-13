using System;
using ASP.NETCLIENTE.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WucIngresarIngenieroResponsable : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetListadoUsuarios();
        }

        private string Idcategoria
        {
            get { return IdDocument; } 
        }


        private void GetListadoUsuarios()
        {
            if(string.IsNullOrEmpty(Idcategoria))return;
            var oReclamo = new ReclamosModule();
            var dt = oReclamo.ListadoIngenieros(Idcategoria);

            ddlIngeniero.DataSource = dt;
            ddlIngeniero.DataValueField = "IdUser";
            ddlIngeniero.DataTextField = "Nombres";
            ddlIngeniero.DataBind();

        }


        public string IngenieroSeleccionado
        {
            get { return ddlIngeniero.SelectedValue; }
        }
    }
}