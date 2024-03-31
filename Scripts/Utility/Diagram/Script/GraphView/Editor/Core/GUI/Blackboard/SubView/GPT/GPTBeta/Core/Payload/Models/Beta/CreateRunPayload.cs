using Newtonsoft.Json;

namespace GPT
{
    public class CreateRunPayload
    {
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Include)]
        public string AssistantID { get; set; }
            
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
            
        [JsonProperty("instructions", NullValueHandling = NullValueHandling.Ignore)]
        public object Instructions { get; set; }
            
        [JsonProperty("additional_instructions", NullValueHandling = NullValueHandling.Ignore)]
        public object AdditionalInstructions { get; set; }
            
        [JsonProperty("tools", NullValueHandling = NullValueHandling.Ignore)]
        public object Tools { get; set; }
            
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object MetaData { get; set; }
    }
}