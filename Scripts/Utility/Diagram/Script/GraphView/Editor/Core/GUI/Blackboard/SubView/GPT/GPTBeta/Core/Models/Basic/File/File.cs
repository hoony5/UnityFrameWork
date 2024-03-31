using Newtonsoft.Json;

namespace GPT
{
    public class File
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("filename")]
        public string Name { get; set; }
        [JsonProperty("purpose")]
        public string Purpose { get; set; }
        [JsonProperty("bytes")]
        public long Bytes { get; set; }
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("status_details")]
        public string StatusDetails { get; set; }
    }
}