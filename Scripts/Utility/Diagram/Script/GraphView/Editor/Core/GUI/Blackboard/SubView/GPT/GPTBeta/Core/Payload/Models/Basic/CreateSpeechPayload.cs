using Newtonsoft.Json;

namespace GPT
{
    public class CreateSpeechPayload
    {
        [JsonProperty("model", NullValueHandling = NullValueHandling.Include)]
        public string Model { get; set; }
        
        [JsonProperty("input", NullValueHandling = NullValueHandling.Include)]
        public string Input { get; set; }
        
        [JsonProperty("voice", NullValueHandling = NullValueHandling.Include)]
        public string Voice { get; set; }
        
        [JsonProperty("response_format", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseFormat { get; set; }
        
        // to 1
        [JsonProperty("speed", NullValueHandling = NullValueHandling.Ignore)]
        public int Speed { get; set; } 
    }
}