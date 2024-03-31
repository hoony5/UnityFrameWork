using System.Collections.Generic;
using Newtonsoft.Json;

namespace GPT
{
    public class Files
    {
        [JsonProperty("data")] public List<File> Data { get; set; }
        [JsonProperty("object")] public string Object { get; set; }
    }
}
