using Newtonsoft.Json;

namespace GPT
{
    public class ModerationCategoryScore
    {
        [JsonProperty("hate")]
        public double Hate { get; set; }
        
        [JsonProperty("hate/threatening")]
        public double HateThreatening { get; set; }
        
        [JsonProperty("harassment")]
        public double Harassment { get; set; }
        
        [JsonProperty("harassment/threatening")]
        public double HarassmentThreatening { get; set; }
        
        [JsonProperty("self-harm")]
        public double SeflHarm { get; set; }
        
        [JsonProperty("self-harm/intent")]
        public double SeflHarmIntent { get; set; }
        
        [JsonProperty("self-harm/instructions")]
        public double SeflHarmInstructions { get; set; }
        
        [JsonProperty("sexual")]
        public double Sexual { get; set; }
        
        [JsonProperty("sexual/minors")]
        public double SexualMinors { get; set; }
        
        [JsonProperty("violence")]
        public double Violence { get; set; }
        
        [JsonProperty("violence/graphic")]
        public double ViolenceGraphic { get; set; }
    }
}