using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.MainModule.Documentos.IServices;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Presenters.Documentos.IViews;
using Presenters.Documentos.Presenters;

namespace Modules.Documentos.Consulta
{
    public partial class FrmDocumentosPublicados
        : ViewPage<VistaDocumentosPublicadosPresenter, IVistaDocumentosPublicadosView>, IVistaDocumentosPublicadosView
    {
        public TreeNodeCollection ColeccionNodos
        {
            get
            {
                if (HttpContext.Current.Session["Documento.ColeccionNodos"] == null)
                    HttpContext.Current.Session["Documento.ColeccionNodos"] = ConstruirArbol();
                return HttpContext.Current.Session["Documento.ColeccionNodos"] as TreeNodeCollection;
            }
            set
            {
                HttpContext.Current.Session["Documento.ColeccionNodos"] = value;
            }
        }

        private List<TBL_ModuloDocumentos_Documento> listaDocumentos;
        public List<TBL_ModuloDocumentos_Documento> ListaDocumentos
        {
            get
            {
                return listaDocumentos;
            }
            set
            {
                ColeccionNodos = null;
                listaDocumentos = value;
            }
        }

        public string FiltroNombre
        {
            get
            {
                return txtFiltroNombre.Text;
            }
            set
            {
                txtFiltroNombre.Text = value;
            }
        }

        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Documentos Publicados");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public void ArbolDocumentos()
        {
            TrvwDocumentos.Nodes.Clear();
            foreach (TreeNode nodo in ColeccionNodos)
                TrvwDocumentos.Nodes.Add(nodo);
            TrvwDocumentos.CollapseAll();
        }

        public TreeNodeCollection ConstruirArbol()
        {
            ISfTBL_ModuloDocumentos_CategoriasManagementServices _categoriaServices;
            TBL_ModuloDocumentos_Categorias categoria = null;
            TBL_ModuloDocumentos_Categorias subCategoria = null;
            TBL_ModuloDocumentos_Categorias tipoDocumento = null;
            int idCatAnt = 0; int idSubCatAnt = 0; int idTipoDocAnt = 0;
            bool cambioCat = false; bool cambioSubCat = false;
            TreeNode tNodeCat = null; TreeNode tNodeSubCat = null;
            TreeNode tNodeTipoDoc = null; TreeNode tNodeDoc = null;
            TreeNodeCollection Nodos = null;
            try
            {
                Nodos = new TreeNodeCollection();
                _categoriaServices = IoC.Resolve<ISfTBL_ModuloDocumentos_CategoriasManagementServices>();
                if (ListaDocumentos.Count() == 0)
                    return Nodos;
                var list = ListaDocumentos.OrderBy(cat => cat.IdCategoria).ThenBy(sub => sub.IdSubCategoria).ThenBy(tdoc => tdoc.IdTipo);
                foreach (var documento in list)
                {
                    if (documento.IdCategoria != idCatAnt)
                    {
                        categoria = _categoriaServices.FindById(documento.IdCategoria);
                        tNodeCat = new TreeNode(categoria.Nombre.ToUpper(), categoria.IdCategoria.ToString());
                        tNodeCat.SelectAction = TreeNodeSelectAction.None;
                        Nodos.Add(tNodeCat);
                        cambioCat = true;
                    }
                    if (documento.IdSubCategoria != idSubCatAnt || cambioCat)
                    {
                        subCategoria = _categoriaServices.FindById(documento.IdSubCategoria);
                        tNodeSubCat = new TreeNode(subCategoria.Nombre.ToUpper());
                        tNodeSubCat.SelectAction = TreeNodeSelectAction.None;
                        tNodeCat.ChildNodes.Add(tNodeSubCat);
                        cambioSubCat = true;
                    }
                    if (documento.IdTipo != idTipoDocAnt || cambioSubCat)
                    {
                        tipoDocumento = _categoriaServices.FindById(documento.IdTipo);
                        tNodeTipoDoc = new TreeNode(tipoDocumento.Nombre.ToUpper());
                        tNodeTipoDoc.SelectAction = TreeNodeSelectAction.None;
                        tNodeSubCat.ChildNodes.Add(tNodeTipoDoc);
                    }

                    tNodeDoc = new TreeNode(documento.Titulo, documento.IdDocumento.ToString());
                    tNodeDoc.NavigateUrl = string.Format("~/pages/modules/documentos/Consulta/FrmVerDocumento.aspx?ModuleId={0}&IdDocumento={1}", ModuleId, documento.IdDocumento);
                    tNodeTipoDoc.ChildNodes.Add(tNodeDoc);

                    idCatAnt = documento.IdCategoria;
                    idSubCatAnt = documento.IdSubCategoria;
                    idTipoDocAnt = documento.IdTipo;
                    cambioCat = false;
                    cambioSubCat = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el arbol: " + ex.Message);
            }
            return Nodos;
        }

        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}