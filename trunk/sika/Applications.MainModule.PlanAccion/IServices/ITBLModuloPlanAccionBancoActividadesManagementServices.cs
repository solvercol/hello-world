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

namespace Applications.MainModule.PlanAccion.IServices
{
    public interface ISfTBL_ModuloPlanAccion_BancoActividadesManagementServices : IGenericServices<TBL_ModuloPlanAccion_BancoActividades>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int CountbyPaged();
    }
}
    