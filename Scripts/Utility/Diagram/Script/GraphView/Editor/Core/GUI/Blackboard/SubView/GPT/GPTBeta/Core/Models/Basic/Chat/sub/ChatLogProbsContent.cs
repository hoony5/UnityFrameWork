using Newtonsoft.Json;

namespace GPT
{
    public class ChatLogProbsContent
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("logprob")]
        public double LogProb { get; set; }
        
        [JsonProperty("bytes")]
        public object Bytes { get; set; }
        
        [JsonProperty("top_logprobs")]
        public object TopLogProbs { get; set; }
    }
}