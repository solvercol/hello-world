using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Application.MainModule.Documentos.IServices;
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

        private readonly ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices _docAdjuntoServices;
        private readonly ISfTBL_ModuloDocumentos_DocumentoAdjuntoHistorialManagementServices _docAdjuntoHistServices;
        private readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices _documentoServices;
        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices _categoriasServices;
        private readonly ISfTBL_ModuloDocumentos_EstadosManagementServices _estadosServices;
        private readonly ISfTBL_ModuloDocumentos_LogCambiosManagementServices _logCambiosServices;
        private readonly ISfTBL_ModuloDocumentos_HistorialDocumentoManagementServices _historialDocumentoServices;
        private readonly ISfTBL_Admin_UsuariosManagementServices _usuariosServices;

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
            this._documentoServices = documentoManagementServices;
            this._categoriasServices = categoriasManagementServices;
            this._estadosServices = estadosManagementServices;
            this._logCambiosServices = logCambiosManagementServices;
            this._usuariosServices = usuariosManagementServices;
            this._historialDocumentoServices = historialDocumentoManagementServices;
            this._docAdjuntoServices = docAdjuntoServices;
            this._docAdjuntoHistServices = docAdjuntoHistServices;

            Estados = _estadosServices.FindBySpec(true);
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.GuardarEvent += ViewGuardarEvent;
            View.PublicarEvent += ViewPublicarEvent;
            View.CancelarEvent += ViewEliminarEvent;
            View.DescargarArchivoEvent += ViewDescargarArchivoEvent;
            View.EliminarAdjuntoEvent += ViewEliminarAdjuntoEvent;
            View.GuardarAdjuntoEvent += ViewGuardarAdjuntoEvent;
        }

        void ViewGuardarAdjuntoEvent(object sender, EventArgs e)
        {
            GuardarArchivoAdjunto(View.IdDocumento,View.NombreArchivo);
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
            var adjunto = _docAdjuntoServices.FindById(idDocAdjunto);
            View.DescargarArchivo(adjunto);
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            LoadCategorias();
            LoadSubCategorias();
            LoadTiposDocumento();
            Responsables();
            CargarObjeto();
            InitView();
        }

        void InitView()
        {
            if (View.IdDocumento != 0)
                return;

            View.ArchivosAdjuntos = new List<DTO_ValueKey>();
            View.Titulo = string.Empty;
            View.Observaciones = string.Empty;

            View.LogInfo = string.Format("Creado por: {0} en {1:dd/MM/yyyy hh:mm tt}, Modificado por: {2} en {3:dd/MM/yyyy hh:mm tt}",
                                            View.UserSession.Nombres, DateTime.Now,
                                            View.UserSession.Nombres, DateTime.Now);

            View.Estado = "BORRADOR";
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

        void LoadCategorias()
        {
            try
            {
                var categorias = _categoriasServices.GetByNivel(1);
                var subCategorias = _categoriasServices.GetByNivel(2);
                var tiposDocumentos = _categoriasServices.GetByNivel(3);

                View.LoadCategorias(categorias);
                View.LoadSubCategorias(subCategorias);
                View.LoadTiposDocumento(tiposDocumentos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "LoadCategorias"), TypeError.Error));
            }
        }

        void LoadSubCategorias()
        {
            try
            {
                var subCategorias = _categoriasServices.GetByNivel(2);
                View.LoadSubCategorias(subCategorias);                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "LoadSubCategorias"), TypeError.Error));
            }
        }

        void LoadTiposDocumento()
        {
            try
            {
                var tiposDocumentos = _categoriasServices.GetByNivel(3);
                View.LoadTiposDocumento(tiposDocumentos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "LoadTiposDocumento"), TypeError.Error));
            }
        }

        private void CargarObjeto()
        {
            try
            {
                if(View.IdDocumento == 0)
                    return;

                var oDocumento = _documentoServices.GetDocumentoByIdWithUsers(Convert.ToInt32(View.IdDocumento));
                
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }

                View.Titulo = oDocumento.Titulo;
                View.Observaciones = oDocumento.Observaciones;

                View.IdCategoria = oDocumento.IdCategoria;
                View.IdSubCategoria = oDocumento.IdSubCategoria;
                View.IdTipoDocumento = oDocumento.IdTipo;
                View.Estado = oDocumento.TBL_ModuloDocumentos_Estados.Nombre;

                View.IdCategoria = oDocumento.IdCategoria;
                View.IdSubCategoria = oDocumento.IdSubCategoria;
                View.IdTipoDocumento = oDocumento.IdTipo;

                View.IdUsuarioResponsable = oDocumento.IdUsuarioResponsable;

                View.LogInfo = string.Format("Creado por: {0} en {1:dd/MM/yyyy hh:mm tt}, Modificado por: {2} en {3:dd/MM/yyyy hh:mm tt}",
                                            oDocumento.TBL_Admin_Usuarios1.Nombres, oDocumento.FechaCreacion,
                                            oDocumento.TBL_Admin_Usuarios2.Nombres, oDocumento.FechaModificacion);

                LoadArchivosAdjuntos();

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
            }
        }

        void LoadArchivosAdjuntos()
        {
            if (View.IdDocumento == 0)
                return;

            try
            {
                var items = _docAdjuntoServices.GetDocumentosAdjuntosByDocId(View.IdDocumento);
                View.ArchivosAdjuntos = new List<DTO_ValueKey>();
                if (items.Any())
                {
                    foreach (var adjunto in items)
                    {
                        var doc = new DTO_ValueKey();
                        doc.Id = adjunto.IdDocumentoAdjunto.ToString();
                        doc.Value = adjunto.NombreArchivo;
                        doc.ComplexValue = adjunto.Archivo;

                        View.ArchivosAdjuntos.Add(doc);
                    }
                }
                View.LoadArchivosAdjuntos(View.ArchivosAdjuntos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.SaveError, TypeError.Error));
            }
        }

        private void Guardar()
        {
            try
            {
                var IdCategoria = View.IdCategoria;

                DateTime fechaAhora = DateTime.Now;
                var oDocumento = new TBL_ModuloDocumentos_Documento();
                oDocumento.Titulo = View.Titulo.ToUpper();
                oDocumento.Observaciones = View.Observaciones.ToUpper();
                oDocumento.Version = "001";
                oDocumento.IdEstado = Estados.Find(est => est.Codigo.Equals("EN_EDICION")).IdEstado;

                oDocumento.IdCategoria = View.IdCategoria;
                oDocumento.IdSubCategoria = View.IdSubCategoria;
                oDocumento.IdTipo = View.IdTipoDocumento;

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
                _documentoServices.Add(oDocumento);

                View.IdDocCreado = oDocumento.IdDocumento;

                if (View.ArchivosAdjuntos.Any())
                {
                    foreach (var adjunto in View.ArchivosAdjuntos)
                    {
                        var docAdjunto = new TBL_ModuloDocumentos_DocumentoAdjunto();
                        docAdjunto.IdDocumento = oDocumento.IdDocumento;
                        docAdjunto.NombreArchivo = adjunto.Value;
                        docAdjunto.Archivo = (byte[])adjunto.ComplexValue;
                        docAdjunto.IsActive = true;
                        docAdjunto.CreateBy = View.UserSession.IdUser.ToString();
                        docAdjunto.CrateOn = DateTime.Now;
                        docAdjunto.ModifiedBy = View.UserSession.IdUser.ToString();
                        docAdjunto.ModifiedOn = DateTime.Now;

                        _docAdjuntoServices.Add(docAdjunto);
                    }
                }

                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, null, string.Format("Documento creado por: {0}", View.UserSession.Nombres));

                View.GoToViewDocument(oDocumento.IdDocumento);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.SaveError, TypeError.Error));
            }
        }

        public void AddCategoria()
        {
            int idNuevaCategoria = GuardarCategoria(View.IdNivelCategoria, View.NuevaCategoria);

            if (idNuevaCategoria != 0)
            {
                switch (View.IdNivelCategoria)
                {
                    case 1:
                        LoadCategorias();
                        View.IdCategoria = idNuevaCategoria;
                        break;
                    case 2:
                        LoadSubCategorias();
                        View.IdSubCategoria = idNuevaCategoria;
                        break;
                    case 3:
                        LoadTiposDocumento();
                        View.IdTipoDocumento = idNuevaCategoria;
                        break;
                }
            }
        }

        private void GuardarArchivoAdjunto(int idDocumento, string nombreArchivo) 
        {
            try
            {
                string msgError = string.Empty;
                //if (View.TamanioArchivoActual == 0)
                //{
                //    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(new Exception(Message.NotSelectFile), MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                //    InvokeMessageBox(new MessageBoxEventArgs(Message.NotSelectFile, TypeError.Error));
                //    return;
                //}
                //if (View.TamanioArchivoActual > View.TamanioMaxArchivoACargar)  // 1MB approx (actually less though)
                //{
                //    msgError = string.Format(Message.SizeMaxFileError, View.TamanioMaxArchivoACargar);
                //    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(new Exception(msgError), MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                //    InvokeMessageBox(new MessageBoxEventArgs(msgError, TypeError.Error));
                //    return;
                //}

                var adjunto = new TBL_ModuloDocumentos_DocumentoAdjunto();
                adjunto.IdDocumento = idDocumento;
                adjunto.Archivo = View.Archivo;
                adjunto.IsActive = true;
                adjunto.ModifiedBy = View.UserSession.IdUser.ToString();
                adjunto.ModifiedOn = DateTime.Now;
                adjunto.CreateBy = View.UserSession.IdUser.ToString();
                adjunto.CrateOn = DateTime.Now;
                adjunto.NombreArchivo = nombreArchivo;
                _docAdjuntoServices.Add(adjunto);

                var oDocumento = _documentoServices.FindById(idDocumento);

                GuardarLog(oDocumento, null, string.Format("El usuario: {0}, agregó archivo adjunto", View.UserSession.Nombres));

                LoadArchivosAdjuntos();
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
                var categoria = _categoriasServices.NewEntity();
                categoria.Nombre = nombre.ToUpper();
                categoria.Nivel = nivel;
                categoria.IsActive = true;
                categoria.CreateBy = View.UserSession.IdUser.ToString();
                categoria.CreateOn = DateTime.Now;
                categoria.ModifiedBy = View.UserSession.IdUser.ToString();
                categoria.ModifiedOn = DateTime.Now;
                _categoriasServices.Add(categoria);
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
            var logDoc = _logCambiosServices.NewEntity();
            logDoc.IdDocumento = oDocumento.IdDocumento;
            logDoc.IdHistorial = idHistorial;
            logDoc.Descripcion = string.Format("{0}", mensaje);
            logDoc.IsActive = true;
            logDoc.CreateBy = View.UserSession.IdUser.ToString();
            logDoc.CreateOn = DateTime.Now;
            _logCambiosServices.Add(logDoc);
        }

        private void Actualizar()
        {
            string msgError = string.Empty;
            if (View.IdDocumento == 0)
            {
                return;
            }
            try
            {
                
                var oDocumento = _documentoServices.GetDocumentoByIdWithAttachments(Convert.ToInt32(View.IdDocumento));

                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }


                ///CREACION DEL HISTORICO SI ESTA PUBLICADO
                var estado = _estadosServices.FindById(oDocumento.IdEstado.GetValueOrDefault());
                decimal? idHistorial = null;
                if (estado.Codigo.Equals("PUBLICADO"))
                    idHistorial = GuardarHistorico(oDocumento);
                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, idHistorial, string.Format("El Documento ha sido modificado por: {0}", View.UserSession.Nombres));
                
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
                oDocumento.ModifiedBy = View.UserSession.IdUser;
                oDocumento.ModifiedOn = DateTime.Now;
                _documentoServices.Modify(oDocumento);

                View.GoToViewDocument(oDocumento.IdDocumento);
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
                var oDocumento = _documentoServices.FindById(Convert.ToInt32(View.IdDocumento));
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }
                oDocumento.IdEstado = Estados.Find(est => est.Codigo.Equals("PUBLICADO")).IdEstado;
                
                _documentoServices.Modify(oDocumento);

                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, null, string.Format("El Documento ha sido publicado por: {0}", View.UserSession.Nombres));

                View.GoToViewDocument(oDocumento.IdDocumento);
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
            var estado = _estadosServices.FindById(idEstadoActual);
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
                var oDocumento = _documentoServices.GetDocumentoByIdWithAttachments(Convert.ToInt32(View.IdDocumento));
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }

                ///CREACION DEL HISTORICO SI ESTA PUBLICADO
                var estado = _estadosServices.FindById(oDocumento.IdEstado.GetValueOrDefault());
                decimal? idHistorial = null;
                if (estado.Codigo.Equals("PUBLICADO"))
                    idHistorial = GuardarHistorico(oDocumento);
                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, idHistorial, string.Format("El Documento ha sido cancelado por: {0}", View.UserSession.Nombres));

                oDocumento.IsActive = false;
                oDocumento.IdEstado = Estados.Find(est => est.Codigo.Equals("CANCELADO")).IdEstado;
                _documentoServices.Modify(oDocumento);


                View.GoToViewDocument(oDocumento.IdDocumento);
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
                var oAdjunto = _docAdjuntoServices.FindById(IdAdjunto);
                if (oAdjunto == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Adjunto"), TypeError.Error));
                    return;
                }

                var oDocumento = _documentoServices.FindById(oAdjunto.IdDocumento);

                _docAdjuntoServices.Remove(oAdjunto);

                ///LOG DE DOCUMENTO
                GuardarLog(oDocumento, null, string.Format("El usuario {0}, eliminó archivo adjunto con nombre: {1}", View.UserSession.Nombres, oAdjunto.NombreArchivo));                                                
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
        }

        private decimal GuardarHistorico(TBL_ModuloDocumentos_Documento oDocumento)
        {
            decimal result = 0;
            try
            {
                var historial = _historialDocumentoServices.NewEntity();
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
                _historialDocumentoServices.Add(historial);
                result = historial.IdHistorial;

                ///Se crea el historico de adjuntos
                foreach (var docAdjunto in oDocumento.TBL_ModuloDocumentos_DocumentoAdjunto)
                {
                    var adjhist = _docAdjuntoHistServices.NewEntity();
                    adjhist.IdHistorial = result;
                    adjhist.IsActive = true;
                    adjhist.ModifiedBy = docAdjunto.ModifiedBy;
                    adjhist.ModifiedOn = docAdjunto.ModifiedOn;
                    adjhist.NombreArchivo = docAdjunto.NombreArchivo;
                    adjhist.Archivo = docAdjunto.Archivo;
                    adjhist.CreateBy = docAdjunto.CreateBy;
                    adjhist.CreateOn = docAdjunto.CrateOn;
                    _docAdjuntoHistServices.Add(adjhist);
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