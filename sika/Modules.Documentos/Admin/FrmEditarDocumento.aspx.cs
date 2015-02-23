using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using Application.MainModule.Documentos.IServices;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Presenters.Documentos.IViews;
using Presenters.Documentos.Presenters;
using Application.Core;

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
        public event EventHandler GuardarAdjuntoEvent;

        public string QueryStringFrom
        {
            get { return Request.QueryString.Get("from"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana((IdDocumento == 0) ? "Nuevo Documento" : "Editar Documento");
            btnCancelar.Attributes.Add("onclick", "return confirm('¿Confirma que desea cancelar el documento actual?');");

            btnPublicar.Visible = !(IdDocumento == 0);
            btnCancelar.Visible = !(IdDocumento == 0);
            
            IdDocCreado = IdDocumento;
            HacerVisibleBotonesSegunEstado();          
        }

        private void HacerVisibleBotonesSegunEstado()
        {
            ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices;
            ISfTBL_ModuloDocumentos_EstadosManagementServices estadoServices;
            if (IdDocumento != 0)
            {
                documentoServices = IoC.Resolve<ISfTBL_ModuloDocumentos_DocumentoManagementServices>();
                estadoServices = IoC.Resolve<ISfTBL_ModuloDocumentos_EstadosManagementServices>();
                var documento = documentoServices.FindById(IdDocumento);
                var estado = estadoServices.FindById(documento.IdEstado.GetValueOrDefault());

                btnPublicar.Visible = estado.Codigo.Equals("EN_EDICION");
                btnCancelar.Visible = !estado.Codigo.Equals("CANCELADO");
            }

            
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditarDocumentoHideControlsevent;
        }

        void FrmEditarDocumentoHideControlsevent(object sender, EventArgs e)
        {
            string url = string.Empty;
            if (!EstaAdjuntandoOEliminando)
            {
                if (IdDocumento == 0)
                {
                    url = string.Format("FrmEditarDocumento.aspx{0}{1}&IdDocumento={2}", GetBaseQueryString(), "&Form=FrmMisDocumentos.aspx", IdDocCreado);
                    Response.Redirect(url);
                }
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
                btnPublicar.Visible = false;
            }
            else
                EstaAdjuntandoOEliminando = false;
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            string url = string.Empty;

            if (IdDocumento == 0)
                url = string.Format("FrmMisDocumentos.aspx?ModuleId={0}", ModuleId);
            else
                url = string.Format("../Consulta/FrmVerDocumento.aspx?ModuleId={0}&IdDocumento={1}&from={2}", ModuleId, IdDocumento, QueryStringFrom);

            Response.Redirect(url);
        }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Titulo))
                messages.Add("El campo Titulo es requerido.");
            
            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

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

        protected void LnkAddCategoria_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var nivel = Convert.ToInt32(btn.CommandArgument);

            switch (nivel)
            {
                case 1:
                    TituloCategoriaAdmin = "Administrar Categoría";
                    break;
                case 2:
                    TituloCategoriaAdmin = "Administrar SubCategoría";
                    break;
                case 3:
                    TituloCategoriaAdmin = "Administrar Tipo Documento";
                    break;
            }


            IdNivelCategoria = nivel;

            txtCategoria.Text = string.Empty;

            ShowAdminCategoria(true);
        }

        protected void BtnSaveCategoria_Click(object sender, EventArgs e)
        {
            Presenter.AddCategoria();
        }

        protected void RptArchivosAdjuntos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (DTO_ValueKey)(e.Item.DataItem);
                // Bindind data

                var hddIdArchivo = e.Item.FindControl("hddIdArchivo") as HiddenField;
                if (hddIdArchivo != null) hddIdArchivo.Value = string.Format("{0}", item.Id);

                var lnkBtnArchivo = e.Item.FindControl("lnkBtnArchivo") as LinkButton;
                if (lnkBtnArchivo != null)
                {
                    lnkBtnArchivo.Text = string.Format("{0}", item.Value);
                    lnkBtnArchivo.CommandArgument = string.Format("{0}", item.Id);
                }

                var ImgBtnEliminar = e.Item.FindControl("ImgBtnEliminar") as ImageButton;
                if (ImgBtnEliminar != null)
                {
                    ImgBtnEliminar.CommandArgument = string.Format("{0}", item.Id);
                }
            }
        }

        #endregion

        #region Propiedades

        private bool EstaAdjuntandoOEliminando
        {
            get
            {
                if (HttpContext.Current.Session["FrmEditarDocumento.EstaAdjuntandoOEliminando"] == null)
                    HttpContext.Current.Session["FrmEditarDocumento.EstaAdjuntandoOEliminando"] = false;
                return Convert.ToBoolean(HttpContext.Current.Session["FrmEditarDocumento.EstaAdjuntandoOEliminando"]);
            }
            set
            {
                HttpContext.Current.Session["FrmEditarDocumento.EstaAdjuntandoOEliminando"] = value;
            }
        }

        private object IdDocCreadoSession
        {
            get
            {
                return HttpContext.Current.Session["IdDocCreadoSession"];
            }
            set
            {
                HttpContext.Current.Session["IdDocCreadoSession"] = value;
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

        public int IdDocCreado
        {
            get
            {
                int idDocCreado = 0;
                if (IdDocCreadoSession != null)
                    idDocCreado = Convert.ToInt32(IdDocCreadoSession);
                return idDocCreado;
            }
            set
            {
                IdDocCreadoSession = value;
            }
        }

        public int IdCategoria 
        {
            get
            {
                return Convert.ToInt32(ddlCategoria.SelectedValue);
            }
            set
            {
                ddlCategoria.SelectedValue = value.ToString();
            }
        }

        public int IdSubCategoria 
        {
            get
            {
                return Convert.ToInt32(ddlSubCategoria.SelectedValue);
            }
            set
            {
                ddlSubCategoria.SelectedValue = value.ToString();
            }
        }

        public int IdTipoDocumento 
        {
            get
            {
                return Convert.ToInt32(ddlTipoDocumento.SelectedValue);
            }
            set
            {
                ddlTipoDocumento.SelectedValue = value.ToString();
            }
        }

        public string CargoResponsable
        {
            get
            {
                return ddlResponsableDoc.SelectedValue;
            }
            set
            {
                ddlResponsableDoc.SelectedValue = value;
            }
        }

        public bool Activo
        {
            get { return true; }
            set { ViewState["EditarDocumento_Activo"] = value; }
        }

        #endregion

        #region Métodos

        void AddErrorMessages(List<string> messages)
        {
            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    var custVal = new CustomValidator();
                    custVal.IsValid = false;
                    custVal.ErrorMessage = msg;
                    custVal.EnableClientScript = false;
                    custVal.Display = ValidatorDisplay.None;
                    custVal.ValidationGroup = "vgGeneral";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }
        
        public void Responsables(List<DTO_ValueKey> responsables)
        {
            if (responsables.Any())
                responsables = responsables.OrderBy(x => x.Value).ToList();

            ddlResponsableDoc.DataSource = responsables;
            ddlResponsableDoc.DataTextField = "Value";
            ddlResponsableDoc.DataValueField = "Value";
            ddlResponsableDoc.DataBind();
        }

        public void LoadCategorias(List<TBL_ModuloDocumentos_Categorias> items)
        {
            ddlCategoria.DataSource = items;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();
        }

        public void LoadSubCategorias(List<TBL_ModuloDocumentos_Categorias> items)
        {
            ddlSubCategoria.DataSource = items;
            ddlSubCategoria.DataTextField = "Nombre";
            ddlSubCategoria.DataValueField = "IdCategoria";
            ddlSubCategoria.DataBind();
        }

        public void LoadTiposDocumento(List<TBL_ModuloDocumentos_Categorias> items)
        {
            ddlTipoDocumento.DataSource = items;
            ddlTipoDocumento.DataTextField = "Nombre";
            ddlTipoDocumento.DataValueField = "IdCategoria";
            ddlTipoDocumento.DataBind();
        }       

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptAdjuntos.DataSource = items;
            rptAdjuntos.DataBind();
        }

        public void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjunto adjunto)
        {
            DownloadDocument(adjunto.Archivo, adjunto.NombreArchivo, "application/octet-stream");
        }

        public void ShowAdminCategoria(bool visible)
        {
            if (visible)
                mpeAdminCategoria.Show();
            else
                mpeAdminCategoria.Hide();
        }

        #endregion

        public string IdModule
        {
            get { return ModuleId; }
        }

        protected void LnkBtnArchivoClick(object sender, EventArgs e)
        {
            var lnkBtnArchivo = (LinkButton) sender;
            if (DescargarArchivoEvent != null)
                DescargarArchivoEvent(Convert.ToInt32(lnkBtnArchivo.CommandArgument), EventArgs.Empty);
        }

        protected void ImgBtnEliminarClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            EstaAdjuntandoOEliminando = true;
            var imgBtnEliminar = (ImageButton) sender;
            if (EliminarAdjuntoEvent != null)
                EliminarAdjuntoEvent(imgBtnEliminar.CommandArgument, EventArgs.Empty);
        }

        protected void BtnAdjuntarClick(object sender, EventArgs e)
        {
            if (!FileUploadArchivo.HasFile)
                return;

            if (IdDocumento == 0)
            {
                var docAdjunto = new DTO_ValueKey();
                docAdjunto.Id = (ArchivosAdjuntos.Count + 1).ToString();
                docAdjunto.Value = NombreArchivo;
                docAdjunto.ComplexValue = Archivo;

                ArchivosAdjuntos.Add(docAdjunto);

                LoadArchivosAdjuntos(ArchivosAdjuntos);
            }
            else
            {
                GuardarAdjuntoEvent(null, EventArgs.Empty);
            }
        }

        public int IdNivelCategoria
        {
            get
            {
                if (ViewState["EditarDocumento_IdNivelCategoria"] == null)
                    ViewState["EditarDocumento_IdNivelCategoria"] = 1;

                return Convert.ToInt32(ViewState["EditarDocumento_IdNivelCategoria"]);
            }
            set
            {
                ViewState["EditarDocumento_IdNivelCategoria"] = value;
            }
        }

        public string TituloCategoriaAdmin
        {
            set { lblAdminCategoriaTitle.Text = value; }
        }

        public string NuevaCategoria
        {
            get
            {
                return txtCategoria.Text;
            }
            set
            {
                txtCategoria.Text = value;
            }
        }

        public List<DTO_ValueKey> ArchivosAdjuntos
        {
            get
            {
                if (Session["EditarDocumento_ArchivosAdjuntos"] == null)
                    Session["EditarDocumento_ArchivosAdjuntos"] = new List<DTO_ValueKey>();

                return Session["EditarDocumento_ArchivosAdjuntos"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["EditarDocumento_ArchivosAdjuntos"] = value;
            }
        }

        public void GoToViewDocument(int idDocumento)
        {
            Response.Redirect(string.Format("../Consulta/FrmVerDocumento.aspx?ModuleId={0}&IdDocumento={1}", IdModule, idDocumento));
        }

        public new string LogInfo
        {
            set { lblLogInfo.Text = value; }
        }

        public string Estado
        {
            set { lblEstado.Text = value; }
        }
    }
}