using Newtonsoft.Json;

namespace GPT
{
    public class MessageFile
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)] 
        public string ID { get; set; }
        
        [JsonProperty("object", NullValueHandling = NullValueHandling.Ignore)]
        public string Object { get; set; }
        
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public long CreateAt { get; set; }
        
        [JsonProperty("message_id", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageID { get; set; }
    }
}