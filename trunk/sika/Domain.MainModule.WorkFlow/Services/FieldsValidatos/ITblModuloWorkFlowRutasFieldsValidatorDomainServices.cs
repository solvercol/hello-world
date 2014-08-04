using System.Collections.Generic;

namespace Domain.MainModule.WorkFlow.Services.FieldsValidatos
{
    public interface ITblModuloWorkFlowRutasFieldsValidatorDomainServices
    {
        bool IsValidField<TEntity, TEntityRules>(TEntity item, TEntityRules rules)
            where TEntity : class
            where TEntityRules : class;

        List<string> GetValidationErrorsMessages { get; }

        List<string> GetStoreProceduresValidadtionFunctions { get; }
    }
}