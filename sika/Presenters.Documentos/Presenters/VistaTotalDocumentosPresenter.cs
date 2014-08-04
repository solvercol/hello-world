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
                var listDoc = _documentoServices.FindBySpec(true);
                //if (!View.UserSession.IsInRole("Administrador"))
                //    if (listDoc.Count() > 0)
                //        listDoc = listDoc.FindAll(doc => doc.IdUsuarioCreacion == View.UserSession.IdUser);
                if (View.FiltroNombre.Length > 0)
                {
                    listDoc =
                    listDoc.FindAll
                        (doc =>
                            _categoriaServices.FindById(doc.IdCategoria).Nombre.ToLower().Contains(View.FiltroNombre.ToLower())
                            ||
                            _categoriaServices.FindById(doc.IdSubCategoria).Nombre.ToLower().Contains(View.FiltroNombre.ToLower())
                            ||
                            _categoriaServices.FindById(doc.IdTipo).Nombre.ToLower().Contains(View.FiltroNombre.ToLower())
                        );
                }
                if (View.FiltroIdEstado != 0)
                    listDoc = listDoc.FindAll(doc => doc.IdEstado == View.FiltroIdEstado);
                if (View.FiltroIdResponsable != 0)
                    listDoc = listDoc.FindAll(doc => doc.IdUsuarioResponsable == View.FiltroIdResponsable);
                View.ListaDocumentos = listDoc;
                View.ArbolDocumentos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}
