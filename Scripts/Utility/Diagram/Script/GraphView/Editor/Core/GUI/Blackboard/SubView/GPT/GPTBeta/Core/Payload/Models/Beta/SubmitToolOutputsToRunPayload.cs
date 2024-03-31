using Newtonsoft.Json;

namespace GPT
{
    public class SubmitToolOutputsToRunPayload
    {
        [JsonProperty("tool_outputs", NullValueHandling = NullValueHandling.Include)]
        public ToolOutputPayload ToolOutputs { get; set; }
    }
}