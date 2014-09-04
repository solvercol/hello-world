//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using Domain.Core;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Contracts
{
    public interface ITBL_Admin_UsuariosRepository : IRepository<TBL_Admin_Usuarios>
    {
        /// <summary>
        /// Retorna el objeto usuario con informacion asociada de la tabla roles.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        TBL_Admin_Usuarios RetornarUsuarioConRoles(ISpecification<TBL_Admin_Usuarios> specification);

        /// <summary>
        /// Retorna el responsable del estado del Work Flow utilizando el rol del mismo como parámetro de busqueda
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        TBL_Admin_Usuarios RetornarUsuarioReponsableAprobacion(string role);

        /// <summary>
        /// Obtiene el usuario filtrado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TBL_Admin_Usuarios GetUsuarioById(int id);

        /// <summary>
        /// Retorna el usuario Autor del Documento (pedido)
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns></returns>
        TBL_Admin_Usuarios RetornarUsuarioAutordocumento(int idDocumento);

        /// <summary>
        /// Retorna el usuario responsable del reclamo
        /// </summary>
        /// <param name="idReclamo"></param>
        /// <returns></returns>
        TBL_Admin_Usuarios RetornarUsuarioResponsabledocumento(int idReclamo);
    }
}
    