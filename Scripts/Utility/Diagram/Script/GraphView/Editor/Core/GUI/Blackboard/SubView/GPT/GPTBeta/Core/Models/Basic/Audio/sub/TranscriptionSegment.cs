using Newtonsoft.Json;

namespace GPT
{
    public class TranscriptionSegment
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("seek")]
        public int Seek { get; set; }
        
        [JsonProperty("start")]
        public double Start { get; set; }
        
        [JsonProperty("end")]
        public double End { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("tokens")]
        public dynamic Tokens { get; set; }
        
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        
        [JsonProperty("avg_logprob")]
        public double AvgLogProb { get; set; }
        
        [JsonProperty("compression_ratio")]
        public double CompressionRatio { get; set; }
        
        [JsonProperty("no_speech_prob")]
        public double NoSpeechProb { get; set; }
    }
}