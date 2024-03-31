using Newtonsoft.Json;

namespace GPT
{
    public class Hyperparameters
    {
        [JsonProperty("n_epochs")]
        public int NEpochs { get; set; }   
    }
}