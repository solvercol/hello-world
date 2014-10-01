using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using AjaxControlToolkit;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Application.MainModule.SqlServices.IServices;
using System.Data;

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

        private readonly IReclamosAdoService _adoServices;
        private List<TBL_ModuloDocumentos_Categorias> AllCategorias;
        private List<TBL_ModuloDocumentos_Categorias> Categorias;
        private List<TBL_ModuloDocumentos_Categorias> SubCategorias;
        private List<TBL_ModuloDocumentos_Categorias> TipoDocumentos;

        public WSScripts()
        {
            _adoServices = IoC.Resolve<IReclamosAdoService>();
          
        }

        #region Eventos de Autocompletar

        [WebMethod]
        [ScriptMethod]
        public string[] buscarUnidad(string prefixText)
        {
                DataTable DT_Resultado = _adoServices.Search_Unidad(prefixText);

                List<string> UnidadList = new List<string>();


                foreach (DataRow row in DT_Resultado.Rows)
                {
                    UnidadList.Add(row[0].ToString());
                }


                string[] strArray = UnidadList.ToArray();
                return strArray;
        }

        [WebMethod]
        [ScriptMethod]
        public string[] buscarZona(string prefixText)
        {
            DataTable DT_Resultado = _adoServices.Search_Zona(prefixText);

            List<string> ZonaList = new List<string>();


            foreach (DataRow row in DT_Resultado.Rows)
            {
                ZonaList.Add(row[0].ToString());
            }


            string[] strArray = ZonaList.ToArray();
           return strArray;
        }

        #endregion
    }
}
