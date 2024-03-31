using Newtonsoft.Json;

namespace GPT
{
    public class ModifyRunPayload
    {
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object MetaData { get; set; }
    }
}