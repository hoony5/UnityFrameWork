using Newtonsoft.Json;

[JsonConverter(typeof(TriggerConditionConverter))]
public enum TriggerCondition
{
    None,
    Most,
    Least,
    Equal,
    Greater,
    Less,
    Random,
    IncreasingInterval,
    DecreasingInterval,
    EveryTeamAttack,
    EveryTeamHit,
    Success,
    Recovery,
    CurrentTurn,
    EveryTurn,
    DifferentSex,
    ChanceHalfDecreased,
    All,
}
