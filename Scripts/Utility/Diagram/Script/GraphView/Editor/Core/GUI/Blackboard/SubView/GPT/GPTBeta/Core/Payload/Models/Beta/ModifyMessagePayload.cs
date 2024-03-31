using Newtonsoft.Json;

namespace GPT
{
    public class ModifyMessagePayload
    {
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object MetaData { get; set; }
    }
}