using Newtonsoft.Json;

namespace GPT
{
    public class TranscriptionWord
    {
        [JsonProperty("word")]
        public string Word { get; set; }
        
        [JsonProperty("start")]
        public double Start { get; set; }
        
        [JsonProperty("end")]
        public double End { get; set; }
    }
}