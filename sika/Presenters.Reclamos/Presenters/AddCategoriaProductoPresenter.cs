using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Application.MainModule.Reclamos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using System.Collections;
using System.Linq;

namespace Presenters.Reclamos.Presenters
{
    public class AddCategoriaProductoPresenter : Presenter<IAddCategoriaProductoView>
    {
        private readonly ISfTBL_ModuloReclamos_CategoriaProductoManagementServices _catProducto;

        private readonly ISfTBL_Admin_UsuariosManagementServices _ingenieroResponsable;

        private readonly ISfTBL_Admin_OptionListManagementServices _optionList;

        private readonly ISfTBL_Admin_RolesManagementServices _rol;

        public AddCategoriaProductoPresenter(ISfTBL_ModuloReclamos_CategoriaProductoManagementServices catProducto, ISfTBL_Admin_UsuariosManagementServices ingenieroResponsable, ISfTBL_Admin_OptionListManagementServices optionList, ISfTBL_Admin_RolesManagementServices rol)
        {
            _catProducto = catProducto;
            _ingenieroResponsable = ingenieroResponsable;
            _optionList = optionList;
            _rol = rol;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }


        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetIngResponsables();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarCategoriaProducto();
        }


        private void Load()
        {
            View.Nombre = string.Empty;
            View.Descripcion = string.Empty;
            View.Activo = false;
            View.CreateBy = View.UserSession.UserName;
            View.CreateOn = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Guarda una categoria de Producto en Base de datos.
        /// </summary>
        private void GuardarCategoriaProducto()
        {

            try
            {
                var cp = _catProducto.NewEntity();

                cp.Nombre = View.Nombre;
                cp.Descripcion = View.Descripcion;
                cp.IsActive = View.Activo;
                cp.CreateBy = View.UserSession.IdUser;
                cp.CreateOn = DateTime.Now;
                cp.ModifiedBy = View.UserSession.IdUser;
                cp.ModifiedOn = DateTime.Now;
                foreach (var item in View.UsuariosCopia)
                {
                    var obj = _ingenieroResponsable.FindById(Convert.ToInt32(item.Id));
                    cp.TBL_Admin_Usuarios2.Add(obj);
                }

                _catProducto.Add(cp);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void GetIngResponsables()
        {
            try
            {
                var idrolresponsable = _optionList.ObtenerOpcionBykey("IdRolIngenieroResponsable");
                var rol = _rol.FindById(int.Parse(idrolresponsable.Value));
                var listado = _ingenieroResponsable.FindBySpecWithRols(true);
                var listadoResponsables = listado.Where(u=> u.TBL_Admin_Roles1.Contains(rol)).ToList();
                View.GetIngenieros(listadoResponsables);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }
    }
}
