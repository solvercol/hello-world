//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por una plantilla T4 de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  Abril 24 de 2012.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System.Collections.Generic;
using Domain.Core;
using Domain.MainModules.Entities;

namespace Applcations.MainModule.DocumentLibrary.IServices
{
    public interface ISfTBL_ModuloDocumentosAnexos_ContenidoManagementServices : IGenericServices<TBL_ModuloDocumentosAnexos_Contenido>
    {
        bool BulkDeleteFromId(int id);

        int CountByIdFolder(int idFolder, string nombreArchivo);

        List<TBL_ModuloDocumentosAnexos_Contenido> FindByIdFolder(int idFolder, string nombreArchivo, int pageIndex,
                                                                  int pageCount);

        TBL_ModuloDocumentosAnexos_Contenido AddContenido(byte[] attach, string nameFile, string contentType,
                                                          string comentarios, string idUser);

    }
}
    