using System.Collections.Generic;
using Evvosoft.VariableEngine.Business.Models;

namespace Evvosoft.VariableEngine.Business.Contracts
{
    public interface IVariableProcessor
    {
        MatchExpression ExtractVariables(string expression);

        string ReplaceVariables(MatchExpression expression, Dictionary<string, string> variables);
    }
}
