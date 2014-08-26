using System;
using System.Linq.Expressions;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.WorkFlow.Spec
{
    //public class VerificarEstadoSuministrosCodeSpecifications : Specification<TBL_ModuloPedidos_ActividadesPedidos>
    //{
    //    private readonly int _idPedido = default(int);
    //    private readonly string _estado = default(string);
    //    private readonly string _nombreActividad = default(string);

    //    public VerificarEstadoSuministrosCodeSpecifications(int idPedido, string estado, string nombreActividad)
    //    {
    //        _idPedido = idPedido;
    //        _nombreActividad = nombreActividad;
    //        _estado = estado;
    //    }

    //    public override Expression<Func<TBL_ModuloPedidos_ActividadesPedidos, bool>> SatisfiedBy()
    //    {
    //        Specification<TBL_ModuloPedidos_ActividadesPedidos> spec = new TrueSpecification<TBL_ModuloPedidos_ActividadesPedidos>();

    //        spec &= new DirectSpecification<TBL_ModuloPedidos_ActividadesPedidos>(u => u.IsActive && u.IdPedido == _idPedido);

    //        if (!String.IsNullOrEmpty(_estado) && !String.IsNullOrWhiteSpace(_estado))
    //        {
    //            spec &= new DirectSpecification<TBL_ModuloPedidos_ActividadesPedidos>(u => u.TBL_Admin_EstadosProceso.Estado.Equals(_estado));
    //        }

    //        if (!String.IsNullOrEmpty(_nombreActividad) && !String.IsNullOrWhiteSpace(_nombreActividad))
    //        {
    //            spec &= new DirectSpecification<TBL_ModuloPedidos_ActividadesPedidos>(u => u.TBL_ModuloPlanAccion_BancoActividades.Descripcion.Equals(_nombreActividad));
    //        }

    //        return spec.SatisfiedBy();
    //    }
    //}
}