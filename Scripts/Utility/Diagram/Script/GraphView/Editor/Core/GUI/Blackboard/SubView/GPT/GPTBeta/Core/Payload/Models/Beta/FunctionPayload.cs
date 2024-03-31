using Newtonsoft.Json;

namespace GPT
{
    public class FunctionPayload
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
        
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public ParameterPayload Parameters { get; set; }
        
    }
}