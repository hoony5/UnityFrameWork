using System;
using Newtonsoft.Json;

public class CalculationTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        CalculationType calculationType = value is CalculationType type ? type : CalculationType.None;
        writer.WriteValue(calculationType switch
        {
            CalculationType.None => "None",
            CalculationType.Add => "Add",
            CalculationType.Subtract => "Subtract",
            CalculationType.Drain => "Drain",
            CalculationType.Give => "Give",
            CalculationType.Multiply => "Multiply",
            CalculationType.Optional => "Optional",
            CalculationType.Equalize => "Equalize",
            _ => throw new CalculationTypeConverterException( $"Not supported CalculationType: {calculationType}")
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string calculationTypeString = reader.Value as string;
        CalculationType result = calculationTypeString switch
        {
            "None" => CalculationType.None,
            "Add" => CalculationType.Add,
            "Subtract" => CalculationType.Subtract,
            "Drain" => CalculationType.Drain,
            "Give" => CalculationType.Give,
            "Multiply" => CalculationType.Multiply,
            "Optional" => CalculationType.Optional,
            "Equalize" => CalculationType.Equalize,
            _ => throw new CalculationTypeConverterException($"Not supported CalculationType: {calculationTypeString}")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(CalculationType);
    }
}

public class CalculationTypeConverterException : Exception
{
    public CalculationTypeConverterException(string message) : base(message)
    {
    }
}
