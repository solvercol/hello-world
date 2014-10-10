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
        private IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloReclamos_AsesoresRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
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
                    if (!oReturn.Contains(usr))
                        oReturn.Add(usr);
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

        public override IEnumerable<TBL_ModuloReclamos_Asesores> GetPagedElements<TS>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<System.Func<TBL_ModuloReclamos_Asesores, TS>> orderByExpression, ISpecification<TBL_ModuloReclamos_Asesores> specification, bool ascending)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Messages.exception_InvalidPageCount, "pageCount");

            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression", "OrderByExpression is null");

            if (specification == null)
                throw new ArgumentNullException("specification", "OrderByExpression is null");

            return (ascending)
                      ?
                      _currentUnitOfWork.TBL_ModuloReclamos_Asesores
                        .Include(x => x.TBL_Admin_Usuarios)
                        .Include(x => x.TBL_ModuloReclamos_Unidad)
                        .Include(x => x.TBL_ModuloReclamos_Zona)
                        .Where(specification.SatisfiedBy())
                        .OrderBy(orderByExpression)
                        .Skip(pageIndex * pageCount)
                        .Take(pageCount)
                      :
                     _currentUnitOfWork.TBL_ModuloReclamos_Asesores
                            .Include(x => x.TBL_Admin_Usuarios)
                            .Include(x => x.TBL_ModuloReclamos_Unidad)
                            .Include(x => x.TBL_ModuloReclamos_Zona)
                            .Where(specification.SatisfiedBy())
                            .OrderByDescending(orderByExpression)
                            .Skip(pageIndex * pageCount)
                            .Take(pageCount);
        }

        public TBL_ModuloReclamos_Asesores GetAsesoresBySpec(ISpecification<TBL_ModuloReclamos_Asesores> specification)
        {
            return _currentUnitOfWork.TBL_ModuloReclamos_Asesores
                        .Include(x => x.TBL_Admin_Usuarios)
                        .Include(x => x.TBL_ModuloReclamos_Unidad)
                        .Include(x => x.TBL_ModuloReclamos_Zona)
                         .Where(specification.SatisfiedBy()).FirstOrDefault();
        }

        public List<TBL_Admin_Usuarios> GetUsuariosAsesoresByIdAsesorado(int idUser)
        {
          
            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var list = activeContext.TBL_ModuloReclamos_Asesores
                                    .Include(x => x.TBL_Admin_Usuarios)
                                    .Where(x=> x.IdUsuario == idUser)
                                    .Select(x => x.TBL_Admin_Usuarios);

                var oReturn = new List<TBL_Admin_Usuarios>();

                foreach (var usr in list)
                {
                    if (!oReturn.Contains(usr))
                        oReturn.Add(usr);
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
    