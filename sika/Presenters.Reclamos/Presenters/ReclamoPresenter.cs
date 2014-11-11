using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
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
        private readonly ISfTBL_Admin_EstadosProcesoManagementServices _estados;
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;

        public ReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                ISfTBL_Admin_OptionListManagementServices optionListService,
                                ISfTBL_ModuloReclamos_LogReclamosManagementServices logServices, 
                                ISfTBL_Admin_EstadosProcesoManagementServices estados, 
                                ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService)
        {
            _reclamoService = reclamoService;
            _solicitudService = solicitudService;
            _estados = estados;
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

                    View.IdIngenieroResponsable = reclamo.IdIngenieroResponsable;

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
                    TextoBotonDevolverEsatdo(reclamo);
                    ValidarBotonCancelar(reclamo);
                    ValidarBotonCambiarIngeniero(reclamo);
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

        private void TextoBotonDevolverEsatdo(TBL_ModuloReclamos_Reclamo oReclamo)
        {
            if (oReclamo == null) return;
            if (oReclamo.TBL_Admin_EstadosProceso == null) return;

            View.TextoBotonDevolucion = string.Empty;

            switch (oReclamo.TBL_Admin_EstadosProceso.Estado)
            {
                case "2":
                    if (View.UserSession.IsInRole("Administrador"))
                        View.TextoBotonDevolucion = "Devolver al Solicitante";
                    break;
                case "6":
                case "4":
                    if (View.UserSession.IsInRole("Administrador"))
                        View.TextoBotonDevolucion = "Devolver al Responsable";
                    break;
                default:
                    View.TextoBotonDevolucion = string.Empty;
                    break;
            }
        }

        private void ValidarBotonCancelar(TBL_ModuloReclamos_Reclamo oReclamo)
        {
            if (oReclamo == null) return;
            if (oReclamo.TBL_Admin_EstadosProceso == null) return;

            //Estamos utilizando el campo Programar Actividades para identificar si el estado permite
            //realizar la canclacion del reclamo.
            View.VerBotonRechazarReclamo = oReclamo.TBL_Admin_EstadosProceso.PermiteProgActividades.GetValueOrDefault() && View.UserSession.IsInRole("Administrador");
        }

        private void ValidarBotonCambiarIngeniero(TBL_ModuloReclamos_Reclamo oReclamo)
        {
            if (oReclamo == null) return;
            if (oReclamo.TBL_Admin_EstadosProceso == null) return;

            View.VerBotonCambiarIngeniero = oReclamo.TBL_Admin_EstadosProceso.Estado == "3" && View.UserSession.IsInRole("Administrador") && oReclamo.IdTipoReclamo == 1;
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

                if(oReclamo.IdAccionApc.HasValue)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format("El reclamo ya tiene asignado un plan de acción."), TypeError.Error));
                    return;
                }

                var oSolicitud = GetModel();

                _solicitudService.Add(oSolicitud);

                if (oSolicitud.IdSolucitudAPC > 0)
                {
                    oReclamo.IndicadorAPC = true;
                    oReclamo.IdAccionApc = oSolicitud.IdSolucitudAPC;
                }

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
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk,TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }


        TBL_ModuloAPC_Solicitud GetModel()
        {
            var estadoRegistrado = _estados.EstadoPorTipoModuloNombreEstado(ModulosAplicacion.AccionesPc,
                                                                            EstadosAplicacion.Registro);

            var model = _solicitudService.NewEntity();


            var op = _optionListService.ObtenerOpcionBykeyModuleId("ConsecitvoAPC", Convert.ToInt32(View.IdModule));
            if (op != null)
                model.Consecutivo = Convert.ToInt32(op.Value) + 1;

            model.IdSolicitante = View.UserSession.IdUser;
            model.IdResponsableActual = View.UserSession.IdUser;
            model.IdResponsableEjecucion = View.UserSession.IdUser;
            model.IdResponsableSeguimiento = View.UserSession.IdUser;
            model.FechaSolicitud = DateTime.Now;
            
            if (!string.IsNullOrEmpty(View.IdReclamo))
                model.IdReclamoCreacion = Convert.ToDecimal(View.IdReclamo);
            
            model.IdEstado = estadoRegistrado == null ? 11 : estadoRegistrado.IdEstado;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

    }
}