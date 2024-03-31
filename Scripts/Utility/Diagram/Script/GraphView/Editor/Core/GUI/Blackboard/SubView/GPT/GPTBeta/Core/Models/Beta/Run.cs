using Newtonsoft.Json;

namespace GPT
{
    public class Run
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
        
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        
        [JsonProperty("required_action", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic RequiredAction { get; set; }
        
        [JsonProperty("last_error", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic LastError { get; set; }
        
        [JsonProperty("expires_at", NullValueHandling = NullValueHandling.Ignore)]
        public long ExpiresAt { get; set; }
        
        [JsonProperty("failed_at", NullValueHandling = NullValueHandling.Ignore)]
        public long FailedAt { get; set; }
        
        [JsonProperty("completed_at", NullValueHandling = NullValueHandling.Ignore)]
        public long CompletedAt { get; set; }
        
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
        
        [JsonProperty("instructions", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Instructions { get; set; }
        
        [JsonProperty("tools", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Tools { get; set; }
        
        [JsonProperty("file_ids", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic FileIds { get; set; }
        
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic MetaData { get; set; }
        
        [JsonProperty("usage", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Usage { get; set; }
    }
}