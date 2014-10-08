using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Documentos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Documentos.IViews;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Documentos.Presenters
{
    public class VistaMisDocumentosPresenter
        : Presenter<IVistaMisDocumentosView>
    {        

        private readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices _documentoServices;
        
        private readonly ISfTBL_ModuloDocumentos_EstadosManagementServices _estadosServices;

        readonly IDocumentosAdoService _documentosAdoService;

        public VistaMisDocumentosPresenter
            (
                 ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices
                ,ISfTBL_ModuloDocumentos_EstadosManagementServices estadosServices
                ,IDocumentosAdoService documentosAdoService
            )
        {            
            _documentoServices = documentoServices;
            _estadosServices = estadosServices;
            _documentosAdoService = documentosAdoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
            View.ClearFilterEvent += ViewClearFilterEvent;
        }

        void ViewClearFilterEvent(object sender, EventArgs e)
        {
            View.FiltroNombre = string.Empty;
            View.FiltroIdEstado = 0;
            GetAll();
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll();
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Estados();
            GetAll();
        }

        private void Estados()
        {
            try
            {
                var estados = _estadosServices.FindBySpec(true);
                if (estados == null) return;
                View.Estados(estados);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Responsables"), TypeError.Error));
            }
        }

        private void GetAll()
        {
            try
            {
                var dt = _documentosAdoService.GetVistaDocumentos(View.UserSession.IdUser, View.FiltroIdEstado, View.FiltroNombre, "misdocs", View.ServerHostPath, View.IdModule);

                View.LoadView(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}
