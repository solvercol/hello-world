using System;
using System.Linq.Expressions;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Reclamos.Spec
{
    public class TBL_ModuloReclamos_UnidadesZonas_Spec : Specification<TBL_ModuloReclamos_UnidadesZonas>
    {

         #region Members

        #endregion

         #region Constructor

        public TBL_ModuloReclamos_UnidadesZonas_Spec()
        { 
        }

        #endregion

        public override Expression<Func<TBL_ModuloReclamos_UnidadesZonas, bool>> SatisfiedBy()
        {
            Specification<TBL_ModuloReclamos_UnidadesZonas> spec = new TrueSpecification<TBL_ModuloReclamos_UnidadesZonas>();

            spec &= new DirectSpecification<TBL_ModuloReclamos_UnidadesZonas>(r => r.IdGerente != null);
            
            return spec.SatisfiedBy();
        }

        public static Specification<TBL_ModuloReclamos_UnidadesZonas> SpecByUnidadZona(string unidad, string zona)
        {
            Specification<TBL_ModuloReclamos_UnidadesZonas> spec = new TrueSpecification<TBL_ModuloReclamos_UnidadesZonas>();

            spec &= new DirectSpecification<TBL_ModuloReclamos_UnidadesZonas>(r => r.TBL_ModuloReclamos_Unidad.Nombre.Contains(unidad)
                                                                                   && r.TBL_ModuloReclamos_Zona.Descripcion.Contains(zona));

            return spec;
        }
    }
}