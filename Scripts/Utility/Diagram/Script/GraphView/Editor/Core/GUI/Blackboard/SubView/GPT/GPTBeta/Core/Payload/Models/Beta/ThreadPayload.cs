using Newtonsoft.Json;

namespace GPT
{
    public class ThreadPayload
    {
        [JsonProperty("messages")]
        public MessagePayload[] Messages { get; set; }
        
        [JsonProperty("metadata")]
        public object MetaData { get; set; }
    }
}