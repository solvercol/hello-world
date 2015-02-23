using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Documentos.IViews;
using Presenters.Documentos.Presenters;

namespace Modules.Documentos.Consulta
{
    public partial class FrmVerDocumento
        : ViewPage<VerDocumentoPresenter, IVerDocumentoView>, IVerDocumentoView
    {
        #region Eventos

        public event EventHandler DescargarArchivoEvent;

        public string QueryStringFrom
        {
            get { return Request.QueryString.Get("from"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("{0}", SubCategoria));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            string url = string.Empty;

            switch (QueryStringFrom)
            {
                case "docspub":
                    url = string.Format("FrmDocumentosPublicados.aspx?ModuleId={0}", IdModule);
                    break;
                case "admindocs":
                    url = string.Format("../Admin/FrmTotalDocumentos.aspx?ModuleId={0}", IdModule);
                    break;
                case "tabledocs":
                    url = string.Format("../Views/FrmDocumentosTable.aspx?ModuleId={0}", IdModule);
                    break;
                default:
                    url = string.Format("../Admin/FrmMisDocumentos.aspx?ModuleId={0}", IdModule);
                    break;
            }

            Response.Redirect(url);
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("../Admin/FrmEditarDocumento.aspx?ModuleId={0}&IdDocumento={1}&from={2}", IdModule, IdDocumento, QueryStringFrom));
        }

        protected void LnkBtnDescargar_Click(object sender, EventArgs e)
        {
            if (DescargarArchivoEvent != null)
                DescargarArchivoEvent(null, EventArgs.Empty);
        }

        protected void lnkBtnArchivo_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnArchivo = (LinkButton)sender;
            if (DescargarArchivoEvent != null)
                DescargarArchivoEvent(Convert.ToInt32(lnkBtnArchivo.CommandArgument), EventArgs.Empty);
        }

        #endregion

        #region Propiedades

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
            set
            {
                txtTitulo.Text = value;
                LblTitulo.Text = value;
            }
        }

        public string Version
        {
            set
            {
                lblVersion.Text = value;
            }
        }
        
        public string Categoria
        {
            set
            {
                txtCategoria.Text = value;
            }
        }

        public string SubCategoria
        {
            get
            {
                return txtSubCategoria.Text;
            }
            set
            {
                txtSubCategoria.Text = value;
            }
        }

        public string TipoDocumento
        {          
            set
            {
                txtTipoDocumento.Text = value;
            }
        }

        public string UsuarioResponsable
        {
            set
            {
                txtResponsableDoc.Text = value;
            }
        }

        #endregion

        #region Métodos

        public void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjunto adjunto)
        {
            ViewPage<EditarDocumentoPresenter, IEditarDocumentoView>
                .DownloadDocument(adjunto.Archivo, adjunto.NombreArchivo, "application/octet-stream");
        }        

        #endregion

        public void Adjuntos(IEnumerable<TBL_ModuloDocumentos_DocumentoAdjunto> adjuntos)
        {
            rptAdjuntos.DataSource = adjuntos;
            rptAdjuntos.DataBind();

            trAnexos.Visible = adjuntos.Any();
        }


        public string IdModule
        {
            get { return ModuleId; }
        }

        public int IdRolAdministradorDocumentos
        {
            get
            {
                if (ViewState["VerDocumento_IdRolAdministradorDocumentos"] == null)
                    ViewState["VerDocumento_IdRolAdministradorDocumentos"] = 0;

                return Convert.ToInt32(ViewState["VerDocumento_IdRolAdministradorDocumentos"]);
            }
            set
            {
                ViewState["VerDocumento_IdRolAdministradorDocumentos"] = value;
            }
        }

        public bool CanEdit
        {
            get
            {
                return btnEditar.Visible;
            }
            set
            {
                btnEditar.Visible = value;
            }
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