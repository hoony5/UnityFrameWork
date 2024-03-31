using Newtonsoft.Json;

namespace GPT
{
    public class CreateThreadOptionalPayload
    {
        [JsonProperty("messages", NullValueHandling = NullValueHandling.Include)]
        public object Messages { get; set; }
            
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object MetaData { get; set; }
    }
}