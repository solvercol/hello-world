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
using System.Collections.Generic;
namespace Presenters.Reclamos.Presenters
{
    public class EditCategoriaProductoPresenter : Presenter<IEditCategoriaProductoView>
    {
        private readonly ISfTBL_ModuloReclamos_CategoriaProductoManagementServices _catProducto;

        private readonly ISfTBL_Admin_UsuariosManagementServices _ingenieroResponsable;

        private readonly ISfTBL_Admin_OptionListManagementServices _optionList;

        private readonly ISfTBL_Admin_RolesManagementServices _rol;

        public EditCategoriaProductoPresenter(ISfTBL_ModuloReclamos_CategoriaProductoManagementServices catProducto, ISfTBL_Admin_UsuariosManagementServices ingenieroResponsable, ISfTBL_Admin_OptionListManagementServices optionList, ISfTBL_Admin_RolesManagementServices rol)
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
            GetCategoriaProducto();
        }

        /// <summary>
        /// Guarda una categoria de Producto en Base de datos.
        /// </summary>
        private void GuardarCategoriaProducto()
        {

            try
            {
                if (string.IsNullOrEmpty(View.IdCategoriaProducto)) return;

                var cp = _catProducto.FindById(Convert.ToInt32(View.IdCategoriaProducto));

                if (cp == null) return;

                cp.Nombre = View.Nombre;
                cp.Descripcion = View.Descripcion;
                cp.IsActive = View.Activo;
                cp.ModifiedBy = View.UserSession.IdUser;
                cp.ModifiedOn = DateTime.Now;
                cp.TBL_Admin_Usuarios2.Clear();

                foreach (var item in View.UsuariosCopia)
                {
                    var obj = _ingenieroResponsable.FindById(Convert.ToInt32(item.Id));
                    cp.TBL_Admin_Usuarios2.Add(obj);
                }

                _catProducto.Modify(cp);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void GetCategoriaProducto()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdCategoriaProducto)) return;
                var cp = _catProducto.FindById(Convert.ToInt32(View.IdCategoriaProducto));
                if (cp == null) return;
                View.Nombre = cp.Nombre;
                View.Descripcion = cp.Descripcion;
                View.Activo = cp.IsActive;
                View.CreateBy = cp.TBL_Admin_Usuarios.Nombres;
                View.CreateOn = cp.CreateOn != null ? cp.CreateOn.ToShortDateString() + " " + cp.CreateOn.ToShortTimeString() : string.Empty;
                View.ModifiedBy = cp.TBL_Admin_Usuarios1.Nombres;
                View.ModifiedOn = cp.ModifiedOn != null ? cp.ModifiedOn.ToShortDateString() + " " + cp.ModifiedOn.ToShortTimeString() : string.Empty;

                List<DTO_ValueKey> Users = new List<DTO_ValueKey>();
                foreach(var User in cp.TBL_Admin_Usuarios2)
                {
                    Users.Add(new DTO_ValueKey() { Id = User.IdUser.ToString(), Value = User.Nombres });

                }
                View.UsuariosCopia = Users;
                View.LoadUsuariosCopia(Users);

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
                if (idrolresponsable.Value != "0")
                {
                    var rol = _rol.FindById(int.Parse(idrolresponsable.Value));
                    var listado = _ingenieroResponsable.FindBySpecWithRols(true);
                    var listadoResponsables = listado.Where(u => u.TBL_Admin_Roles1.Contains(rol)).ToList();
                    View.GetIngenieros(listadoResponsables);
                }
                else
                {
                    var listado = _ingenieroResponsable.FindBySpec(true);
                    View.GetIngenieros(listado);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }
    }
}
