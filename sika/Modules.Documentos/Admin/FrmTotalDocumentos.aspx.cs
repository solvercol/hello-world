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

namespace Modules.Documentos.Admin
{
    public partial class FrmTotalDocumentos
        : ViewPage<VistaTotalDocumentosPresenter, IVistaTotalDocumentosView>, IVistaTotalDocumentosView
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

        public int FiltroIdEstado
        {
            get
            {
                return Convert.ToInt32(ddlEstado.SelectedValue);
            }
            set
            {
                ddlEstado.SelectedIndex = -1;
                ddlEstado.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        public int FiltroIdResponsable
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

        public event EventHandler FilterEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ClearFilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Total Documentos");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnNuevoClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditarDocumento.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        protected void BtnClearFilterClick(object sender, EventArgs e)
        {
            if (ClearFilterEvent != null)
                ClearFilterEvent(null, EventArgs.Empty);
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

        public void Estados(IEnumerable<TBL_ModuloDocumentos_Estados> estados)
        {
            ddlEstado.Items.Clear();
            foreach (var est in estados)
                ddlEstado.Items.Add(new ListItem(est.Nombre, est.IdEstado.ToString()));
            var li = new ListItem("Seleccione", "0");
            ddlEstado.Items.Insert(0, li);
        }

        public void Responsables(IEnumerable<TBL_Admin_Usuarios> responsables)
        {
            ddlResponsableDoc.Items.Clear();
            foreach (var rp in responsables)
                ddlResponsableDoc.Items.Add(new ListItem(rp.Nombres, rp.IdUser.ToString()));
            var li = new ListItem("Seleccione", "0");
            ddlResponsableDoc.Items.Insert(0, li);
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
                    tNodeDoc.NavigateUrl = string.Format("~/pages/modules/documentos/Admin/FrmEditarDocumento.aspx?IdDocumento={0}", documento.IdDocumento);
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