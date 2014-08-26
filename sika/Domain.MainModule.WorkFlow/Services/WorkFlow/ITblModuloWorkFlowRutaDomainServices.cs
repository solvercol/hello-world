using System.Collections.Generic;
using System.Data;
using Domain.MainModule.WorkFlow.Contracts.DTO;
using Domain.MainModules.Entities;

namespace Domain.MainModule.WorkFlow.Services.WorkFlow
{
    public interface ITblModuloWorkFlowRutaDomainServices
    {
        WorkFlowDto CargarWorkFlow(IEnumerable<TBL_ModuloWorkFlow_Rutas> listadoRutas, DataTable dtDocument, string idDocument);

        string MapearExpresion(string strexpression, DataTable dt);

        string GetResponsablePedidobyRuta(IEnumerable<TBL_ModuloWorkFlow_Rutas> listadoRutas, DataTable dtPedido);
    }
}