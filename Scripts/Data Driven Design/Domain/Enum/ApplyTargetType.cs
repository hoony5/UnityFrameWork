using Newtonsoft.Json;

[JsonConverter(typeof(ApplyTargetTypeConverter))]
public enum ApplyTargetType
{
    None,
    Ally,
    Enemy,
    Optional,
    All,
}