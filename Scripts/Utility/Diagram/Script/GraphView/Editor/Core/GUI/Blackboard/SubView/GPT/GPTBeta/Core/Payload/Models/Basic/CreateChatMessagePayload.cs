using Newtonsoft.Json;

namespace GPT
{
    public class CreateChatMessagePayload // system / user/ assistant / tool
    {
        // system / user/ assistant / tool
        [JsonProperty("role", NullValueHandling = NullValueHandling.Include)]
        public string Role { get; set; }
        
        [JsonProperty("content", NullValueHandling = NullValueHandling.Include)]
        public string Content { get; set; }
        
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}