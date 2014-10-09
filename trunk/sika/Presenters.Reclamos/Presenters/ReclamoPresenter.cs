using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
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
        private ISfTBL_ModuloReclamos_LogReclamosManagementServices _logServices;

        public ReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                ISfTBL_Admin_OptionListManagementServices optionListService,
                                ISfTBL_ModuloReclamos_LogReclamosManagementServices logServices)
        {
            _reclamoService = reclamoService;
            _logServices = logServices;
            _seccionesServices = seccionesServices;
            _optionListService = optionListService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            LoadOptionListValue();
            LoadReclamo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadOptionListValue();
            LoadReclamo();
            CargarSecciones();
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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
                            View.Categoria = string.Format("{0} / {1}", reclamo.TBL_ModuloReclamos_CategoriasReclamo.Nombre, reclamo.TBL_ModuloReclamos_CategoriasReclamo.TBL_Admin_Usuarios.Nombres);
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        if (reclamo.IngenieroResponsable != null)
                            View.Responsable = reclamo.IngenieroResponsable.Nombres;
                        View.TotalCostoReclamo = string.Format("{0:0,0.0} {1}", reclamo.CostoTotal, View.MonedaLocal);
                        if (reclamo.IdCategoriaProducto.HasValue)
                            View.IdCategoria = reclamo.IdCategoriaProducto.ToString();
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
                        View.Categoria = string.Format("{0} / {1}", reclamo.TBL_ModuloReclamos_CategoriasReclamo.Nombre, reclamo.TBL_ModuloReclamos_CategoriasReclamo.TBL_Admin_Usuarios.Nombres);
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        if (reclamo.IngenieroResponsable != null)
                            View.Responsable = reclamo.IngenieroResponsable.Nombres;
                        View.TotalCostoReclamo = string.Format("{0:0,0.0} {1}", reclamo.CostoTotal, View.MonedaLocal);
                    }

                    ValidarBotonCambiarPrecio(reclamo);
                    ValidarBotonEditar(reclamo);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oReclamo"></param>
        private void ValidarBotonCambiarPrecio(TBL_ModuloReclamos_Reclamo oReclamo)
        {
            if(oReclamo == null)return;
            if (oReclamo.TBL_Admin_EstadosProceso == null)return;

            if (oReclamo.TBL_Admin_EstadosProceso.Descripcion == "En Proceso" && (!oReclamo.IndicadorAlt || !oReclamo.IndicadorAPC || !oReclamo.IndicadorSol))   
            {
                View.VerCrearAccion = View.UserSession.IdUser == oReclamo.IdResponsableActual;
            }
            else
            {
                View.VerCrearAccion = false;
            }
        }

        private void ValidarBotonEditar(TBL_ModuloReclamos_Reclamo oReclamo)
        {
            if (oReclamo == null) return;
            if (oReclamo.TBL_Admin_EstadosProceso == null) return;

            if (oReclamo.TBL_Admin_EstadosProceso.PermiteEdicionCampos.GetValueOrDefault())
            {
                View.VerBotonEdicion = View.UserSession.IdUser == oReclamo.IdResponsableActual;
            }
            else
            {
                View.VerBotonEdicion = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CrearAcciones()
        {
            if( string.IsNullOrEmpty( View.IdReclamo) ) return;

            try
            {
                var oReclamo = _reclamoService.GetReclamoById(Convert.ToInt32(View.IdReclamo));

                if(oReclamo == null)return;

                oReclamo.IndicadorAlt = true;
                oReclamo.IndicadorAPC = true;
                oReclamo.IndicadorSol = true;


                _reclamoService.Modify(oReclamo);

                 var texto = string.Format("El usuario {0} Creo creó el plan de acción.", View.UserSession.Nombres);

                _logServices.Add(new TBL_ModuloReclamos_LogReclamos
                                     {
                                         IdLog = Guid.NewGuid(),
                                         CreateBy = View.UserSession.IdUser,
                                         CreateOn = DateTime.Now,
                                         IdReclamo = Convert.ToDecimal(View.IdReclamo),
                                         IsActive = true,
                                         Descripcion = texto
                                     });
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}