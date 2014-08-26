using System;
using System.Linq;
using Application.Core;
using Presenters.Reclamos.IViews;
using Applications.MainModule.Admin.IServices;
using System.Reflection;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.Presenters
{
    public class AdminCostosReclamoPresenter : Presenter<IAdminCostosReclamoView>
    {
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_CostosProductoManagementServices _costosReclamoService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;

        public AdminCostosReclamoPresenter(ISfTBL_Admin_OptionListManagementServices optionListService,
                                           ISfTBL_ModuloReclamos_CostosProductoManagementServices costosReclamoService,
                                           ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService)
        {
            _optionListService = optionListService;
            _costosReclamoService = costosReclamoService;
            _reclamoService = reclamoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInitData();
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

        void LoadCostosReclamo()
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