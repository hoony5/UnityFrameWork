using System;
using Newtonsoft.Json;

public class ApplyTargetTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        ApplyTargetType applyTargetType = value is ApplyTargetType type ? type : ApplyTargetType.None;
        writer.WriteValue(applyTargetType switch
        {
            ApplyTargetType.None => "None",
            ApplyTargetType.All => "All",
            ApplyTargetType.Enemy => "Enemy",
            ApplyTargetType.Ally => "Ally",
            ApplyTargetType.Optional => "Optional",
            _ => throw new ApplyTargetTypeConverterException( $"ApplyTargetTypeConverter: {applyTargetType} is not defined.")
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string value = reader.Value as string;
        ApplyTargetType result = value switch
        {
            "None" => ApplyTargetType.None,
            "All" => ApplyTargetType.All,
            "Enemy" => ApplyTargetType.Enemy,
            "Ally" => ApplyTargetType.Ally,
            "Optional" => ApplyTargetType.Optional,
            _ => throw new ApplyTargetTypeConverterException( $"ApplyTargetTypeConverter: {value} is not defined.")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ApplyTargetType);
    }
}

public class ApplyTargetTypeConverterException : Exception
{
    public ApplyTargetTypeConverterException(string message) : base(message)
    {
    }
}
