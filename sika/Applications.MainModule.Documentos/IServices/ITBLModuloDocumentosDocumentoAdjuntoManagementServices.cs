using Domain.Core;
using Domain.MainModules.Entities;
using System.Collections.Generic;

namespace Application.MainModule.Documentos.IServices
{
    public interface ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices : IGenericServices<TBL_ModuloDocumentos_DocumentoAdjunto>
    {
        List<TBL_ModuloDocumentos_DocumentoAdjunto> GetDocumentosAdjuntosByDocId(int idDocumento);
    }
}