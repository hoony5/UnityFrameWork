using Newtonsoft.Json;

namespace GPT
{
    public class CreateAssistantFilePayload
    {
        [JsonProperty("file_id", NullValueHandling = NullValueHandling.Include)]
        public string FileID { get; set; }
    }
}