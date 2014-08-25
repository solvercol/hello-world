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

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Calidad Total");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            string url = string.IsNullOrEmpty(FormRequest)
                                  ? string.Format("FrmDocumentosPublicados.aspx{0}", GetBaseQueryString())
                                  : string.Format("{0}{1}", FormRequest, GetBaseQueryString());
            Response.Redirect(url);
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

        public string Observaciones
        {
            set
            {
                txtObservaciones.Text = value;
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
        }


        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}