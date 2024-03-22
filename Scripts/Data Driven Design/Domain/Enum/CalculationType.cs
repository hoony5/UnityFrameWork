using Newtonsoft.Json;

[JsonConverter(typeof(CalculationTypeConverter))]
public enum CalculationType
{
    None,
    Equalize,
    Add,
    Subtract,
    Give,
    Drain,
    Multiply,
    Optional,
}