using Newtonsoft.Json;

namespace GPT
{
    public class ToolOutputPayload
    {
        [JsonProperty("tool_call_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ToolCallId { get; set; }
        
        [JsonProperty("output", NullValueHandling = NullValueHandling.Ignore)]
        public object Output { get; set; }
    }
}