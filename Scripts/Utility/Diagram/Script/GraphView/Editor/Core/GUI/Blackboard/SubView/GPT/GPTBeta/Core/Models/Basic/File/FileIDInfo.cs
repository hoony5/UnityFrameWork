using Newtonsoft.Json;

namespace GPT
{
    public class FileIDInfo
    {
        [JsonProperty("file_id")]
        public string FileID { get; set; }
    }
}