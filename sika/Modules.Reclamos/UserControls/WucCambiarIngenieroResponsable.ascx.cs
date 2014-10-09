using System;
using ASP.NETCLIENTE.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WucCambiarIngenieroResponsable : BaseUserControl
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
            if (string.IsNullOrEmpty(Idcategoria)) return;
            var oReclamo = new ReclamosModule();
            var dt = oReclamo.ListadoIngenieros(Idcategoria);

            ddlIngeniero.DataSource = dt;
            ddlIngeniero.DataValueField = "IdUser";
            ddlIngeniero.DataTextField = "Nombres";
            ddlIngeniero.DataBind();
            if(string.IsNullOrEmpty(IdResponsable))return;
            ddlIngeniero.SelectedValue = IdResponsable;

        }


        public string IngenieroSeleccionado
        {
            get { return ddlIngeniero.SelectedValue; }
        }

        public string NombreIngenieroSeleccionado
        {
            get { return ddlIngeniero.SelectedItem.Text; }
        }

        public string Comentarios
        {
            get { return txtComentariosCambioIngeniero.Text; }
        }
    }
}