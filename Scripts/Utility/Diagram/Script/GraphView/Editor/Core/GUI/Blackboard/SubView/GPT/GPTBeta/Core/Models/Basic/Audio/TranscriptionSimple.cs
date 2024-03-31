using Newtonsoft.Json;

namespace GPT
{
    public class TranscriptionSimple
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}