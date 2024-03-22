using System;
using Newtonsoft.Json;

public class EffectTimingConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        EffectTiming effectTiming = value is EffectTiming timing ? timing : EffectTiming.None;
        writer.WriteValue( effectTiming switch
        {
            EffectTiming.None => "None",
            EffectTiming.PreTurn => "PreTurn",
            EffectTiming.OnKill => "OnKill",
            EffectTiming.OnDeath => "OnDeath",
            EffectTiming.OnHit => "OnHit",
            EffectTiming.OnAnyAttack => "OnAnyAttack",
            EffectTiming.OnNormalAttack => "OnNormalAttack",
            EffectTiming.OnSkillAttack => "OnSkillAttack",
            EffectTiming.OnTeamAttack => "OnTeamAttack",
            EffectTiming.NextTurn => "NextTurn",
            EffectTiming.OnRealTime => "OnRealTime",
            EffectTiming.OnPassive => "OnPassive",
            _ => throw new AttackTimingConverterException( $"AttackTimingConverter: {effectTiming} is not defined.") 
        }); 
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string value = reader.Value as string;
        EffectTiming result = value switch
        {
            "None" => EffectTiming.None,
            "PreTurn" => EffectTiming.PreTurn,
            "OnKill" => EffectTiming.OnKill,
            "OnDeath" => EffectTiming.OnDeath,
            "OnHit" => EffectTiming.OnHit,
            "OnAnyAttack" => EffectTiming.OnAnyAttack,
            "OnNormalAttack" => EffectTiming.OnNormalAttack,
            "OnSkillAttack" => EffectTiming.OnSkillAttack,
            "OnTeamAttack" => EffectTiming.OnTeamAttack,
            "NextTurn" => EffectTiming.NextTurn,
            "OnRealTime" => EffectTiming.OnRealTime,
            "OnPassive" => EffectTiming.OnPassive,
            _ => throw new AttackTimingConverterException( $"AttackTimingConverter: {value} is not defined.")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(EffectTiming);
    }
}

public class AttackTimingConverterException : Exception
{
    public AttackTimingConverterException(string message) : base(message)
    {
    }
}
