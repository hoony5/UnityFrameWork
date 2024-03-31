using Newtonsoft.Json;

namespace GPT
{
    public class ChatToolCalls
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("function")]
        public dynamic Function { get; set; }
    }
}