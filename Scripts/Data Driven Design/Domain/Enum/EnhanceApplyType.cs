using Newtonsoft.Json;

[JsonConverter(typeof(EnhanceApplyTypeConverter))]
public enum EnhanceApplyType
{
    None,
    Add,
    Override,
    Reduce
}