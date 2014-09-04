using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Domain.MainModule.Reclamos.DTO;

namespace Presenters.Reclamos.Presenters
{
    public class ReclamoPresenter : Presenter<IReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_Admin_SeccionesManagementServices _seccionesServices;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;


        public ReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                ISfTBL_Admin_OptionListManagementServices optionListService)
        {
            _reclamoService = reclamoService;
            _seccionesServices = seccionesServices;
            _optionListService = optionListService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadOptionListValue();
            LoadReclamo();
            CargarSecciones();
        }

        void LoadOptionListValue()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("ModenaLocal", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.MonedaLocal = op.Value;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadReclamo()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdReclamo)) return;

                var reclamo = _reclamoService.GetReclamoWithNavById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    var dtoProducto = (Dto_Producto)reclamo.DtoProducto;
                    var dtoCliente = (Dto_Cliente)reclamo.DtoCliente;

                    View.TipoReclamo = reclamo.TBL_ModuloReclamos_TipoReclamo.Nombre;
                    View.NumeroReclamo = reclamo.NumeroReclamo;

                    // Load Nav
                    View.IdCategoriaReclamo = reclamo.IdCategoriaReclamo.ToString();
                    if (reclamo.TBL_ModuloReclamos_CategoriasReclamo != null)
                        View.IdGrupoInformacion = reclamo.TBL_ModuloReclamos_CategoriasReclamo.GrupoInformacion.ToString();

                    if (View.TipoReclamo == "Producto")
                    {
                        View.TitleReclamo = dtoProducto.NombreProducto;
                        View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        View.Unidad = reclamo.UnidadZona;
                        if (reclamo.TBL_ModuloReclamos_CategoriasReclamo != null)
                            View.Area = string.Format("{0} / {1}", reclamo.TBL_ModuloReclamos_CategoriasReclamo.Area, reclamo.TBL_ModuloReclamos_CategoriasReclamo.TBL_Admin_Usuarios.Nombres);
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        View.Asesor = reclamo.AsesoradoPor.Nombres;
                        View.TotalCostoReclamo = string.Format("{0:0,0.0} {1}", reclamo.CostoTotal, View.MonedaLocal);
                    }
                    else
                    {
                        View.TitleReclamo = reclamo.TBL_ModuloReclamos_CategoriasReclamo.Nombre;
                        if (dtoCliente != null && !string.IsNullOrEmpty(dtoCliente.NombreCliente))
                        {
                            View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        }
                        else
                        {
                            View.TitleReclamoFrom = string.Format("{0} / {1}", reclamo.NombreReclama, reclamo.ProcedimientoInternoAfectado);
                        }
                        View.Unidad = reclamo.UnidadZona;
                        View.Area = string.Format("{0} / {1}", reclamo.TBL_ModuloReclamos_CategoriasReclamo.Area, reclamo.TBL_ModuloReclamos_CategoriasReclamo.TBL_Admin_Usuarios.Nombres);
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        if (reclamo.AsesoradoPor != null)
                            View.Asesor = reclamo.AsesoradoPor.Nombres;
                        View.TotalCostoReclamo = string.Format("{0:0,0.0} {1}", reclamo.CostoTotal, View.MonedaLocal);
                    }
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void CargarSecciones()
        {
            try
            {
                var secciones = _seccionesServices.ListadoSeccionesPorModulo(Convert.ToInt32(View.IdModule));
                View.LoadSecciones(secciones);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}