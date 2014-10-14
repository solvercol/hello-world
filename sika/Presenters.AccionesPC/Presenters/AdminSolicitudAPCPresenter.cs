using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Resources;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminSolicitudAPCPresenter : Presenter<IAdminSolicitudAPCView>
    {
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;
        readonly ISfTBL_ModuloAPC_AnexosSolicitudManagementServices _anexosService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloAPC_AreasManagementServices _areasService;

        public AdminSolicitudAPCPresenter(ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                            ISfTBL_Admin_OptionListManagementServices optionListService,
                                            ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService,
                                            ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                            ISfTBL_ModuloAPC_AreasManagementServices areasService,
                                            ISfTBL_ModuloAPC_AnexosSolicitudManagementServices anexosService)
        {
            _usuariosService = usuariosService;
            _optionListService = optionListService;
            _solicitudService = solicitudService;
            _reclamoService = reclamoService;
            _areasService = areasService;
            _anexosService = anexosService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            InitViewValues();
            LoadConsecutivoReclamo();
            LoadAreasAccion();            

            if (!string.IsNullOrEmpty(View.IdSolicitud))
                LoadSolicitudAPC();

            if (string.IsNullOrEmpty(View.IdSolicitud) && !string.IsNullOrEmpty(View.IdReclamo))
                LoadReclamo(Convert.ToDecimal(View.IdReclamo));
        }

        void InitViewValues()
        {
            View.Gerente = string.Empty;
            View.DescripcionAccion = string.Empty;
            View.FechaDesde = DateTime.Now;
            View.FechaHasta = DateTime.Now.AddMonths(1);
            View.ShowInfoReclamo = false;
            View.ArchivosAdjuntos = new List<DTO_ValueKey>();
        }

        void LoadConsecutivoReclamo()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("ConsecitvoAPC", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.ConsecutivoSolicitud = op.Value;
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
                var op = _optionListService.ObtenerOpcionBykeyModuleId("ConsecitvoAPC", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    op.Value = (Convert.ToInt32(View.ConsecutivoSolicitud) + 1).ToString();
                    _optionListService.Modify(op);
                    View.ConsecutivoSolicitud = op.Value;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadAreasAccion()
        {
            try
            {
                var items = _areasService.GetEntitiesWithGerente();

                if (items.Any())
                    items = items.OrderBy(x => x.Nombre).ToList();

                View.LoadAreaAcion(items);
                LoadProcesos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadSolicitudAPC()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud))
                return;

            try
            {
                var item = _solicitudService.GetById(Convert.ToDecimal(View.IdSolicitud));

                if (item != null)
                {
                    View.IdAreaAccion = item.IdAreaAccion;
                    LoadProcesos();
                    View.Proceso = item.Proceso;
                    View.TipoAccion = item.TipoAccion;
                    View.DescripcionAccion = item.DescripcionAccion;
                    View.FechaDesde = item.FechaDesde;
                    View.FechaHasta = item.FechaHasta;

                    if (item.IdReclamoCreacion.HasValue)
                        LoadReclamo(item.IdReclamoCreacion.GetValueOrDefault());

                    View.ShowArchivosAdjuntos = false;
                    //LoadArhchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadProcesos()
        {
            try
            {
                var area = _areasService.GetById(View.IdAreaAccion);

                var procesos = new List<DTO_ValueKey>();

                if (area != null)
                {
                    var spltProc = area.Procesos.Split('|');

                    foreach (var proc in spltProc)
                    {
                        procesos.Add(new DTO_ValueKey() { Id = proc, Value = proc });
                    }

                    View.IdGerente = area.IdGerente;
                    View.Gerente = area.TBL_Admin_Usuarios1.Nombres;
                }

                View.LoadProcesos(procesos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void SaveSolicitudAPC()
        {
            try
            {
                var model = GetModel();
                
                _solicitudService.Add(model);

                IncrementConsecutivoReclamo();

                foreach (var adjunto in View.ArchivosAdjuntos)
                {
                    var archivo = new TBL_ModuloAPC_AnexosSolicitud();
                    archivo.IdSolicitudAPC = Convert.ToDecimal(model.IdSolucitudAPC);
                    archivo.NombreArchivo = adjunto.Value;
                    archivo.Archivo = (byte[])adjunto.ComplexValue;
                    archivo.CreateBy = View.UserSession.IdUser;
                    archivo.CreateOn = DateTime.Now;
                    archivo.ModifiedBy = View.UserSession.IdUser;
                    archivo.ModifiedOn = DateTime.Now;

                    _anexosService.Add(archivo);
                }

                InvokeMessageBox(new MessageBoxEventArgs(string.Format("Datos Guardados Con Exito."), TypeError.Ok));
                

                View.GoToSolicitudView(string.Format("{0}", model.IdSolucitudAPC));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadReclamo(decimal idReclamo)
        {
            try
            {
                var reclamo = _reclamoService.GetReclamoWithNavById(idReclamo);

                if (reclamo != null)
                {
                    var dtoProducto = (Dto_Producto)reclamo.DtoProducto;
                    var dtoCliente = (Dto_Cliente)reclamo.DtoCliente;

                    View.TipoReclamo = reclamo.TBL_ModuloReclamos_TipoReclamo.Nombre;
                    View.NumeroReclamo = reclamo.NumeroReclamo;

                    if (View.TipoReclamo == "Producto")
                    {
                        View.TitleReclamo = dtoProducto.NombreProducto;
                        View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        View.Unidad = reclamo.UnidadZona;
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        View.Asesor = reclamo.AsesoradoPor.Nombres;
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
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        if (reclamo.AsesoradoPor != null)
                            View.Asesor = reclamo.AsesoradoPor.Nombres;
                    }

                    View.ShowInfoReclamo = true;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddArchivoAdjunto()
        {
            try
            {
                var archivo = new TBL_ModuloAPC_AnexosSolicitud();
                archivo.IdSolicitudAPC = Convert.ToDecimal(View.IdSolicitud);
                archivo.NombreArchivo = View.NombreArchivoAdjunto;
                archivo.Archivo = View.ArchivoAdjunto;
                archivo.CreateBy = View.UserSession.IdUser;
                archivo.CreateOn = DateTime.Now;
                archivo.ModifiedBy = View.UserSession.IdUser;
                archivo.ModifiedOn = DateTime.Now;

                _anexosService.Add(archivo);

                LoadArhchivosAdjuntos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    _anexosService.Remove(model);

                    LoadArhchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void DownloadArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    var archivo = new DTO_ValueKey();
                    archivo.ComplexValue = model.Archivo;
                    archivo.Id = string.Format("{0}", model.IdAnexoSolicitud);
                    archivo.Value = model.NombreArchivo;

                    View.DescargarArchivo(archivo);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadArhchivosAdjuntos()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var anexos = _anexosService.GetByIdSolicitud(Convert.ToDecimal(View.IdSolicitud));
                var archivosAdjuntos = new List<DTO_ValueKey>();
                if (anexos.Any())
                {
                    foreach (var anexo in anexos)
                    {
                        var archivo = new DTO_ValueKey();
                        archivo.Id = string.Format("{0}", anexo.IdAnexoSolicitud);
                        archivo.Value = anexo.NombreArchivo;
                        archivo.ComplexValue = anexo.Archivo;

                        archivosAdjuntos.Add(archivo);
                    }
                }
                View.LoadArchivosAdjuntos(archivosAdjuntos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloAPC_Solicitud GetModel()
        {
            var model = new TBL_ModuloAPC_Solicitud();

            model.Consecutivo = Convert.ToInt32(View.ConsecutivoSolicitud) + 1;
            model.IdSolicitante = View.UserSession.IdUser;
            model.IdResponsableActual = View.UserSession.IdUser;
            model.IdResponsableEjecucion = View.UserSession.IdUser;
            model.IdResponsableSeguimiento = View.UserSession.IdUser;
            model.TipoAccion = View.TipoAccion;
            model.FechaSolicitud = DateTime.Now;
            model.Codigo = string.Format("APC{0}{1}", DateTime.Now.Year, model.Consecutivo.ToString().PadLeft(5, '0'));
            
            if (!string.IsNullOrEmpty(View.IdReclamo))
                model.IdReclamoCreacion = Convert.ToDecimal(View.IdReclamo);

            model.IdAreaAccion = View.IdAreaAccion;
            model.Proceso = View.Proceso;
            model.IdGerente = View.IdGerente;
            model.DescripcionAccion = View.DescripcionAccion;
            model.FechaDesde = View.FechaDesde;
            model.FechaHasta = View.FechaHasta;
            model.IdEstado = 1; // Registrado - Borrador
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}