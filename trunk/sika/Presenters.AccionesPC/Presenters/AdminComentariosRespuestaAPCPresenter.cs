using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;
using System.Collections.Generic;
using Application.MainModule.SqlServices.IServices;
//using Domain.MainModule.AccionesPC.Enum;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminComentariosRespuestaAPCPresenter : Presenter<IAdminComentariosRespuestaAPCView>
    {
        readonly ISfTBL_ModuloAPC_ComentariosRespuestaManagementServices _comentariosRespuestaService;
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_ModuloAPC_AnexosComentarioRespuestaManagementServices _anexosService;
        readonly IReclamosAdoService _reclamoAdoService;

        public AdminComentariosRespuestaAPCPresenter(ISfTBL_ModuloAPC_ComentariosRespuestaManagementServices comentariosRespuestaService,
                                                         ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService,
                                                         ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                         ISfTBL_ModuloAPC_AnexosComentarioRespuestaManagementServices anexosService,
                                                         IReclamosAdoService reclamoAdoService)
        {
            _comentariosRespuestaService = comentariosRespuestaService;
            _solicitudService = solicitudService;
            _usuariosService = usuariosService;
            _anexosService = anexosService;
            _reclamoAdoService = reclamoAdoService;
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
            LoadComentariosSolicitud();
            LoadUsuarioAsignacion();
            LoadUsuarioCopia();
        }

        void LoadComentariosSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var items = _comentariosRespuestaService.GetByIdSolicitud(Convert.ToDecimal(View.IdSolicitud));

                if (items != null && items.Any())
                {
                    var totalItems = items;
                    items = items.Where(x => x.IdComentarioRelacionado == null).ToList();
                    foreach (var itm in items)
                    {
                        var children = totalItems.Where(x => x.IdComentarioRelacionado == itm.IdComentario);
                        if (children != null && children.Any())
                            itm.ComentariosAsociados = children.ToList();
                    }
                }
                View.LoadComentariosSolicitud(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
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

        void LoadUsuarioCopia()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadUsuarioCopia(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddComentarioReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var model = GetModel();

                _comentariosRespuestaService.Add(model);

                if (View.UsuariosCopia.Any())
                {
                    foreach (var item in View.UsuariosCopia)
                    {
                        _reclamoAdoService.InsertUsuarioCopiaComentario(item.Id, string.Format("{0}", model.IdComentario));
                    }
                }

                if (View.ArchivosAdjuntos.Any())
                {
                    foreach (var archivo in View.ArchivosAdjuntos)
                    {
                        var anexo = new TBL_ModuloAPC_AnexosComentarioRespuesta();
                        anexo.IdComentario = model.IdComentario;
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

                LoadComentariosSolicitud();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateComentarioReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud) || string.IsNullOrEmpty(View.IdSelectedComentario)) return;

            try
            {
                var model = _comentariosRespuestaService.GetById(Convert.ToDecimal(View.IdSelectedComentario));

                if (model != null)
                {
                    model.Asunto = View.Asunto;
                    model.Comentario = View.Comentario;
                    model.IdUsuarioDestino = Convert.ToInt32(View.IdUsuarioDestino);
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;

                    _comentariosRespuestaService.Modify(model);

                    LoadComentariosSolicitud();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadComentarioReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedComentario)) return;

            try
            {
                var model = _comentariosRespuestaService.GetById(Convert.ToDecimal(View.IdSelectedComentario));

                if (model != null)
                {
                    View.Asunto = model.Asunto;
                    View.Comentario = model.Comentario;
                    View.IdUsuarioDestino = model.IdUsuarioDestino.ToString();
                    View.ArchivosAdjuntos = new List<DTO_ValueKey>();
                    if (model.TBL_ModuloAPC_AnexosComentarioRespuesta.Any())
                    {
                        foreach (var anexo in model.TBL_ModuloAPC_AnexosComentarioRespuesta)
                        {
                            var archivo = new DTO_ValueKey();
                            archivo.Id = string.Format("{0}", anexo.IdAnexoComentario);
                            archivo.Value = anexo.NombreArchivo;
                            archivo.ComplexValue = anexo.Archivo;
                            View.ArchivosAdjuntos.Add(archivo);
                        }
                    }
                    View.LoadArchivosAdjuntos(View.ArchivosAdjuntos);
                    View.IsNewComentario = false;
                    View.ShowAdminComentarioWindow(true);
                    View.EnableEdit(false);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveComentarioReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedComentario)) return;

            try
            {
                var model = _comentariosRespuestaService.GetById(Convert.ToDecimal(View.IdSelectedComentario));

                if (model != null)
                {
                    _comentariosRespuestaService.Remove(model);

                    LoadComentariosSolicitud();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloAPC_ComentariosRespuesta GetModel()
        {

            var model = new TBL_ModuloAPC_ComentariosRespuesta();

            model.IdSolicitudAPC = Convert.ToInt32(View.IdSolicitud);
            model.Asunto = View.Asunto;
            model.Comentario = View.Comentario;
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
