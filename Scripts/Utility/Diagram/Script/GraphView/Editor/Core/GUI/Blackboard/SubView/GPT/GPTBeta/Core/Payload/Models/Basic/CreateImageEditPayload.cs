using Newtonsoft.Json;

namespace GPT
{
    public class CreateImageEditPayload
    {
        [JsonProperty("image", NullValueHandling = NullValueHandling.Include)]
        public Files Image { get; set; }
        
        [JsonProperty("prompt", NullValueHandling = NullValueHandling.Include)]
        public string Prompt { get; set; }
        
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
        
        [JsonProperty("n", NullValueHandling = NullValueHandling.Ignore)]
        public int N { get; set; }
        
        [JsonProperty("mask", NullValueHandling = NullValueHandling.Ignore)]
        public Files Mask { get; set; }
        
        [JsonProperty("response_format", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseFormat { get; set; }
        
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public string Size { get; set; }
        
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }
    }
}