using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Application.MainModule.AccionesPC.IServices
{
    public interface ISfTBL_ModuloAPC_ComentariosRespuestaManagementServices : IGenericServices<TBL_ModuloAPC_ComentariosRespuesta>
    {
        TBL_ModuloAPC_ComentariosRespuesta GetById(decimal id);
        List<TBL_ModuloAPC_ComentariosRespuesta> GetByIdSolicitud(decimal idSolicitud);
        List<TBL_ModuloAPC_ComentariosRespuesta> GetByIdComentarioRelacionado(decimal idComentario);

    }
}
    