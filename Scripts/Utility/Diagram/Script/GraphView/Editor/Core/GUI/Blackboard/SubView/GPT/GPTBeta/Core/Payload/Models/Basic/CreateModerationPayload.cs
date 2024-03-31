using Newtonsoft.Json;

namespace GPT
{
    public class CreateModerationPayload
    {
        [JsonProperty("input", NullValueHandling = NullValueHandling.Include)]
        public object Input { get; set; }
        
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }
}