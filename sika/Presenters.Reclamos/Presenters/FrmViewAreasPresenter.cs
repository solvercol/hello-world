using System;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class FrmViewAreasPresenter : Presenter<IFrmViewAreasView>
    {
        private readonly ISfTBL_ModuloAPC_AreasManagementServices _areas;

        public FrmViewAreasPresenter(ISfTBL_ModuloAPC_AreasManagementServices areas)
        {
            _areas = areas;
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
                var total = _areas.CountByPaged(View.Search);

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _areas.FindPaged(currentPage, View.PageZise, View.Search);

                View.GetAreas(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod                                                                               ().Name, Logtype.Archivo));
            }
        }
    }
}