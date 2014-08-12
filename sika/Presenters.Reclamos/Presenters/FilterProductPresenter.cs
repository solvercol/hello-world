using System;
using Application.Core;
using Presenters.Reclamos.IViews;
using Application.MainModule.SqlServices.IServices;
using System.Reflection;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Presenters.Reclamos.Presenters
{
    public class FilterProductPresenter : Presenter<IFilterProductView>
    {
        readonly IReclamosExternalInterfacesService _reclmaosInterfaceService;

        public FilterProductPresenter(IReclamosExternalInterfacesService reclmaosInterfaceService)
        {
            _reclmaosInterfaceService = reclmaosInterfaceService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
            View.Filterevent += ViewFilterevent;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            View.SelectedProduct = null;
        }

        void ViewFilterevent(object sender, EventArgs e)
        {
            LoadProductos(sender == null ? 0 : Convert.ToInt32(sender));
        }

        public void LoadTotalProductos()
        {
            try
            {
                var productos = _reclmaosInterfaceService.GetAllProductsByFilterCount(View.FilterText);
                View.TotalRegistrosPaginador = productos;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadProductos(int indexPage)
        {
            try
            {
                var productos = _reclmaosInterfaceService.GetAllProductsByFilter(View.FilterText, View.PageZise, indexPage);

                View.LoadProructos(productos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void SelectProduct(string codigoProducto)
        {
            try
            {
                var producto = _reclmaosInterfaceService.GetProductByCodigoProducto(codigoProducto);

                View.SelectedProduct = producto;
                View.LoadSelectedProducto(producto);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}