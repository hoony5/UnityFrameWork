using Newtonsoft.Json;

namespace GPT
{
    public class RunStep
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
        public string RunID { get; set; }
        
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        
        [JsonProperty("step_details", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic StepDetails { get; set; }
        
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        
        [JsonProperty("last_error", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic LastError { get; set; }
        
        [JsonProperty("expires_at", NullValueHandling = NullValueHandling.Ignore)]
        public long ExpiresAt { get; set; }
        
        [JsonProperty("failed_at", NullValueHandling = NullValueHandling.Ignore)]
        public long FailedAt { get; set; }
        
        [JsonProperty("completed_at", NullValueHandling = NullValueHandling.Ignore)]
        public long CompletedAt { get; set; }
        
        [JsonProperty("cancelled_at", NullValueHandling = NullValueHandling.Ignore)]
        public long CancelledAt { get; set; }
        
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic MetaData { get; set; }
        
        [JsonProperty("usage", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Usage { get; set; }
    }
}