using System;
using System.Linq;
using Application.Core;
using Presenters.Reclamos.IViews;
using Applications.MainModule.Admin.IServices;
using System.Reflection;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModules.Entities;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Reclamos.Presenters
{
    public class AdminCostosReclamoPresenter : Presenter<IAdminCostosReclamoView>
    {
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_CostosProductoManagementServices _costosReclamoService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly IReclamosExternalInterfacesService _reclmaosInterfaceService;

        public AdminCostosReclamoPresenter(ISfTBL_Admin_OptionListManagementServices optionListService,
                                           ISfTBL_ModuloReclamos_CostosProductoManagementServices costosReclamoService,
                                           ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                           IReclamosExternalInterfacesService reclmaosInterfaceService)
        {
            _optionListService = optionListService;
            _costosReclamoService = costosReclamoService;
            _reclamoService = reclamoService;
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
            LoadInitData();
        }

        void ViewFilterevent(object sender, EventArgs e)
        {
            LoadProductos(sender == null ? 0 : Convert.ToInt32(sender));
        }

        public void LoadInitData()
        {
            LoadParametrosGenerales();
            LoadReclamo();
            LoadCostosReclamo();
        }

        void LoadParametrosGenerales()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("PorcentajeDescuento", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.PorcentajeDescuento = Convert.ToDecimal(op.Value) / 100;
                }

                op = _optionListService.ObtenerOpcionBykeyModuleId("ValorDisposicion", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.ValorDisposicion = Convert.ToDecimal(op.Value);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadCostosReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var items = _costosReclamoService.GetCostosByReclamo(Convert.ToDecimal(View.IdReclamo));

                if (items.Any())
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        items[i].NoCosto = i + 1;
                    }                    
                }

                View.LoadCostoProductos(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    View.CostoProductoReclamo = reclamo.CostoReposicionProducto;
                    View.CostoTransporte = reclamo.CostoTransporteMateriales;
                    View.CostoDisposicion = reclamo.CostoDisposicion;
                    View.CostoPruebasCampo = reclamo.CostoPruebas;
                    View.CostoManoObra = reclamo.CostoManoObraDirecta;
                    View.OtrosCostos = reclamo.CostoExterno;
                    View.CostosAsistenciaTecnica = reclamo.CostoAsistenciaTecnica;
                    View.CostosAsistenciaRegional = reclamo.CostoAsistenciaTecnicaRegional;
                    View.CostoViajePersonas = reclamo.CostoViajes;
                    View.CostoEquiposHerramientas = reclamo.CostoArriendosEyH;
                    View.TotalCostosReclamo = reclamo.CostoTotal;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateCostosReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    reclamo.CostoReposicionProducto = View.TotalCostosReclamo;
                    reclamo.CostoTransporteMateriales = View.CostoTransporte;
                    reclamo.CostoDisposicion = View.CostoDisposicion;
                    reclamo.CostoPruebas = View.CostoPruebasCampo;
                    reclamo.CostoManoObraDirecta = View.CostoManoObra;
                    reclamo.CostoExterno = View.OtrosCostos;
                    reclamo.CostoAsistenciaTecnica = View.CostosAsistenciaTecnica;
                    reclamo.CostoAsistenciaTecnicaRegional = View.CostosAsistenciaRegional;
                    reclamo.CostoViajes = View.CostoViajePersonas;
                    reclamo.CostoArriendosEyH = View.CostoEquiposHerramientas;
                    reclamo.CostoTotal = View.TotalCostosReclamo;
                    reclamo.ModifiedBy = View.UserSession.IdUser;
                    reclamo.ModifiedOn = DateTime.Now;

                    _reclamoService.Modify(reclamo);

                    LoadCostosReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddCostosReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = GetModel();

                _costosReclamoService.Add(model);

                LoadCostosReclamo();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveCostosReclamo(decimal idCosto)
        {
            try
            {
                var reclamo = _costosReclamoService.GetCostosById(idCosto);

                _costosReclamoService.Remove(reclamo);

                LoadCostosReclamo();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
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
                View.PesoNetoProducto = string.Format("{0:0,0.0}", producto.PesoNeto);
                View.PrecioListaProducto = string.Format("{0:0,0.0}", producto.PrecioLista);
                View.CheckCostosProductoSelect();
                View.CheckCostosDisponibilidadProductoSelect();
                View.NombreProducto = producto.NombreProducto;                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_CostosProducto GetModel()
        {
            var model = new TBL_ModuloReclamos_CostosProducto();

            model.IdReclamo = Convert.ToInt32(View.IdReclamo);
            model.NumItem = 0;
            model.CodigoProducto = View.SelectedProduct.CodigoProducto;
            model.NombreProducto = View.SelectedProduct.Producto;
            model.PesoNeto = View.SelectedProduct.PesoNeto;
            model.PrecioLista = View.SelectedProduct.PrecioLista;
            model.Unidades = View.UnidadesProducto;
            model.CostoProducto = View.CostoProducto;
            model.Kilos = View.KilosProducto;
            model.UnidadesDisponibles = View.UnidadesDisponerProducto;
            model.CostoDisponible = View.CostoDisponibleProducto;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}