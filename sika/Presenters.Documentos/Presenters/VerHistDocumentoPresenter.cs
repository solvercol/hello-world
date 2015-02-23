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
    public class VerHistDocumentoPresenter
        : Presenter<IVerHistDocumentoView>
    {

        private readonly ISfTBL_ModuloDocumentos_HistorialDocumentoManagementServices histDocumentoServices;
        private readonly ISfTBL_ModuloDocumentos_DocumentoAdjuntoHistorialManagementServices docAdjuntoHistServices;
        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasServices;
        private readonly ISfTBL_Admin_UsuariosManagementServices usuarioServices;

        public VerHistDocumentoPresenter
            (
                ISfTBL_ModuloDocumentos_HistorialDocumentoManagementServices histDocumentoServices
                ,ISfTBL_ModuloDocumentos_DocumentoAdjuntoHistorialManagementServices docAdjuntoHistServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasManagementServices
                ,ISfTBL_Admin_UsuariosManagementServices usuarioServices
            )
        {
            this.histDocumentoServices = histDocumentoServices;
            this.docAdjuntoHistServices = docAdjuntoHistServices;
            this.categoriasServices = categoriasManagementServices;
            this.usuarioServices = usuarioServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.DescargarArchivoEvent += ViewDescargarArchivoEvent;

        }

        void ViewDescargarArchivoEvent(object sender, EventArgs e)
        {
            var idDocAdjuntoHist = (Int32)sender;
            var adjunto = docAdjuntoHistServices.FindById(idDocAdjuntoHist);
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
                if(View.IdHistDocumento == 0)
                    return;

                var oHistDocumento = histDocumentoServices.GetHistorialByIdWithAttachments(Convert.ToInt32(View.IdHistDocumento));
                
                if (oHistDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Historial Documento"), TypeError.Error));
                    return;
                }

                View.Titulo = oHistDocumento.Titulo;
                View.Version = oHistDocumento.Version;
                View.IdDocumento = oHistDocumento.IdDocumento;
                View.Estado = oHistDocumento.TBL_ModuloDocumentos_Estados.Nombre;

                View.Categoria = oHistDocumento.TBL_ModuloDocumentos_Categorias.Nombre;
                View.SubCategoria = oHistDocumento.TBL_ModuloDocumentos_Categorias1.Nombre;
                View.TipoDocumento = oHistDocumento.TBL_ModuloDocumentos_Categorias2.Nombre;

                View.UsuarioResponsable = oHistDocumento.CargoResponsable;

                View.LogInfo = string.Format("Creado por: {0} en {1:dd/MM/yyyy hh:mm tt}, Modificado por: {2} en {3:dd/MM/yyyy hh:mm tt}",
                                            oHistDocumento.TBL_Admin_Usuarios.Nombres, oHistDocumento.FechaCreacion,
                                            oHistDocumento.TBL_Admin_Usuarios1.Nombres, oHistDocumento.CreateOn);

                View.Adjuntos(oHistDocumento.TBL_ModuloDocumentos_DocumentoAdjuntoHistorial);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
            }
        }

    }
}
