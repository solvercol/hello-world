using System;
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
using System.Collections.Generic;

namespace Infrastructure.Data.MainModule.Reclamos.Repositories
{
    public class TBL_ModuloReclamos_DocumentosAnexoReclamoRepository : GenericRepository<TBL_ModuloReclamos_DocumentosAnexoReclamo>, ITBL_ModuloReclamos_DocumentosAnexoReclamoRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloReclamos_DocumentosAnexoReclamoRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloReclamos_DocumentosAnexoReclamo GetCompleteEntity(ISpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_DocumentosAnexoReclamo
                                    .Include(x => x.TBL_Admin_Usuarios) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // MOdifie                                    
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloReclamos_DocumentosAnexoReclamo> GetCompleteEntityList(ISpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_DocumentosAnexoReclamo
                                    .Include(x => x.TBL_Admin_Usuarios) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // MOdifie    
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