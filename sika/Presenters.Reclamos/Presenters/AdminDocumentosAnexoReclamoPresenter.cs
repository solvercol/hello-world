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
    public class AdminDocumentosAnexoReclamoPresenter : Presenter<IAdminDocumentosAnexoReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_DocumentosAnexoReclamoManagementServices _anexosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_LogReclamosManagementServices _log;

        public AdminDocumentosAnexoReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_ModuloReclamos_DocumentosAnexoReclamoManagementServices anexosService,
                                                ISfTBL_Admin_OptionListManagementServices optionListService,
                                                ISfTBL_ModuloReclamos_LogReclamosManagementServices log)
        {
            _reclamoService = reclamoService;
            _anexosService = anexosService;
            _optionListService = optionListService;
            _log = log;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInit();
            InitView();
        }

        public void LoadInit()
        {
            LoadCategorias();
            LoadAnexos();
        }

        void InitView()
        {
        }

        public void LoadCategorias()
        {
            var items = new List<DTO_ValueKey>();
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("CategoriaDocumentosAnexosReclamo", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    var splitValue = op.Value.Split('|');

                    foreach (var s in splitValue)
                    {
                        items.Add(new DTO_ValueKey() { Id = s, Value = s });
                    }
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadCategorias(items);
        }              
        
        public void LoadAnexos()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;
            try
            {
                var items = _anexosService.GetAnexosByReclamoCategoria(Convert.ToInt32(View.IdReclamo), View.Categoria);
                View.LoadAnexos(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public TBL_ModuloReclamos_DocumentosAnexoReclamo GetAnexoDoumento(Guid id)
        {
            try
            {
                var documento = _anexosService.GetById(id);

                return documento;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            return null;
        }

        public void SaveDocumento()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var contrato = _reclamoService.FindById(Convert.ToInt32(View.IdReclamo));
                var model = GetModel();

                _anexosService.Add(model);

                var log = GetLog();
                log.Descripcion = string.Format("El usuario [{0}], ha adicionado un documento para la categoría [{1}], con titulo [{2}]."
                                                , View.UserSession.Nombres
                                                , View.CategoriaDocumento
                                                , View.Titulo);
                _log.Add(log);

                contrato.ModifiedBy = View.UserSession.IdUser;
                contrato.ModifiedOn = log.CreateOn.GetValueOrDefault();

                _reclamoService.Modify(contrato);

                LoadAnexos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_DocumentosAnexoReclamo GetModel()
        {
            var model = new TBL_ModuloReclamos_DocumentosAnexoReclamo();

            model.IdDocumentoReclamo = Guid.NewGuid();
            model.IdReclamo = Convert.ToInt32(View.IdReclamo);
            model.Titulo = View.Titulo;
            model.Descripcion = View.Descripcion;
            model.Categoria = View.CategoriaDocumento;
            model.NombreArchivo = View.NombreArchivo;
            model.Archivo = View.ArchivoAnexo;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

        TBL_ModuloReclamos_LogReclamos GetLog()
        {
            var model = new TBL_ModuloReclamos_LogReclamos();

            model.IdLog = Guid.NewGuid();
            model.IdReclamo = Convert.ToInt32(View.IdReclamo);
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}