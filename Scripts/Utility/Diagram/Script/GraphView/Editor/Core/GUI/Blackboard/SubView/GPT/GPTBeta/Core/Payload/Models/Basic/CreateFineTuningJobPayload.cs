using Newtonsoft.Json;

namespace GPT
{
    public class CreateFineTuningJobPayload
    {
        [JsonProperty("model", NullValueHandling = NullValueHandling.Include)]
        public string Model { get; set; }
        
        [JsonProperty("training_file", NullValueHandling = NullValueHandling.Include)]
        public string TrainingFile { get; set; }
        
        [JsonProperty("validation_file", NullValueHandling = NullValueHandling.Include)]
        public string ValidationFile { get; set; }
        
        [JsonProperty("hyperparameters", NullValueHandling = NullValueHandling.Include)]
        public object HyperParameters { get; set; }
        
        [JsonProperty("suffix", NullValueHandling = NullValueHandling.Ignore)]
        public string Suffix { get; set; }
        
    }
}