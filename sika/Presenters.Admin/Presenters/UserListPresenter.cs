﻿using System;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class UserListPresenter : Presenter<IUsersListView>
    {
        private readonly ISfTBL_Admin_UsuariosManagementServices _usuarios;

        public UserListPresenter(ISfTBL_Admin_UsuariosManagementServices usuarios)
        {
            _usuarios = usuarios;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(sender == null ? 0 : Convert.ToInt32(sender));
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAll(0);
        }

        private void GetAll(int currentPage)
        {
            try
            {
                var total = _usuarios.GetTotalUsers(View.SearchText);

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _usuarios.GetUsers(View.SearchText, currentPage, View.PageZise);

                View.GetUsers(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}
