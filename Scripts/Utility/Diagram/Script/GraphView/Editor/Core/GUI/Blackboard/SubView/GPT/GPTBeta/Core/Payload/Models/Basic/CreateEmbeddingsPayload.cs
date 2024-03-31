using Newtonsoft.Json;

namespace GPT
{
    public class CreateEmbeddingsPayload
    {
        [JsonProperty("input", NullValueHandling = NullValueHandling.Include)]
        public object Input { get; set; }
        
        [JsonProperty("model", NullValueHandling = NullValueHandling.Include)]
        public string Model { get; set; }
        
        [JsonProperty("encoding_format", NullValueHandling = NullValueHandling.Ignore)]
        public string EncodingFormat { get; set; }
        
        [JsonProperty("dimensions", NullValueHandling = NullValueHandling.Ignore)]
        public int Dimensions { get; set; }
        
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }
    }
}