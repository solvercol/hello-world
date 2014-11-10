using System;
using System.Collections.Generic;
using System.Reflection;
using Applcations.MainModule.DocumentLibrary.IServices;
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
    public class AdminRecServicioT3Presenter : Presenter<IAdminRecServicioT3View>
    {
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;
        readonly IReclamosAdoService _reclamosAdoService;
        readonly ISfTBL_ModuloReclamos_LogReclamosManagementServices _logReclamoService;
        private readonly ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices _carpetasServices;


        public AdminRecServicioT3Presenter(ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                            ISfTBL_Admin_OptionListManagementServices optionListService,
                                            ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                            ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService,
                                            IReclamosAdoService reclamosAdoService,
                                            ISfTBL_ModuloReclamos_LogReclamosManagementServices logReclamoService, 
                                            ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices carpetasServices)
        {
            _usuariosService = usuariosService;
            _carpetasServices = carpetasServices;
            _optionListService = optionListService;
            _reclamoService = reclamoService;
            _categoriasReclamoService = categoriasReclamoService;
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
            LoadCategoria();
            InitViewValues();
            LoadUnidadZonaAsesor();

            if (!string.IsNullOrEmpty(View.IdReclamo))
                LoadReclamo();
        }

        void InitViewValues()
        {
            View.IdAsesor = View.UserSession.IdUser.ToString();
            View.IdAtendidoPor = View.UserSession.IdUser.ToString();            
            View.FechaCompromiso = DateTime.Now;
            View.FechaPedido = DateTime.Now;
        }

        void LoadCategoria()
        {
            try
            {
                var categoria = _categoriasReclamoService.FindById(Convert.ToInt32(View.IdCategoriaReclamo));

                if (categoria != null)
                {
                    View.CategoriaReclamo = categoria.Nombre;
                    View.IdCategoriaReclamo = categoria.IdCategoriaReclamo.ToString();
                    View.Area = categoria.Area;
                    View.IdResponsableCategoriaReclamo = categoria.IdResponsable;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
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
            if (string.IsNullOrEmpty(View.IdAsesor)) return;

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

                //Creando l carpeta Documentacion para el reclamo actual
                _carpetasServices.SaveFolder(string.Empty, string.Empty, "Documentación", model.IdReclamo.ToString(), View.UserSession.IdUser.ToString());

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

                model.IdCategoriaReclamo = Convert.ToInt32(View.IdCategoriaReclamo);
                model.Area = View.Area;
                model.Planta = View.Planta;
                model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);
                model.NumPFR = View.PedidoRemisionFactura;
                model.NumDiarioInventario = View.DiarioInventario;
                model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
                model.TipoContacto = View.TipoContacto;
                model.CodigoCliente = View.SelectedCliente.CodigoCliente;
                model.NombreCliente = View.SelectedCliente.NombreCliente;
                model.UnidadZona = View.UnidadZona;
                model.Contacto = View.NombreContacto;
                model.EmailContacto = View.EmailContacto;
                model.FechaPedido = View.FechaPedido;
                model.FechaCompromiso = View.FechaCompromiso;
                model.DescripcionProblema = View.DescripcionProblema;
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

                View.IdCategoriaReclamo = model.IdCategoriaReclamo.ToString();
                View.Area = model.Area;
                View.Planta = model.Planta;
                View.IdAsesor = model.IdAsesoradoPor.ToString();
                View.PedidoRemisionFactura = model.NumPFR;
                View.DiarioInventario = model.NumDiarioInventario;
                View.IdAtendidoPor = model.IdAtendidoPor.ToString();
                View.TipoContacto = model.TipoContacto;
                View.SelectedCliente.CodigoCliente = model.CodigoCliente;
                View.UnidadZona = model.UnidadZona;
                View.NombreContacto = model.Contacto;
                View.EmailContacto = model.EmailContacto;
                View.FechaPedido = model.FechaPedido.GetValueOrDefault();
                View.FechaCompromiso = model.FechaCompromiso.GetValueOrDefault();
                View.DescripcionProblema = model.DescripcionProblema;

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
            model.IdCategoriaReclamo = Convert.ToInt32(View.IdCategoriaReclamo);
            model.Area = View.Area;
            model.Planta = View.Planta;
            model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);
            model.NumPFR = View.PedidoRemisionFactura;
            model.NumDiarioInventario = View.DiarioInventario;
            model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
            model.TipoContacto = View.TipoContacto;
            model.CodigoCliente = View.SelectedCliente.CodigoCliente;
            model.NombreCliente = View.SelectedCliente.NombreCliente;
            model.UnidadZona = View.UnidadZona;
            model.Contacto = View.NombreContacto;
            model.EmailContacto = View.EmailContacto;
            model.FechaPedido = View.FechaPedido;
            model.FechaCompromiso = View.FechaCompromiso;
            model.DescripcionProblema = View.DescripcionProblema;
            model.IdResponsableActual = View.UserSession.IdUser;
            model.IdIngenieroResponsable = View.IdResponsableCategoriaReclamo;
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