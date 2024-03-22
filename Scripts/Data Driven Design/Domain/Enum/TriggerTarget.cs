using Newtonsoft.Json;

[JsonConverter(typeof(TriggerTargetConverter))]
public enum TriggerTarget
{
    None,
    Self,
    Ally,
    EnemyOne,
}
