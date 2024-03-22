using Newtonsoft.Json;

[JsonConverter(typeof(EffectTimingConverter))]
public enum EffectTiming
{
    None,
    PreTurn,
    OnRealTime,
    OnDeath,
    OnKill,
    OnHit,
    OnAnyAttack,
    OnNormalAttack,
    OnSkillAttack,
    OnTeamAttack,
    NextTurn,
    OnPassive,
}