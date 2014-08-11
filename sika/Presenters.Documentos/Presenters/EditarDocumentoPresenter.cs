using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Application.Core;
using Application.MainModule.Documentos.IServices;
using Application.MainModule.Documentos.Services;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Documentos.IViews;

namespace Presenters.Documentos.Presenters
{
    public class EditarDocumentoPresenter
        : Presenter<IEditarDocumentoView>
    {
        private List<TBL_ModuloDocumentos_Estados> Estados { get; set; }

        private readonly ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices docAdjuntoServices;
        private readonly ISfTBL_ModuloDocumentos_DocumentoAdjuntoHistorialManagementServices docAdjuntoHistServices;
        private readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices;
        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasServices;
        private readonly ISfTBL_ModuloDocumentos_EstadosManagementServices estadosServices;
        private readonly ISfTBL_ModuloDocumentos_LogCambiosManagementServices logCambiosServices;
        private readonly ISfTBL_ModuloDocumentos_HistorialDocumentoManagementServices historialDocumentoServices;
        private readonly ISfTBL_Admin_UsuariosManagementServices usuariosServices;

        public EditarDocumentoPresenter
            (
                ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoManagementServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasManagementServices
                ,ISfTBL_ModuloDocumentos_EstadosManagementServices estadosManagementServices
                ,ISfTBL_ModuloDocumentos_LogCambiosManagementServices logCambiosManagementServices
                ,ISfTBL_Admin_UsuariosManagementServices usuariosManagementServices
                ,ISfTBL_ModuloDocumentos_HistorialDocumentoManagementServices historialDocumentoManagementServices 
                ,ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices docAdjuntoServices
                ,ISfTBL_ModuloDocumentos_DocumentoAdjuntoHistorialManagementServices docAdjuntoHistServices
            )
        {
            this.documentoServices = documentoManagementServices;
            this.categoriasServices = categoriasManagementServices;
            this.estadosServices = estadosManagementServices;
            this.logCambiosServices = logCambiosManagementServices;
            this.usuariosServices = usuariosManagementServices;
            this.historialDocumentoServices = historialDocumentoManagementServices;
            this.docAdjuntoServices = docAdjuntoServices;
            this.docAdjuntoHistServices = docAdjuntoHistServices;

            Estados = estadosServices.FindBySpec(true);
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.GuardarEvent += ViewGuardarEvent;
            View.PublicarEvent += ViewPublicarEvent;
            View.CancelarEvent += ViewEliminarEvent;
            View.DescargarArchivoEvent += ViewDescargarArchivoEvent;
            View.EliminarAdjuntoEvent += ViewEliminarAdjuntoEvent;
        }

        void ViewEliminarAdjuntoEvent(object sender, EventArgs e)
        {
            EliminarAdjunto(Convert.ToInt32(sender));
        }
        
        void ViewEliminarEvent(object sender, EventArgs e)
        {
            CancelarDocumento();
        }

        void ViewGuardarEvent(object sender, EventArgs e)
        {
            if(View.IdDocumento == 0)
                Guardar();
            else
                Actualizar();
        }

        void ViewPublicarEvent(object sender, EventArgs e)
        {
            Publicar();
        }

        void ViewDescargarArchivoEvent(object sender, EventArgs e)
        {
            var idDocAdjunto = (Int32) sender;
            var adjunto = docAdjuntoServices.FindById(idDocAdjunto);
            View.DescargarArchivo(adjunto);
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            Responsables();
            CargarObjeto();
        }

        private void Responsables()
        {
            try
            {
                var responsables = usuariosServices.FindBySpec(true);
                if (responsables == null) return;
                View.Responsables(responsables);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Responsables"), TypeError.Error));
            }
        }

        private void CargarObjeto()
        {
            try
            {
                if(View.IdDocumento == 0)
                    return;

                var oDocumento = documentoServices.GetDocumentoByIdWithAttachments(Convert.ToInt32(View.IdDocumento));
                
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }

                View.Adjuntos(oDocumento.TBL_ModuloDocumentos_DocumentoAdjunto);

                View.Titulo = oDocumento.Titulo;
                View.Observaciones = oDocumento.Observaciones;

                View.IdCategoria = oDocumento.IdCategoria;
                View.IdSubCategoria = oDocumento.IdSubCategoria;
                View.IdTipoDocumento = oDocumento.IdTipo;

                if (oDocumento.TBL_ModuloDocumentos_Categorias == null)
                    oDocumento.TBL_ModuloDocumentos_Categorias = categoriasServices.FindById(oDocumento.IdCategoria);
                View.Categoria = oDocumento.TBL_ModuloDocumentos_Categorias.Nombre;
                if (oDocumento.TBL_ModuloDocumentos_Categorias1 == null)
                    oDocumento.TBL_ModuloDocumentos_Categorias1 = categoriasServices.FindById(oDocumento.IdSubCategoria);
                View.SubCategoria = oDocumento.TBL_ModuloDocumentos_Categorias1.Nombre;
                if (oDocumento.TBL_ModuloDocumentos_Categorias2 == null)
                    oDocumento.TBL_ModuloDocumentos_Categorias2 = categoriasServices.FindById(oDocumento.IdTipo);
                View.TipoDocumento = oDocumento.TBL_ModuloDocumentos_Categorias2.Nombre;

                View.IdUsuarioResponsable = oDocumento.IdUsuarioResponsable;
                View.Activo = oDocumento.IsActive;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
            }
        }

        private void Guardar()
        {
            string msgError = string.Empty;
            try
            {
                if (View.TamanioArchivoActual == 0)
                {
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(new Exception(Message.NotSelectFile), MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                    InvokeMessageBox(new MessageBoxEventArgs(Message.NotSelectFile, TypeError.Error));
                    return;
                }
                if (View.TamanioArchivoActual > View.TamanioMaxArchivoACargar)  // 1MB approx (actually less though)
                {
                    msgError = string.Format(Message.SizeMaxFileError, View.TamanioMaxArchivoACargar);
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(new Exception(msgError), MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                    InvokeMessageBox(new MessageBoxEventArgs(msgError, TypeError.Error));
                    return;
                }


                var IdCategoria = View.IdCategoria;
                if (View.IdCategoria == 0)
                    IdCategoria = GuardarCategoria(1, View.Categoria);

                var IdSubCategoria = View.IdSubCategoria;
                if (View.IdSubCategoria == 0)
                    IdSubCategoria = GuardarCategoria(2, View.SubCategoria);

                var IdTipoDocumento = View.IdTipoDocumento;
                if (View.IdTipoDocumento == 0)
                    IdTipoDocumento = GuardarCategoria(3,View.TipoDocumento);

                DateTime fechaAhora = DateTime.Now;
                var oDocumento = documentoServices.NewEntity();
                oDocumento.Titulo = View.Titulo;
                oDocumento.Observaciones = View.Observaciones;
                oDocumento.Version = "001";
                oDocumento.IdEstado = Estados.Find(est => est.Codigo.Equals("EN_EDICION")).IdEstado;

                oDocumento.IdCategoria = IdCategoria;
                oDocumento.IdSubCategoria = IdSubCategoria;
                oDocumento.IdTipo = IdTipoDocumento;

                oDocumento.IdUsuarioResponsable = View.IdUsuarioResponsable;
                oDocumento.IdUsuarioCreacion = View.UserSession.IdUser;
                oDocumento.IdUsuarioModificacion = View.UserSession.IdUser;
                oDocumento.FechaCreacion = fechaAhora;
                oDocumento.FechaModificacion = fechaAhora;
                oDocumento.IsActive = View.Activo;
                oDocumento.CreateBy = View.UserSession.IdUser;
                oDocumento.CreateOn = fechaAhora;
                oDocumento.ModifiedBy = View.UserSession.IdUser;
                oDocumento.ModifiedOn = fechaAhora;
                documentoServices.Add(oDocumento);

                GuardarAdjunto(oDocumento.IdDocumento, View.NombreArchivo);

                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, null, "Documento creado");

                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));

                View.Adjuntos(oDocumento.TBL_ModuloDocumentos_DocumentoAdjunto);

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.SaveError, TypeError.Error));
            }
        }

        private void GuardarAdjunto(int idDocumento, string nombreArchivo) 
        {
            try
            {
                var adjunto = docAdjuntoServices.NewEntity();
                adjunto.IdDocumento = idDocumento;
                adjunto.Archivo = View.Archivo;
                adjunto.IsActive = true;
                adjunto.ModifiedBy = View.UserSession.IdUser.ToString();
                adjunto.ModifiedOn = DateTime.Now;
                adjunto.CreateBy = View.UserSession.IdUser.ToString();
                adjunto.CrateOn = DateTime.Now;
                adjunto.NombreArchivo = nombreArchivo;
                docAdjuntoServices.Add(adjunto);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.SaveError, TypeError.Error));
            }
        }

        private int GuardarCategoria(int nivel,string nombre)
        {
            int result = 0;
            try
            {
                var categoria = categoriasServices.NewEntity();
                categoria.Nombre = nombre;
                categoria.Nivel = nivel;
                categoria.IsActive = true;
                categoria.CreateBy = View.UserSession.IdUser.ToString();
                categoria.CreateOn = DateTime.Now;
                categoria.ModifiedBy = View.UserSession.IdUser.ToString();
                categoria.ModifiedOn = DateTime.Now;
                categoriasServices.Add(categoria);
                result = categoria.IdCategoria;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Categoria"), TypeError.Error));
            }
            return result;
        }

        private void GuardarLog(TBL_ModuloDocumentos_Documento oDocumento, decimal? idHistorial, string mensaje)
        {
            var logDoc = logCambiosServices.NewEntity();
            logDoc.IdDocumento = oDocumento.IdDocumento;
            logDoc.IdHistorial = idHistorial;
            logDoc.Descripcion = string.Format("{0}: {1}", mensaje, oDocumento.Titulo);
            logDoc.IsActive = true;
            logDoc.CreateBy = View.UserSession.IdUser.ToString();
            logDoc.CreateOn = DateTime.Now;
            logCambiosServices.Add(logDoc);
        }

        private void Actualizar()
        {
            string msgError = string.Empty;
            try
            {
                if (View.IdDocumento == 0)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " registro Documento"), TypeError.Error));
                    return;
                }
                var oDocumento = documentoServices.GetDocumentoByIdWithAttachments(Convert.ToInt32(View.IdDocumento));
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }

                if (View.Archivo.Length > 0 && View.TamanioArchivoActual > View.TamanioMaxArchivoACargar)  
                {
                    msgError = string.Format(Message.SizeMaxFileError, View.TamanioMaxArchivoACargar);
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(new Exception(msgError), MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                    InvokeMessageBox(new MessageBoxEventArgs(msgError, TypeError.Error));
                    return;
                }


                ///CREACION DEL HISTORICO SI ESTA PUBLICADO
                var estado = estadosServices.FindById(oDocumento.IdEstado.GetValueOrDefault());
                decimal? idHistorial = null;
                if (estado.Codigo.Equals("PUBLICADO"))
                    idHistorial = GuardarHistorico(oDocumento);
                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, idHistorial, "Documento modificado");

                
                if (View.IdCategoria == 0)
                    View.IdCategoria = GuardarCategoria(1, View.Categoria);

                if (View.IdSubCategoria == 0)
                    View.IdSubCategoria = GuardarCategoria(2, View.SubCategoria);

                if (View.IdTipoDocumento == 0)
                    View.IdTipoDocumento = GuardarCategoria(3, View.TipoDocumento);


                DateTime fechaAhora = DateTime.Now;
                
                oDocumento.Titulo = View.Titulo;
                oDocumento.Observaciones = View.Observaciones;
                oDocumento.Version = GetNextVersion(oDocumento.Version, oDocumento.IdEstado.GetValueOrDefault());
                oDocumento.IdCategoria = View.IdCategoria;
                oDocumento.IdSubCategoria = View.IdSubCategoria;
                oDocumento.IdTipo = View.IdTipoDocumento;
                oDocumento.IdUsuarioResponsable = View.IdUsuarioResponsable;
                oDocumento.IdUsuarioModificacion = View.UserSession.IdUser;
                oDocumento.FechaModificacion = fechaAhora;
                oDocumento.IsActive = View.Activo;
                oDocumento.ModifiedBy = View.UserSession.IdUser;
                oDocumento.ModifiedOn = DateTime.Now;
                documentoServices.Modify(oDocumento);

                GuardarAdjunto(oDocumento.IdDocumento, View.NombreArchivo);

                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));

                View.Adjuntos(oDocumento.TBL_ModuloDocumentos_DocumentoAdjunto);

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.EditError, TypeError.Error));
            }
        }

        private void Publicar()
        {
            try
            {
                if (View.IdDocumento == 0)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " registro Documento"), TypeError.Error));
                    return;
                }
                var oDocumento = documentoServices.FindById(Convert.ToInt32(View.IdDocumento));
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }
                oDocumento.IdEstado = Estados.Find(est => est.Codigo.Equals("PUBLICADO")).IdEstado;
                oDocumento.IsActive = true;
                documentoServices.Modify(oDocumento);

                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, null, "Documento Publicado");

                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError, "Actividad"), TypeError.Error));
            }

        }

        private string GetNextVersion(string actualVersion, int idEstadoActual)
        {
            string result = actualVersion;
            var estado = estadosServices.FindById(idEstadoActual);
            if (estado.Codigo.Equals("PUBLICADO"))
                result = Convert.ToString(Convert.ToInt32(actualVersion) + 1).PadLeft(3, '0');
            return result;
        }

        private void CancelarDocumento()
        {
            try
            {
                if (View.IdDocumento == 0)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " registro Documento"), TypeError.Error));
                    return;
                }
                var oDocumento = documentoServices.GetDocumentoByIdWithAttachments(Convert.ToInt32(View.IdDocumento));
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }

                ///CREACION DEL HISTORICO SI ESTA PUBLICADO
                var estado = estadosServices.FindById(oDocumento.IdEstado.GetValueOrDefault());
                decimal? idHistorial = null;
                if (estado.Codigo.Equals("PUBLICADO"))
                    idHistorial = GuardarHistorico(oDocumento);
                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, idHistorial, "Documento cancelado");

                oDocumento.IsActive = false;
                oDocumento.IdEstado = Estados.Find(est => est.Codigo.Equals("CANCELADO")).IdEstado;
                documentoServices.Modify(oDocumento);

                
                LimpiarVista();
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError, "Actividad"), TypeError.Error));
            }
        }

        private void EliminarAdjunto(Int32 IdAdjunto)
        {
            try
            {
                if (IdAdjunto == 0)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " registro Adjunto"), TypeError.Error));
                    return;
                }
                var oAdjunto = docAdjuntoServices.FindById(IdAdjunto);
                if (oAdjunto == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Adjunto"), TypeError.Error));
                    return;
                }

                var oDocumento = documentoServices.GetDocumentoByIdWithAttachments(oAdjunto.IdDocumento);

                ///CREACION DEL HISTORICO SI ESTA PUBLICADO
                var estado = estadosServices.FindById(oDocumento.IdEstado.GetValueOrDefault());
                decimal? idHistorial = null;
                if (estado.Codigo.Equals("PUBLICADO"))
                    idHistorial = GuardarHistorico(oDocumento);
                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, idHistorial, "Se eliminó un archivo adjunto");

                docAdjuntoServices.Remove(oAdjunto);
                
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));

                View.Adjuntos(oDocumento.TBL_ModuloDocumentos_DocumentoAdjunto);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError, "Actividad"), TypeError.Error));
            }
        }

        private void LimpiarVista()
        {
            View.Titulo = string.Empty;
            View.Observaciones = string.Empty;
            View.IdUsuarioResponsable = 0;
            View.IdCategoria = 0;
            View.IdSubCategoria = 0;
            View.IdTipoDocumento = 0;
            View.Categoria = string.Empty;
            View.SubCategoria = string.Empty;
            View.TipoDocumento = string.Empty;
            View.Activo = true;
            View.Adjuntos(null);
        }

        private decimal GuardarHistorico(TBL_ModuloDocumentos_Documento oDocumento)
        {
            decimal result = 0;
            try
            {
                var historial = historialDocumentoServices.NewEntity();
                historial.IdDocumento = oDocumento.IdDocumento;
                historial.Titulo = oDocumento.Titulo;
                historial.Observaciones = oDocumento.Observaciones;
                historial.Version = oDocumento.Version;
                historial.IdCategoria = oDocumento.IdCategoria;
                historial.IdEstado = oDocumento.IdEstado.GetValueOrDefault();
                historial.IdSubCategoria = oDocumento.IdSubCategoria;
                historial.IdTipo = oDocumento.IdTipo;
                historial.IdUsuarioResposable = oDocumento.IdUsuarioResponsable;
                historial.IdUsuarioCreacion = oDocumento.IdUsuarioCreacion;
                historial.IdUsuarioModificacion = oDocumento.IdUsuarioModificacion;
                historial.FechaCreacion = oDocumento.FechaCreacion;
                historial.IsActive = oDocumento.IsActive;
                historial.CreateBy = oDocumento.CreateBy.ToString();
                historial.CreateOn = oDocumento.CreateOn;
                historial.ModifiedBy = oDocumento.ModifiedBy.ToString();
                historial.ModifiedOn = oDocumento.ModifiedOn;
                historialDocumentoServices.Add(historial);
                result = historial.IdHistorial;

                ///Se crea el historico de adjuntos
                foreach (var docAdjunto in oDocumento.TBL_ModuloDocumentos_DocumentoAdjunto)
                {
                    var adjhist = docAdjuntoHistServices.NewEntity();
                    adjhist.IdHistorial = result;
                    adjhist.IsActive = true;
                    adjhist.ModifiedBy = docAdjunto.ModifiedBy;
                    adjhist.ModifiedOn = docAdjunto.ModifiedOn;
                    adjhist.NombreArchivo = docAdjunto.NombreArchivo;
                    adjhist.Archivo = docAdjunto.Archivo;
                    adjhist.CreateBy = docAdjunto.CreateBy;
                    adjhist.CreateOn = docAdjunto.CrateOn;
                    docAdjuntoHistServices.Add(adjhist);
                }

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.EditError, TypeError.Error));
            }
            return result;
        }

    }
}

