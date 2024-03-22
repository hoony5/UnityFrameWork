using System;
using Newtonsoft.Json;

public class EffectTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        EffectType effectType = value is EffectType type ? type : EffectType.None;
        writer.WriteValue(effectType switch
        {
            EffectType.None => "None",
            EffectType.Buff => "Buff",
            EffectType.Debuff => "Debuff",
            EffectType.Damage => "Damage",
            EffectType.Recovery => "Recovery",
            EffectType.Immune => "Immune",
            EffectType.State => "State",
            EffectType.Revive => "Revive",
            EffectType.ClearAll => "ClearAll",
            _ => throw new EffectTypeConverterException( $"Not supported EffectType: {effectType}")
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string effectTypeString = reader.Value as string;
        EffectType result = effectTypeString switch
        {
            "None" => EffectType.None,
            "Buff" => EffectType.Buff,
            "Debuff" => EffectType.Debuff,
            "Damage" => EffectType.Damage,
            "Recovery" => EffectType.Recovery,
            "Immune" => EffectType.Immune,
            "State" => EffectType.State,
            "Revive" => EffectType.Revive,
            "ClearAll" => EffectType.ClearAll,
            _ => throw new EffectTypeConverterException($"Not supported EffectType: {effectTypeString}")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(EffectType);
    }
}

public class EffectTypeConverterException : Exception
{
    public EffectTypeConverterException(string message) : base(message)
    {
    }
}
