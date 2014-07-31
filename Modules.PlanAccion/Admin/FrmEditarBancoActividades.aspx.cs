using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.PlanAccion.IViews;
using Presenters.PlanAccion.Presenters;

namespace Modules.PlanAccion.Admin
{
    public partial class FrmEditarBancoActividades : ViewPage<EditarActividadesPresenter, IEditarActividadView>, IEditarActividadView
    {
        public event EventHandler GuardarEvent;
        public event EventHandler EliminarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdActividad) ? "Nueva Actividad" : "Editar Actividad");
            btnCancelar.Attributes.Add("onclick", "return confirm('¿Confirma que desea eliminar el registro actual?');");
            btnCancelar.Visible = !string.IsNullOrEmpty(IdActividad);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditarCategoriaHideControlsevent;
        }

        void FrmEditarCategoriaHideControlsevent(object sender, EventArgs e)
        {
            btnCancelar.Visible = false;
            btnGuardar.Visible = false;
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            Response.Redirect(string.IsNullOrEmpty(FormRequest)
                                  ? string.Format("FrmVistaBancoAcividades.aspx{0}", GetBaseQueryString())
                                  : string.Format("{0}{1}", FormRequest, GetBaseQueryString()));
        }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {

            if(chkTienePregunta.Checked)
            {
                if(string.IsNullOrEmpty(txtPregunta.Text))
                {
                    ShowError("Debe ingresar una pregunta!!");
                    txtPregunta.Focus();
                    return;
                }

                if( string.IsNullOrEmpty(ddlTipoRespuesta.SelectedValue))
                {
                    ShowError("Debe seleccionar una tipo de respuesta!!");
                    return;
                }

                if (ddlTipoRespuesta.SelectedValue != "S/N")
                 {
                     if(lbValues.Items.Count == 0)
                     {
                         ShowError("Debe ingresar los valores para la respuesta!!");
                         txtValueInput.Focus();
                         return;
                     }
                 }
            }

            if (GuardarEvent != null)
                GuardarEvent(null, EventArgs.Empty);
        }

        protected void BtnCancelarClick(object sender, EventArgs e)
        {
            if (EliminarEvent != null)
                EliminarEvent(null, EventArgs.Empty);
        }

        protected void ChkTienePreguntaCheckedChanged(object sender, EventArgs e)
        {
            divPregunta.Visible = chkTienePregunta.Checked;
            txtPregunta.Focus();
        }

        protected void BtnAddClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValueInput.Text))return;
            if(ddlTipoRespuesta.SelectedValue=="Unica")
            {
                if(lbValues.Items.Count >= 1)
                {
                    txtValueInput.Text = string.Empty;
                    return;
                }
            }

            lbValues.Items.Add(new ListItem(txtValueInput.Text.ToUpper(), txtValueInput.Text.ToUpper()));
            txtValueInput.Text = string.Empty;
            txtValueInput.Focus();
        }

        protected void BtnRemoveClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbValues.SelectedValue)) return;
            lbValues.Items.RemoveAt(lbValues.SelectedIndex);
        }

        protected void DdlTiposrespuestaSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoRespuesta.SelectedValue == "S/N" || string.IsNullOrEmpty(ddlTipoRespuesta.SelectedValue))
            {
                lbValues.Items.Clear();
                txtValueInput.Enabled = false;
                btnRemove.Enabled = false;
                btnAdd.Enabled = false;
                trValorRespuesta.Visible = false;
                trRespuestaObligatoria.Visible = true;
            }
            else 
            {
                lbValues.Items.Clear();
                txtValueInput.Enabled = true;
                btnRemove.Enabled = true;
                btnAdd.Enabled = true;
                trValorRespuesta.Visible = true;
                trRespuestaObligatoria.Visible = false;
            }

            txtValueInput.Text = string.Empty;
            txtValueInput.Focus();
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdActividad
        {
            get { return Request.QueryString["IdActividad"]; }
        }

        public void TiposRespuesta(IEnumerable<string> tipos)
        {
            ddlTipoRespuesta.Items.Clear();
            foreach (var tipo in tipos)
            {
                ddlTipoRespuesta.Items.Add(new ListItem(tipo,tipo));
            }

            var li = new ListItem("Seleccione", "");
            ddlTipoRespuesta.Items.Insert(0,li);
        }

        public string Codigo
        {
            get { return txtCodigo.Text; }
            set { txtCodigo.Text = value; }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public bool Tienepregunta
        {
            get { return chkTienePregunta.Checked; }
            set
            {
                chkTienePregunta.Checked = value;
                divPregunta.Visible = chkTienePregunta.Checked;
                txtPregunta.Focus();
            }
        }

        public string Pregunta
        {
            get { return txtPregunta.Text; }
            set { txtPregunta.Text = value; }
        }

        public string TipoRespuesta
        {
            get { return ddlTipoRespuesta.SelectedValue; }
            set
            {
                ddlTipoRespuesta.SelectedValue = value;
                if(!string.IsNullOrEmpty(value))
                {
                    if(value != "S/N")
                    {
                        trValorRespuesta.Visible = true;
                    }
                    else
                    {
                        trRespuestaObligatoria.Visible = true;
                    }
                }

            }
        }

        public string[] ValorRespuestas
        {
            get { return RetornarRespuestas(); }
            set { InsertarRespuestas(value); }
        }

        public string RespuestaObligatoria
        {
            get { return ddlrespuestaObligatoria.SelectedValue; }
            set { ddlrespuestaObligatoria.SelectedValue = value; }
        }

        public bool RequiereAnexo
        {
            get { return chkAnexo.Checked; }
            set { chkAnexo.Checked = value; }
        }

        public bool RequiereComentarios
        {
            get { return chkComentarios.Checked; }
            set { chkComentarios.Checked = value; }
        }

        public bool Activa
        {
            get { return chkActiva.Checked; }
            set { chkActiva.Checked = value; }
        }

        private string [] RetornarRespuestas()
        {
            if (lbValues.Items.Count == 0) return null;
            var respuestas = new string[lbValues.Items.Count];
            var index = 0;
            foreach (ListItem item in lbValues.Items)
            {
                if (string.IsNullOrEmpty(item.Value))continue;
                respuestas[index] = item.Value;
                index++;
            }
            return respuestas;
        }

        private void InsertarRespuestas(string[] respuestas)
        {
            if (respuestas == null)
            {
                lbValues.Items.Clear();
                return;
            }

            if(respuestas.Length == 0)return;
            lbValues.Items.Clear();
            foreach (var respuesta in respuestas)
            {
                if (string.IsNullOrEmpty(respuesta))continue;
                lbValues.Items.Add(new ListItem(respuesta,respuesta));
            }
        }
    }
}