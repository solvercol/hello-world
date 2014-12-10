using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Collections;
using System.Linq;

namespace Presenters.Admin.Presenters
{
    public class EditUserPresenter : Presenter<IEditUserView>
    {
        private readonly ISfTBL_Admin_UsuariosManagementServices _user;

        private readonly ISfTBL_Admin_RolesManagementServices _roles;

        public EditUserPresenter(ISfTBL_Admin_UsuariosManagementServices user, ISfTBL_Admin_RolesManagementServices roles)
        {
            _user = user;
            _roles = roles;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAllRoles();
            Load();
            ValidarRol();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(View.IdUser))
                GuardarUsuario();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarUsuario();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdUser)) return;

            var user = _user.FindById(Convert.ToInt32(View.IdUser));

            if (user == null) return;

            View.UserCode = user.CodigoUser;
            View.Names = user.Nombres;
            View.IncomeDate = user.FechaIngreso;
            View.UserName = user.UserName;
            View.Password = user.Password;
            View.Email = user.Email;
            View.Activo = user.IsActive;
            View.CreateBy = user.CreateBy;
            View.CreateOn = user.CreateOn.Value != null ? user.CreateOn.GetValueOrDefault().ToShortDateString() : string.Empty;
            View.ModifiedBy = user.ModifiedBy;
            View.ModifiedOn = user.ModifiedOn != null ? user.ModifiedOn.GetValueOrDefault().ToShortDateString() : string.Empty;

        }

        /// <summary>
        /// Actualiza un usuario en Base de datos.
        /// </summary>
        private void GuardarUsuario()
        {

            try
            {
                if (string.IsNullOrEmpty(View.IdUser)) return;

                var usuario = _user.FindById(Convert.ToInt32(View.IdUser));

                usuario.TBL_Admin_Roles1.Clear();
                var roles = View.GetSelectdRole();

                foreach (var objRol in
                    from object r in roles select _roles.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    usuario.TBL_Admin_Roles1.Add(objRol);
                }

                if (usuario == null) return;
                usuario.CodigoUser = View.UserCode;
                usuario.Nombres = View.Names;
                usuario.FechaIngreso = View.IncomeDate;
                usuario.UserName = View.UserName;
                usuario.Password = View.Password;
                usuario.Email = View.Email;
                usuario.IsActive = View.Activo;
                usuario.ModifiedOn = DateTime.Now;
                usuario.ModifiedBy = View.UserSession.IdUser.ToString();
                _user.Modify(usuario);
                Load();
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        /// <summary>
        /// Elimina un usuario seleccionada en Base de Datos
        /// </summary>
        private void EliminarUsuario()
        {
            try
            {
                if (View.IdUser == "") return;
                var usuario = _user.FindById(Convert.ToInt32(View.IdUser));
                if (usuario == null) return;
                _user.Remove(usuario);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }

        private void GetAllRoles()
        {
            var listado = _roles.FindPaged(0, 10); 
            View.GetAllRoles(listado);
        }

        private void ValidarRol()
        {
            if (string.IsNullOrEmpty(View.IdUser)) return;
            var user = _user.FindById(Convert.ToInt32(View.IdUser));
            View.RolesAsigandos(user.TBL_Admin_Roles1);
        }
    }
}
