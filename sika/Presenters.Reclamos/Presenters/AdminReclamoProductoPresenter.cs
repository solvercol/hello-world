using System;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AdminReclamoProductoPresenter : Presenter<IAdminReclamoProductoView>
    {
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;

        public AdminReclamoProductoPresenter(ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                            ISfTBL_Admin_OptionListManagementServices optionListService,
                                            ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService)
        {
            _usuariosService = usuariosService;
            _optionListService = optionListService;
            _reclamoService = reclamoService;
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
            InitViewValues();
        }

        void InitViewValues()
        {
            View.IdAsesor = View.UserSession.IdUser.ToString();
            View.IdAtendidoPor = View.UserSession.IdUser.ToString();
            View.CantidadVendidaUnidad = 0;
            View.CantidadReclamadaUnidad = 0;
            View.FechaVenta = DateTime.Now;
            View.NoRecordatorios = 0;
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
            model.IdAsesoradoPor = Convert.ToInt32(View.IdAsesor);
            model.Planta = View.Planta;
            model.CodigoProducto = View.SelectedProduct.CodigoProducto;
            model.CantidadVendida = View.CantidadVendidaUnidad;
            model.CantidadReclamada = View.CantidadReclamadaUnidad;
            model.Aplicado = View.Aplicado;
            model.FechaVenta = View.FechaVenta;
            model.NumeroDeVeces = View.NoRecordatorios;
            model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
            model.TipoContrato = View.TipoContacto;
            model.RespuestaInmediata = View.RespuestaInmediata;
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
            model.UsoDescripcion = View.DescripcionProducto;
            model.Lote = View.NumeroLote;
            model.MuestraDisponible = View.MuestraDisponible;
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