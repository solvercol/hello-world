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
    public class TBL_ModuloAPC_SolicitudRepository : GenericRepository<TBL_ModuloAPC_Solicitud>, ITBL_ModuloAPC_SolicitudRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloAPC_SolicitudRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloAPC_Solicitud GetCompleteEntity(ISpecification<TBL_ModuloAPC_Solicitud> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Solicitud
                                    .Include(x => x.TBL_Admin_Usuarios)  // IdAdministrador
                                    .Include(x => x.TBL_Admin_Usuarios1) // IdAdministradorFuncional
                                    .Include(x => x.TBL_Admin_Usuarios2) // IdUsuarioCierre
                                    .Include(x => x.TBL_Admin_Usuarios3) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios4) // IdGerente
                                    .Include(x => x.TBL_Admin_Usuarios5) // ModifiedBy
                                    .Include(x => x.TBL_Admin_Usuarios6) // IdResponsableActual
                                    .Include(x => x.TBL_Admin_Usuarios7) // IdResponsableEjecucion
                                    .Include(x => x.TBL_Admin_Usuarios8) // IdResponsableSeguimiento
                                    .Include(x => x.TBL_Admin_Usuarios9) // IdSolicitante
                                    .Include(x => x.TBL_Admin_EstadosProceso)
                                    .Include(x => x.TBL_ModuloAPC_Areas)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloAPC_Solicitud> GetCompleteEntityList(ISpecification<TBL_ModuloAPC_Solicitud> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Solicitud
                                    .Include(x => x.TBL_Admin_Usuarios)  // IdAdministrador
                                    .Include(x => x.TBL_Admin_Usuarios1) // IdAdministradorFuncional
                                    .Include(x => x.TBL_Admin_Usuarios2) // IdUsuarioCierre
                                    .Include(x => x.TBL_Admin_Usuarios3) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios4) // IdGerente
                                    .Include(x => x.TBL_Admin_Usuarios5) // ModifiedBy
                                    .Include(x => x.TBL_Admin_Usuarios6) // IdResponsableActual
                                    .Include(x => x.TBL_Admin_Usuarios7) // IdResponsableEjecucion
                                    .Include(x => x.TBL_Admin_Usuarios8) // IdResponsableSeguimiento
                                    .Include(x => x.TBL_Admin_Usuarios9) // IdSolicitante
                                    .Include(x => x.TBL_Admin_EstadosProceso)
                                    .Include(x => x.TBL_ModuloAPC_Areas)
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public TBL_ModuloAPC_Solicitud GetSolicitudById(decimal id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_ModuloAPC_Solicitud>();

                return set.Where(c => c.IdSolucitudAPC == id)
                                    .Include(x => x.TBL_Admin_Usuarios6) // IdResponsableActual
                                    .Include(x => x.TBL_Admin_Usuarios9) // IdSolicitante
                                    .Include(x => x.TBL_ModuloAPC_Areas) // Areas
                                    .Include(x => x.TBL_Admin_EstadosProceso)
                          .Select(c => c)
                          .SingleOrDefault();
            }
            return null;
        }


        public TBL_ModuloAPC_Solicitud GetSolicitudCierreWf(decimal id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_ModuloAPC_Solicitud>();

                return set.Where(c => c.IdSolucitudAPC == id)
                                    .Include(x => x.TBL_Admin_Usuarios9) // Solicitante
                                    .Include(x => x.TBL_ModuloAPC_Areas) // Areas
                                    .Include(x => x.TBL_Admin_EstadosProceso)
                                    .Include(x => x.TBL_ModuloAPC_Actividades.Select(y => y.TBL_Admin_Usuarios3)) // Resposable Seguimiento
                                    .Include(x => x.TBL_ModuloAPC_Actividades.Select(y => y.TBL_Admin_Usuarios2)) //Resposable Ejecucion
                          .Select(c => c)
                          .Select(c => c)
                          .SingleOrDefault();
            }
            return null;
        }

    }
}
    