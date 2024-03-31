using Newtonsoft.Json;

namespace GPT
{
    public class ModerationResult
    {
        [JsonProperty("flagged")]
        public bool Flagged { get; set; }
        
        // list
        [JsonProperty("categories")] 
        public dynamic Categories { get; set; }
        
        // list
        [JsonProperty("category_scores")]
        public dynamic CategoryScores { get; set; }
    }
}