using System;
using Newtonsoft.Json;

public class EnhanceApplyTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        EnhanceApplyType enhanceApplyType = value is EnhanceApplyType type ? type : EnhanceApplyType.None;
        writer.WriteValue(enhanceApplyType switch
        {
            EnhanceApplyType.None => "None",
            EnhanceApplyType.Add => "Addition",
            EnhanceApplyType.Override => "Override",
            EnhanceApplyType.Reduce => "Delete",
            _ => throw new EnhanceApplyTypeConverterException( $"Not supported EnhanceApplyType: {enhanceApplyType}")
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string enhanceApplyTypeString = reader.Value as string;
        EnhanceApplyType result = enhanceApplyTypeString switch
        {
            "None" => EnhanceApplyType.None,
            "Addition" => EnhanceApplyType.Add,
            "Override" => EnhanceApplyType.Override,
            "Delete" => EnhanceApplyType.Reduce,
            _ => throw new EnhanceApplyTypeConverterException($"Not supported EnhanceApplyType: {enhanceApplyTypeString}")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DataUnitType);
    }
}

public class EnhanceApplyTypeConverterException : Exception
{
    public EnhanceApplyTypeConverterException(string message) : base( message)
    {
        
    }
}
