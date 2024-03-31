using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GPT
{
    public abstract class ChatCompletionBase
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("choices")]
        public dynamic Choices { get; set; }
        
        [JsonProperty("system_fingerprint")]
        public string SystemFingerPrint { get; set; }
        
        [JsonProperty("object")]
        public abstract string Object { get; protected set; }
    }
}