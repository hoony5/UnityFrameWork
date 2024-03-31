using Newtonsoft.Json;

namespace GPT
{
    public class Message
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)] 
        public string ID { get; set; }
        
        [JsonProperty("object", NullValueHandling = NullValueHandling.Ignore)]
        public string Object { get; set; }
        
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public long CreateAt { get; set; }
        
        [JsonProperty("thread_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ThreadID { get; set; }
        
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AssistantID { get; set; }
        
        [JsonProperty("run_id", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic RunID { get; set; }
        
        [JsonProperty("file_ids", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic FileIds { get; set; }
        
        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }
        
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Content { get; set; }
        
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic MetaData { get; set; }
    }
}