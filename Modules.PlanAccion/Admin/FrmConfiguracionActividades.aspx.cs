using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.PlanAccion.IViews;
using Presenters.PlanAccion.Presenters;

namespace Modules.PlanAccion.Admin
{
    public partial class FrmConfiguracionActividades : ViewPage<ConfigurarActividadesPresenter, IConfigurarActividadesView>, IConfigurarActividadesView
    {
        public event EventHandler GuardarEvent;
        public event EventHandler EliminarEvent;
        public event EventHandler CargarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Configuración de Actividades");
            btnCancelar.Attributes.Add("onclick", "return confirm('¿Confirma que desea eliminar el registro actual?');");
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
            btnNuevo.Visible = false;
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            Response.Redirect(!string.IsNullOrEmpty(FormRequest)
                                  ? string.Format("{0}{1}", FormRequest, GetBaseQueryString())
                                  : string.Format("FrmVistaCategorias.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {
            if (GuardarEvent != null)
                GuardarEvent(null, EventArgs.Empty);
            
            LimpiarVista();
            pnlConfig.Enabled = false;
        }

        protected void BtnCancelarClick(object sender, EventArgs e)
        {
            if (EliminarEvent != null)
                EliminarEvent(null, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        

        public void Categorias(IEnumerable<TBL_ModuloPlanAccion_Categorias> list)
        {
            tvcategorias.Nodes.Clear();

            if (!string.IsNullOrEmpty(Request.QueryString["Idcategoria"]))
            {
                var id = Convert.ToInt32(Request.QueryString["Idcategoria"]);
                foreach (var node in list.Where(x => x.IdCategoria == id).Select(oNode => new TreeNode
                                                                                              {
                                                                                                  Value = oNode.IdCategoria.ToString(), 
                                                                                                  Text = oNode.Descripcion, 
                                                                                                  PopulateOnDemand = true, 
                                                                                                  ImageUrl = "~/Resources/Images/Categorias.png"
                                                                                              }))
                {
                    tvcategorias.Nodes.Add(node);
                }
            }
            else
            {
                foreach (var node in list.Select(oNode => new TreeNode
                                                              {
                                                                  Value = oNode.IdCategoria.ToString(), 
                                                                  Text = oNode.Descripcion, 
                                                                  PopulateOnDemand = true, 
                                                                  ImageUrl = "~/Resources/Images/Categorias.png"
                                                              }))
                {
                    tvcategorias.Nodes.Add(node);
                }
            }
        }

        public void Actividades(List<TBL_ModuloPlanAccion_BancoActividades> list)
        {
            ddlActividades.DataSource = list;
            ddlActividades.DataValueField = "IdActividad";
            ddlActividades.DataTextField = "Descripcion";
            ddlActividades.DataBind();

            var li = new ListItem("--Seleccione--", string.Empty);
            ddlActividades.Items.Insert(0,li);

            if (!string.IsNullOrEmpty(Request.QueryString["IdActividad"]))
                ddlActividades.SelectedValue = Request.QueryString["IdActividad"];
        }

        public void Roles(List<TBL_Admin_Roles> list)
        {
            ddlRolExclusivo.DataSource = list;
            ddlRolExclusivo.DataValueField = "IdRol";
            ddlRolExclusivo.DataTextField = "NombreRol";
            ddlRolExclusivo.DataBind();

            var li = new ListItem("--Seleccione--", string.Empty);
            ddlRolExclusivo.Items.Insert(0, li);
        }

        public string IdConfiguracion
        {
            get { return ViewState["IdConfiguracion"] == null ? string.Empty : ViewState["IdConfiguracion"].ToString(); }
        }

        public string IdActividad
        {
            get { return ddlActividades.SelectedValue; }
            set { ddlActividades.SelectedValue = value; }
        }

        public string Idcategoria
        {
            get { return ViewState["Idcategoria"] == null ? string.Empty : ViewState["Idcategoria"].ToString(); }
            set { ViewState["Idcategoria"] = value; }
        }

        public bool Obligatoria
        {
            get { return chkObligatoria.Checked; }
            set { chkObligatoria.Checked = value; }
        }

        public bool Final
        {
            get { return chkEsFinal.Checked; }
            set { chkEsFinal.Checked = value; }
        }

        public bool Exclusiva
        {
            get { return chkEsExclusiva.Checked; }
            set
            {
                chkEsExclusiva.Checked = value;
                trRolExclusivo.Visible = value;
            }
        }

        public string RolExclusiva
        {
            get { return ddlRolExclusivo.SelectedValue; }
            set { ddlRolExclusivo.SelectedValue = value; }
        }

        public bool ProgramarActividad
        {
            get { return chkPreprogramar.Checked; }
            set
            {
                chkPreprogramar.Checked = value;
                trDiasHabiles.Visible = value;
            }
        }

        public int DiasHabiles
        {
            get { return txtDiasHabiles.ValueInt; }
            set { txtDiasHabiles.ValueInt = value; }
        }

        public int Secuencia
        {
            get { return txtSecuencia.ValueInt; }
            set { txtSecuencia.ValueInt = value; }
        }

        protected void TvCategoriasPopulate(object sender, TreeNodeEventArgs e)
        {
            var list = Presenter.ListadoActividadesConfiguracion(e.Node.Value);
            if(list == null)return;
            foreach (var node in list.Select(act => new TreeNode
                                                        {
                                                            Value = act.IdConfiguracion.ToString(), 
                                                            Text = act.TBL_ModuloPlanAccion_BancoActividades.Descripcion,
                                                            ImageUrl = "~/Resources/Images/Actividades.gif"
                                                        }))
            {
                e.Node.ChildNodes.Add(node);
            }

        }

        protected void SelectedNodeChanged(object sender, EventArgs e)
        {
            if(tvcategorias.SelectedNode == null )return;
            if (tvcategorias.SelectedNode.Depth == 0)
            {
                litCategoriaSeleccionada.Text = tvcategorias.SelectedNode.Text;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                Idcategoria = tvcategorias.SelectedNode.Value;
            }
            else
            {
                if(tvcategorias.SelectedNode.Parent != null)
                {
                    litCategoriaSeleccionada.Text = tvcategorias.SelectedNode.Parent.Text;
                    btnNuevo.Visible = false;
                    ViewState["IdConfiguracion"] = tvcategorias.SelectedNode.Value;
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    pnlConfig.Enabled = true;
                    if (CargarEvent != null)
                        CargarEvent(null, EventArgs.Empty);
                }
            }
        }

        protected void ExclusivaCheckedChanged(object sender, EventArgs e)
        {
            trRolExclusivo.Visible = chkEsExclusiva.Checked;
            ddlRolExclusivo.SelectedValue = string.Empty;
        }

        protected void PreProgramarCheckedChanged(object sender, EventArgs e)
        {
            trDiasHabiles.Visible = chkPreprogramar.Checked;
            txtDiasHabiles.Text = string.Empty;
        }

        protected void BtnNuevoClick(object sender, EventArgs e)
        {
            pnlConfig.Enabled = true;
            btnGuardar.Visible = true;
            LimpiarVista();
            trDiasHabiles.Visible = false;
            trRolExclusivo.Visible = false;
        }

        private void LimpiarVista()
        {
            if(string.IsNullOrEmpty(Request.QueryString["IdActividad"]))
                ddlActividades.SelectedValue = string.Empty;

            chkObligatoria.Checked = false;
            chkEsFinal.Checked = false;
            chkEsExclusiva.Checked = false;
            ddlRolExclusivo.SelectedValue = string.Empty;
            chkPreprogramar.Checked = false;
            txtSecuencia.Text = string.Empty;
            txtDiasHabiles.Text = string.Empty;
            ViewState["IdConfiguracion"] = null;
        }
    }
}