using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Application.MainModule.AccionesPC.Util;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;
using Applications.MainModule.Admin.IServices;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminComentarioRespuestaAPCPresenter : Presenter<IAdminComentarioRespuestaAPCView>
    {
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;
        readonly ISfTBL_ModuloAPC_ComentariosRespuestaManagementServices _comentariosService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloAPC_AnexosComentarioRespuestaManagementServices _anexosService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        private readonly ISendEmail _sendMail;

        public AdminComentarioRespuestaAPCPresenter(ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService,
                                                ISfTBL_ModuloAPC_ComentariosRespuestaManagementServices comentariosService,
                                                ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_ModuloAPC_AnexosComentarioRespuestaManagementServices anexosService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService, ISendEmail sendMail)
        {
            _solicitudService = solicitudService;
            _sendMail = sendMail;
            _comentariosService = comentariosService;
            _reclamoService = reclamoService;
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
            LoadSolicitud();
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
                    View.LogInfoMessage = string.Format("Creado por {0} en {1:dd/MM/yyyy hh:mm ss tt}. Modificado por {2} en {3:dd/MM/yyyy hh:mm ss tt}.",
                                                model.TBL_Admin_Usuarios1.Nombres, model.CreateOn,
                                                model.TBL_Admin_Usuarios2.Nombres, model.ModifiedOn);
                    var usuariosCopia = new List<DTO_ValueKey>();
                    if (model.TBL_Admin_Usuarios3.Any())
                    {
                        foreach (var itm in model.TBL_Admin_Usuarios3)
                        {
                            var usuarioCopia = new DTO_ValueKey() { Id = itm.IdUser.ToString(), Value = itm.Nombres };
                            usuariosCopia.Add(usuarioCopia);
                        }
                    }
                    View.LoadUsuariosCopia(usuariosCopia);

                    View.EnableEdit(false);
                    LoadComentarioRelacionados();
                    LoadArhchivosAdjuntos();
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

    

        public void AddComentarioRelacionado()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var model = GetModel();

                _comentariosService.Add(model);

                LoadComentarioRespuestaReclamo();

                try
                {
                    _sendMail.EnviarCorreoelectronicoComentarios(model.IdComentario, View.UserSession);
                }
                catch (Exception ex)
                {
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                }

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
                var archivo = new TBL_ModuloAPC_AnexosComentarioRespuesta();
                archivo.IdComentario = Convert.ToDecimal(View.IdComentario);
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
                    archivo.Id = string.Format("{0}", model.IdAnexoComentario);
                    archivo.Value = model.NombreArchivo;

                    View.DescargarArchivo(archivo);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var item = _solicitudService.GetWithNavById(Convert.ToDecimal(View.IdSolicitud));

                if (item != null)
                {
                    View.TipoAccion = item.TipoAccion;
                    View.CodSolicitud = item.Codigo;
                    View.Area = item.TBL_ModuloAPC_Areas.Nombre;
                    View.GerenteArea = item.TBL_Admin_Usuarios4.Nombres;
                    View.ResponsableAccion = item.TBL_Admin_Usuarios8.Nombres;
                    View.FechaInicio = string.Format("{0:dd/MM/yyyy}", item.FechaDesde);
                    View.FechaFinal = string.Format("{0:dd/MM/yyyy}", item.FechaHasta);


                    View.ShowInfoReclamo = false;

                    if (item.IdReclamoCreacion.HasValue)
                        LoadReclamo(item.IdReclamoCreacion.GetValueOrDefault());
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
                var archivosAdjuntos = new List<DTO_ValueKey>();
                if (anexos.Any())
                {
                    foreach (var anexo in anexos)
                    {
                        var archivo = new DTO_ValueKey();
                        archivo.Id = string.Format("{0}", anexo.IdAnexoComentario);
                        archivo.Value = anexo.NombreArchivo;
                        archivo.ComplexValue = anexo.Archivo;
                        archivo.CreateBy = anexo.CreateBy;

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

        TBL_ModuloAPC_ComentariosRespuesta GetModel()
        {
            var model = new TBL_ModuloAPC_ComentariosRespuesta();

            model.IdSolicitudAPC= Convert.ToInt32(View.IdSolicitud);
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
