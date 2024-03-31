using Newtonsoft.Json;

namespace GPT
{
    public class CreateTranslationPayload
    {
        [JsonProperty("model", NullValueHandling = NullValueHandling.Include)]
        public string Model { get; set; }
        
        [JsonProperty("file", NullValueHandling = NullValueHandling.Include)]
        public Files Files { get; set; }
        
        [JsonProperty("prompt", NullValueHandling = NullValueHandling.Ignore)]
        public string Prompt { get; set; }
        
        [JsonProperty("response_format", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseFormat { get; set; }
        
        [JsonProperty("temperature", NullValueHandling = NullValueHandling.Ignore)]
        public double Temperature { get; set; }
    }
}