using System;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Resources;

namespace Presenters.Reclamos.Presenters
{
    public class AdminReclamoProductoPresenter : Presenter<IAdminReclamoProductoView>
    {
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly IReclamosAdoService _reclamosAdoService;
        readonly ISfTBL_ModuloReclamos_LogReclamosManagementServices _logReclamoService;

        public AdminReclamoProductoPresenter(ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                            ISfTBL_Admin_OptionListManagementServices optionListService,
                                            ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                            IReclamosAdoService reclamosAdoService,
                                            ISfTBL_ModuloReclamos_LogReclamosManagementServices logReclamoService)
        {
            _usuariosService = usuariosService;
            _optionListService = optionListService;
            _reclamoService = reclamoService;
            _reclamosAdoService = reclamosAdoService;
            _logReclamoService = logReclamoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadAsesores();
            LoadUsuariosAtendidos();
            LoadPlantas();
            LoadMensajesReclamoConf();
            LoadConsecutivoReclamo();
            InitViewValues();
            LoadUnidadZonaAsesor();

            if (!string.IsNullOrEmpty(View.IdReclamo))
                LoadReclamo();
        }

        void InitViewValues()
        {
            View.IdAsesor = View.UserSession.IdUser.ToString();
            View.IdAtendidoPor = View.UserSession.IdUser.ToString();
            View.CantidadVendidaUnidad = 0;
            View.CantidadReclamadaUnidad = 0;
            View.FechaVenta = DateTime.Now;
        }

        void LoadConsecutivoReclamo()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("ConsecutivoReclamo", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.ConsecutivoReclamo = op.Value;
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

                if (op != null)
                {
                    op.Value = (Convert.ToInt32(View.ConsecutivoReclamo) + 1).ToString();
                    _optionListService.Modify(op);
                    View.ConsecutivoReclamo = op.Value;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadAsesores()
        {
            try
            {
                var asesores = _reclamosAdoService.GetAllAsesores();
                View.LoadAsesores(asesores);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuariosAtendidos()
        {
            try
            {
                var asesores = _usuariosService.FindBySpec(true);

                View.LoadAtendidoPor(asesores);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadUnidadZonaAsesor()
        {
            if(string.IsNullOrEmpty(View.IdAsesor)) return;

            try
            {
                var asesor = _reclamosAdoService.GetByIdAsesor(Convert.ToInt32(View.IdAsesor));

                View.UnidadZona = string.Format("{0}-{1}", asesor.Unidad, asesor.Zona);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
        
        public void LoadPlantas()
        {
            try
            {
                var plantasOP = _optionListService.ObtenerOpcionBykeyModuleId("Plantas", Convert.ToInt32(View.IdModule));

                if (plantasOP != null)
                {
                    var dtos = new List<DTO_ValueKey>();
                    var plantas = plantasOP.Value.Split('|');
                    
                    foreach (var p in plantas)
                    {
                        dtos.Add(new DTO_ValueKey() { Id = p, Value = p });
                    }

                    View.LoadPlantas(dtos);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadMensajesReclamoConf()
        {
            try
            {
                var optionList = _optionListService.ObtenerOpcionBykeyModuleId("MensajeDescripcionProblema", Convert.ToInt32(View.IdModule));

                if (optionList != null)
                {
                    View.MensajeDescripcionProblema = optionList.Value;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void SaveReclamo()
        {
            try
            {
                var model = GetModel();
                
                _reclamoService.Add(model);
                IncrementConsecutivoReclamo();
                InvokeMessageBox(new MessageBoxEventArgs(string.Format("Datos Guardados Con Exito."), TypeError.Ok));

                var log = new TBL_ModuloReclamos_LogReclamos();
                log.IdLog = Guid.NewGuid();
                log.IdReclamo = model.IdReclamo;
                log.Descripcion = string.Format(Messages.SaveReclamo, View.UserSession.Nombres, DateTime.Now);
                log.IsActive = true;
                log.CreateBy = View.UserSession.IdUser;
                log.CreateOn = DateTime.Now;

                _logReclamoService.Add(log);

                View.GoToReclamoView(string.Format("{0}", model.IdReclamo));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = _reclamoService.GetReclamoWithNavById(Convert.ToDecimal(View.IdReclamo));

                model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);
                model.Planta = View.Planta;
                model.CodigoProducto = View.SelectedProduct.CodigoProducto;
                model.IdCategoriaProducto = View.SelectedProduct.IdCategoriaProducto;
                model.CantidadVendida = View.CantidadVendidaUnidad;
                model.CantidadReclamada = View.CantidadReclamadaUnidad;
                model.Aplicado = View.Aplicado;
                model.FechaVenta = View.FechaVenta;
                model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
                model.TipoContacto = View.TipoContacto;
                model.CodigoCliente = View.SelectedCliente.CodigoCliente;
                model.UnidadZona = View.UnidadZona;
                model.Contacto = View.NombreContacto;
                model.EmailContacto = View.EmailContacto;
                model.NombreObra = View.NombreObra;
                model.AplicadoPor = View.AplicadoPor;
                model.PropietarioObra = View.PropietarioObra;
                model.EmailPropietarioObra = View.EmailPropietario;
                model.EmailQuienAplica = View.EmailQuienAplica;
                model.AspectoEnvase = View.AspectoExteriorEnvase;
                model.AspectoProducto = View.AspectoProducto;
                model.DescripcionAspectoEnvase = View.DescripcionAspectoEnvase;
                model.DescripcionAspectoProducto = View.DescripcionAspectoProducto;
                model.Lote = View.NumeroLote;
                model.Lote2 = View.NumeroLote2;
                model.Lote3 = View.NumeroLote3;
                model.MuestraDisponible = View.MuestraDisponible;
                model.DescripcionProblema = View.DescripcionProblema;
                model.DiagnosticoPrevio = View.Diagnostico;
                model.ConclusionesPrevias = View.ConclusionesPrevias;
                model.ObservacionesSolucion = View.Solucion;
                model.ModifiedBy = View.UserSession.IdUser;
                model.ModifiedOn = DateTime.Now;

                _reclamoService.Modify(model);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format("Datos Guardados Con Exito."), TypeError.Ok));

                var log = new TBL_ModuloReclamos_LogReclamos();
                log.IdLog = Guid.NewGuid();
                log.IdReclamo = model.IdReclamo;
                log.Descripcion = string.Format(Messages.UpdateReclamo, View.UserSession.Nombres, DateTime.Now);
                log.IsActive = true;
                log.CreateBy = View.UserSession.IdUser;
                log.CreateOn = DateTime.Now;

                _logReclamoService.Add(log);

                View.GoToReclamoView(string.Format("{0}", model.IdReclamo));
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
                var model = _reclamoService.GetReclamoWithNavById(Convert.ToDecimal(View.IdReclamo));

                View.IdTipoReclamo = model.IdTipoReclamo;
                View.IdAsesor = model.IdAsesoradoPor.ToString();
                View.Planta = model.Planta;
                View.SelectedProduct.CodigoProducto = model.CodigoProducto;
                View.CantidadVendidaUnidad = model.CantidadVendida.GetValueOrDefault();
                View.CantidadReclamadaUnidad = model.CantidadReclamada.GetValueOrDefault();
                View.Aplicado = model.Aplicado.GetValueOrDefault();
                View.FechaVenta = model.FechaVenta.GetValueOrDefault();
                View.IdAtendidoPor = model.IdAtendidoPor.GetValueOrDefault().ToString();
                View.TipoContacto = model.TipoContacto;
                View.SelectedCliente.CodigoCliente = model.CodigoCliente;
                View.UnidadZona = model.UnidadZona;
                View.NombreContacto = model.Contacto;
                View.EmailContacto = model.EmailContacto;
                View.NombreObra = model.NombreObra;
                View.AplicadoPor = model.AplicadoPor;
                View.PropietarioObra = model.PropietarioObra;
                View.EmailPropietario = model.EmailPropietarioObra;
                View.EmailQuienAplica = model.EmailQuienAplica;
                View.AspectoExteriorEnvase = model.AspectoEnvase;
                View.AspectoProducto = model.AspectoProducto;
                View.DescripcionAspectoEnvase = model.DescripcionAspectoEnvase;
                View.DescripcionAspectoProducto = model.DescripcionAspectoProducto;
                View.NumeroLote = model.Lote;
                View.NumeroLote2 = model.Lote2;
                View.NumeroLote3 = model.Lote3;
                View.MuestraDisponible = model.MuestraDisponible.GetValueOrDefault();
                View.DescripcionProblema = model.DescripcionProblema;
                View.Diagnostico = model.DiagnosticoPrevio;
                View.ConclusionesPrevias = model.ConclusionesPrevias;
                View.Solucion = model.ObservacionesSolucion;
                View.ProblemaSolucionado = model.ProblemaSolucionado.GetValueOrDefault();

                if (model.DtoProducto != null)
                    View.SetSelectedProduct((Dto_Producto)model.DtoProducto);

                if (model.DtoCliente != null)
                    View.SetSelectedClient((Dto_Cliente)model.DtoCliente);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_Reclamo GetModel()
        {
            var model = new TBL_ModuloReclamos_Reclamo();

            model.Consecutivo = Convert.ToInt32(View.ConsecutivoReclamo) + 1;
            model.IdSolicitante = View.UserSession.IdUser;
            model.IdTipoReclamo = View.IdTipoReclamo;
            model.FechaReclamo = DateTime.Now;
            model.NumeroReclamo = string.Format("{0}-{1}", DateTime.Now.Year, model.Consecutivo.ToString().PadLeft(5, '0'));
            model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);
            model.Planta = View.Planta;
            model.CodigoProducto = View.SelectedProduct.CodigoProducto;
            model.NombreProducto = View.SelectedProduct.NombreProducto;
            model.IdCategoriaProducto = View.SelectedProduct.IdCategoriaProducto;
            model.CantidadVendida = View.CantidadVendidaUnidad;
            model.CantidadReclamada = View.CantidadReclamadaUnidad;
            model.Aplicado = View.Aplicado;
            model.FechaVenta = View.FechaVenta;
            model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
            model.TipoContacto = View.TipoContacto;
            model.CodigoCliente = View.SelectedCliente.CodigoCliente;
            model.NombreCliente = View.SelectedCliente.NombreCliente;
            model.UnidadZona = View.UnidadZona;
            model.Contacto = View.NombreContacto;
            model.EmailContacto = View.EmailContacto;
            model.NombreObra = View.NombreObra;
            model.AplicadoPor = View.AplicadoPor;
            model.PropietarioObra = View.PropietarioObra;
            model.EmailPropietarioObra = View.EmailPropietario;
            model.EmailQuienAplica = View.EmailQuienAplica;
            model.AspectoEnvase = View.AspectoExteriorEnvase;
            model.AspectoProducto = View.AspectoProducto;
            model.DescripcionAspectoEnvase = View.DescripcionAspectoEnvase;
            model.DescripcionAspectoProducto = View.DescripcionAspectoProducto;
            model.Lote = View.NumeroLote;
            model.Lote2 = View.NumeroLote2;
            model.Lote3 = View.NumeroLote3;
            model.MuestraDisponible = View.MuestraDisponible;
            model.DescripcionProblema = View.DescripcionProblema;
            model.DiagnosticoPrevio = View.Diagnostico;
            model.ConclusionesPrevias = View.ConclusionesPrevias;
            model.ObservacionesSolucion = View.Solucion;
            model.ProblemaSolucionado = View.ProblemaSolucionado;
            model.IdResponsableActual = View.UserSession.IdUser;
            model.IdEstado = 1; // Registrado
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}