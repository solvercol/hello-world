using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;

using Presenters.DefaultPresenter;

namespace ASP.NETCLIENTE.Pages.Globals
{
    public partial class FrmOpcionesMenu : ViewPage<OpcionesMenuPresenter, IOpcionesMenuView>, IOpcionesMenuView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler LoadDetalleEvent;

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInitialize();
            if (IsPostBack) return;

        }

        protected override void OnInit(EventArgs e)
        {
            //Presenter.MessageBox += PresenterMessageBox;
            rptRoles.ItemDataBound += RptRolesItemDataBound;
            base.OnInit(e);
        }


        private void LoadInitialize()
        {
            ImprimirTituloVentana("Administración De Opciones Menú Principal.");
            btnDelete.Attributes.Add("onclick", "return confirm(\"¿Confirma que desea continuar?\")");            
        }

        #endregion

        #region Eventos

        //protected void UwtOpcionesMenuNodeClicked(object sender, WebTreeNodeEventArgs e)
        //{
        //    LimpiarMensajes();
        //    var nodoselected = e.Node;
        //    if (nodoselected == null)
        //    {
        //        ShowError("Error al seleccionar el nodo de la lista!!!");
        //        return;
        //    }
        //    if (nodoselected.DataKey == null)
        //    {
        //        btnDelete.Enabled = false;
        //        btnSave.Enabled = false;
        //        IdOpcionMenu = null;
        //        return;
        //    }
        //    btnDelete.Enabled = true;
        //    btnSave.Enabled = true;
        //    IdOpcionMenu = Convert.ToInt32(nodoselected.DataKey);
        //    if (LoadDetalleEvent != null)
        //        LoadDetalleEvent(nodoselected.DataKey, EventArgs.Empty);
        //    ViewState["node"] = "Edit";
        //}

        //protected void BtnNewClick(object sender, EventArgs e)
        //{
        //    LimpiarMensajes();
        //    var nodoselected = uwtOpcionesMenu.SelectedNode;
        //    if (nodoselected == null)
        //    {
        //        ShowError("Debe seleccionar una opción de la lista!!!");
        //        return;
        //    }
        //    txtDescripcion.Focus();
        //    btnSave.Enabled = true;
        //    ViewState["node"] = "Insert";

        //    foreach (var chkRole in from RepeaterItem ri in rptRoles.Items select (CheckBox)ri.FindControl("chkRole"))
        //    {
        //        chkRole.Checked = false;
        //    }

        //    txtDescripcion.Text = string.Empty;
        //    chkActive.Checked = false;
        //    txtIcono.Text = string.Empty;
        //    txtPosicion.Text = string.Empty;
        //    txtUrl.Text = string.Empty;
        //}

        protected void BtnSaveClick(object sender, EventArgs e)
        {

            if (ViewState["node"] == null) return;
            if (SaveEvent == null) return;
            SaveEvent(ViewState["node"].ToString() == "Insert" ? "Save" : "Update", EventArgs.Empty);
            IdOpcionMenu = null;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            IdOpcionMenu = null;
        }

        void RptRolesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var role = e.Item.DataItem as TBL_Maestra_Roles;
            if (role == null) return;
            ViewState[e.Item.UniqueID] = role.IdRol;
            var chkEdit = (CheckBox)e.Item.FindControl("chkRole");
            chkEdit.Checked = true;
        }

        #endregion

        #region Members

        public void GetAllRoles(IEnumerable<TBL_Maestra_Roles> items)
        {
            rptRoles.DataSource = items;
            rptRoles.DataBind();
        }

        public void RolesAsigandos(IEnumerable<TBL_Maestra_Roles> items)
        {
            foreach (RepeaterItem ri in rptRoles.Items)
            {
                var roleId = (int)ViewState[ri.UniqueID];
                var chk = (CheckBox)ri.FindControl("chkRole");
                chk.Checked = items.Any(r => r.IdRol == roleId);
            }
        }

        public void OpcionesMenu(IEnumerable<TBL_Maestra_OpcionesMenu> items)
        {
            //uwtOpcionesMenu.Nodes.Clear();
            //var rootNode = new Node
            //{
            //    Text = "Nivel Principal",
            //    Tag = 0,
            //    ShowExpand = true,
            //    Expanded = true,
            //    ImageUrl = "~/Resources/images/RootNode.png"
            //};

            //uwtOpcionesMenu.Nodes.Add(rootNode);

            //var grupos = items.Where(n => n.IdopcionPadre == null).OrderBy(x => x.Posicion);
            //foreach (var node in grupos)
            //{
            //    var tieneHijos = items.Where(i => i.IdopcionPadre == node.IdOpcionMenu).Count() > 0;
            //    var itemMenu = new Node
            //    {
            //        Text = node.TituloOpcion,
            //        DataKey = node.IdOpcionMenu,
            //        ImageUrl = "~/Resources/images/ChildNode.png",
            //        ShowExpand = tieneHijos,
            //        Expanded = tieneHijos
            //    };
            //    rootNode.Nodes.Add(itemMenu);
            //    AddChildMenu(items, itemMenu);
            //}
        }

        //private static void AddChildMenu(IEnumerable<TBL_Maestra_OpcionesMenu> items, Node parent)
        //{
        //    var parentId = Convert.ToInt32(parent.DataKey);
        //    var childItemsMenu = items.Where(i => i.IdopcionPadre == parentId).OrderBy(x => x.Posicion);
        //    foreach (var child in
        //       childItemsMenu.Select(objItem => new Node
        //       {
        //           Text = objItem.TituloOpcion,
        //           DataKey = objItem.IdOpcionMenu,
        //           ImageUrl = "~/Resources/images/ChildNode.png",
        //           ShowExpand = items.Where(i => i.IdopcionPadre == objItem.IdOpcionMenu).Count() > 0,
        //           Expanded = items.Where(i => i.IdopcionPadre == objItem.IdOpcionMenu).Count() > 0
        //       }))
        //    {
        //        parent.Nodes.Add(child);
        //        AddChildMenu(items, child);
        //    }
        //}

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        public string Ulr
        {
            get { return txtUrl.Text; }
            set { txtUrl.Text = value; }
        }

        public string Posicion
        {
            get { return txtPosicion.Text; }
            set { txtPosicion.Text = value; }
        }

        public string Icono
        {
            get { return txtIcono.Text; }
            set { txtIcono.Text = value; }
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public bool ShowInNavigation
        {
            get { return chkShowInNavigarion.Checked; }
            set { chkShowInNavigarion.Checked = value; }
        }

        public int? IdOpcionMenu
        {
            get
            {
                if (ViewState["NodoSeleccionado"] == null)
                    return null;
                return Convert.ToInt32(ViewState["NodoSeleccionado"]);
            }
            set { ViewState["NodoSeleccionado"] = value; }
        }

        public int AplicationId
        {
            get { return 1; }
        }

        public ArrayList GetSelectdRole()
        {
            var arrayList = new ArrayList();
            foreach (var roleId in from RepeaterItem ri in rptRoles.Items
                                   let roleId = (int)ViewState[ri.UniqueID]
                                   let chk = (CheckBox)ri.FindControl("chkRole")
                                   where chk.Checked
                                   select roleId)
            {
                arrayList.Add(roleId);
            }
            return arrayList;
        }

        //void PresenterMessageBox(object sender, MessageBoxEventArgs e)
        //{
        //    switch (e.Tipo)
        //    {
        //        case TypeError.Ok:
        //            ShowMessageOk(e.Message);
        //            break;
        //        default:
        //            ShowError(e.Message);
        //            break;
        //    }
        //}

        #endregion
    }
}