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
    public class TBL_ModuloReclamos_ReclamoRepository : GenericRepository<TBL_ModuloReclamos_Reclamo>, ITBL_ModuloReclamos_ReclamoRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloReclamos_ReclamoRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloReclamos_Reclamo GetCompleteEntity(ISpecification<TBL_ModuloReclamos_Reclamo> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_Reclamo
                                    .Include(x => x.TBL_Admin_Usuarios)     // Responsable Actual
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Asesorado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Atendido Por
                                    .Include(x => x.TBL_Admin_Usuarios3)    // CreadoPor
                                    .Include(x => x.TBL_Admin_Usuarios4)    // Ingeniero Responsable
                                    .Include(x => x.TBL_Admin_Usuarios5)    // Modified By
                                    .Include(x => x.TBL_Admin_Usuarios6)    // Solicitante// Usuario Cierre
                                    .Include(x => x.TBL_Admin_Usuarios7)    // Usuario Cierre
                                    .Include(x => x.TBL_Admin_EstadosProceso)
                                    .Include(x => x.TBL_ModuloReclamos_CategoriasReclamo)
                                    .Include(x => x.TBL_ModuloReclamos_CategoriasReclamo.TBL_Admin_Usuarios)
                                    .Include(x => x.TBL_ModuloReclamos_TipoReclamo)                                    
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloReclamos_Reclamo> GetCompleteEntityList(ISpecification<TBL_ModuloReclamos_Reclamo> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloReclamos_Reclamo
                                    .Include(x => x.TBL_Admin_Usuarios)     // Asesorado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Atendido Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // CreadoPor
                                    .Include(x => x.TBL_Admin_Usuarios3)    // Ingeniero Responsable
                                    .Include(x => x.TBL_Admin_Usuarios4)    // Modified By
                                    .Include(x => x.TBL_Admin_Usuarios5)    // Solicitante
                                    .Include(x => x.TBL_Admin_Usuarios6)    // Usuario Cierre
                                    .Include(x => x.TBL_ModuloReclamos_CategoriasReclamo)
                                    .Include(x => x.TBL_ModuloReclamos_TipoReclamo)
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public TBL_ModuloReclamos_Reclamo GetReclamoById(int id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_ModuloReclamos_Reclamo>();

                return set.Where(c => c.IdReclamo == id)
                          .Include(x => x.TBL_ModuloReclamos_TipoReclamo)
                          .Include(x => x.TBL_Admin_Usuarios)     // Responsable Actual
                          .Include(x => x.TBL_Admin_Usuarios3)
                          .Select(c => c)
                          .SingleOrDefault();
            }
            //var actualContext = UnitOfWork as IMainModuleUnitOfWork;
            //if (actualContext != null)
            //{
            //    return (from rec in actualContext.TBL_ModuloReclamos_Reclamo
            //            join o in actualContext.TBL_Admin_Usuarios
            //            on rec.IdResponsableActual equals o.IdUser
            //            join ir in  actualContext.TBL_Admin_Usuarios
            //            on rec.IdIngenieroResponsable equals ir.IdUser
            //            join tr in actualContext.TBL_ModuloReclamos_TipoReclamo
            //            on rec.IdTipoReclamo equals tr.IdTipoReclamo
            //            where rec.IdReclamo == id
            //            select
            //                rec).SingleOrDefault();
            //}

            return null;
        }
    }
}
    