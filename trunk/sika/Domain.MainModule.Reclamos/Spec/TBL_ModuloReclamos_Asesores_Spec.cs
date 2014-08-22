using System;
using System.Linq.Expressions;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Spec
{
    public class TBL_ModuloReclamos_Asesores_Spec : Specification<TBL_ModuloReclamos_Asesores>
    {

         #region Members

        #endregion

         #region Constructor

        public TBL_ModuloReclamos_Asesores_Spec()
        { 
        }

        #endregion

        public override Expression<Func<TBL_ModuloReclamos_Asesores, bool>> SatisfiedBy()
        {
            Specification<TBL_ModuloReclamos_Asesores> spec = new TrueSpecification<TBL_ModuloReclamos_Asesores>();

            spec &= new DirectSpecification<TBL_ModuloReclamos_Asesores>(r => r.IdUsuario != null);
            
            return spec.SatisfiedBy();
        }
    }
}