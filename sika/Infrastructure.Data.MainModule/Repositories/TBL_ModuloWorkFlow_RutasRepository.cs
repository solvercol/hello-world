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
using Domain.MainModule.WorkFlow.Contracts;
using Infraestructure.Data.Core;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting.Logging;
using Domain.MainModules.Entities;
using Infrastructure.Data.MainModule.Resources;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infrastructure.Data.MainModule.Repositories
{
    public class TBL_ModuloWorkFlow_RutasRepository : GenericRepository<TBL_ModuloWorkFlow_Rutas>, ITBL_ModuloWorkFlow_RutasRepository
    {
        //private IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloWorkFlow_RutasRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            //_currentUnitOfWork = unitOfWork;
        }

        public List<TBL_ModuloWorkFlow_Rutas> FindRutasBySpec(ISpecification<TBL_ModuloWorkFlow_Rutas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");


            //var set = _currentUnitOfWork.CreateSet<TBL_ModuloWorkFlow_Rutas>();
            //return set.Include()

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloWorkFlow_Rutas
                                    .Include(r => r.TBL_Admin_EstadosProceso) //Estado Origen
                                    .Include(r => r.TBL_Admin_EstadosProceso1) //Estado Destino
                                    .Include(x=> x.TBL_ModuloWorkFlow_ValidacionesSalida)
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
    