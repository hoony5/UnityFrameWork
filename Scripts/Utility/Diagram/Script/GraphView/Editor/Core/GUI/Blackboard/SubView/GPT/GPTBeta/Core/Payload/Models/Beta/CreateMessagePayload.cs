using Newtonsoft.Json;

namespace GPT
{
    public class CreateMessagePayload
    {
        [JsonProperty("role", NullValueHandling = NullValueHandling.Include)]
        public string Role { get; set; }
            
        [JsonProperty("content", NullValueHandling = NullValueHandling.Include)]
        public string Content { get; set; }
            
        [JsonProperty("file_ids", NullValueHandling = NullValueHandling.Ignore)]
        public object FileIds { get; set; }
            
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object MetaData { get; set; }
    }
}