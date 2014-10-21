using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Core.Specification;
using Domain.MainModule.AccionesPC.Contracts;
using Domain.MainModules.Entities;
using Infraestructura.Data.AccionesPC.Resources;
using Infraestructure.Data.Core;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infrastructure.Data.MainModule.AccionesPC.Repositories
{
    public class TBL_ModuloAPC_ComentariosRespuestaRepository : GenericRepository<TBL_ModuloAPC_ComentariosRespuesta>, ITBL_ModuloAPC_ComentariosRespuestaRepository 
    {

        private IMainModuleUnitOfWork _currentUnitOfWork;


        public TBL_ModuloAPC_ComentariosRespuestaRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloAPC_ComentariosRespuesta GetCompleteEntityBySpec(ISpecification<TBL_ModuloAPC_ComentariosRespuesta> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            if (_currentUnitOfWork != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return _currentUnitOfWork.TBL_ModuloAPC_ComentariosRespuesta
                                    .Include(x => x.TBL_ModuloAPC_ComentariosRespuesta2) // Comentario Relacionado
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Destinatario no existe
                                    .Include(x => x.TBL_Admin_Usuarios3)//copia 
                                    .Include(x => x.TBL_ModuloAPC_AnexosComentarioRespuesta)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloAPC_ComentariosRespuesta> GetCompleteListBySpec(ISpecification<TBL_ModuloAPC_ComentariosRespuesta> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");
              var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return _currentUnitOfWork.TBL_ModuloAPC_ComentariosRespuesta
                                    .Include(x => x.TBL_ModuloAPC_ComentariosRespuesta2) // Comentario Relacionado
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Destinatario no existe
                                    .Include(x => x.TBL_Admin_Usuarios3)//copia 
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public TBL_ModuloAPC_ComentariosRespuesta GetComentarioById(decimal id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_ModuloAPC_ComentariosRespuesta>();

                return set.Where(c => c.IdComentario == id)
                          .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                          .Include(x => x.TBL_Admin_Usuarios2)    // Destinatario
                           .Include(x => x.TBL_Admin_Usuarios3)    // CC
                          .Include(x => x.TBL_ModuloAPC_Solicitud)    // Reclamo
                          .Include(x => x.TBL_ModuloAPC_AnexosComentarioRespuesta)    // Anxos
                          .Select(c => c)
                          .SingleOrDefault();
            }

            return null;
        }
    }
}
    