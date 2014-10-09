using System;
using ASP.NETCLIENTE.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WucComentariosDevolverReclamo : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string RetornarComentario
        {
            get { return txtComentariosDevolver.Text; }
        }

    }
}