using Newtonsoft.Json;

namespace GPT
{
    public class CreateFineTuningHyperparametersPayload
    {
        [JsonProperty("batch_size")]
        public int BatchSize { get; set; }
        
        [JsonProperty("learning_rate_multiplier")]
        public double LearningRateMultiplier { get; set; }
        
        [JsonProperty("n_epochs")]
        public int NEpochs { get; set; }   
    }
}