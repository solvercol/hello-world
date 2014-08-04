using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.MainModule.WorkFlow.Aggregates;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Extensions;

namespace Domain.MainModule.WorkFlow.Services.FieldsValidatos
{
    public class TblModuloWorkFlowRutasFieldsValidatorDomainServices : ITblModuloWorkFlowRutasFieldsValidatorDomainServices
    {
        readonly List<string> _validationErrors = new List<string>();
        readonly List<string> _procedimientos = new List<string>();

        public bool IsValidField<TEntity, TEntityRules>(TEntity item, TEntityRules rules)
            where TEntity : class
            where TEntityRules : class
        {

            if (item == null)
                return false;

            if (rules == null)
                return false;

           _validationErrors.Clear();

            ValidationAttributeEntity(item, rules);

            return !_validationErrors.Any();
        }

       

        void ValidationAttributeEntity<TEntity, TEntityRules>(TEntity item, TEntityRules rules) 
            where TEntity : class
            where TEntityRules : class
        {

           
            var oEstado = rules as TBL_Admin_EstadosProceso;

            if (oEstado != null)
            {
                foreach (var camposValidacion in oEstado.TBL_ModuloWorkFlow_CamposValidacion)
                {
                    if(camposValidacion.TipoValidacion == "Formula")
                    {
                        if(!string.IsNullOrEmpty(camposValidacion.ReglaDependencia) && !string.IsNullOrEmpty(camposValidacion.CampoDependencia))
                        {
                            var expression = ValidationField(item, camposValidacion.ReglaDependencia);
                            var result = ExpressionEvalServices.EvaluateExpression(expression);
                            if(result)
                            {
                                expression = ValidationField(item, camposValidacion.ReglaValidacion);
                                result = ExpressionEvalServices.EvaluateExpression(expression);
                                if(!result)
                                {
                                    _validationErrors.Add(string.Format("Campo:[{0}] - Mensaje:[{1}]",camposValidacion.CampoValidar, camposValidacion.MensajeValidacion));
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            var expression = ValidationField(item, camposValidacion.ReglaValidacion);
                            var result = ExpressionEvalServices.EvaluateExpression(expression);
                            if (!result)
                            {
                                _validationErrors.Add(string.Format("Campo:[{0}] - Mensaje:[{1}]",camposValidacion.CampoValidar, camposValidacion.MensajeValidacion));
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(camposValidacion.ReglaValidacion))
                            _procedimientos.Add(camposValidacion.ReglaValidacion);
                    }
                }
            }
        }


        static string ValidationField<TEntity>(TEntity item, string strFormula) where TEntity : class
        {
            var strNewExpresion = strFormula;

             //var pattern = new Regex(@"\[(.*)\]", RegexOptions.Compiled | RegexOptions.Singleline);

             var properties = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
                             select new
                             {
                                 NameProperty = property.Name,
                                 Value = property.GetValue(item)
                             };

            foreach (var property in properties)
            {
                var patternField = string.Format(@"{0}", property.NameProperty);
                if (!strFormula.ContainsField(patternField, StringComparison.OrdinalIgnoreCase)) continue;

                var value = "";
                if (property.Value is bool)
                    value = Convert.ToBoolean(property.Value) ? "SI" : "NO";
                else if (property.Value == null)
                    value = string.Empty;

                strNewExpresion = Regex.Replace(strNewExpresion, patternField, value);
            }

            return strNewExpresion;
        }


        public List<string> GetValidationErrorsMessages
        {
            get { return _validationErrors; }
        }

        public List<string> GetStoreProceduresValidadtionFunctions
        {
            get { return _procedimientos; }
        }

    }
}