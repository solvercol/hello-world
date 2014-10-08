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
        private readonly ISfTBL_Admin_OptionListManagementServices optionListServices;

        public VerDocumentoPresenter
            (
                ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoManagementServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasManagementServices
                ,ISfTBL_Admin_UsuariosManagementServices usuarioServices
                ,ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices docAdjuntoServices
                , ISfTBL_Admin_OptionListManagementServices optionListServices
            )
        {
            this.documentoServices = documentoManagementServices;
            this.categoriasServices = categoriasManagementServices;
            this.usuarioServices = usuarioServices;
            this.docAdjuntoServices = docAdjuntoServices;
            this.optionListServices = optionListServices;
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
            LoadOptionListConfigValues();
            CargarObjeto();
        }

        void LoadOptionListConfigValues()
        {
            try
            {
                var op = optionListServices.ObtenerOpcionBykeyModuleId("IdRolAdministradorDocumentos", Convert.ToInt32(View.IdModule));
                
                if (op != null)
                {
                    View.IdRolAdministradorDocumentos = Convert.ToInt32(op.Value);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "LoadOptionListConfigValues"), TypeError.Error));
            }
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
                View.Estado = oDocumento.TBL_ModuloDocumentos_Estados.Nombre;
                View.Observaciones = oDocumento.Observaciones;

                View.Categoria = oDocumento.TBL_ModuloDocumentos_Categorias.Nombre;
                View.SubCategoria = oDocumento.TBL_ModuloDocumentos_Categorias1.Nombre;
                View.TipoDocumento = oDocumento.TBL_ModuloDocumentos_Categorias2.Nombre;

                View.UsuarioResponsable = oDocumento.TBL_Admin_Usuarios.Nombres;

                View.CanEdit = oDocumento.IdUsuarioResponsable == View.UserSession.IdUser
                                || oDocumento.IdUsuarioCreacion == View.UserSession.IdUser
                                || View.UserSession.IsInRoleId(View.IdRolAdministradorDocumentos);

                View.LogInfo = string.Format("Creado por: {0} en {1:dd/MM/yyyy hh:mm tt}, Modificado por: {2} en {3:dd/MM/yyyy hh:mm tt}" ,
                                            oDocumento.TBL_Admin_Usuarios1.Nombres, oDocumento.FechaCreacion,
                                            oDocumento.TBL_Admin_Usuarios2.Nombres, oDocumento.FechaModificacion);

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
