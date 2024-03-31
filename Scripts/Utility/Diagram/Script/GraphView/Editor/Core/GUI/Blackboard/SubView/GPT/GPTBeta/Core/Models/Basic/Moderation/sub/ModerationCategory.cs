using Newtonsoft.Json;

namespace GPT
{
    public class ModerationCategory
    {
        [JsonProperty("hate")]
        public bool Hate { get; set; }
        
        [JsonProperty("hate/threatening")]
        public bool HateThreatening { get; set; }
        
        [JsonProperty("harassment")]
        public bool Harassment { get; set; }
        
        [JsonProperty("harassment/threatening")]
        public bool HarassmentThreatening { get; set; }
        
        [JsonProperty("self-harm")]
        public bool SeflHarm { get; set; }
        
        [JsonProperty("self-harm/intent")]
        public bool SeflHarmIntent { get; set; }
        
        [JsonProperty("self-harm/instructions")]
        public bool SeflHarmInstructions { get; set; }
        
        [JsonProperty("sexual")]
        public bool Sexual { get; set; }
        
        [JsonProperty("sexual/minors")]
        public bool SexualMinors { get; set; }
        
        [JsonProperty("violence")]
        public bool Violence { get; set; }
        
        [JsonProperty("violence/graphic")]
        public bool ViolenceGraphic { get; set; }
        
    }
}