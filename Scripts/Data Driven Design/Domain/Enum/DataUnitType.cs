using Newtonsoft.Json;

[JsonConverter(typeof(DataUnitTypeConverter))]
public enum DataUnitType
{
    None,
    Numeric = 1,
    Percentage = 2,
}