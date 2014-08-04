using ExpressionEvaluation;

namespace Domain.MainModule.WorkFlow.Aggregates
{
    public static class ExpressionEvalServices{
        
        public static bool EvaluateExpression(string expressions)
        {
            if (string.IsNullOrEmpty(expressions)) return false;

            var eval = new ExpressionEval { Expression = expressions };
            var result = (bool)eval.Evaluate();
            return result;
        }
    }
}