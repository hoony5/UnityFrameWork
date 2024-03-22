using System;
using Newtonsoft.Json;

public class TriggerConditionConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        TriggerCondition triggerCondition = value is TriggerCondition type ? type : TriggerCondition.None;
        writer.WriteValue(triggerCondition switch
        {
            TriggerCondition.None => "None",
            TriggerCondition.Greater => "Greater",
            TriggerCondition.Least => "Least",
            TriggerCondition.Equal => "Equal",
            TriggerCondition.Less => "Less",
            TriggerCondition.Most => "Most",
            TriggerCondition.Random => "Random",
            TriggerCondition.IncreasingInterval => "IncreasingInterval",
            TriggerCondition.DecreasingInterval => "DecreasingInterval",
            TriggerCondition.EveryTeamAttack => "EveryTeamAttack",
            TriggerCondition.EveryTeamHit => "EveryTeamHit",
            TriggerCondition.Success => "Success",
            TriggerCondition.Recovery => "Recovery",
            TriggerCondition.EveryTurn => "EveryTurn",
            TriggerCondition.CurrentTurn => "CurrentTurn",
            TriggerCondition.DifferentSex => "DifferentSex",
            TriggerCondition.ChanceHalfDecreased => "ChanceHalfDecreased",
            TriggerCondition.All => "All",

            _ => throw new StatCompareTypeConverterException( $"StatSearchTypeConverter: {triggerCondition} is not defined.")
        });
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string statSearchTypeString = reader.Value as string;
        TriggerCondition result = statSearchTypeString switch
        {
            "None" => TriggerCondition.None,
            "Greater" => TriggerCondition.Greater,
            "Least" => TriggerCondition.Least,
            "Equal" => TriggerCondition.Equal,
            "Less" => TriggerCondition.Less,
            "Most" => TriggerCondition.Most,
            "Random" => TriggerCondition.Random,
            "IncreasingInterval" => TriggerCondition.IncreasingInterval,
            "DecreasingInterval" => TriggerCondition.DecreasingInterval,
            "EveryTeamAttack" => TriggerCondition.EveryTeamAttack,
            "EveryTeamHit" => TriggerCondition.EveryTeamHit,
            "Success" => TriggerCondition.Success,
            "Recovery" => TriggerCondition.Recovery,
            "EveryTurn" => TriggerCondition.EveryTurn,
            "CurrentTurn" => TriggerCondition.CurrentTurn,
            "DifferentSex" => TriggerCondition.DifferentSex,
            "ChanceHalfDecreased" => TriggerCondition.ChanceHalfDecreased,
            "All" => TriggerCondition.All,
            _ => throw new StatCompareTypeConverterException($"Not supported StatSearchType: {statSearchTypeString}")
        };
        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TriggerCondition);
    }
}

public class StatCompareTypeConverterException : Exception
{
    public StatCompareTypeConverterException(string message) : base(message)
    {
    }
}
