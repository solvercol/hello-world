using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Application.MainModule.Documentos.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Documentos.IViews;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Documentos.Presenters
{
    public class VistaDocumentosPublicadosPresenter
        : Presenter<IVistaDocumentosPublicadosView>
    {
        private List<TBL_ModuloDocumentos_Estados> Estados { get; set; }
        private TBL_ModuloDocumentos_Estados EstadoPublicado { get; set; }
        private readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices _documentoServices;
        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices _categoriaServices;
        private readonly ISfTBL_ModuloDocumentos_EstadosManagementServices _estadosServices;
        readonly IDocumentosAdoService _documentosAdoService;

        public VistaDocumentosPublicadosPresenter
            (
                 ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriaServices
                ,ISfTBL_ModuloDocumentos_EstadosManagementServices estadosServices
                ,IDocumentosAdoService documentosAdoService
            )
        {            
            _documentoServices = documentoServices;
            _categoriaServices = categoriaServices;
            _estadosServices = estadosServices;
            _documentosAdoService = documentosAdoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll();
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAll();
        }

        private void GetAll()
        {
            try
            {
                Estados = _estadosServices.FindBySpec(true);
                EstadoPublicado = Estados.Find(est => est.Codigo.Equals("PUBLICADO"));

                var dt = _documentosAdoService.GetVistaDocumentos(0, EstadoPublicado.IdEstado, View.FiltroNombre, "docspub", View.ServerHostPath, View.IdModule);

                View.LoadView(dt);
                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}
