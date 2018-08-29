using Evvosoft.VariableEngine.Business.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using Evvosoft.VariableEngine.Business.Models;

namespace Evvosoft.VariableEngine.Business.Processor
{
    public class VariableProcessor : IVariableProcessor
    {
        private readonly char parenthesesChar = '"';
        
        private readonly Regex funcCallsRegex = new Regex(@"([a-zA-Z""_]+([0-9]+)?)?(?=\()");

        private readonly Regex noOperatorsRegex = new Regex(@"([a-zA-Z""_]+([0-9]+)?)");

        #region Public IVariableProcessor methods

        public MatchExpression ExtractVariables(string expression)
        {
            var funcCallsMatches = funcCallsRegex.Matches(expression);
            var notOperatorsMatches = noOperatorsRegex.Matches(expression);

            var result = new MatchExpression { Expression = expression };
            var isContainsParentheses = false;

            foreach (var el in notOperatorsMatches)
            {
                var match = (Match) el;
                var count = match.Value.Count(c => c == parenthesesChar);
              
                if (count == 1)
                {
                    isContainsParentheses = !isContainsParentheses;
                    continue;
                }

                if (count > 1 
                    || isContainsParentheses 
                    || funcCallsMatches.Cast<Match>().Any(x => x.Value.StartsWith(match.Value)))
                        continue;
               
                result.Matches.Add(new MatchVariable
                {
                    Index = match.Index,
                    Length = match.Length,
                    Value = match.Value
                });
            }

            return result;
        }        

        public string ReplaceVariables(MatchExpression expression, Dictionary<string, string> replaces)
        {
            var input = new StringBuilder(expression.Expression);
            var padIndex = 0;

            foreach (var match in expression.Matches)
            {
                var replace = replaces[match.Value];
               
                if (replace == null)
                    continue;

                var calculatedIndex = match.Index + padIndex;
                input.Remove(calculatedIndex, match.Length);
                input.Insert(calculatedIndex, replace);

                if (match.Length != replace.Length)
                {
                    padIndex += replace.Length - match.Length;
                }
            }

            return input.ToString();
        }

        #endregion
    }
}
