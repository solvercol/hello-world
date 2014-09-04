using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.Reclamos.IServices
{
    public interface ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices : IGenericServices<TBL_ModuloReclamos_ComentariosRespuesta>
    {
        TBL_ModuloReclamos_ComentariosRespuesta GetById(decimal id);
        List<TBL_ModuloReclamos_ComentariosRespuesta> GetByIdReclamo(decimal idReclamo);
        List<TBL_ModuloReclamos_ComentariosRespuesta> GetByIdComentarioRelacionado(decimal idComentario);
    }
}