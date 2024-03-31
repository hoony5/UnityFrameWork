using Newtonsoft.Json;

namespace GPT
{
    public class ChatCompletionChunk : ChatCompletionBase
    {
        [JsonProperty("object")] 
        public override string Object { get; protected set; } = "chat.completion.chunk";
    }
}