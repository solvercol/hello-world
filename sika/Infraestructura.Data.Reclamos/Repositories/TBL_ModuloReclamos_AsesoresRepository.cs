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
    public class TBL_ModuloReclamos_AsesoresRepository : GenericRepository<TBL_ModuloReclamos_Asesores>, ITBL_ModuloReclamos_AsesoresRepository 
    {
        public TBL_ModuloReclamos_AsesoresRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }

        public List<TBL_Admin_Usuarios> GetUsuariosBySpec(ISpecification<TBL_ModuloReclamos_Asesores> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                var list = activeContext.TBL_ModuloReclamos_Asesores
                                    .Include(x => x.TBL_Admin_Usuarios)
                                    .Where(specific)
                                    .Select(x => x.TBL_Admin_Usuarios);

                var oReturn = new List<TBL_Admin_Usuarios>();

                foreach (var usr in list)
                {
                    foreach (var u in usr)
                    {
                        if (!oReturn.Contains(u))
                            oReturn.Add(u);
                    }
                }

                if (oReturn.Any())
                {
                    oReturn = oReturn.Distinct().ToList();
                }

                return oReturn;
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }
    }
}
    