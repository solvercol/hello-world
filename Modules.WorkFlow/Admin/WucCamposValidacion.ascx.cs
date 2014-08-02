using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Infragistics.Web.UI.NavigationControls;

namespace Modules.WorkFlow.Admin
{
    public partial class WucCamposValidacion : BaseUserControl
    {

        private WorkFlowModule _module;

        protected void Page_Load(object sender, EventArgs e)
        {
            _module = Module as WorkFlowModule;
            if (IsLoadUserControl) return;
            GetEstados(ddlFiltroEstados);
            GetCampos();
            IsLoadUserControl = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RegistrarScript();
        }
       
    
        private string IdCampoSeleccionado
        {
            get { return hndNodeSelected.Value; }
            set { hndNodeSelected.Value  = value; }
        }

        private void GetEstados(ListControl ddlControl)
        {
            try
            {
                var list = _module.EstadosProcesoService.FindBySpec(true);
                ddlControl.DataSource = list;
                ddlControl.DataValueField = "IdEstado";
                ddlControl.DataTextField = "Descripcion";
                ddlControl.DataBind();

                var li = new ListItem("--Seleccione--", string.Empty);
                ddlControl.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void GetCampos()
        {
            try
            {

                var list = _module.CamposRequeridosServices.FindCamposByStatus(ddlFiltroEstados.SelectedValue);

                wdtRutas.Nodes.Clear();

                var rootNode = new DataTreeNode
                {
                    Text = "Arbol de Campos",
                    Key = string.Empty,
                    Value = string.Empty,
                    Expanded = true,
                    ImageUrl = "~/Resources/Images/RootNode.png"
                };

                wdtRutas.Nodes.Add(rootNode);

                foreach (var n in list.Select(opcion => new DataTreeNode
                {
                    Text = string.Format("[{0}] -> {1}",
                    opcion.TBL_Admin_EstadosProceso == null ? "??" : opcion.TBL_Admin_EstadosProceso.Descripcion,
                    opcion.CampoValidar),

                    Key = opcion.IdRequerido.ToString(),
                    ImageUrl = "~/Resources/Images/Field.png",
                    Value = opcion.IdRequerido.ToString(),
                    IsEmptyParent = false
                    //Expanded = true// ViewState["Expanded"] == null ? false : ( ViewState["Expanded"].ToString() == opcion.IdOpcionMenu.ToString() ? true : false)

                }))
                {
                    rootNode.Nodes.Add(n);
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Guardar()
        {
            try
            {
                var oCampo = _module.CamposRequeridosServices.NewEntity();
                oCampo.CampoValidar = ((TextBox)dvRutas.FindControl("txtCampoValidarEdit")).Text;
                oCampo.CreateBy = AuthenticatedUser.IdUser.ToString();
                oCampo.CreateOn = DateTime.Now;
                if (!string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue))
                    oCampo.IdEstado = Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue);
                oCampo.IsActive = true;
                oCampo.MensajeValidacion = ((TextBox)dvRutas.FindControl("txtMensajeValidacionEdit")).Text;
                oCampo.ModifiedBy = AuthenticatedUser.IdUser.ToString();
                oCampo.ModifiedOn = DateTime.Now;
                oCampo.ReglaDependencia = ((TextBox)dvRutas.FindControl("txtFormulaDependenciaEdit")).Text;
                oCampo.ReglaValidacion = ((TextBox)dvRutas.FindControl("txtFormulaValidacionEdit")).Text;
                oCampo.TipoValidacion = ((DropDownList)dvRutas.FindControl("ddlTipoValidacion")).SelectedValue;
                _module.CamposRequeridosServices.Add(oCampo);
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Proceso realizado satisfactoriamente!!"), TypeError.Ok));
            }
            catch (Exception ex)
            {
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al guardar el registro!!"), TypeError.Error));
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Actualizar()
        {
            try
            {
                if (string.IsNullOrEmpty(IdCampoSeleccionado)) return ;

                var oCampo = _module.CamposRequeridosServices.FindById(Convert.ToInt32(IdCampoSeleccionado));
                if(oCampo == null)
                {
                    InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al obtener el Objeto {0} desde la base de datos", IdCampoSeleccionado), TypeError.Error));
                    return;
                }
                oCampo.CampoValidar = ((TextBox)dvRutas.FindControl("txtCampoValidarEdit")).Text;
                if (!string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue))
                    oCampo.IdEstado = Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue);
                oCampo.MensajeValidacion = ((TextBox)dvRutas.FindControl("txtMensajeValidacionEdit")).Text;
                oCampo.ModifiedBy = AuthenticatedUser.IdUser.ToString();
                oCampo.ModifiedOn = DateTime.Now;
                oCampo.ReglaDependencia = ((TextBox)dvRutas.FindControl("txtFormulaDependenciaEdit")).Text;
                oCampo.ReglaValidacion = ((TextBox)dvRutas.FindControl("txtFormulaValidacionEdit")).Text;
                oCampo.TipoValidacion = ((DropDownList)dvRutas.FindControl("ddlTipoValidacion")).SelectedValue;
                _module.CamposRequeridosServices.Modify(oCampo);
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Proceso realizado satisfactoriamente!!"), TypeError.Ok));
            }
            catch (Exception ex)
            {
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al actualizar el registro!!"), TypeError.Error));
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Cargar(string idCampo)
        {
            try
            {
                var oCampo = _module.CamposRequeridosServices.FindBySpec( string.IsNullOrEmpty(idCampo) ? -1 : Convert.ToInt32(idCampo));
                if (oCampo == null)
                {
                    InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al obtener el Objeto {0} desde la base de datos", IdCampoSeleccionado), TypeError.Error));
                    return;
                }

                dvRutas.DataSource = oCampo;
                dvRutas.DataBind();

                ConfigurarControles();
                
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void EliminarEvent()
        {
            try
            {
                if (string.IsNullOrEmpty(IdCampoSeleccionado)) return;

                var oCampo = _module.CamposRequeridosServices.FindById(Convert.ToInt32(IdCampoSeleccionado));
                if (oCampo == null)
                {
                    InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al obtener el Objeto {0} desde la base de datos", IdCampoSeleccionado), TypeError.Error));
                    return;
                }

                _module.CamposRequeridosServices.Remove(oCampo);
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Proceso realizado satisfactoriamente!!"), TypeError.Ok));
                IdCampoSeleccionado = string.Empty;
            }
            catch (Exception ex)
            {
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al eliminar el registro!!"), TypeError.Error));
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void ActualizarDetailsView(DetailsViewMode mode)
        {
            dvRutas.ChangeMode(mode);
            Cargar(hndNodeSelected.Value);
        }

        private void RegistrarScript()
        {
            var strScript = " var lastNode = null; " +
                            " function MenuItem_Click(menu, eventArgs) { " +
                            " switch (eventArgs.getItem().get_key()) { " +
                            " case 'Edit': " +
                            "   if (lastNode != null  ) { " +
                            "     if(lastNode.get_key() == '')" +
                            "       eventArgs.set_cancel(true); " +
                            "   } " +
                            "   break; " +
                            " case 'expand': " +
                            "   if (lastNode != null) {" +
                            "       lastNode.toggle(true, true); " +
                            "       eventArgs.set_cancel(true); " +
                            "  } break; " +
                            "   } " +
                            "}" +

                            " function Node_Click(tree, eventArgs) { " +
                            " lastNode = eventArgs.getNode(); " +
                            " var menu = $find('" + ContextMenu.ClientID + "'); " +
                            " var oHdnSelected = document.getElementById('" + hndNodeSelected.ClientID + "'); " +
                            " if (menu != null && eventArgs.get_browserEvent() != null && eventArgs.get_browserEvent().button == 2) {" +
                            "       oHdnSelected.value = eventArgs.getNode().get_key(); " +
                            "       menu.showAt(null, null, eventArgs.get_browserEvent()); " +
                            "    } " +
                            " }";

            var sm = ScriptManager.GetCurrent(Page);
            if (null != sm && sm.IsInAsyncPostBack)
                ScriptManager.RegisterStartupScript(this, GetType(), "customControlScript", strScript, true);
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "customControlScript", strScript, true);
        }

        #region eventos

        protected void DdlFiltroselectedIndexChanged(object sender, EventArgs e)
        {
            GetCampos();
        }

        private void ConfigurarControles()
        {
            if (dvRutas.CurrentMode == DetailsViewMode.Edit)
            {
                btnGuardar.Text = @"Guardar";
                btnEliminar.Text = @"Cancelar";
                btnGuardar.Visible = true;
                btnEliminar.Visible = true;
            }
            else if (dvRutas.CurrentMode == DetailsViewMode.ReadOnly)
            {
                if (!string.IsNullOrEmpty(hndNodeSelected.Value))
                {
                    btnEliminar.Text = @"Eliminar";
                    btnGuardar.Text = @"Editar";
                    btnGuardar.Visible = true;
                    btnEliminar.Visible = true;
                }
                else
                {
                    btnGuardar.Visible = false;
                    btnEliminar.Visible = false;
                }
            }
            else
            {
                btnGuardar.Text = @"Guardar";
                btnEliminar.Text = @"Cancelar";
                btnGuardar.Visible = true;
                btnEliminar.Visible = true;
            }
        }

        protected void ContextMenuClick(object sender, DataMenuItemEventArgs e)
        {
            switch (e.Item.Key)
            {
                case "Edit":
                    dvRutas.ChangeMode(DetailsViewMode.Edit);
                    Cargar(hndNodeSelected.Value);
                    break;
                case "Select":
                    dvRutas.ChangeMode(DetailsViewMode.ReadOnly);
                    Cargar(hndNodeSelected.Value);
                    btnEliminar.Attributes.Add("onclick", "return confirm('¿Confirma que desea eliminar el registro seleccionado?');");
                    break;

                case "New":
                    dvRutas.ChangeMode(DetailsViewMode.Insert);
                    Cargar(hndNodeSelected.Value);
                    hndNodeSelected.Value = string.Empty;
                    btnEliminar.Attributes.Remove("onclick");
                    break;
            }

        }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(hndNodeSelected.Value))
            {
                if (dvRutas.CurrentMode == DetailsViewMode.ReadOnly)
                {
                    ActualizarDetailsView(DetailsViewMode.Edit);
                    btnEliminar.Attributes.Remove("onclick");
                }
                else
                {
                    Actualizar();
                    ActualizarDetailsView(DetailsViewMode.ReadOnly);
                }
            }
            else
            {
                Guardar();
                ActualizarDetailsView(DetailsViewMode.ReadOnly);
            }
            GetCampos();
        }

        protected void BtnEliminarClick(object sender, EventArgs e)
        {
            if (btnEliminar.Text == @"Cancelar")
            {

                ActualizarDetailsView(DetailsViewMode.ReadOnly);
                if (!string.IsNullOrEmpty(hndNodeSelected.Value))
                    btnEliminar.Attributes.Add("onclick", "return confirm('¿Confirma que desea eliminar el registro seleccionado?');");
            }
            else
            {
                EliminarEvent();
                ActualizarDetailsView(DetailsViewMode.ReadOnly);
                GetCampos();
                btnEliminar.Attributes.Remove("onclick");
            }
        }

        protected void OnDataBoundEvent(object sender, EventArgs e)
        {
            var ddlEstado = dvRutas.FindControl("ddlEstadosEdit") as DropDownList;
            if (ddlEstado != null)
            {
                GetEstados(ddlEstado);
                var hdnEstadoInicial = dvRutas.FindControl("hdnEstadoInicial") as HtmlInputHidden;
                if (hdnEstadoInicial != null)
                    ddlEstado.SelectedValue = hdnEstadoInicial.Value;
            }

        }

        #endregion

        
    }
}