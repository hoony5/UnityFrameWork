using Newtonsoft.Json;

namespace GPT
{
    public class Moderation
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [JsonProperty("results")]
        public dynamic Results { get; set; }
    }
}