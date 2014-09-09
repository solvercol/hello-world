using System;
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
    public class AdminRecServicioT1Presenter : Presenter<IAdminRecServicioT1View>
    {
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;
        readonly IReclamosAdoService _reclamosAdoService;
        readonly ISfTBL_ModuloReclamos_LogReclamosManagementServices _logReclamoService;

        public AdminRecServicioT1Presenter(ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                            ISfTBL_Admin_OptionListManagementServices optionListService,
                                            ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                            ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService,
                                            IReclamosAdoService reclamosAdoService,
                                            ISfTBL_ModuloReclamos_LogReclamosManagementServices logReclamoService)
        {
            _usuariosService = usuariosService;
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
            View.DiarioInventario = 0;
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

                model.IdCategoriaReclamo = Convert.ToInt32(View.IdCategoriaReclamo);
                model.Area = View.Area;
                model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);
                model.NumPFR = View.PedidoRemisionFactura;
                model.NumDiarioInventario = View.DiarioInventario.ToString();
                model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
                model.TipoContrato = View.TipoContacto;
                model.CodigoCliente = View.SelectedCliente.CodigoCliente;
                model.UnidadZona = View.UnidadZona;
                model.Contacto = View.NombreContacto;
                model.EmailContacto = View.EmailContacto;
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
                View.IdAsesor = model.IdAsesoradoPor.ToString();
                View.PedidoRemisionFactura = model.NumPFR;
                View.DiarioInventario = Convert.ToInt32(model.NumDiarioInventario);
                View.IdAtendidoPor = model.IdAtendidoPor.ToString();
                View.TipoContacto = model.TipoContrato;
                View.SelectedCliente.CodigoCliente = model.CodigoCliente;
                View.UnidadZona = model.UnidadZona;
                View.NombreContacto = model.Contacto;
                View.EmailContacto = model.EmailContacto;
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
            model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);
            model.NumPFR = View.PedidoRemisionFactura;
            model.NumDiarioInventario = View.DiarioInventario.ToString();
            model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
            model.TipoContrato = View.TipoContacto;
            model.CodigoCliente = View.SelectedCliente.CodigoCliente;
            model.UnidadZona = View.UnidadZona;
            model.Contacto = View.NombreContacto;
            model.EmailContacto = View.EmailContacto;
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