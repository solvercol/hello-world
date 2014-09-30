using System;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Applications.MainModule.Admin.IServices;

namespace Presenters.Reclamos.Presenters
{
    public class AsesoresListPresenter : Presenter<IAsesoresListView>
    {
        private readonly ISfTBL_ModuloReclamos_AsesoresManagementServices _asesores;
        private readonly ISfTBL_Admin_UsuariosManagementServices _user;

        public AsesoresListPresenter(ISfTBL_ModuloReclamos_AsesoresManagementServices asesores , ISfTBL_Admin_UsuariosManagementServices user)
        {
            _user = user;
            _asesores = asesores;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
            View.PagerEvent += ViewPagerEvent;
        }


        void ViewPagerEvent(object sender, EventArgs e)
        {
            GetAll(sender == null ? 0 : Convert.ToInt32(sender));
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(0);
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
                var total = _asesores.CountByPaged(View.Search);

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _asesores.FindPaged(currentPage, View.PageZise, View.Search);

                View.GetAsesores(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public string GetNameAsesor(int id)
        {
            try
            {
            var Asesor = _user.FindById(id);
            return Asesor.Nombres;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                return string.Empty;
            }
        }

    }
}
