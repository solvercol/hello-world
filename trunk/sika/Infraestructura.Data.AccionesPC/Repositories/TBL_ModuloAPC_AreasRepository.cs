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
    public class TBL_ModuloAPC_AreasRepository : GenericRepository<TBL_ModuloAPC_Areas>, ITBL_ModuloAPC_AreasRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloAPC_AreasRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloAPC_Areas GetCompleteEntity(ISpecification<TBL_ModuloAPC_Areas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Areas
                                    .Include(x => x.TBL_Admin_Usuarios) //CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // Gerente
                                    .Include(x => x.TBL_Admin_Usuarios2) // ModifiedBy
                                    .Include(x => x.TBL_ModuloAPC_Solicitud)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloAPC_Areas> GetCompleteEntityList(ISpecification<TBL_ModuloAPC_Areas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Areas
                                    .Include(x => x.TBL_Admin_Usuarios) //CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // Gerente
                                    .Include(x => x.TBL_Admin_Usuarios2) // ModifiedBy
                                    .Include(x => x.TBL_ModuloAPC_Solicitud)
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public TBL_ModuloAPC_Areas GetEntityWithGerente(ISpecification<TBL_ModuloAPC_Areas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Areas
                                    .Include(x => x.TBL_Admin_Usuarios) //CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // Gerente
                                    .Include(x => x.TBL_Admin_Usuarios2) // ModifiedBy
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloAPC_Areas> GetEntityListWithGerente(ISpecification<TBL_ModuloAPC_Areas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Areas
                                    .Include(x => x.TBL_Admin_Usuarios) //CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // Gerente
                                    .Include(x => x.TBL_Admin_Usuarios2) // ModifiedBy
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