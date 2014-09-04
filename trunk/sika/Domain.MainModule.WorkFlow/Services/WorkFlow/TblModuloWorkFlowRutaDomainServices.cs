using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Applications.MainModule.SystemActions.Service;
using Domain.Core;
using Domain.MainModule.WorkFlow.Contracts.DTO;
using Domain.MainModules.Entities;
using ExpressionEvaluation;
using Infrastructure.CrossCutting.IDtoService;
using Infrastructure.CrossCutting.NetFramework.Extensions;

namespace Domain.MainModule.WorkFlow.Services.WorkFlow
{
    public class TblModuloWorkFlowRutaDomainServices : ITblModuloWorkFlowRutaDomainServices
    {

       
        private readonly ISystemActionsManagementServices _systemActionsServices;

        public TblModuloWorkFlowRutaDomainServices(
            
            ISystemActionsManagementServices systemActionsServices)
        {
          
            _systemActionsServices = systemActionsServices;
        }


        public string GetResponsablePedidobyRuta(IEnumerable<TBL_ModuloWorkFlow_Rutas> listadoRutas, DataTable dtPedido)
        {
            var strRolResponsable = string.Empty;
            foreach (var ruta in listadoRutas)
            {
                var newExpression = NewExpression(ruta.FormulaValidacion, dtPedido);
                if (!string.IsNullOrEmpty(newExpression))
                {
                    var result = EvaluateExpression(newExpression);
                    if (result)
                    {
                        strRolResponsable = ruta.RolResponsableActual;
                        break;
                    }
                }
            }
            return strRolResponsable;
        }

        public WorkFlowDto CargarWorkFlow(IEnumerable<TBL_ModuloWorkFlow_Rutas> listadoRutas, DataTable dtDocument, IDocumentDto oDocument)
        {


            return (from tblModuloWorkFlowRutase in listadoRutas
                    let strExpressions = NewExpression(tblModuloWorkFlowRutase, dtDocument, oDocument)
                    where !string.IsNullOrEmpty(strExpressions)
                    let result = EvaluateExpression(strExpressions)
                    where result
                    select new WorkFlowDto
                               {
                                   NextStatus = tblModuloWorkFlowRutase.TBL_Admin_EstadosProceso1.Descripcion,
                                   IdNextStatus = tblModuloWorkFlowRutase.SiguienteEstado.ToString(),
                                   TextControl = tblModuloWorkFlowRutase.BotonAccionesRutas,
                                   RoleNextResponsible = tblModuloWorkFlowRutase.RolResponsableActual,
                                   IdRuta = tblModuloWorkFlowRutase.IdRuta.ToString(),
                                   CurrentStatus = tblModuloWorkFlowRutase.TBL_Admin_EstadosProceso.Descripcion

                               }).FirstOrDefault();
        }

        /// <summary>
        /// evalua la expresion del tipo (a==b) ? x : y; utilizando la ejecución dinámica de codigo.
        /// </summary>
        /// <param name="strexpression"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string MapearExpresion(string strexpression, DataTable dt)
        {
            var m = DefinedRegexEvaluation.Function.Match(strexpression);
            if (m.Success)
            {
                var formula = m.Groups[1].Value.Trim();

                var mappingExpresion = NewExpression(formula, dt);

                //Incoca la funcionque ejecuta códoco .cs en memoria.
                var resul = _systemActionsServices.Execute(mappingExpresion);

                return resul != null ? resul.ToString() : string.Empty;
            }

            return string.Empty;
        }

        private string NewExpression(TBL_ModuloWorkFlow_Rutas ruta, DataTable dt, IDocumentDto oDocument)
        {
            var strNewExpresion = ruta.FormulaValidacion;

            var m = DefinedRegexEvaluation.StoredProcedure.Match(strNewExpresion);

            if (m.Success)
            {
                var spName = m.Groups[1].Value.Trim();
                if (!string.IsNullOrEmpty(spName))
                {
                    
                    var sistemAction = ruta.TBL_ModuloWorkFlow_ValidacionesSalida.Where(x => x.NombreMetodo == spName).SingleOrDefault();
                    if(sistemAction != null)
                    {
                        _systemActionsServices.AssemblyQualifiedName = sistemAction.NombreEnsamblado;
                        _systemActionsServices.MethodName = sistemAction.NombreMetodo;
                        _systemActionsServices.Params = new object[] { oDocument };
                        var result = _systemActionsServices.Execute();
                        if(result != null)
                        {
                            if(result is bool)
                                strNewExpresion = DefinedRegexEvaluation.StoredProcedure.Replace(strNewExpresion, Convert.ToBoolean(result) ? "True" : "False");
                        }
                    }
                }
            }

            m = DefinedRegexEvaluation.FielsExpression.Match(strNewExpresion);
            if (m.Success)
            {
                var spName = m.Groups[1].Value.Trim();
                if (!string.IsNullOrEmpty(spName))
                {
                    var dosP = DefinedRegexEvaluation.DosPuntos.Match(spName);
                    if (dosP.Success)
                    {
                        var arr = spName.Split(':');
                        var newArr = (string[]) arr.Clone();
                        if (arr.Length > 0)
                        {
                            for (var i = 0; i < arr.Length; i += 2)
                            {
                                newArr[i] = ReturnValue(dt, arr[i]);
                            }

                            var strExpresion = newArr.Aggregate(string.Empty,
                                                                (current, t) => current + string.Format("{0} ", t));

                            var evaluacion = EvaluateExpression(strExpresion);
                            strNewExpresion = DefinedRegexEvaluation.FielsExpression.Replace(strNewExpresion,
                                                                                             evaluacion
                                                                                                 ? "TRUE"
                                                                                                 : "FALSE");
                        }
                    }
                    else
                    {
                        strNewExpresion = DefinedRegexEvaluation.FielsExpression.Replace(strNewExpresion,"FALSE");
                    }
                }
            }

            foreach (DataColumn column in dt.Columns)
            {
                if (!ruta.FormulaValidacion.ContainsField(string.Format("{0}", column.ColumnName), StringComparison.OrdinalIgnoreCase)) continue;
                foreach (DataRow dr in dt.Rows)
                {
                    var pattern = string.Format(@"\b{0}\b", column.ColumnName);
                    var value = (dr[column.ColumnName] is DBNull) ? string.Empty : dr[column.ColumnName].ToString().ToUpper();
                    strNewExpresion = Regex.Replace(strNewExpresion, pattern, value.ToUpper());
                }
            }
            return strNewExpresion;
        }

        /// <summary>
        /// Evalua la expresion recibida en el parametro string Ruta
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string NewExpression(string ruta, DataTable dt)
        {
            var strNewExpresion = ruta;

            foreach (DataColumn column in dt.Columns)
            {
                if (!ruta.ContainsField(string.Format("{0}", column.ColumnName), StringComparison.OrdinalIgnoreCase)) continue;
                foreach (DataRow dr in dt.Rows)
                {
                    var pattern = string.Format(@"\b{0}\b", column.ColumnName);
                    var value = (dr[column.ColumnName] is DBNull) ? string.Empty : dr[column.ColumnName].ToString().ToUpper();
                    strNewExpresion = Regex.Replace(strNewExpresion, pattern, value);
                }
            }
            return strNewExpresion;
        }

       
        private static string ReturnValue(DataTable dt, string field)
        {
            var exist = dt.Rows[0].ExistsColumn(field);
            return exist ? dt.Rows[0].GetValueColumn(field) : string.Empty;
        }

        private static bool EvaluateExpression(string strExpressions)
        {
            try
            {
                var eval = new ExpressionEval { Expression = strExpressions };
                return (bool)eval.Evaluate();
            }
            catch
            {
                return false;
            }

        }
    }
}