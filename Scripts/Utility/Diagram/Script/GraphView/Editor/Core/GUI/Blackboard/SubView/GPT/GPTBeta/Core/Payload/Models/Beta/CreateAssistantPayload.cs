using Newtonsoft.Json;

namespace GPT
{
        public class CreateAssistantPayload
        {
            [JsonProperty("model", NullValueHandling = NullValueHandling.Include)]
            public string Name { get; set; }
            
            [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
            public string Description { get; set; }
            
            [JsonProperty("instructions", NullValueHandling = NullValueHandling.Ignore)]
            public string Instructions { get; set; }
            
            [JsonProperty("tools", NullValueHandling = NullValueHandling.Ignore)]
            public object Tools { get; set; }
            
            [JsonProperty("file_ids", NullValueHandling = NullValueHandling.Ignore)]
            public object FileIds { get; set; }
            
            [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
            public object MetaData { get; set; }
        }

        // assistant file
}