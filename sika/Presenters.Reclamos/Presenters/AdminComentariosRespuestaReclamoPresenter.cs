using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AdminComentariosRespuestaReclamoPresenter : Presenter<IAdminComentariosRespuestaReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices _comentariosRespuestaService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;

        public AdminComentariosRespuestaReclamoPresenter(ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices comentariosRespuestaService,
                                                         ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                         ISfTBL_Admin_UsuariosManagementServices usuariosService)
        {
            _comentariosRespuestaService = comentariosRespuestaService;
            _reclamoService = reclamoService;
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
            LoadComentariosReclamo();
            LoadUsuarioAsignacion();
        }

        void LoadComentariosReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var items = _comentariosRespuestaService.GetByIdReclamo(Convert.ToDecimal(View.IdReclamo));

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

        public void AddComentarioReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = GetModel();

                _comentariosRespuestaService.Add(model);

                LoadComentariosReclamo();
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
                    View.IsNewComentario = false;
                    View.ShowAdminComentarioWindow(true);
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

            model.IdReclamo = Convert.ToDecimal(View.IdReclamo);
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