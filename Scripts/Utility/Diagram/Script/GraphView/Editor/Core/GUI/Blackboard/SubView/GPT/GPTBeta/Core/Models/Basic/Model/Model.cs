using Newtonsoft.Json;

namespace GPT
{
    public class Model
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [JsonProperty("object")]
        public string Object { get; private set; } = "model";
        
        [JsonProperty("owned_by")]
        public string OwnedBy { get; set; }
    }
}