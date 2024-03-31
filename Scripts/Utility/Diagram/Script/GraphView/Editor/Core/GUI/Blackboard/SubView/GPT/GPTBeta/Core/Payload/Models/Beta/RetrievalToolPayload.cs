using Newtonsoft.Json;

namespace GPT
{
    public class RetrievalToolPayload
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }
    }
}