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
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Applications.MainModule.Admin.IServices
{
    public interface ISfTBL_Admin_EstadosProcesoManagementServices : IGenericServices<TBL_Admin_EstadosProceso>
    {
        /// <summary>
        /// Retorna el objeto estado filtrado por tipo de modulo y nombre del estado.
        /// </summary>
        /// <param name="tipoModulo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        TBL_Admin_EstadosProceso EstadoPorTipoModuloNombreEstado(ModulosAplicacion tipoModulo, EstadosAplicacion estado);
    }
}
    