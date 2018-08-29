using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Evvosoft.VariableEngine.Business.Models
{
    [DataContract]
    public sealed class MatchExpression
    {
        [DataMember]
        public string Expression { get; set; } 

        [DataMember]
        public List<MatchVariable> Matches { get; set; } = new List<MatchVariable>();
        
        [DataMember]
        public List<string> Variables
        {
            get
            {
                var result = new List<string>();

                foreach (var match in this.Matches)
                {
                    if (!result.Contains(match.Value))
                        result.Add(match.Value);
                }

                return result;
            }
        }
    }
}