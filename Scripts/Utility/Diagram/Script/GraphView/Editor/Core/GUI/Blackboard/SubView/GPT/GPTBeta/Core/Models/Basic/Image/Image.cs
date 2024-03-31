using Newtonsoft.Json;

namespace GPT
{
    public class Image
    {
        [JsonProperty("b64_json")]
        public string B64Json { get; set; }
        
        [JsonProperty("url")]
        public string URL { get; set; }
        
        [JsonProperty("revised_prompt")]
        public string RevisedPrompt { get; set; }
    }
}