using Newtonsoft.Json;

[JsonConverter(typeof(EffectTypeConverter))]
public enum EffectType
{
    None,
    Damage,
    Recovery,
    Buff,
    Debuff,
    State,
    ClearAll,
    Revive,
    Immune,
}