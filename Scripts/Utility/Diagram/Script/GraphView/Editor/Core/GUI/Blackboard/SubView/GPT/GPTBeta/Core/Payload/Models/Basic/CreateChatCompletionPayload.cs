using Newtonsoft.Json;

namespace GPT
{
    public class CreateChatCompletionPayload
    {
        [JsonProperty("model", NullValueHandling = NullValueHandling.Include)]
        public string Model { get; set; }
        
        [JsonProperty("messages", NullValueHandling = NullValueHandling.Include)]
        public dynamic Messages { get; set; }
        
        [JsonProperty("frequency_penalty", NullValueHandling = NullValueHandling.Ignore)]
        public float FrequencyPenalty { get; set; }
        
        [JsonProperty("presence_penalty", NullValueHandling = NullValueHandling.Ignore)]
        public float PresencePenalty { get; set; }
        
        [JsonProperty("stop", NullValueHandling = NullValueHandling.Ignore)]
        public string Stop { get; set; }
        
        [JsonProperty("temperature", NullValueHandling = NullValueHandling.Ignore)]
        public double Temperature { get; set; }
        
        [JsonProperty("max_tokens", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxTokens { get; set; }
        
        [JsonProperty("n", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic N { get; set; }
        
        [JsonProperty("stream", NullValueHandling = NullValueHandling.Ignore)]
        public bool Stream { get; set; }
        
        [JsonProperty("seed", NullValueHandling = NullValueHandling.Ignore)]
        public int Seed { get; set; }
        
        [JsonProperty("top_p", NullValueHandling = NullValueHandling.Ignore)]
        public double TopP { get; set; }
        
        [JsonProperty("tools", NullValueHandling = NullValueHandling.Ignore)]
        public object Tools { get; set; }
        
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }
    }
}