using Newtonsoft.Json;

namespace GPT
{
    public class ChatChoices
    {
        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
        
        [JsonProperty("index")]
        public int Index { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
        
        [JsonProperty("logprobs")]
        public dynamic Logprobs { get; set; }
    }
}