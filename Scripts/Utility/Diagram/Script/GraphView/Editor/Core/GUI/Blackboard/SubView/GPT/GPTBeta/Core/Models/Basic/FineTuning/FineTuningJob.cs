using Newtonsoft.Json;

namespace GPT
{
    public class FineTuningJob
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("object")]
        public string Object { get; private set; } = "fine_tuning.job";
        
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }
        
        [JsonProperty("fine_tuned_model")]
        public string FineTunedModel { get; set; }
        
        [JsonProperty("finished_at")]
        public int FinishedAt { get; set; }
        
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [JsonProperty("organization_id")]
        public string OrganizationID { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("training_files")]
        public string TrainingFiles { get; set; }
        
        [JsonProperty("validation_file")]
        public string ValidationFile { get; set; }
        
        [JsonProperty("trained_tokens")]
        public int TrainedTokens { get; set; }
        
        [JsonProperty("result_files")]
        public dynamic ResultFiles { get; set; }
        
        [JsonProperty("error")]
        public dynamic Error { get; set; }
        
        [JsonProperty("hyperparameters")]
        public dynamic Hyperparameters { get; set; }
    }
}