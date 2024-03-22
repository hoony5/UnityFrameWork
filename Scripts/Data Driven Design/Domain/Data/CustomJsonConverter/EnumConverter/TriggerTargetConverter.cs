using System;
using Newtonsoft.Json;

public class TriggerTargetConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        TriggerTarget triggerTarget = value is TriggerTarget type ? type : TriggerTarget.Self;
        writer.WriteValue(triggerTarget switch
        {
            TriggerTarget.None => "None",
            TriggerTarget.Self => "Self",
            TriggerTarget.Ally => "Ally",
            TriggerTarget.EnemyOne => "EnemyOne",
            _ => throw new TriggerTargetConverterException( $"TriggerTargetConverter: {triggerTarget} is not defined.")
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string value = reader.Value as string; 
        TriggerTarget result = value switch
        {
            "None" => TriggerTarget.None, 
            "Self" => TriggerTarget.Self,
            "Ally" => TriggerTarget.Ally,
            "EnemyOne" => TriggerTarget.EnemyOne,
            _ => throw new TriggerTargetConverterException( $"TriggerTargetConverter: {value} is not defined.")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TriggerTarget);
    }
}

public class TriggerTargetConverterException : Exception
{
    public TriggerTargetConverterException(string msg) : base(msg)
    {

    }
}