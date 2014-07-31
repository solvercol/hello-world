using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IView;
using Presenters.Admin.Presenters;


namespace ASP.NETCLIENTE.Pages.UserControls
{
    public partial class WucMenuPrincipal :  ViewUserControl<MenuPrincipalPresenter, IMenuPrincipalView>, IMenuPrincipalView
    {
        public string ModuleId
        {
            get { return Request.QueryString["ModuleId"]; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
        }

       
        public void OpcionesMenu(IEnumerable<TBL_Admin_OpcionesMenu> items)
        {
            mnuMenuPrincipal.Items.Clear();
            foreach (var item in items)
            {
                if (item.IdopcionPadre == null)
                {
                    var objItem = new MenuItem
                    {
                        Value = item.IdOpcionMenu.ToString(),
                        Text = item.TituloOpcion,
                        ToolTip = item.TituloOpcion,
                        NavigateUrl = item.LinkUrl
                    };
                    mnuMenuPrincipal.Items.Add(objItem);
                    AddMenuItem(items, objItem);
                }
            }
        }

        private static void AddMenuItem(IEnumerable<TBL_Admin_OpcionesMenu> items, MenuItem itemMenu)
        {

            foreach (var item in items)
            {
                if (item.IdopcionPadre != null && (Convert.ToInt32(itemMenu.Value) == item.TBL_Admin_OpcionesMenu2.IdOpcionMenu))
                {
                    var objItem = new MenuItem
                    {
                        Value = item.IdOpcionMenu.ToString(),
                        Text = item.TituloOpcion,
                        ToolTip = item.TituloOpcion,
                        NavigateUrl = string.Format("{0}?ModuleId={1}", item.LinkUrl, item.AplicationId) 
                    };
                    itemMenu.ChildItems.Add(objItem);
                    AddMenuItem(items, objItem);
                }
            }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }
    }
}