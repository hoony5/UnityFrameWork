using Newtonsoft.Json;

namespace GPT
{
    public class ModifyThreadPayload
    {
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object MetaData { get; set; }
    }
}