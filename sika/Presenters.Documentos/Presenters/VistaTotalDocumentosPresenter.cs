using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Application.Core;
using Application.MainModule.Documentos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Documentos.IViews;

namespace Presenters.Documentos.Presenters
{
    public class VistaTotalDocumentosPresenter
        : Presenter<IVistaTotalDocumentosView>
    {        

        private readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices _documentoServices;
        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices _categoriaServices;
        private readonly ISfTBL_ModuloDocumentos_EstadosManagementServices _estadosServices;
        private readonly ISfTBL_Admin_UsuariosManagementServices _usuariosServices;

        public VistaTotalDocumentosPresenter
            (
                 ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriaServices
                ,ISfTBL_ModuloDocumentos_EstadosManagementServices estadosServices
                ,ISfTBL_Admin_UsuariosManagementServices usuariosManagementServices
            )
        {            
            _documentoServices = documentoServices;
            _categoriaServices = categoriaServices;
            _estadosServices = estadosServices;
            _usuariosServices = usuariosManagementServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
            View.FilterEvent += ViewFilterEvent;
            View.ClearFilterEvent += ViewClearFilterEvent;
        }

        void ViewClearFilterEvent(object sender, EventArgs e)
        {
            View.FiltroNombre = string.Empty;
            View.FiltroIdEstado = 0;
            View.FiltroIdResponsable = 0;
            GetAll();
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll();
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Estados();
            Responsables();
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

        private void Responsables()
        {
            try
            {
                var responsables = _usuariosServices.FindBySpec(true);
                if (responsables == null) return;
                View.Responsables(responsables);
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
                View.ListaDocumentos = _documentoServices.FindTotalDocsByFilters(View.FiltroNombre, View.FiltroIdEstado,
                                                                                 View.FiltroIdResponsable);
                View.ArbolDocumentos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}
