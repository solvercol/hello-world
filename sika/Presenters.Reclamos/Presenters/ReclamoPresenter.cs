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
using Presenters.Reclamos.Resources;

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
        private readonly ISfTBL_Admin_ModulosManagementServices _modulosServices;

        public ReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                ISfTBL_Admin_OptionListManagementServices optionListService,
                                ISfTBL_ModuloReclamos_LogReclamosManagementServices logServices, 
                                ISfTBL_Admin_EstadosProcesoManagementServices estados, 
                                ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService, 
                                ISfTBL_Admin_ModulosManagementServices modulosServices)
        {
            _reclamoService = reclamoService;
            _modulosServices = modulosServices;
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
            GetModuleApc();
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
                    ValidarBotonAsociarPlanAccion(reclamo);
                    View.ConfigurarHiperlinkAcciones = reclamo.IdAccionApc.HasValue? reclamo.IdAccionApc.ToString() : string.Empty;
                    View.TextHyperlinkAcciones = reclamo.IdAccionApc.HasValue
                        ? (string.IsNullOrEmpty(reclamo.TBL_ModuloAPC_Solicitud1.Codigo) ? "Acción Correctiva Preventiva" : reclamo.TBL_ModuloAPC_Solicitud1.Codigo)
                                                     : string.Empty;

                    View.LogInfoMessage = string.Format("Creado por {0} en {1:dd/MM/yyyy hh:mm ss tt}. Modificado por {2} en {3:dd/MM/yyyy hh:mm ss tt}.",
                                                      reclamo.TBL_Admin_Usuarios3.Nombres, reclamo.CreateOn,
                                                      reclamo.TBL_Admin_Usuarios5.Nombres, reclamo.ModifiedOn);
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

        public void CopiarReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    var clone = GetClone(reclamo);

                    reclamo.ModifiedBy = View.UserSession.IdUser;
                    reclamo.ModifiedOn = DateTime.Now;
                    reclamo.CampoRelacion = View.CampoRelacionado;

                    _reclamoService.Add(clone);
                    IncrementConsecutivoReclamo();

                    var log = new TBL_ModuloReclamos_LogReclamos();
                    log.IdLog = Guid.NewGuid();
                    log.IdReclamo = clone.IdReclamo;
                    log.Descripcion = string.Format(Messages.SaveReclamo, View.UserSession.Nombres, DateTime.Now);
                    log.IsActive = true;
                    log.CreateBy = View.UserSession.IdUser;
                    log.CreateOn = DateTime.Now;

                    _logServices.Add(log);

                    _reclamoService.Modify(reclamo);

                    log = new TBL_ModuloReclamos_LogReclamos();
                    log.IdLog = Guid.NewGuid();
                    log.IdReclamo = reclamo.IdReclamo;
                    log.Descripcion = string.Format("EL usuario {0}, ha copiado el reclamo a las {1:dd/MM/yyyy hh:mm ss tt}", View.UserSession.Nombres, DateTime.Now);
                    log.IsActive = true;
                    log.CreateBy = View.UserSession.IdUser;
                    log.CreateOn = DateTime.Now;

                    _logServices.Add(log);

                    LoadReclamo();

                    InvokeMessageBox(new MessageBoxEventArgs(string.Format("Se ha copiado el reclamo satisfactoriamente."), TypeError.Ok));
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void IncrementConsecutivoReclamo()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("ConsecutivoReclamo", Convert.ToInt32(View.IdModule));
                var consecutivo = string.Empty;

                if (op != null)
                {
                    consecutivo = op.Value;
                    op.Value = (Convert.ToInt32(consecutivo) + 1).ToString();
                    _optionListService.Modify(op);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_Reclamo GetClone(TBL_ModuloReclamos_Reclamo reclamo)
        {
            var op = _optionListService.ObtenerOpcionBykeyModuleId("ConsecutivoReclamo", Convert.ToInt32(View.IdModule));
            var consecutivo = string.Empty;

            if (op != null)
            {
                consecutivo = op.Value;
            }

            var clone = new TBL_ModuloReclamos_Reclamo();
            clone.IdDocumentoLotus = reclamo.IdDocumentoLotus;
            clone.IdDocumentoLotusRelacionado = reclamo.IdDocumentoLotusRelacionado;
            clone.Consecutivo = Convert.ToInt32(consecutivo);
            clone.IdSolicitante = View.UserSession.IdUser;
            clone.IdTipoReclamo = reclamo.IdTipoReclamo;
            clone.FechaReclamo = DateTime.Now;
            clone.IdReclamoRelacionado = reclamo.IdReclamo;
            clone.CampoRelacion = View.CampoRelacionado;
            reclamo.CampoRelacion = View.CampoRelacionado;
            clone.NumeroReclamo = string.Format("{0}-{1}", DateTime.Now.Year, clone.Consecutivo.ToString().PadLeft(5, '0'));
            clone.IdAsesoradoPor = reclamo.IdAsesoradoPor;
            clone.Planta = reclamo.Planta;
            clone.CodigoProducto = reclamo.CodigoProducto;
            clone.CantidadVendida = reclamo.CantidadVendida;
            clone.CantidadReclamada = reclamo.CantidadReclamada;
            clone.Aplicado = reclamo.Aplicado;
            clone.FechaAplicacion = reclamo.FechaAplicacion;
            clone.FechaVenta = reclamo.FechaVenta;
            clone.IdAtendidoPor = reclamo.IdAtendidoPor;
            clone.TipoContacto = reclamo.TipoContacto;
            clone.CodigoCliente = reclamo.CodigoCliente;
            clone.UnidadZona = reclamo.UnidadZona;
            clone.Contacto = reclamo.Contacto;
            clone.EmailContacto = reclamo.EmailContacto;
            clone.NombreObra = reclamo.NombreObra;
            clone.AplicadoPor = reclamo.AplicadoPor;
            clone.EmailQuienAplica = reclamo.EmailQuienAplica;
            clone.PropietarioObra = reclamo.PropietarioObra;
            clone.EmailPropietarioObra = reclamo.EmailPropietarioObra;
            clone.Contratista = reclamo.Contratista;
            clone.EmailContratista = reclamo.EmailContratista;
            clone.AspectoEnvase = reclamo.AspectoEnvase;
            clone.AspectoProducto = reclamo.AspectoProducto;
            clone.DescripcionAspectoEnvase = reclamo.DescripcionAspectoEnvase;
            clone.DescripcionAspectoProducto = reclamo.DescripcionAspectoProducto;
            clone.Lote = reclamo.Lote;
            clone.Lote2 = reclamo.Lote2;
            clone.Lote3 = reclamo.Lote3;
            clone.MuestraDisponible = reclamo.MuestraDisponible;
            clone.IdCategoriaReclamo = reclamo.IdCategoriaReclamo;
            clone.Area = reclamo.Area;
            clone.SubCategoria = reclamo.SubCategoria;
            clone.NumPFR = reclamo.NumPFR;
            clone.NumDiarioInventario = reclamo.NumDiarioInventario;
            clone.FechaPedido = reclamo.FechaPedido;
            clone.FechaCompromiso = reclamo.FechaCompromiso;
            clone.FechaRealEntrega = reclamo.FechaRealEntrega;
            clone.DiasDiferencia = reclamo.DiasDiferencia;
            clone.NombreReclama = reclamo.NombreReclama;
            clone.AreaIncumple = reclamo.AreaIncumple;
            clone.ProcedimientoInternoAfectado = reclamo.ProcedimientoInternoAfectado;
            clone.DescripcionProblema = reclamo.DescripcionProblema;
            clone.DiagnosticoPrevio = reclamo.DiagnosticoPrevio;
            clone.ConclusionesPrevias = reclamo.ConclusionesPrevias;
            clone.ProblemaSolucionado = reclamo.ProblemaSolucionado;
            clone.ObservacionesSolucion = reclamo.ObservacionesSolucion;
            clone.IdResponsableActual = View.UserSession.IdUser;
            clone.IdCategoriaProducto = reclamo.IdCategoriaProducto;
            clone.NombreCliente = reclamo.NombreCliente;
            clone.NombreProducto = reclamo.NombreProducto;
            clone.IdAccionApc = reclamo.IdAccionApc;
            clone.IdEstado = 1; // Registrado
            clone.IsActive = true;
            clone.CreateBy = View.UserSession.IdUser;
            clone.CreateOn = DateTime.Now;
            clone.ModifiedBy = View.UserSession.IdUser;
            clone.ModifiedOn = DateTime.Now;

            return clone;
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

        private void ValidarBotonAsociarPlanAccion(TBL_ModuloReclamos_Reclamo reclamo)
        {
            View.MostrarBotonAsociacinPlanAccion = !reclamo.IdAccionApc.HasValue &&
                                                   View.UserSession.IdUser == reclamo.IdResponsableActual &&
                                                   reclamo.TBL_Admin_EstadosProceso.Descripcion == "En Proceso";
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

                LoadReclamo();
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


            var op = _optionListService.ObtenerOpcionBykey("ConsecitvoAPC");
            if (op != null)
            {
                model.Consecutivo = Convert.ToInt32(op.Value) + 1;

                op.Value = (Convert.ToInt32(op.Value) + 1).ToString();

                _optionListService.Modify(op);
            }

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

        private void GetModuleApc()
        {
            try
            {
                var oModule = _modulosServices.GetModuleByName("AccionesPC");
                if(oModule == null)return;
                View.IdModuloApc = oModule.IdModulo.ToString();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}