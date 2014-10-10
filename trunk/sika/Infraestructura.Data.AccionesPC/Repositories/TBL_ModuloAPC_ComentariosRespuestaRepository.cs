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
                                    .Include(x => x.TBL_ModuloAPC_UsuarioCopiaComentariosRespuesta)//copia 
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
                                    .Include(x=> x.TBL_ModuloAPC_UsuarioCopiaComentariosRespuesta)//copia 
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
    