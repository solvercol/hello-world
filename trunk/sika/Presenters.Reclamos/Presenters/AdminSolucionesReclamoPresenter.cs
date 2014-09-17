using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Domain.MainModule.Reclamos.Enum;

namespace Presenters.Reclamos.Presenters
{
    public class AdminSolucionesReclamoPresenter : Presenter<IAdminSolucionesReclamoView>
    {
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_SolucionesManagementServices _solucionesReclamo;
        readonly ISfTBL_ModuloReclamos_AnexosSolucionManagementServices _anexosService;

        public AdminSolucionesReclamoPresenter(ISfTBL_Admin_OptionListManagementServices optionListService,
                                               ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                               ISfTBL_ModuloReclamos_SolucionesManagementServices solucionesReclamo,
                                               ISfTBL_ModuloReclamos_AnexosSolucionManagementServices anexosService)
        {
            _optionListService = optionListService;
            _reclamoService = reclamoService;
            _solucionesReclamo = solucionesReclamo;
            _anexosService = anexosService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInitData();
        }

        public void LoadInitData()
        {
            LoadReclamo();
            LoadDepartamentosSolucion();
            LoadSolucionesReclamo();            
        }

        void LoadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    View.CanEditSoluciones = (reclamo.IdEstado == EstadosReclamo.EnProceso && reclamo.IdResponsableActual == View.UserSession.IdUser)
                                         || View.UserSession.IsInRole("Administrador");
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }


        void LoadSolucionesReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var items = _solucionesReclamo.GetByIdReclamo(Convert.ToDecimal(View.IdReclamo));

                View.LoadSolucionesReclamo(items);

                CheckIndicadorSolucionReclamo(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void CheckIndicadorSolucionReclamo(List<TBL_ModuloReclamos_Soluciones> items)
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));

                reclamo.IndicadorSol = items.Any();

                _reclamoService.Modify(reclamo);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadDepartamentosSolucion()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("AreaSolucion", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    var departamentos = op.Value.Split('|');
                    var items = new List<DTO_ValueKey>();

                    foreach (var d in departamentos)
                    {
                        items.Add(new DTO_ValueKey() { Id = d, Value = d });
                    }

                    View.LoadDepartamentos(items);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddSolucionReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = GetModel();

                _solucionesReclamo.Add(model);

                if (View.ArchivosAdjuntos.Any())
                {
                    foreach (var archivo in View.ArchivosAdjuntos)
                    {
                        var anexo = new TBL_ModuloReclamos_AnexosSolucion();
                        anexo.IdSolucion = model.IdSolucion;
                        anexo.NombreArchivo = archivo.Value;
                        anexo.Archivo = (byte[])archivo.ComplexValue;
                        anexo.IsActive = true;
                        anexo.CreateBy = View.UserSession.IdUser;
                        anexo.CreateOn = DateTime.Now;
                        anexo.ModifiedBy = View.UserSession.IdUser;
                        anexo.ModifiedOn = DateTime.Now;

                        _anexosService.Add(anexo);
                    }
                }

                LoadSolucionesReclamo();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateSolucionReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo) || string.IsNullOrEmpty(View.IdSelectedSolucion)) return;

            try
            {
                var model = _solucionesReclamo.GetById(Convert.ToDecimal(View.IdSelectedSolucion));

                if (model != null)
                {
                    model.Departamento = View.Departamento;
                    model.Observaciones = View.Observaciones;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;

                    _solucionesReclamo.Modify(model);

                    LoadSolucionesReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadSolucionReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedSolucion)) return;

            try
            {
                var model = _solucionesReclamo.GetById(Convert.ToDecimal(View.IdSelectedSolucion));

                if (model != null)
                {
                    View.Departamento = model.Departamento;
                    View.Observaciones = model.Observaciones;
                    View.ArchivosAdjuntos = new List<DTO_ValueKey>();
                    if (model.TBL_ModuloReclamos_AnexosSolucion.Any())
                    {
                        foreach (var anexo in model.TBL_ModuloReclamos_AnexosSolucion)
                        {
                            var archivo = new DTO_ValueKey();
                            archivo.Id = string.Format("{0}", anexo.IdAnexoSolucion);
                            archivo.Value = anexo.NombreArchivo;
                            archivo.ComplexValue = anexo.Archivo;
                            View.ArchivosAdjuntos.Add(archivo);
                        }
                    }
                    View.LoadArchivosAdjuntos(View.ArchivosAdjuntos);
                    View.IsNewSolucion = false;
                    View.ShowAdminSolucionWindow(true);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveSolucionReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedSolucion)) return;

            try
            {
                var model = _solucionesReclamo.GetById(Convert.ToDecimal(View.IdSelectedSolucion));

                if (model != null)
                {
                    _solucionesReclamo.Remove(model);

                    LoadSolucionesReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_Soluciones GetModel()
        {
            var model = new TBL_ModuloReclamos_Soluciones();

            model.IdReclamo = Convert.ToInt32(View.IdReclamo);
            model.Departamento = View.Departamento;
            model.Observaciones = View.Observaciones;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}