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
    public partial class FrmVerHistDocumento
        : ViewPage<VerHistDocumentoPresenter, IVerHistDocumentoView>, IVerHistDocumentoView
    {
        #region Eventos

        public event EventHandler DescargarArchivoEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Calidad Total - Historico");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnRegresarClick(object sender, EventArgs e)
        {
            string url =
                string.Format("~/pages/modules/documentos/Consulta/FrmVerDocumento.aspx?ModuleId={0}&IdDocumento={1}",
                              ModuleId, IdDocumento);
            Response.Redirect(url);
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

        public int IdHistDocumento
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["IdHistDocumento"]))
                    id = Convert.ToInt32(Request.QueryString["IdHistDocumento"]);
                return id;

            }
        }

        public decimal IdDocumento
        {
            get
            {
                return Convert.ToDecimal(HdfIdDocumento.Value);
            }
            set
            {
                HdfIdDocumento.Value = value.ToString();
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

        public void DescargarArchivo(TBL_ModuloDocumentos_DocumentoAdjuntoHistorial adjunto)
        {
            ViewPage<EditarDocumentoPresenter, IEditarDocumentoView>
                .DownloadDocument(adjunto.Archivo, adjunto.NombreArchivo, "application/octet-stream");
        }

        public void Adjuntos(IEnumerable<TBL_ModuloDocumentos_DocumentoAdjuntoHistorial> adjuntos)
        {
            rptAdjuntos.DataSource = adjuntos;
            rptAdjuntos.DataBind();
        }


        #endregion

        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}