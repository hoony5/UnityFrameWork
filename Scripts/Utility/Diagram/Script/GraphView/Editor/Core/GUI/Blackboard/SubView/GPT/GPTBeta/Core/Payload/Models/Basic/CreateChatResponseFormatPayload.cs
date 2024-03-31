using Newtonsoft.Json;

namespace GPT
{
    public class CreateChatResponseFormatPayload
    {
        // text or json_object
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; } = "json_object";
    }
}