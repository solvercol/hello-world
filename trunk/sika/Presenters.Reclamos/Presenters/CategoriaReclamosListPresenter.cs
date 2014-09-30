using System;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class CategoriaReclamosListPresenter : Presenter<ICategoriaReclamosListView>
    {

        private readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _CategoriaReclamos;

        public CategoriaReclamosListPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices CategoriaReclamos)
        {
            _CategoriaReclamos = CategoriaReclamos;
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
                var total = _CategoriaReclamos.CountByPaged(View.Search);

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _CategoriaReclamos.FindPaged(currentPage, View.PageZise,View.Search);

                View.GetCategoriaReclamos(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        
    }
}
