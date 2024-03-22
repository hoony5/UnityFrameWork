using Newtonsoft.Json;

[JsonConverter(typeof(IncrementTypeConverter))]
public enum IncrementType
{
    None,
    Linear,
    Square,
    Step,
    Logarithmic,
    Exponential
}