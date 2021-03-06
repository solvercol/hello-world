﻿using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Application.MainModule.Reclamos.Util;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using System.Collections.Generic;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModule.Reclamos.Enum;

namespace Presenters.Reclamos.Presenters
{
    public class AdminComentariosRespuestaReclamoPresenter : Presenter<IAdminComentariosRespuestaReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices _comentariosRespuestaService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices _anexosService;
        readonly IReclamosAdoService _reclamoAdoService;
        private readonly ISendEmail _senMailServices;


        public AdminComentariosRespuestaReclamoPresenter(ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices comentariosRespuestaService,
                                                         ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                         ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                         ISfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices anexosService,
                                                         IReclamosAdoService reclamoAdoService, ISendEmail senMailServices)
        {
            _comentariosRespuestaService = comentariosRespuestaService;
            _senMailServices = senMailServices;
            _reclamoService = reclamoService;
            _usuariosService = usuariosService;
            _anexosService = anexosService;
            _reclamoAdoService = reclamoAdoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            LoadInfoReclamo();
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInitData();
        }

        public void LoadInitData()
        {
            LoadComentariosReclamo();
            LoadUsuarioAsignacion();
            LoadUsuarioCopia();
            LoadInfoReclamo();
        }

        void LoadComentariosReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var items = _comentariosRespuestaService.GetByIdReclamo(Convert.ToDecimal(View.IdReclamo));

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

                View.LoadComentariosReclamo(items);
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

        void LoadInfoReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;
            try
            {
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));

                View.MailContactoTmp = reclamo.EmailContacto;
                View.CanSendMailToCLient = reclamo.IdEstado == EstadosReclamo.RespuestaAlCliente;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddComentarioReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

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
                        var anexo = new TBL_ModuloReclamos_AnexosComentarioRespuesta();
                        anexo.IdComentarioRespuesta = model.IdComentario;
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

                try
                {
                    _senMailServices.EnviarCorreoelectronicoAutorReclamo(model.IdComentario, View.UserSession);
                }
                catch (Exception ex)
                {
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                }

                LoadComentariosReclamo();
                View.ShowAdminComentarioWindow(false);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddComentarioReclamoCliente()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = new TBL_ModuloReclamos_ComentariosRespuesta();

                model.IdReclamo = Convert.ToInt32(View.IdReclamo);
                model.Asunto = View.Asunto;
                model.Comentario = View.Comentario;
                model.IdUsuarioDestino = View.UserSession.IdUser;
                model.EsComentarioCliente = true;
                model.EmailDestinatarioCliente = View.MailContacto;
                model.IsActive = true;
                model.CreateBy = View.UserSession.IdUser;
                model.CreateOn = DateTime.Now;
                model.ModifiedBy = View.UserSession.IdUser;
                model.ModifiedOn = DateTime.Now;

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
                        var anexo = new TBL_ModuloReclamos_AnexosComentarioRespuesta();
                        anexo.IdComentarioRespuesta = model.IdComentario;
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


                try
                {
                    _senMailServices.EnviarCorreoelectronicoRespuestaCliente(model.IdComentario, View.UserSession);
                }
                catch (Exception ex)
                {
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                }

                LoadComentariosReclamo();

                View.ShowAdminComentarioWindow(false);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateComentarioReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo) || string.IsNullOrEmpty(View.IdSelectedComentario)) return;

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

                    LoadComentariosReclamo();

                    View.ShowAdminComentarioWindow(false);
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
                    if (model.TBL_ModuloReclamos_AnexosComentarioRespuesta.Any())
                    {
                        foreach (var anexo in model.TBL_ModuloReclamos_AnexosComentarioRespuesta)
                        {
                            var archivo = new DTO_ValueKey();
                            archivo.Id = string.Format("{0}", anexo.IdAnexoComentarioRespuesta);
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

                    LoadComentariosReclamo();
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