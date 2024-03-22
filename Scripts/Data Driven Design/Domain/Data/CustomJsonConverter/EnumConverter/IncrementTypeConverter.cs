using System;
using Newtonsoft.Json;

public class IncrementTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        IncrementType incrementalType = value is IncrementType type ? type : IncrementType.None;
        writer.WriteValue(incrementalType switch
        {
            IncrementType.None => "None",
            IncrementType.Linear => "Linear",
            IncrementType.Square => "Square",
            IncrementType.Step => "Step",
            IncrementType.Logarithmic => "Logarithmic",
            IncrementType.Exponential => "Exponential",
            _ => throw new IncrementTypeConverterException( $"Not supported IncrementType: {incrementalType}")
        });
;    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string incrementTypeString = reader.Value as string;
        IncrementType result = incrementTypeString switch
        {
            "None" => IncrementType.None,
            "Linear" => IncrementType.Linear,
            "Square" => IncrementType.Square,
            "Step" => IncrementType.Step,
            "Logarithmic" => IncrementType.Logarithmic,
            "Exponential" => IncrementType.Exponential,
            _ => throw new IncrementTypeConverterException($"Not supported IncrementType: {incrementTypeString}")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DataUnitType);
    }
}

public class IncrementTypeConverterException : Exception
{
    public IncrementTypeConverterException(string message) : base(message)
    {
    }
}