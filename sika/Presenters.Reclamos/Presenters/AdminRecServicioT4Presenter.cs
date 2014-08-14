using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using System.Collections.Generic;

namespace Presenters.Reclamos.Presenters
{
    public class AdminRecServicioT4Presenter : Presenter<IAdminRecServicioT4View>
    {
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;

        public AdminRecServicioT4Presenter(ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                            ISfTBL_Admin_OptionListManagementServices optionListService,
                                            ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                            ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService)
        {
            _usuariosService = usuariosService;
            _optionListService = optionListService;
            _reclamoService = reclamoService;
            _categoriasReclamoService = categoriasReclamoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadAsesores();
            LoadPlantas();
            LoadMensajesReclamoConf();
            LoadConsecutivoReclamo();
            LoadCategoria();
            InitViewValues();
        }

        void InitViewValues()
        {
            View.IdAsesor = View.UserSession.IdUser.ToString();
            View.IdAtendidoPor = View.UserSession.IdUser.ToString();
            View.NoRecordatorios = 0;            
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

                    // Cargando SubCategorias
                    var dtos = new List<DTO_ValueKey>();
                    var subCategorias = categoria.SubCategoria.Split('|');

                    foreach (var p in subCategorias)
                    {
                        dtos.Add(new DTO_ValueKey() { Id = p, Value = p });
                    }

                    View.LoadSubCategoriaReclamo(dtos);
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
                var asesores = _usuariosService.FindBySpec(true);
                View.LoadAsesores(asesores);
                View.LoadAtendidoPor(asesores);
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

                View.GoToReclamoView(string.Format("{0}", model.IdReclamo));
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
            model.SubCategoria = View.SubCategoriaReclamo;
            model.Area = View.Area;
            model.Planta = View.Planta;
            model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);            
            model.NumeroDeVeces = View.NoRecordatorios;
            model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
            model.TipoContrato = View.TipoContacto;
            model.RespuestaInmediata = View.RespuestaInmediata;
            model.CodigoCliente = View.SelectedCliente.CodigoCliente;
            model.UnidadZona = View.UnidadZona;
            model.Contacto = View.NombreContacto;
            model.EmailContacto = View.EmailContacto;
            model.NombreObra = View.NombreObra;
            model.PropietarioObra = View.PropietarioObra;
            model.EmailPropietarioObra = View.EmailPropietario;
            model.EmailQuienAplica = View.EmailQuienAplica;
            model.DescripcionProblema = View.DescripcionProblema;
            model.DiagnosticoPrevio = View.Diagnostico;
            model.ConclusionesPrevias = View.ConclusionesPrevias;
            model.ObservacionesSolucion = View.Solucion;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}