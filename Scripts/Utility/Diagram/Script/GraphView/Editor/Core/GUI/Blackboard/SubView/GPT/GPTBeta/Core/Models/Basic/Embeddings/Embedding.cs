using Newtonsoft.Json;

namespace GPT
{
    public class Embedding
    {
        [JsonProperty("index")]
        public int Index { get; set; }
        
        [JsonProperty("object")]
        public string Object { get; private set; } = "embedding";
        
        // string list
        [JsonProperty("embedding")] 
        public dynamic EmbeddingData { get; set; }
    }
}