using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Applications.MainModule.Admin.IServices;

namespace Presenters.Reclamos.Presenters
{
    public class AdminComentarioRespuestaReclamoPresenter : Presenter<IAdminComentarioRespuestaReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices _comentariosService;
        readonly ISfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices _anexosService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;

        public AdminComentarioRespuestaReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices comentariosService,
                                                ISfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices anexosService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService)
        {
            _reclamoService = reclamoService;
            _comentariosService = comentariosService;
            _anexosService = anexosService;
            _usuariosService = usuariosService;
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
            LoadUsuarioAsignacion();
            LoadComentarioRespuestaReclamo();
        }

        void LoadUsuarioAsignacion()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadDestinatarios(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadComentarioRespuestaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var model = _comentariosService.GetById(Convert.ToDecimal(View.IdComentario));

                if (model != null)
                {
                    View.Asunto = model.Asunto;
                    View.Mensaje = model.Comentario;
                    View.Destinatario = model.TBL_Admin_Usuarios2.Nombres;
                    View.FechaComentario = model.CreateOn;
                    View.IdUsuarioDestino = model.CreateBy.ToString();

                    View.EnableEdit(false);
                    LoadComentarioRelacionados();
                    LoadArhchivosAdjuntos();
                    LoadReclamo(model.IdReclamo);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadComentarioRelacionados()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var items = _comentariosService.GetByIdComentarioRelacionado(Convert.ToDecimal(View.IdComentario));

                View.LoadComentariosRelacionados(items);

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
                    View.IdReclamo = string.Format("{0}", idReclamo);

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
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddComentarioRelacionado()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var model = GetModel();

                _comentariosService.Add(model);

                LoadComentarioRespuestaReclamo();
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
                var archivo = new TBL_ModuloReclamos_AnexosComentarioRespuesta();
                archivo.IdComentarioRespuesta = Convert.ToDecimal(View.IdComentario);
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
                    archivo.Id = string.Format("{0}", model.IdAnexoComentarioRespuesta);
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
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var anexos = _anexosService.GetByComentarioId(Convert.ToDecimal(View.IdComentario));

                if (anexos.Any())
                {

                    var archivosAdjuntos = new List<DTO_ValueKey>();
                    foreach (var anexo in anexos)
                    {
                        var archivo = new DTO_ValueKey();
                        archivo.Id = string.Format("{0}", anexo.IdAnexoComentarioRespuesta);
                        archivo.Value = anexo.NombreArchivo;
                        archivo.ComplexValue = anexo.Archivo;

                        archivosAdjuntos.Add(archivo);
                    }

                    View.LoadArchivosAdjuntos(archivosAdjuntos);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_ComentariosRespuesta GetModel()
        {
            var model = new TBL_ModuloReclamos_ComentariosRespuesta();

            model.IdReclamo = Convert.ToInt32(View.IdReclamo);
            model.Asunto = View.Asunto;
            model.Comentario = View.NuevoComentario;
            model.IdComentarioRelacionado = Convert.ToDecimal(View.IdComentario);
            model.IdUsuarioDestino = Convert.ToInt32(View.IdUsuarioDestino);
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}