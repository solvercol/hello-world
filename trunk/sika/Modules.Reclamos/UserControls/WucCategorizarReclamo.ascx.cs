using System;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WucCategorizarReclamo : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Getcategorias();
            LiastadoAreas();
        }


        private void Getcategorias()
        {
            var oReclamo = new ReclamosModule();


            ddlCategoria.DataSource = oReclamo.ListadoCategorias();
            ddlCategoria.DataValueField = "IdCategoriaReclamo";
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataBind();

            var li = new ListItem("--Seleccione--", string.Empty);
            ddlCategoria.Items.Insert(0,li);
        }

        public string CategoriaSeleccionada
        {
            get { return ddlCategoria.SelectedValue; }
        }

        private void LiastadoAreas()
        {
            var oReclamo = new ReclamosModule();

            var list = oReclamo.ListadoAreas();
            ddlArea.Items.Clear();

            foreach (var oArea in list)
            {
                ddlArea.Items.Add(new ListItem(oArea,oArea));
            }

            var li = new ListItem("--Seleccione--", string.Empty);
            ddlArea.Items.Insert(0, li);
        }

        public string AreaSeleccionada
        {
            get { return ddlArea.SelectedValue; }
        }
    }
}