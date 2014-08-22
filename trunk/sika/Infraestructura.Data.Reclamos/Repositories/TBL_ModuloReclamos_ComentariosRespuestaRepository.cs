using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Domain.MainModules.Entities;
using Infraestructura.Data.Reclamos.Resources;
using Infraestructure.Data.Core;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infrastructure.Data.MainModule.Reclamos.Repositories
{
    public class TBL_ModuloReclamos_ComentariosRespuestaRepository : GenericRepository<TBL_ModuloReclamos_ComentariosRespuesta>, ITBL_ModuloReclamos_ComentariosRespuestaRepository 
    {
        public TBL_ModuloReclamos_ComentariosRespuestaRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }


        public TBL_ModuloReclamos_ComentariosRespuesta GetCompleteEntityBySpec(ISpecification<TBL_ModuloReclamos_ComentariosRespuesta> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_ComentariosRespuesta
                                    .Include(x => x.TBL_ModuloReclamos_ComentariosRespuesta2) // Comentario Relacionado
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Destinatario
                                    .Include(x => x.TBL_ModuloReclamos_AnexosComentarioRespuesta)    // Destinatario
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloReclamos_ComentariosRespuesta> GetCompleteListBySpec(ISpecification<TBL_ModuloReclamos_ComentariosRespuesta> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_ComentariosRespuesta
                                    .Include(x => x.TBL_ModuloReclamos_ComentariosRespuesta2) // Comentario Relacionado
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Destinatario
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }
    }
}
    