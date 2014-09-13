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
    public class TBL_ModuloReclamos_ActividadesRepository : GenericRepository<TBL_ModuloReclamos_Actividades>, ITBL_ModuloReclamos_ActividadesRepository 
    {
        readonly IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloReclamos_ActividadesRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloReclamos_Actividades GetCompleteEntityBySpec(ISpecification<TBL_ModuloReclamos_Actividades> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_Actividades
                                    .Include(x => x.TBL_ModuloReclamos_ActividadesReclamo)
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Asignado
                                    .Include(x => x.TBL_Admin_Usuarios3)    // Usuarios Copia
                                    .Include(x => x.TBL_ModuloReclamos_AnexosActividad)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloReclamos_Actividades> GetCompleteListBySpec(ISpecification<TBL_ModuloReclamos_Actividades> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_Actividades
                                    .Include(x => x.TBL_ModuloReclamos_ActividadesReclamo)
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Asignado
                                    .Include(x => x.TBL_Admin_Usuarios3)    // Usuarios Copia
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public bool VerificarActividadesPorEstadoPorreclamo(string estado, decimal idReclamo)
        {

            var set = _currentUnitOfWork.CreateSet<TBL_ModuloReclamos_Actividades>();

            var list = set.Where(c => c.Estado == estado && c.IdReclamo == idReclamo)
                          .Select(c => c)
                          .ToList();

            return list.Count > 0;

        }

      
    }
}
    