using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using Application.MainModule.Documentos.IServices;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Presenters.Documentos.IViews;
using Presenters.Documentos.Presenters;

namespace Modules.Documentos.Admin
{
    public partial class FrmEditarDocumento
        : ViewPage<EditarDocumentoPresenter, IEditarDocumentoView>, IEditarDocumentoView
    {

        #region Eventos

        public event EventHandler GuardarEvent;
        public event EventHandler CancelarEvent;
        public event EventHandler DescargarArchivoEvent;
        public event EventHandler PublicarEvent;
        public event EventHandler EliminarAdjuntoEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana((IdDocumento == 0) ? "Nuevo Documento" : "Editar Documento");
            btnCancelar.Attributes.Add("onclick", "return confirm('¿Confirma que desea cancelar el documento actual?');");

            btnPublicar.Visible = !(IdDocumento == 0);
            btnCancelar.Visible = !(IdDocumento == 0);
            HacerVisibleBotonesSegunEstado();
            ValidacionExistenIdCategorias();            
        }

        private void HacerVisibleBotonesSegunEstado()
        {
            ISfTBL_ModuloDocumentos_DocumentoManagementServices _documentoServices;
            ISfTBL_ModuloDocumentos_EstadosManagementServices _estadoServices;
            if (IdDocumento != 0)
            {
                _documentoServices = IoC.Resolve<ISfTBL_ModuloDocumentos_DocumentoManagementServices>();
                _estadoServices = IoC.Resolve<ISfTBL_ModuloDocumentos_EstadosManagementServices>();
                var documento = _documentoServices.FindById(IdDocumento);
                var estado = _estadoServices.FindById(documento.IdEstado.GetValueOrDefault());

                btnPublicar.Visible = estado.Codigo.Equals("EN_EDICION");
                btnCancelar.Visible = !estado.Codigo.Equals("CANCELADO");
            }

            
        }

        private void ValidacionExistenIdCategorias()
        {
            txtCategoria.Attributes.Add("onBlur", "DespuesDeDigitarCategoria('Categoria');");
            txtSubCategoria.Attributes.Add("onBlur", "DespuesDeDigitarCategoria('SubCategoria');");
            txtTipoDocumento.Attributes.Add("onBlur", "DespuesDeDigitarCategoria('TipoDocumento');");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditarDocumentoHideControlsevent;
        }

        void FrmEditarDocumentoHideControlsevent(object sender, EventArgs e)
        {
            btnCancelar.Visible = false;
            btnGuardar.Visible = false;
            btnPublicar.Visible = false;
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            //string url = string.IsNullOrEmpty(FormRequest)
            //                      ? string.Format("FrmMisDocumentos.aspx{0}", GetBaseQueryString())
            //                      : string.Format("{0}{1}", FormRequest, GetBaseQueryString());
            string url = string.Format("{0}{1}", FormRequest, GetBaseQueryString());
            Response.Redirect(url);
        }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {
            if (GuardarEvent != null)
                GuardarEvent(null, EventArgs.Empty);
        }


        public double TamanioArchivoActual
        {
            get
            {
                HttpPostedFile file = (HttpPostedFile)(FileUploadArchivo.PostedFile);
                double result = 0;
                double div = 1048576.0;
                if (file == null)
                    return result;
                ///Lo divido en este valor para pasarlo a MB
                result = (file.ContentLength / div);
                return result;
            }
        }

        public double TamanioMaxArchivoACargar
        {
            get
            {
                double tamanio = 0;
                double.TryParse(ConfigurationManager.AppSettings["TamanioMaxArchivoAcargar"], out tamanio);
                if (tamanio == 0)
                    tamanio = 2;
                return tamanio;
            }
        }

        protected void BtnPublicarClick(object sender, EventArgs e)
        {
            if (PublicarEvent != null)
                PublicarEvent(null, EventArgs.Empty);
        }

        protected void BtnCancelarClick(object sender, EventArgs e)
        {
            if (CancelarEvent != null)
                CancelarEvent(null, EventArgs.Empty);
        }

        #endregion

        #region Propiedades

        private static object IdCategoriaSession
        {
            get
            {
                return HttpContext.Current.Session["IdCategoria"];
            }
            set
            {
                HttpContext.Current.Session["IdCategoria"] = value;
            }
        }

        private static object IdSubCategoriaSession
        {
            get
            {
                return HttpContext.Current.Session["IdSubCategoria"];
            }
            set
            {
                HttpContext.Current.Session["IdSubCategoria"] = value;
            }
        }

        private static object IdTipoDocumentoSession
        {
            get
            {
                return HttpContext.Current.Session["IdTipoDocumento"];
            }
            set
            {
                HttpContext.Current.Session["IdTipoDocumento"] = value;
            }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public int IdDocumento
        {
            get 
            { 
                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["IdDocumento"]))
                    id = Convert.ToInt32(Request.QueryString["IdDocumento"]);
                return id;

            }
        }

        public string Titulo 
        {
            get 
            { 
                return txtTitulo.Text.ToUpper(); 
            }
            set
            {
                txtTitulo.Text = value;
            }
        }

        public string Observaciones
        {
            get
            {
                return txtObservaciones.Text;
            }
            set
            {
                txtObservaciones.Text = value;
            }
        }

        public byte[] Archivo 
        {
            get
            {
                return FileUploadArchivo.FileBytes;
            }
        }

        public string NombreArchivo
        {
            get
            {
                string result = string.Empty;
                FileInfo fileInfo = null;
                if (!string.IsNullOrEmpty(FileUploadArchivo.FileName))
                {
                    fileInfo = new FileInfo(FileUploadArchivo.FileName);
                    result = fileInfo.Name;
                }
                return result;
            }
        }

        public int IdCategoria 
        {
            get
            {
                int idCategoria = 0;
                if (IdCategoriaSession != null)
                    idCategoria = Convert.ToInt32(IdCategoriaSession);
                return idCategoria;
            }
            set
            {
                IdCategoriaSession = value;
            }
        }

        public int IdSubCategoria 
        {
            get
            {
                int idSubCategoria = 0;
                if (IdSubCategoriaSession != null)
                    idSubCategoria = Convert.ToInt32(IdSubCategoriaSession);
                return idSubCategoria;
            }
            set
            {
                IdSubCategoriaSession = value;
            }
        }

        public int IdTipoDocumento 
        {
            get
            {
                int idTipoDocumento = 0;
                if (IdTipoDocumentoSession != null)
                    idTipoDocumento = Convert.ToInt32(IdTipoDocumentoSession);
                return idTipoDocumento;
            }
            set
            {
                IdTipoDocumentoSession = value;
            }
        }

        public string Categoria
        {
            get
            {
                return txtCategoria.Text.ToUpper();
            }
            set
            {
                txtCategoria.Text = value;
            }
        }

        public string SubCategoria
        {
            get
            {
                return txtSubCategoria.Text.ToUpper();
            }
            set
            {
                txtSubCategoria.Text = value;
            }
        }

        public string TipoDocumento
        {
            get
            {
                return txtTipoDocumento.Text.ToUpper();
            }
            set
            {
                txtTipoDocumento.Text = value;
            }
        }

        public int IdUsuarioResponsable
        {
            get
            {
                return Convert.ToInt32(ddlResponsableDoc.SelectedValue);
            }
            set
            {
                ddlResponsableDoc.SelectedIndex = -1;
                ddlResponsableDoc.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        public bool Activo
        {
            get { return chkActiva.Checked; }
            set { chkActiva.Checked = value; }
        }

        #endregion

        #region Métodos
        
        public void Responsables(IEnumerable<TBL_Admin_Usuarios> responsables)
        {
            ddlResponsableDoc.Items.Clear();
            foreach (var rp in responsables)
                ddlResponsableDoc.Items.Add(new ListItem(rp.Nombres, rp.IdUser.ToString()));
            var li = new ListItem("Seleccione", "0");
            ddlResponsableDoc.Items.Insert(0, li);
        }

        public void Adjuntos(IEnumerable<TBL_ModuloDocumentos_DocumentoAdjunto> adjuntos)
        {
            GrdViewArchivos.DataSource = adjuntos;
            GrdViewArchivos.DataBind();
        }

        public void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjunto adjunto)
        {
            ViewPage<EditarDocumentoPresenter, IEditarDocumentoView>
                .DownloadDocument(adjunto.Archivo, adjunto.NombreArchivo, "application/octet-stream");
        }

        #region Métodos Web

        [WebMethod]
        public static void AsignarCategoria(string Tipo, int Id)
        {
            try
            {
                switch (Tipo)
                {
                    case "categoria":
                        {
                            IdCategoriaSession = Id;
                            break;
                        }
                    case "subcategoria":
                        {
                            IdSubCategoriaSession = Id;
                            break;
                        }
                    case "tipodocumento":
                        {
                            IdTipoDocumentoSession = Id;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar el identificador de ctegoria: " + ex.Message);
            }
        }

        [WebMethod]
        public static void DespuesDeDigitarCategoria(string Categoria, string Contenido)
        {
            ISfTBL_ModuloDocumentos_CategoriasManagementServices _categoriasServices;
            TBL_ModuloDocumentos_Categorias categoria = null;
            try
            {
                _categoriasServices = IoC.Resolve<ISfTBL_ModuloDocumentos_CategoriasManagementServices>();
                switch (Categoria)
                {
                    case "Categoria":
                        {
                            if (IdCategoriaSession == null)
                                break;
                            categoria = _categoriasServices.FindById(Convert.ToInt32(IdCategoriaSession));
                            if (!categoria.Nombre.ToLower().Equals(Contenido.ToLower()))
                                IdCategoriaSession = null;
                            break;
                        }
                    case "SubCategoria":
                        {
                            if (IdSubCategoriaSession == null)
                                break;
                            categoria = _categoriasServices.FindById(Convert.ToInt32(IdSubCategoriaSession));
                            if (!categoria.Nombre.ToLower().Equals(Contenido.ToLower()))
                                IdSubCategoriaSession = null;
                            break;
                        }
                    case "TipoDocumento":
                        {
                            if (IdTipoDocumentoSession == null)
                                break;
                            categoria = _categoriasServices.FindById(Convert.ToInt32(IdTipoDocumentoSession));
                            if (!categoria.Nombre.ToLower().Equals(Contenido.ToLower()))
                                IdTipoDocumentoSession = null;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Despues De Digitar la Categoria: " + ex.Message);
            }
        }

        #endregion

        #endregion

        public string IdModule
        {
            get { return ModuleId; }
        }

        protected void lnkBtnArchivo_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnArchivo = (LinkButton) sender;
            if (DescargarArchivoEvent != null)
                DescargarArchivoEvent(Convert.ToInt32(lnkBtnArchivo.CommandArgument), EventArgs.Empty);
        }

        protected void ImgBtnEliminar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ImageButton ImgBtnEliminar = (ImageButton) sender;
            if (EliminarAdjuntoEvent != null)
                EliminarAdjuntoEvent(ImgBtnEliminar.CommandArgument, EventArgs.Empty);
        }
    }
}