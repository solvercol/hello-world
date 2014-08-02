using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Application.Core;
using Application.MainModule.IApplicationServices;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Logging;
using Domain.MainModules.Entities;

namespace Presenters.DefaultPresenter
{
    public class OpcionesMenuPresenter : Presenter<IOpcionesMenuView>
    {

        private readonly IMenuManagementServices _iMenuManagementServices;
        private readonly IRoleManagementServices _iRoleManagementServices;
        readonly ITraceManager _traceManager;

        public OpcionesMenuPresenter(IMenuManagementServices iMenuManagementServices, ITraceManager traceManager, IRoleManagementServices iRoleManagementServices)
        {
            _iMenuManagementServices = iMenuManagementServices;
            _iRoleManagementServices = iRoleManagementServices;
            _traceManager = traceManager;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
            View.LoadDetalleEvent += ViewLoadDetalleEvent;
        }

       

        #region Eventos

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadObjects();
            GetAllRoles();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            try
            {
                if (View.IdOpcionMenu == null) return;
                var node = _iMenuManagementServices.FindById(Convert.ToInt32(View.IdOpcionMenu));
                if(node == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError), TypeError.Error));
                    return;
                }

               
                var childNode = _iMenuManagementServices.FindByIdParent(node.IdOpcionMenu);
                if (childNode)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ChildNodes, node.TituloOpcion), TypeError.Error));
                    return;
                }
                

                _iMenuManagementServices.Remove(node);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
                InitializeView();
                LoadObjects();
                GetAllRoles();
            }
            catch (Exception ex)
            {

                _traceManager.LogInfo(
                  string.Format(CultureInfo.InvariantCulture,
                                Message.DeleteError + " - " + ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                                "User"),
                                LogType.Notify);
            }

        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            if (sender == null) return;
            if (sender.Equals("Save"))
                Save();
            else
                Update();
        }

        private void Save()
        {

           
            var user = _iMenuManagementServices.FindByDescription(View.Descripcion);
            if (user != null)
            {
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.NodeExist, View.Descripcion), TypeError.Error));
                return;
            }


            try
            {
                var newNode = _iMenuManagementServices.NewEntity();

                var roles = View.GetSelectdRole();
                foreach (var objRol in
                    from object r in roles select _iRoleManagementServices.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    newNode.TBL_Maestra_Roles.Add(objRol);
                }
                newNode.TituloOpcion = View.Descripcion;
                newNode.Posicion = string.IsNullOrEmpty(View.Posicion) ? 0 : Convert.ToInt32(View.Posicion);
                newNode.Icono = View.Icono;
                newNode.LinkUrl = View.Ulr;
                newNode.VerEnMenu = View.ShowInNavigation;
                newNode.Activo = View.Activo;
                newNode.AplicationId = View.AplicationId;
                newNode.CreateOn = DateTime.Now;
                //newNode.CreateBy = ((TBL_Maestra_Usuarios)HttpContext.Current.User.Identity).NombreUsuario;
                newNode.ModifiedOn = DateTime.Now;
                //newNode.ModifiedBy = ((TBL_Maestra_Usuarios)HttpContext.Current.User.Identity).NombreUsuario;
                newNode.IdopcionPadre = View.IdOpcionMenu ?? null;
                _iMenuManagementServices.Add(newNode);
                LoadObjects();
                GetAllRoles();
                InitializeView();
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(
                  string.Format(CultureInfo.InvariantCulture,
                                Message.SaveError + " - " + ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                                "OpcionesMenuPresenter"),
                                LogType.Notify);
            }
        }

        private static string GetClassName(string url)
        {
          
            if (string.IsNullOrEmpty(url)) return "";
            var formName = url.Substring(url.LastIndexOf("/") + 1);
            var className = formName.Substring(0, formName.IndexOf("."));
            
            //if (className == "Default")
            //    className = "_Default";

            return className;
        }

        private void Update()
        {
            try
            {
                if (View.IdOpcionMenu == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SelectedNode), TypeError.Error));
                    return;
                }

                var opcion = _iMenuManagementServices.FindById( Convert.ToInt32(View.IdOpcionMenu));
                if (opcion == null) return;

                opcion.TBL_Maestra_Roles.Clear();
                var roles = View.GetSelectdRole();

                foreach (var objRol in
                    from object r in roles select _iRoleManagementServices.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    opcion.TBL_Maestra_Roles.Add(objRol);
                }

                opcion.TituloOpcion = View.Descripcion;
                opcion.Posicion = string.IsNullOrEmpty(View.Posicion) ? 0 : Convert.ToInt32(View.Posicion);
                opcion.Icono = View.Icono;
                opcion.LinkUrl = View.Ulr;
                opcion.VerEnMenu = View.ShowInNavigation;
                opcion.Activo = View.Activo;
                opcion.AplicationId = View.AplicationId;
                opcion.ModifiedOn = DateTime.Now;
                //opcion.ModifiedBy = ((TBL_Maestra_Usuarios)HttpContext.Current.User.Identity).NombreUsuario;
                _iMenuManagementServices.Modify(opcion);
                LoadObjects();
                GetAllRoles();
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
                InitializeView();
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(
                  string.Format(CultureInfo.InvariantCulture,
                                Message.SaveError + " - " + ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                                "OpcionesMenuPresenter"),
                                LogType.Notify);
            }
        }

        /// <summary>
        /// carga el detalle del nodo seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ViewLoadDetalleEvent(object sender, EventArgs e)
        {
            if(sender == null)return;
            var opcion = _iMenuManagementServices.FindById(Convert.ToInt32(sender));
            if (opcion == null)
            {
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError,""), TypeError.Error));
                return;
            }

            View.Descripcion = opcion.TituloOpcion;
            View.ShowInNavigation = opcion.VerEnMenu;
            View.Activo = opcion.Activo;
            View.Icono = opcion.Icono;
            View.Posicion = opcion.Posicion.ToString();
            View.Ulr = opcion.LinkUrl;
            ValidarRol(Convert.ToInt32(sender));
        }


        #endregion

        #region Members

        private void LoadObjects()
        {
            var opciones = _iMenuManagementServices.FindListByIdModule(View.AplicationId);
            View.OpcionesMenu(opciones);
        }
        
        private void GetAllRoles()
        {
            var listado = _iRoleManagementServices.FindPaged(0, 10);
            View.GetAllRoles(listado);
        }

        private void ValidarRol(int nodeId)
        {
            if (nodeId == 0) return;
            var opcionMenu = _iMenuManagementServices.FindById(nodeId);
            View.RolesAsigandos(opcionMenu.TBL_Maestra_Roles);
        }

        private void InitializeView()
        {
            View.Descripcion = string.Empty;
            View.Activo = false;
            View.Icono = string.Empty;
            View.Posicion = string.Empty;
            View.Ulr = string.Empty;
        }
        #endregion
    }
}