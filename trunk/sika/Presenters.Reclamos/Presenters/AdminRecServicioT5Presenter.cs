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
    public class AdminRecServicioT5Presenter : Presenter<IAdminRecServicioT5View>
    {
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;

        public AdminRecServicioT5Presenter(ISfTBL_Admin_UsuariosManagementServices usuariosService,
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
            LoadAreaIncumplimiento();
            LoadProcedimientoAfectado();
            LoadMensajesReclamoConf();
            LoadConsecutivoReclamo();
            LoadCategoria();
            InitViewValues();

            if (!string.IsNullOrEmpty(View.IdReclamo))
                LoadReclamo();
        }

        void InitViewValues()
        {            
            View.IdAtendidoPor = View.UserSession.IdUser.ToString();
            View.QuienReclama = View.UserSession.IdUser.ToString();
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
                View.LoadQuienReclama(asesores);
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

        public void LoadAreaIncumplimiento()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("Areas", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    var dtos = new List<DTO_ValueKey>();
                    var plantas = op.Value.Split('|');

                    foreach (var p in plantas)
                    {
                        dtos.Add(new DTO_ValueKey() { Id = p, Value = p });
                    }

                    View.LoadAreaIncumpleProcedimiento(dtos);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadProcedimientoAfectado()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("ProcedimientosInternos", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    var dtos = new List<DTO_ValueKey>();
                    var plantas = op.Value.Split('|');

                    foreach (var p in plantas)
                    {
                        dtos.Add(new DTO_ValueKey() { Id = p, Value = p });
                    }

                    View.LoadProcedimientoInternoAfectado(dtos);
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

        public void UpdateReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = _reclamoService.GetReclamoWithNavById(Convert.ToDecimal(View.IdReclamo));

                model.IdCategoriaReclamo = Convert.ToInt32(View.IdCategoriaReclamo);
                model.Area = View.Area;
                model.Planta = View.Planta;
                model.NumeroDeVeces = View.NoRecordatorios;
                model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
                model.UnidadZona = View.UnidadZona;
                model.NombreReclama = View.QuienReclama;
                model.AreaIncumple = View.AreaIncumpleProcedimiento;
                model.ProcedimientoInternoAfectado = View.ProcedimientoInternoAfectado;
                model.DescripcionProblema = View.DescripcionProblema;
                model.ModifiedBy = View.UserSession.IdUser;
                model.ModifiedOn = DateTime.Now;

                _reclamoService.Modify(model);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format("Datos Guardados Con Exito."), TypeError.Ok));

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
                View.NoRecordatorios = model.NumeroDeVeces;
                View.IdAtendidoPor = model.IdAtendidoPor.ToString();
                View.UnidadZona = model.UnidadZona;
                View.QuienReclama = model.NombreReclama;
                View.AreaIncumpleProcedimiento = model.AreaIncumple;
                View.ProcedimientoInternoAfectado = model.ProcedimientoInternoAfectado;
                View.DescripcionProblema = model.DescripcionProblema;
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
            model.NumeroDeVeces = View.NoRecordatorios;
            model.IdAtendidoPor = Convert.ToInt32(View.IdAtendidoPor);
            model.UnidadZona = View.UnidadZona;            
            model.NombreReclama  = View.QuienReclama;
            model.AreaIncumple = View.AreaIncumpleProcedimiento;
            model.ProcedimientoInternoAfectado = View.ProcedimientoInternoAfectado;
            model.DescripcionProblema = View.DescripcionProblema;
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