using System;
using Application.Core;
using Applications.MainModule.PlanAccion.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.PlanAccion.IViews;

namespace Presenters.PlanAccion.Presenters
{
    public class VistaBancoActividadesPresenter : Presenter<IVistaBancoActividadesView>
    {
        private readonly ISfTBL_ModuloPlanAccion_BancoActividadesManagementServices _actividadesServices;

        public VistaBancoActividadesPresenter(ISfTBL_ModuloPlanAccion_BancoActividadesManagementServices actividadesServices)
        {
            _actividadesServices = actividadesServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
            View.FilterEvent += ViewFilterEvent;
            View.DeleteEvent += ViewDeleteEvent;
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            if (sender == null) return;
            DeleteEvent(Convert.ToInt32(sender));
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(sender == null ? 0 : Convert.ToInt32(sender));
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAll(0);
        }

        private void GetAll(int currentPage)
        {
            try
            {
                var total = _actividadesServices.CountbyPaged();
                View.TotalRegistrosPaginador = total == 0 ? 1 : total;
                var list = _actividadesServices.FindPaged(currentPage, View.PageZise);
                View.ListaBancoActividades(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void DeleteEvent(int idActividad)
        {
            try
            {
                var result = _actividadesServices.Delete(idActividad);
                InvokeMessageBox(result
                                     ? new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok)
                                     : new MessageBoxEventArgs(string.Format(Message.DeleteError, "Categoría"),
                                                               TypeError.Error));
                GetAll(0);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}