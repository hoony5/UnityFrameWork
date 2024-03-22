using System;
using Newtonsoft.Json;

public class TargetSpaceConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        TargetSpace targetSpace = value is TargetSpace type ? type : TargetSpace.None;
        writer.WriteValue(targetSpace switch
        {
            TargetSpace.None => "None",
            TargetSpace.All => "All",
            TargetSpace.Random_0 => "Random_0",
            TargetSpace.Random_1 => "Random_1",
            TargetSpace.Random_2 => "Random_2",
            TargetSpace.Random_3 => "Random_3",
            TargetSpace.Column_0 => "Column_0",
            TargetSpace.Column_1 => "Column_1",
            TargetSpace.Row_0 => "Row_0",
            TargetSpace.Row_1 => "Row_1",
            TargetSpace.Row_2 => "Row_2",
            TargetSpace.Near => "Near",
            TargetSpace.One_Front => "One_Front",
            TargetSpace.One_Back => "One_Back",
            TargetSpace.Self => "Self",
            _ => "None"
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string value = reader.Value as string;
        TargetSpace result = value switch
        {
            "None" => TargetSpace.None,
            "All" => TargetSpace.All,
            "Random_0" => TargetSpace.Random_0,
            "Random_1" => TargetSpace.Random_1,
            "Random_2" => TargetSpace.Random_2,
            "Random_3" => TargetSpace.Random_3,
            "Column_0" => TargetSpace.Column_0,
            "Column_1" => TargetSpace.Column_1,
            "Row_0" => TargetSpace.Row_0,
            "Row_1" => TargetSpace.Row_1,
            "Row_2" => TargetSpace.Row_2,
            "Near" => TargetSpace.Near,
            "One_Front" => TargetSpace.One_Front,
            "One_Back" => TargetSpace.One_Back,
            "Self" => TargetSpace.Self,
            _ => TargetSpace.None
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TargetSpace);
    }
}

public class TargetSpaceConverterException : Exception
{
    public TargetSpaceConverterException(string message) : base(message)
    {
    }
}
