using System.Runtime.Serialization;

namespace Evvosoft.VariableEngine.Business.Models
{
    [DataContract]
    public sealed class MatchVariable
    {
        [DataMember]
        public int Length { get; set; }

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
