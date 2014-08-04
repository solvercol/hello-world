using System;
using Application.Core;
using Applications.MainModule.PlanAccion.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.PlanAccion.IViews;

namespace Presenters.PlanAccion.Presenters
{
    public class VistaCategoriasPresenter : Presenter<IVistaCategoriasView>
    {
        private readonly ISfTBL_ModuloPlanAccion_CategoriasManagementServices _categoryServices;

        public VistaCategoriasPresenter(ISfTBL_ModuloPlanAccion_CategoriasManagementServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
            View.DeleteEvent += ViewDeleteEvent;
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            if(sender==null)return;
            DeleteEvent(Convert.ToInt32(sender));
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(sender==null?0:Convert.ToInt32(sender));
        }

        void ViewLoad(object sender, EventArgs e)
        {
           if(View.IsPostBack)return;
            GetAll(0);
        }

        private void GetAll(int currentPage)
        {
            try
            {
                var total = _categoryServices.CountbyPaged();
                View.TotalRegistrosPaginador = total == 0 ? 1 : total;
                var list = _categoryServices.FindPaged(currentPage, View.PageZise);
                View.ListaCategorias(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void DeleteEvent(int idCategoria)
        {
            try
            {
                var result = _categoryServices.Delete(idCategoria);
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