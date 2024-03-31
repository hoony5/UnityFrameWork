using Newtonsoft.Json;

namespace GPT
{
    public class Transcription
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        
        [JsonProperty("duration")]
        public string Duration { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("words")]
        public dynamic Words { get; set; }
        
        [JsonProperty("segments")]
        public dynamic Segments { get; set; }

    }
}