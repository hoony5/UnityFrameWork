using Newtonsoft.Json;

namespace GPT
{
    public class CreateThreadAndRunPayload
    {
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Include)]
        public string AssistantID { get; set; }
            
        [JsonProperty("thread", NullValueHandling = NullValueHandling.Ignore)]
        public ThreadPayload Thread { get; set; }
            
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
            
        [JsonProperty("instructions", NullValueHandling = NullValueHandling.Ignore)]
        public string Instructions { get; set; }
            
        [JsonProperty("tools", NullValueHandling = NullValueHandling.Ignore)]
        public object Tools { get; set; }
            
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object MetaData { get; set; }
    }
}