﻿using System;
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
    public partial class FrmMisDocumentos
        : ViewPage<VistaMisDocumentosPresenter, IVistaMisDocumentosView>, IVistaMisDocumentosView
    {

        public string QueryStringFrom
        {
            get { return Request.QueryString.Get("from"); }
        }

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

        public event EventHandler FilterEvent;
        public event EventHandler ClearFilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Mis Documentos");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void BtnNuevoClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditarDocumento.aspx{0}{1}", GetBaseQueryString(), "&Form=FrmMisDocumentos.aspx"));
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


        public TreeNodeCollection ConstruirArbol()
        {
            int idCatAnt = 0; int idSubCatAnt = 0; int idTipoDocAnt = 0;
            bool cambioCat = false; bool cambioSubCat = false;
            TreeNode tNodeCat = null; TreeNode tNodeSubCat = null;
            TreeNode tNodeTipoDoc = null; TreeNode tNodeDoc = null;
            TreeNodeCollection Nodos = null;
            try
            {
                Nodos = new TreeNodeCollection();
                if (ListaDocumentos.Count() == 0)
                    return Nodos;
                var list = ListaDocumentos.OrderBy(cat => cat.IdCategoria).ThenBy(sub => sub.IdSubCategoria).ThenBy(tdoc => tdoc.IdTipo);
                foreach (var documento in list)
                {
                    if (documento.IdCategoria != idCatAnt)
                    {
                        tNodeCat = new TreeNode(documento.TBL_ModuloDocumentos_Categorias.Nombre.ToUpper(), documento.IdCategoria.ToString());
                        tNodeCat.SelectAction = TreeNodeSelectAction.None;
                        Nodos.Add(tNodeCat);
                        cambioCat = true;
                    }
                    if (documento.IdSubCategoria != idSubCatAnt || cambioCat)
                    {
                        tNodeSubCat = new TreeNode(documento.TBL_ModuloDocumentos_Categorias1.Nombre.ToUpper(),documento.IdSubCategoria.ToString());
                        tNodeSubCat.SelectAction = TreeNodeSelectAction.None;
                        tNodeCat.ChildNodes.Add(tNodeSubCat);
                        cambioSubCat = true;
                    }
                    if (documento.IdTipo != idTipoDocAnt || cambioSubCat)
                    {
                        tNodeTipoDoc = new TreeNode(documento.TBL_ModuloDocumentos_Categorias2.Nombre.ToUpper(), documento.IdTipo.ToString());
                        tNodeTipoDoc.SelectAction = TreeNodeSelectAction.None;
                        tNodeSubCat.ChildNodes.Add(tNodeTipoDoc);
                    }

                    tNodeDoc = new TreeNode(documento.Titulo, documento.IdDocumento.ToString());

                    if (!documento.TBL_ModuloDocumentos_Estados.Codigo.Equals("CANCELADO"))
                        tNodeDoc.NavigateUrl =
                            string.Format(
                                "../Consulta/FrmVerDocumento.aspx?ModuleId={0}&IdDocumento={1}&from={2}",
                                ModuleId, documento.IdDocumento, "misdocs");
                    else
                        tNodeDoc.SelectAction = TreeNodeSelectAction.None;

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