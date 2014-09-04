using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices : IGenericServices<TBL_ModuloReclamos_AnexosComentarioRespuesta>
    {
        List<TBL_ModuloReclamos_AnexosComentarioRespuesta> GetByComentarioId(decimal idComentario);
        TBL_ModuloReclamos_AnexosComentarioRespuesta GetById(decimal id);
    }
}