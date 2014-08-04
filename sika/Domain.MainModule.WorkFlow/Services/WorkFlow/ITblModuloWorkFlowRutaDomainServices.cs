using System.Collections.Generic;
using System.Data;
using Domain.MainModule.WorkFlow.Contracts.DTO;
using Domain.MainModules.Entities;

namespace Domain.MainModule.WorkFlow.Services.WorkFlow
{
    public interface ITblModuloWorkFlowRutaDomainServices
    {
        WorkFlowDto CargarWorkFlow(IEnumerable<TBL_ModuloWorkFlow_Rutas> listadoRutas, DataTable dtPedido);

        string MapearExpresion(string strexpression, DataTable dt);
    }
}