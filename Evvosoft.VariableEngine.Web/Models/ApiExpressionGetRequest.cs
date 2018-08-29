using System.Collections.Generic;
using Newtonsoft.Json;

namespace Evvosoft.VariableEngine.Web.Models
{
    public class ApiExpressionGetRequest
    {
        //[JsonProperty("userId")]
        //public string UserId { get; set; }

        [JsonProperty("variables")]
        public Dictionary<string, string> Variables { get; set; }
    }
}
