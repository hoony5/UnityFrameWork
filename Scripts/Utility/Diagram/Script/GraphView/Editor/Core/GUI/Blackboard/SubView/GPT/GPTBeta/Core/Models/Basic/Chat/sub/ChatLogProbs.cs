using Newtonsoft.Json;

namespace GPT
{
    public class ChatLogProbs
    {
        [JsonProperty("content")]
        public dynamic Content { get; set; }
    }
}