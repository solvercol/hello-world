using System;
using System.Linq;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Infragistics.Web.UI.EditorControls;
using Infragistics.Web.UI.NavigationControls;
using Infrastructure.CrossCutting.NetFramework.Enums;
using System.Web.UI;

namespace Modules.WorkFlow.Admin
{
    public partial class WucRutasWorkFlow : BaseUserControl
    {
        private WorkFlowModule _module;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            _module = Module as WorkFlowModule;
            if (IsLoadUserControl) return;
            GetRutas();
            IsLoadUserControl = true;
            
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RegistrarScript();
        }

      
        #region Eventos

        protected void ContextMenuClick(object sender, DataMenuItemEventArgs e)
        {
            switch (e.Item.Key)
            {
                case "Edit":
                    dvRutas.ChangeMode(DetailsViewMode.Edit);
                    CargarRuta(hndNodeSelected.Value);
                    break;
                case "Select":
                    dvRutas.ChangeMode(DetailsViewMode.ReadOnly);
                    CargarRuta(hndNodeSelected.Value);
                    btnEliminar.Attributes.Add("onclick", "return confirm('¿Confirma que desea eliminar el registro seleccionado?');");
                    break;

                case "New":
                     dvRutas.ChangeMode(DetailsViewMode.Insert);
                     CargarRuta(hndNodeSelected.Value);
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

            GetRutas();
        }

        protected void BtnEliminarClick(object sender, EventArgs e)
        {
            if(btnEliminar.Text == @"Cancelar")
            {
               
                    ActualizarDetailsView(DetailsViewMode.ReadOnly);
                    if(!string.IsNullOrEmpty(hndNodeSelected.Value))
                        btnEliminar.Attributes.Add("onclick", "return confirm('¿Confirma que desea eliminar el registro seleccionado?');");
             }
            else
            {
                EliminarEvent();
                ActualizarDetailsView(DetailsViewMode.ReadOnly);
                GetRutas();
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

            var ddlEstadoFinalEdit = dvRutas.FindControl("ddlEstadoFinalEdit") as DropDownList;
            if (ddlEstadoFinalEdit != null)
            {
                GetEstados(ddlEstadoFinalEdit);
                var hdnEstadoFinal = dvRutas.FindControl("hdnEstadoFinal") as HtmlInputHidden;
                if (hdnEstadoFinal != null)
                    ddlEstadoFinalEdit.SelectedValue = hdnEstadoFinal.Value;
            }

        }

        #endregion

        private void ActualizarDetailsView(DetailsViewMode mode)
        {
            dvRutas.ChangeMode(mode);
            CargarRuta(hndNodeSelected.Value);
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

        private void Guardar()
        {
            try
            {
                var oRuta = _module.RutasWorkFlowServices.NewEntity();
                oRuta.BotonAccionesRutas = ((TextBox)dvRutas.FindControl("txtTextoBotonAccionesEdit")).Text;
                oRuta.FormulaValidacion = ((TextBox)dvRutas.FindControl("txtFormulaValidacionEdit")).Text;
                if (!string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue))
                    oRuta.IdEstado = Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue);
                oRuta.ModifiedBy = AuthenticatedUser.IdUser.ToString();
                oRuta.ModifiedOn = DateTime.Now;
                oRuta.CreateBy = AuthenticatedUser.IdUser.ToString();
                oRuta.CreateOn = DateTime.Now;
                oRuta.IsActive = true;
                oRuta.Rol = ((TextBox)dvRutas.FindControl("txtRolEdit")).Text;
                if (!string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlEstadoFinalEdit")).SelectedValue))
                    oRuta.SiguienteEstado = Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlEstadoFinalEdit")).SelectedValue);
                oRuta.Secuencia = oRuta.Secuencia = ((WebNumericEditor)dvRutas.FindControl("txtSecuenciaEdit")).ValueInt;
                oRuta.ValidaRequeridos = ((CheckBox)dvRutas.FindControl("chkValidaRequeridosEdit")).Checked;
                oRuta.RolResponsableActual = ((TextBox)dvRutas.FindControl("txtRolResponsableEdit")).Text;
                //oRuta.AccionesSistema = ((TextBox)dvRutas.FindControl("txtAccionesSistemaEdit")).Text;
                oRuta.TipoModulo = ModulosAplicacion.Pedidos.ToString();
                _module.RutasWorkFlowServices.Add(oRuta);
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Proceso realizado satisfactoriamente!!"), TypeError.Ok));
            }
            catch (Exception ex)
            {
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al guardar el registro!!"), TypeError.Error));
                LogError(MethodBase.GetCurrentMethod().Name, ex); 
            }
        }

        private void  Actualizar()
        {
            try
            {
                if (string.IsNullOrEmpty(hndNodeSelected.Value)) return;
                var oRuta = _module.RutasWorkFlowServices.FindById(Convert.ToInt32(hndNodeSelected.Value));
                if (oRuta == null)
                {
                    InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al obtener el Objeto {0} desde la base de datos", IdRutaSelected), TypeError.Error));
                    return ;
                }

                oRuta.BotonAccionesRutas = ((TextBox) dvRutas.FindControl("txtTextoBotonAccionesEdit")).Text;
                oRuta.Rol = ((TextBox)dvRutas.FindControl("txtRolEdit")).Text;
                oRuta.FormulaValidacion = ((TextBox)dvRutas.FindControl("txtFormulaValidacionEdit")).Text;
                if (!string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue))
                    oRuta.IdEstado = Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlEstadosEdit")).SelectedValue);
                oRuta.ModifiedBy = AuthenticatedUser.IdUser.ToString();
                oRuta.ModifiedOn = DateTime.Now;
                oRuta.RolResponsableActual = ((TextBox)dvRutas.FindControl("txtRolEdit")).Text;
                if (!string.IsNullOrEmpty(((DropDownList)dvRutas.FindControl("ddlEstadoFinalEdit")).SelectedValue))
                    oRuta.SiguienteEstado = Convert.ToInt32(((DropDownList)dvRutas.FindControl("ddlEstadoFinalEdit")).SelectedValue);
                oRuta.Secuencia =  ((WebNumericEditor)dvRutas.FindControl("txtSecuenciaEdit")).ValueInt;
                oRuta.ValidaRequeridos = ((CheckBox)dvRutas.FindControl("chkValidaRequeridosEdit")).Checked;
                oRuta.RolResponsableActual = ((TextBox)dvRutas.FindControl("txtRolResponsableEdit")).Text;
                //oRuta.AccionesSistema = ((TextBox)dvRutas.FindControl("txtAccionesSistemaEdit")).Text;
               
                _module.RutasWorkFlowServices.Modify(oRuta);
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Proceso realizado satisfactoriamente!!"), TypeError.Ok));
            }
            catch (Exception ex)
            {
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al actualizar el registro!!"), TypeError.Error));
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
        private void EliminarEvent()
        {
            try
            {
                if (string.IsNullOrEmpty(IdRutaSelected)) return;
                var oRuta = _module.RutasWorkFlowServices.FindById(Convert.ToInt32(IdRutaSelected));
                if (oRuta == null)
                {
                    InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al obtener el Objeto {0} desde la base de datos", IdRutaSelected), TypeError.Error));
                    return ;
                }
                _module.RutasWorkFlowServices.Remove(oRuta);
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Proceso realizado satisfactoriamente!!"), TypeError.Ok));
                IdRutaSelected = string.Empty;
            }
            catch (Exception ex)
            {
                InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al eliminar el registro!!"), TypeError.Error));
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void GetRutas()
        {
            try
            {

                var list = _module.RutasWorkFlowServices.ListadoRutasPorIdModule(ModulosAplicacion.Pedidos);
                wdtRutas.Nodes.Clear();

                var rootNode = new DataTreeNode
                {
                    Text = "Arbol de Rutas",
                    Key = string.Empty,
                    Value = string.Empty,
                    Expanded = true,
                    ImageUrl = "~/Resources/Images/Rutas.png"
                };

                wdtRutas.Nodes.Add(rootNode);

                var listaAgrupada = from g in list
                                    group g by new
                                               {
                                                   
                                                   g.TBL_Admin_EstadosProceso.Descripcion,
                                                   g.IdEstado

                                               };
                
                foreach (var grp in listaAgrupada)
                {
                    var oEstado = new DataTreeNode
                                      {
                                          Text = grp.Key.Descripcion, 
                                          Value = grp.Key.IdEstado.ToString(),
                                          ImageUrl = "~/Resources/Images/ChildNode32.png",
                                      };
                    rootNode.Nodes.Add(oEstado);

                    var grp1 = grp;
                    foreach (var rutase in list.Where(x=> x.IdEstado == grp1.Key.IdEstado ).OrderBy(x=> x.Secuencia) )
                    {
                         var oEstadoFin = new DataTreeNode
                                              {
                                                  Text = string.Format("[{0}] {1}", rutase.Secuencia, rutase.TBL_Admin_EstadosProceso1.Descripcion), 
                                                  Value = rutase.IdRuta.ToString(),
                                                  Key = rutase.IdRuta.ToString(),
                                                  ImageUrl = "~/Resources/Images/ChildNode4.png",
                                              };
                        oEstado.Nodes.Add(oEstadoFin);
                    }
                }

                //foreach (var n in list.Select(opcion => new DataTreeNode
                //{
                //    Text = string.Format("[{0}] - {1} -> {2}", opcion.Secuencia,
                //    opcion.TBL_Admin_EstadosProceso == null ?"??" : opcion.TBL_Admin_EstadosProceso.Descripcion, 
                //    opcion.TBL_Admin_EstadosProceso1 == null ? "??" : opcion.TBL_Admin_EstadosProceso1.Descripcion),

                //    Key = opcion.IdRuta.ToString(),
                //    ImageUrl = "~/Resources/Images/ChildNode32.png",
                //    Value = opcion.IdRuta.ToString(),
                //    IsEmptyParent = false
                //}))
                //{
                //    rootNode.Nodes.Add(n);
                //}

            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void CargarRuta(string  idRuta)
        {
            try
            {
               
                var oRuta = _module.RutasWorkFlowServices.FindBySpec( string.IsNullOrEmpty(idRuta) ? -1 : Convert.ToInt32(idRuta));
                if (oRuta == null)
                {
                    InvokeViewResult(new MessageBoxEventArgs(string.Format("Error al obtener el Objeto {0} desde la base de datos", idRuta), TypeError.Error));
                    return;
                }

                dvRutas.DataSource = oRuta;
                dvRutas.DataBind();

                ConfigurarControles();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private string IdRutaSelected
        {
            get { return string.IsNullOrEmpty(hndNodeSelected.Value) ? string.Empty : hndNodeSelected.Value; }
            set { hndNodeSelected.Value = value; }
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
       
    }
}