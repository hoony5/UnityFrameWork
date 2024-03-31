using Newtonsoft.Json;

namespace GPT
{
    public class ParameterPayload
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        /*
         * i.e
         *     "properties": {
         *         "testA": {"type": "string", "description" : "testA"},
         *         "unit": {"type": "string", "enum" : ["m", "cm", "mm"]}
         *     },
         *     "required": ["testA"]
         */
        [JsonProperty("properties")]
        public object Properties { get; set; }
        
        [JsonProperty("required")]
        public string[] Required { get; set; }
    }

}