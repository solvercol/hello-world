using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using AjaxControlToolkit;
using Application.MainModule.Documentos.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;

namespace Modules.Documentos.Admin
{
    /// <summary>
    /// Descripción breve de WSAutocompletar
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [ScriptService]
    public class WSScripts 
        : WebService
    {

        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices _categoriasServices;
        private List<TBL_ModuloDocumentos_Categorias> AllCategorias;
        private List<TBL_ModuloDocumentos_Categorias> Categorias;
        private List<TBL_ModuloDocumentos_Categorias> SubCategorias;
        private List<TBL_ModuloDocumentos_Categorias> TipoDocumentos;

        public WSScripts()
        {
            _categoriasServices = IoC.Resolve<ISfTBL_ModuloDocumentos_CategoriasManagementServices>();
            if (_categoriasServices == null)
                throw new Exception("_categoriasServices es nulo");
            AllCategorias = _categoriasServices.FindBySpec(true);
            Categorias = AllCategorias.FindAll(cat => cat.Nivel == 1);
            SubCategorias = AllCategorias.FindAll(cat => cat.Nivel == 2);
            TipoDocumentos = AllCategorias.FindAll(cat => cat.Nivel == 3);
        }

        #region Eventos de Autocompletar

        [WebMethod]
        [ScriptMethod]
        public string[] GetCategorias(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>();
            var list = (from cat in Categorias where cat.Nombre.ToUpper().Contains(prefixText.ToUpper()) select cat);
            foreach (var item in list)
                items.Add(AutoCompleteExtender.CreateAutoCompleteItem(item.Nombre.ToUpper(), item.IdCategoria.ToString()));
            return items.ToArray();
        }

        [WebMethod]
        [ScriptMethod]
        public string[] GetSubCategorias(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>();
            var list = (from subcat in SubCategorias where subcat.Nombre.ToUpper().Contains(prefixText.ToUpper()) select subcat);
            foreach (var item in list)
                items.Add(AutoCompleteExtender.CreateAutoCompleteItem(item.Nombre.ToUpper() + "\n", item.IdCategoria.ToString()));
            return items.ToArray();
        }

        [WebMethod]
        [ScriptMethod]
        public string[] GetTipoDocumentos(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>();
            var list = (from tipodoc in TipoDocumentos where tipodoc.Nombre.ToUpper().Contains(prefixText.ToUpper()) select tipodoc);
            foreach (var item in list)
                items.Add(AutoCompleteExtender.CreateAutoCompleteItem(item.Nombre.ToUpper() + "\n", item.IdCategoria.ToString()));
            return items.ToArray();
        }

        #endregion
    }
}
