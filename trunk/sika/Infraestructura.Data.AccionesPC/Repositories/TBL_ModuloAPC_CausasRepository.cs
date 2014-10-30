using System;
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
using System.Collections.Generic;

namespace Infrastructure.Data.MainModule.AccionesPC.Repositories
{
    public class TBL_ModuloAPC_CausasRepository : GenericRepository<TBL_ModuloAPC_Causas>, ITBL_ModuloAPC_CausasRepository
    {
        public TBL_ModuloAPC_CausasRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }

        public TBL_ModuloAPC_Causas GetCompleteEntity(ISpecification<TBL_ModuloAPC_Causas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Causas
                                    .Include(x => x.TBL_Admin_Usuarios)  // Create By
                                    .Include(x => x.TBL_Admin_Usuarios1) // Modified By
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloAPC_Causas> GetCompleteEntityList(ISpecification<TBL_ModuloAPC_Causas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Causas
                                    .Include(x => x.TBL_Admin_Usuarios)  // Create By
                                    .Include(x => x.TBL_Admin_Usuarios1) // Modified By                                    
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