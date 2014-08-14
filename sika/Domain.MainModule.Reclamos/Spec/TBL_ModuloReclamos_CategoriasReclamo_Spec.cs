using System;
using System.Linq.Expressions;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Spec
{
    public class TBL_ModuloReclamos_CategoriasReclamo_Spec : Specification<TBL_ModuloReclamos_CategoriasReclamo>
    {

         #region Members

        #endregion

         #region Constructor

        public TBL_ModuloReclamos_CategoriasReclamo_Spec()
        { 
        }

        #endregion

        public override Expression<Func<TBL_ModuloReclamos_CategoriasReclamo, bool>> SatisfiedBy()
        {
            Specification<TBL_ModuloReclamos_CategoriasReclamo> spec = new TrueSpecification<TBL_ModuloReclamos_CategoriasReclamo>();

            spec &= new DirectSpecification<TBL_ModuloReclamos_CategoriasReclamo>(r => r.IsActive);
            
            return spec.SatisfiedBy();
        }

        public static Specification<TBL_ModuloReclamos_CategoriasReclamo> SpecByTipoReclamo(int idTipoReclamo)
        {
            Specification<TBL_ModuloReclamos_CategoriasReclamo> spec = new TrueSpecification<TBL_ModuloReclamos_CategoriasReclamo>();

            spec &= new DirectSpecification<TBL_ModuloReclamos_CategoriasReclamo>(r => r.IdTipoReclamo == idTipoReclamo);

            return spec;
        }
    }
}