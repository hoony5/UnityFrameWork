using Newtonsoft.Json;

namespace GPT
{
    public class ChatCompletionChunkDelta
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        
        [JsonProperty("tool_calls")]
        public dynamic ToolCalls { get; set; }
        
        [JsonProperty("role")]
        public string Role { get; set; }
    }
}