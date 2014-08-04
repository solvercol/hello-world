using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.SystemActions.Service;
using Domain.MainModule.WorkFlow.Contracts.DTO;
using Domain.MainModules.Entities;
using ExpressionEvaluation;
using Infrastructure.CrossCutting.NetFramework.Extensions;

namespace Domain.MainModule.WorkFlow.Services.WorkFlow
{
    public class TblModuloWorkFlowRutaDomainServices : ITblModuloWorkFlowRutaDomainServices
    {

        private readonly IPedidosEmpacorServices _sqlPedidosServices;
        private readonly ISystemActionsManagementServices _systemActionsServices;

        public TblModuloWorkFlowRutaDomainServices(
            IPedidosEmpacorServices sqlPedidosServices, 
            ISystemActionsManagementServices systemActionsServices)
        {
            _sqlPedidosServices = sqlPedidosServices;
            _systemActionsServices = systemActionsServices;
        }


        public WorkFlowDto CargarWorkFlow(IEnumerable<TBL_ModuloWorkFlow_Rutas> listadoRutas, DataTable dtPedido)
        {

            return (from tblModuloWorkFlowRutase in listadoRutas
                    let strExpressions = NewExpression(tblModuloWorkFlowRutase.FormulaValidacion, dtPedido)
                    where !string.IsNullOrEmpty(strExpressions)
                    let eval = new ExpressionEval {Expression = strExpressions}
                    let result = (bool) eval.Evaluate()
                    where result
                    select new WorkFlowDto
                               {
                                   NextStatus = tblModuloWorkFlowRutase.TBL_Admin_EstadosProceso1.Descripcion,
                                   IdNextStatus = tblModuloWorkFlowRutase.SiguienteEstado.ToString(),
                                   TextControl = tblModuloWorkFlowRutase.BotonAccionesRutas,
                                   CurrenteResponsible = tblModuloWorkFlowRutase.RolResponsableActual,
                                   IdRuta = tblModuloWorkFlowRutase.IdRuta.ToString(),
                                   CurrentStatus = tblModuloWorkFlowRutase.TBL_Admin_EstadosProceso.Descripcion

                    }).FirstOrDefault();
        }

        /// <summary>
        /// evalua la expresion del tipo (a==b) ? x : y;
        /// </summary>
        /// <param name="strexpression"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string MapearExpresion(string strexpression, DataTable dt)
        {
            var subjectRegex = new Regex(@"\[Fn\](.*)\[\/Fn\]", RegexOptions.Compiled | RegexOptions.Singleline);

            var formula = subjectRegex.Match(strexpression).Groups[1].Value;

            var mappingExpresion =  NewExpression(formula, dt);

            var resul = _systemActionsServices.Execute(mappingExpresion);

            return resul != null ? resul.ToString() : string.Empty;
        }

        private string NewExpression(string formula, DataTable dt)
        {
            var strNewExpresion = formula;

            if (formula.Contains("[SP]"))
            {
                var subjectRegex = new Regex(@"\[SP\](.*)\[\/SP\]", RegexOptions.Compiled | RegexOptions.Singleline);
                var spName = subjectRegex.Match(formula).Groups[1].Value;
                if (!string.IsNullOrEmpty(spName))
                {
                    var result = EjecutarSp(spName);
                    strNewExpresion = subjectRegex.Replace(formula, result.ToString());
                }
            }


            foreach (DataColumn column in dt.Columns)
            {
                if (!formula.ContainsField(string.Format("{0}", column.ColumnName), StringComparison.OrdinalIgnoreCase)) continue;
                foreach (DataRow dr in dt.Rows)
                {
                    var pattern = string.Format(@"\b{0}\b", column.ColumnName);
                    var value = (dr[column.ColumnName] is DBNull) ? string.Empty : dr[column.ColumnName].ToString();
                    strNewExpresion = Regex.Replace(strNewExpresion, pattern, value);
                }
            }
            return strNewExpresion;
        }


        private bool EjecutarSp(string sp)
        {
            var result = _sqlPedidosServices.VerificarSuministros(sp);
            return Convert.ToBoolean(result);
        }

    }
}