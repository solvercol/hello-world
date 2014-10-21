//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

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
    public class TBL_ModuloAPC_ActividadesRepository : GenericRepository<TBL_ModuloAPC_Actividades>, ITBL_ModuloAPC_ActividadesRepository 
    {
        readonly IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloAPC_ActividadesRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }
        public TBL_ModuloAPC_Actividades GetCompleteEntityBySpec(ISpecification<TBL_ModuloAPC_Actividades> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Actividades
                                    .Include(x => x.TBL_ModuloAPC_Solicitud)     // SOLICITUD
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Resposable Ejecucion
                                    .Include(x => x.TBL_Admin_Usuarios3)    // Resposable Seguimiento
                                    .Include(x => x.TBL_ModuloAPC_AnexosActividades)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<TBL_ModuloAPC_Actividades> GetCompleteListBySpec(ISpecification<TBL_ModuloAPC_Actividades> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloAPC_Actividades
                                    .Include(x => x.TBL_ModuloAPC_Solicitud)     // SOLICITUD
                                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                                    .Include(x => x.TBL_Admin_Usuarios2)    // Resposable Ejecucion
                                    .Include(x => x.TBL_Admin_Usuarios3)    // Resposable Seguimiento
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public bool VerificarActividadesPorEstadoPorsolicitud(string estado, decimal idSolicitud)
        {

            var set = _currentUnitOfWork.CreateSet<TBL_ModuloAPC_Actividades>();

            var list = set.Where(c => c.EstadoActividad == estado && c.IdSolicitudAPC == idSolicitud)
                          .Select(c => c)
                          .ToList();

            return list.Count > 0;

        }


        public TBL_ModuloAPC_Actividades GetActividadById(decimal id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_ModuloAPC_Actividades>();

                return set.Where(c => c.IdActividad == id)
                    .Include(x => x.TBL_ModuloAPC_Solicitud)     // SOLICITUD
                    .Include(x => x.TBL_Admin_Usuarios)     // Creado Por
                    .Include(x => x.TBL_Admin_Usuarios1)    // Modificado Por
                    .Include(x => x.TBL_Admin_Usuarios2)    // Resposable Ejecucion
                    .Include(x => x.TBL_Admin_Usuarios3)    // Resposable Seguimiento
                    .Include(x => x.TBL_ModuloAPC_AnexosActividades)
                    .Select(c => c)
                    .SingleOrDefault();
            }
            return null;
        }
    }
}
    