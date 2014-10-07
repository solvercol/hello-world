using Domain.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Application.MainModule.Documentos.IServices
{
    public interface ISfTBL_ModuloDocumentos_CategoriasManagementServices : IGenericServices<TBL_ModuloDocumentos_Categorias>
    {
        List<TBL_ModuloDocumentos_Categorias> GetByNivel(int nivel);
    }
}