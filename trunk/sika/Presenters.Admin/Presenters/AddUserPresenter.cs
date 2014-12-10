using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Linq;

namespace Presenters.Admin.Presenters
{
    public class AddUserPresenter : Presenter<IAddUserView>
    {
        private readonly ISfTBL_Admin_UsuariosManagementServices _user;

        private readonly ISfTBL_Admin_RolesManagementServices _roles;

        public AddUserPresenter(ISfTBL_Admin_UsuariosManagementServices user, ISfTBL_Admin_RolesManagementServices roles)
        {
            _user = user;
            _roles = roles;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAllRoles();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
                GuardarUsuario();
        }

        /// <summary>
        /// Inserta un nuevo usuario en Base de datos.
        /// </summary>
        private void GuardarUsuario()
        {
            try
            {
                
                var usuario = _user.NewEntity();
                usuario.CodigoUser = View.UserCode;
                usuario.Nombres = View.Names;
                usuario.FechaIngreso = View.IncomeDate;
                usuario.UserName = View.UserName;
                usuario.Password = View.Password;
                usuario.Email = View.Email;
                usuario.IsActive = View.Activo;
                usuario.CreateOn = DateTime.Now;
                usuario.CreateBy = View.UserSession.IdUser.ToString();
                usuario.ModifiedOn = DateTime.Now;
                usuario.ModifiedBy = View.UserSession.IdUser.ToString();
                var roles = View.GetSelectdRole();
                foreach (var objRol in
                from object r in roles select _roles.FindById(Convert.ToInt32(r)))
                {
                    if (objRol == null) return;
                    usuario.TBL_Admin_Roles1.Add(objRol);
                }
                _user.Add(usuario);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }

        private void GetAllRoles()
        {
            var listado = _roles.FindPaged(0, 10);
            View.GetAllRoles(listado);
        }

    }
}
