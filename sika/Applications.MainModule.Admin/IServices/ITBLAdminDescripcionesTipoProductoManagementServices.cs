//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por una plantilla T4 de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  Abril 24 de 2012.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using Domain.Core;
using Domain.MainModules.Entities;

namespace Applications.MainModule.Admin.IServices
{
    public interface ISfTBL_Admin_DescripcionesTipoProductoManagementServices : IGenericServices<TBL_Admin_DescripcionesTipoProducto>
    {

        /// <summary>
        /// Obtiene el objeto descrición filtrado por el Codigo proveniente de la referencia.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        TBL_Admin_DescripcionesTipoProducto ObtenerDescripcionPorCodigo(string codigo);
    }
}
    