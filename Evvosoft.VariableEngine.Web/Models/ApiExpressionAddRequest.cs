using Newtonsoft.Json;

namespace Evvosoft.VariableEngine.Web.Models
{
    public class ApiExpressionAddRequest
    {
        [JsonProperty("expression")]
        public string Expression { get; set; }
    }
}
