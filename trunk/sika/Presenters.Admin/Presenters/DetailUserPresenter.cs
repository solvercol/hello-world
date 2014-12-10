using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class DetailUserPresenter : Presenter<IDetailUserView>
    {
        private readonly ISfTBL_Admin_UsuariosManagementServices _user;

        private readonly ISfTBL_Admin_RolesManagementServices _roles;

        public DetailUserPresenter(ISfTBL_Admin_UsuariosManagementServices user, ISfTBL_Admin_RolesManagementServices roles)
        {
            _user = user;
            _roles = roles;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAllRoles();
            Load();
            ValidarRol();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdUser)) return;

            var user = _user.FindById(Convert.ToInt32(View.IdUser));

            if (user == null) return;
            View.UserCode = user.CodigoUser;
            View.Names = user.Nombres;
            View.IncomeDate = user.FechaIngreso != null ? user.FechaIngreso.ToShortDateString() : string.Empty;
            View.UserName = user.UserName;
            View.Email = user.Email;
            View.Activo = user.IsActive;
            View.CreateBy = user.CreateBy;
            View.CreateOn = user.CreateOn.Value != null ? user.CreateOn.GetValueOrDefault().ToShortDateString() : string.Empty;
            View.ModifiedBy = user.ModifiedBy;
            View.ModifiedOn = user.ModifiedOn != null ? user.ModifiedOn.GetValueOrDefault().ToShortDateString() : string.Empty;

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
