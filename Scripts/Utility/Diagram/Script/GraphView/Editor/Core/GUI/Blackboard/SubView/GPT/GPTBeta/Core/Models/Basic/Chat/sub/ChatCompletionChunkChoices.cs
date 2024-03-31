using Newtonsoft.Json;

namespace GPT
{
    public class ChatCompletionChunkChoices
    {
        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
        
        [JsonProperty("index")]
        public int Index { get; set; }
        
        [JsonProperty("delta")]
        public dynamic ChatCompletionChunkDelta { get; set; }
        
        [JsonProperty("logprobs")]
        public dynamic Logprobs { get; set; }
    }
}