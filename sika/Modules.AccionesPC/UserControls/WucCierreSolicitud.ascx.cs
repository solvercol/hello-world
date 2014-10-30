using System;
using ASP.NETCLIENTE.UI;

namespace Modules.AccionesPC.UserControls
{
    public partial class WucCierreSolicitud : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litText.Text = @"Según los resultados obtenidos en la ejecución " +
                           @"e implementación de la acción correctiva/preventiva "+
                           @"descrita en el presente documento, determine si la "+
                           @"acción fue:";
        }


        public string Adecuada
        {
            get { return chkAdecuada.Checked ? "Adecuada" : string.Empty; }
        }

        public string Eficaz
        {
            get { return chkEficaz.Checked ? "Eficáz" : string.Empty; }
        }

        public string ConformidadEliminada
        {
            get { return string.IsNullOrEmpty(rblEliminada.SelectedValue) ? string.Empty : rblEliminada.SelectedValue; }
        }

        public string Observaciones
        {
            get { return txtObservaciones.Text; }
        }
    }
}