using Newtonsoft.Json;

namespace GPT
{
    public class FineTuningJobEvent
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("object")]
        public string Object { get; private set; } = "fine_tuning.job.event";
        
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }
        
        [JsonProperty("level")]
        public string Level { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}