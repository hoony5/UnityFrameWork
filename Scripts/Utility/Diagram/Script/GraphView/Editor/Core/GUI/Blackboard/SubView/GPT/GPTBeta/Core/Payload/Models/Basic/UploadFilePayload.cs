using Newtonsoft.Json;

namespace GPT
{
    public class UploadFilePayload
    {
        [JsonProperty("file", NullValueHandling = NullValueHandling.Include)]
        public Files Files { get; set; }
        
        [JsonProperty("purpose", NullValueHandling = NullValueHandling.Include)]
        public string Purpose { get; set; }
    }
}