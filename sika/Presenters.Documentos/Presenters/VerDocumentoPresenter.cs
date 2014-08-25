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
    public class VerDocumentoPresenter
        : Presenter<IVerDocumentoView>
    {
        private readonly ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices docAdjuntoServices;
        private readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices;
        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasServices;
        private readonly ISfTBL_Admin_UsuariosManagementServices usuarioServices;

        public VerDocumentoPresenter
            (
                ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoManagementServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasManagementServices
                ,ISfTBL_Admin_UsuariosManagementServices usuarioServices
                ,ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices docAdjuntoServices
            )
        {
            this.documentoServices = documentoManagementServices;
            this.categoriasServices = categoriasManagementServices;
            this.usuarioServices = usuarioServices;
            this.docAdjuntoServices = docAdjuntoServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.DescargarArchivoEvent += ViewDescargarArchivoEvent;

        }

        void ViewDescargarArchivoEvent(object sender, EventArgs e)
        {
            var idDocAdjunto = (Int32)sender;
            var adjunto = docAdjuntoServices.FindById(idDocAdjunto);
            View.DescargarArchivo(adjunto);
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            CargarObjeto();
        }

        private void CargarObjeto()
        {
            try
            {
                if(View.IdDocumento == 0)
                    return;

                var oDocumento = documentoServices.GetDocumentoByIdWithCategories(Convert.ToInt32(View.IdDocumento));
                
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }

                View.Titulo = oDocumento.Titulo;
                View.Version = oDocumento.Version;

                View.Observaciones = oDocumento.Observaciones;

                View.Categoria = oDocumento.TBL_ModuloDocumentos_Categorias.Nombre;
                View.SubCategoria = oDocumento.TBL_ModuloDocumentos_Categorias1.Nombre;
                View.TipoDocumento = oDocumento.TBL_ModuloDocumentos_Categorias2.Nombre;

                View.UsuarioResponsable = usuarioServices.FindById(oDocumento.IdUsuarioResponsable).Nombres;

                View.Adjuntos(oDocumento.TBL_ModuloDocumentos_DocumentoAdjunto);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
            }
        }

    }
}
