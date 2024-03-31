using Newtonsoft.Json;

namespace GPT
{
    public class ChatFunction
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("arguments")]
        public string Arguments { get; set; }
    }
}