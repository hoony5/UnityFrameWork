using Newtonsoft.Json;

namespace GPT
{
    public class ChatCompletion : ChatCompletionBase
    {
        [JsonProperty("usage")]
        public dynamic Usage { get; set; }

        [JsonProperty("object")]
        public override string Object { get; protected set; } = "chat.completion";
    }
}