//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using Domain.MainModule.Reclamos.Contracts;
using Infraestructure.Data.Core;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting.Logging;
using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Infrastructure.Data.MainModule.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using Infraestructura.Data.Reclamos.Resources;
using System;


namespace Infrastructure.Data.MainModule.Reclamos.Repositories
{
    public class TBL_ModuloReclamos_CategoriasReclamoRepository : GenericRepository<TBL_ModuloReclamos_CategoriasReclamo>, ITBL_ModuloReclamos_CategoriasReclamoRepository 
    {
        private IMainModuleUnitOfWork _currentUnitOfWork;
         
        public TBL_ModuloReclamos_CategoriasReclamoRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public override IEnumerable<TBL_ModuloReclamos_CategoriasReclamo> GetPagedElements<TS>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<System.Func<TBL_ModuloReclamos_CategoriasReclamo, TS>> orderByExpression, ISpecification<TBL_ModuloReclamos_CategoriasReclamo> specification, bool ascending)
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
                      _currentUnitOfWork.TBL_ModuloReclamos_CategoriasReclamo
                        .Include(x => x.TBL_Admin_Usuarios)
                        .Include(x => x.TBL_ModuloReclamos_TipoReclamo)
                        .Where(specification.SatisfiedBy())
                        .OrderBy(orderByExpression)
                        .Skip(pageIndex * pageCount)
                        .Take(pageCount)
                      :
                     _currentUnitOfWork.TBL_ModuloReclamos_CategoriasReclamo                          
                          .Include(x => x.TBL_Admin_Usuarios)
                          .Include(x => x.TBL_ModuloReclamos_TipoReclamo)
                          .Where(specification.SatisfiedBy())
                          .OrderByDescending(orderByExpression)
                          .Skip(pageIndex * pageCount)
                          .Take(pageCount);
        }


        public TBL_ModuloReclamos_CategoriasReclamo GetCategoriaBySpec(ISpecification<TBL_ModuloReclamos_CategoriasReclamo> specification)
        {
           return  _currentUnitOfWork.TBL_ModuloReclamos_CategoriasReclamo
                        .Include(x => x.TBL_Admin_Usuarios)
                        .Include(x => x.TBL_Admin_Usuarios1)
                        .Include(x => x.TBL_Admin_Usuarios2)
                        .Include(x => x.TBL_ModuloReclamos_TipoReclamo)
                        .Where(specification.SatisfiedBy()).FirstOrDefault();
        }
         
    }
}
    