using Newtonsoft.Json;

namespace GPT
{
    public class FunctionToolPayload
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }
        
        [JsonProperty("function", NullValueHandling = NullValueHandling.Include)]
        public FunctionPayload Function { get; set; }
    }
}