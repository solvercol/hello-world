using System;
using System.Linq.Expressions;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Spec
{
    public class TBL_ModuloReclamos_Reclamo_Spec : Specification<TBL_ModuloReclamos_Reclamo>
    {

         #region Members

        #endregion

         #region Constructor

        public TBL_ModuloReclamos_Reclamo_Spec()
        {
        }

        #endregion

        public override Expression<Func<TBL_ModuloReclamos_Reclamo, bool>> SatisfiedBy()
        {
            Specification<TBL_ModuloReclamos_Reclamo> spec = new TrueSpecification<TBL_ModuloReclamos_Reclamo>();

            spec &= new DirectSpecification<TBL_ModuloReclamos_Reclamo>(r => r.IsActive);
            
            return spec.SatisfiedBy();
        }

        public static Specification<TBL_ModuloReclamos_Reclamo> SpecByIdReclamo(decimal idReclamo)
        {
            Specification<TBL_ModuloReclamos_Reclamo> spec = new TrueSpecification<TBL_ModuloReclamos_Reclamo>();

            spec &= new DirectSpecification<TBL_ModuloReclamos_Reclamo>(r => r.IdReclamo == idReclamo);

            return spec;
        }
    }
}