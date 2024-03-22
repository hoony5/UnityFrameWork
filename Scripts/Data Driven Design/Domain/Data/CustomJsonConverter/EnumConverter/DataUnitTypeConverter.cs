using System;
using Newtonsoft.Json;

public class DataUnitTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        DataUnitType dataUnitType = value is DataUnitType type ? type : DataUnitType.None;
        writer.WriteValue(dataUnitType switch
        {
            DataUnitType.None => "None",
            DataUnitType.Numeric => "Numeric",
            DataUnitType.Percentage => "Percentage",
            _ => throw new DataUnitTypeConverterException( $"Not supported DataUnitType: {dataUnitType}")
        });
;    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string dataUnitTypeString = reader.Value as string;
        DataUnitType result = dataUnitTypeString switch
        {
            "None" => DataUnitType.None,
            "Numeric" => DataUnitType.Numeric,
            "Percentage" => DataUnitType.Percentage,
            _ => throw new DataUnitTypeConverterException($"Not supported DataUnitType: {dataUnitTypeString}")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DataUnitType);
    }
}

public class DataUnitTypeConverterException : Exception
{
    public DataUnitTypeConverterException(string message) : base(message)
    {
    }
}