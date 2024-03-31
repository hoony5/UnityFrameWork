using Newtonsoft.Json;

namespace GPT
{
    public class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
        
        [JsonProperty("param")]
        public string Param { get; set; }
    }
}