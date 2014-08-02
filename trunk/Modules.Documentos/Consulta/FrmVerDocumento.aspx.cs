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

        #endregion

        #region Propiedades

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
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

        public byte[] Archivo
        {
            set
            {
                LnkBtnDescargar.Visible = (value.Length > 0);
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

        public bool Activo
        {
            get { return chkActiva.Checked; }
            set { chkActiva.Checked = value; }
        }

        #endregion

        #region Métodos

        public void DescargarArchivo(TBL_ModuloDocumentos_Documento documento)
        {
            ViewPage<EditarDocumentoPresenter, IEditarDocumentoView>
                .DownloadDocument(documento.Archivo, documento.NombreArchivo, "application/octet-stream");
        }        

        #endregion
    }
}