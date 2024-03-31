using Newtonsoft.Json;

namespace GPT
{
    public class CodeInterpreterToolPayload
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }
    }
}