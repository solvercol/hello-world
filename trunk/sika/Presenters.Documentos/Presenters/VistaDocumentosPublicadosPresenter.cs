using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Core;
using Application.MainModule.Documentos.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Documentos.IViews;

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

        public VistaDocumentosPublicadosPresenter
            (
                 ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriaServices
                ,ISfTBL_ModuloDocumentos_EstadosManagementServices estadosServices
            )
        {            
            _documentoServices = documentoServices;
            _categoriaServices = categoriaServices;
            _estadosServices = estadosServices;
            Estados = estadosServices.FindBySpec(true);
            EstadoPublicado = Estados.Find(est => est.Codigo.Equals("PUBLICADO"));
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
                View.ListaDocumentos = _documentoServices.FindDocsPublicadosByFilters(View.FiltroNombre);
                View.ArbolDocumentos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}
