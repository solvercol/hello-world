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
    public class TBL_ModuloReclamos_AlternativasRepository : GenericRepository<TBL_ModuloReclamos_Alternativas>, ITBL_ModuloReclamos_AlternativasRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloReclamos_AlternativasRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloReclamos_Alternativas GetCompleteEntityBySpec(ISpecification<TBL_ModuloReclamos_Alternativas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_Alternativas
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Responsable
                                    .Include(x => x.TBL_ModuloReclamos_AnexosAlternativa)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloReclamos_Alternativas> GetCompleteListBySpec(ISpecification<TBL_ModuloReclamos_Alternativas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_Alternativas
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Responsable
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }



        public TBL_ModuloReclamos_Alternativas GetAlternativaById(decimal id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_ModuloReclamos_Alternativas>();

                return set.Where(c => c.IdAlternativa == id)
                          .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                          .Include(x => x.TBL_Admin_Usuarios2)    // Responsable
                          .Include(x => x.TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_TipoReclamo)    // Reclamo
                          .Include(x => x.TBL_ModuloReclamos_AnexosAlternativa)    // Anxos
                          .Select(c => c)
                          .SingleOrDefault();
            }

            return null;
        }

    }
} 