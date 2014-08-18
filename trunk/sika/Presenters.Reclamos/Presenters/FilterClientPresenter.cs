using System;
using Application.Core;
using Presenters.Reclamos.IViews;
using Application.MainModule.SqlServices.IServices;
using System.Reflection;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Presenters.Reclamos.Presenters
{
    public class FilterClientPresenter : Presenter<IFilterClientView>
    {
        readonly IReclamosExternalInterfacesService _reclmaosInterfaceService;

        public FilterClientPresenter(IReclamosExternalInterfacesService reclmaosInterfaceService)
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
            if (string.IsNullOrEmpty(View.IdReclamo))
                View.SelectedClient = null;
        }

        void ViewFilterevent(object sender, EventArgs e)
        {
            LoadClientes(sender == null ? 0 : Convert.ToInt32(sender));
        }

        public void LoadTotalClientes()
        {
            try
            {
                var clientes = _reclmaosInterfaceService.GetAllClientsByFilterCount(View.FilterText);
                View.TotalRegistrosPaginador = clientes;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadClientes(int indexPage)
        {
            try
            {
                var clientes = _reclmaosInterfaceService.GetAllClientsByFilter(View.FilterText, View.PageZise, indexPage);

                View.LoadClientes(clientes);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void SelectCliente(string codigoCliente)
        {
            try
            {
                var cliente = _reclmaosInterfaceService.GetClientByCodigoCliente(codigoCliente);

                View.SelectedClient = cliente;
                View.LoadSelectedClient(cliente);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}