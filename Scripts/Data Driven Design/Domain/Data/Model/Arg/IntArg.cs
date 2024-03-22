using System;
using Share;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IntArg : ArgBase<int>
{
    public IntArg(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public int FlipSelf()
    {
        Value = -Value;
        return Value;
    }
    public sealed override void Modify(int value, ModificationType modificationType)
    {
        switch (modificationType)
        {
            case ModificationType.Set:
            case ModificationType.Round:
                Set(value);
                break;
            case ModificationType.Add:
                Add(value);
                break;
            case ModificationType.Flip:
                Flip(value);
                break;
            case ModificationType.Randomize:
                Randomize(value);
                break;
            case ModificationType.RandomDirectionValue:
                RandomDirectionValue(value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(modificationType), modificationType, null);
        }
    }

    public void ModifyFloat(float value, ModificationType modificationType)
    {
        Modify((int)value, modificationType);
    }

    public void ModifyDouble(double value, ModificationType modificationType)
    {
        Modify((int)value, modificationType);
    }
    public void ModifyLong(long value, ModificationType modificationType)
    {
        Modify((int)value, modificationType);
    }

    protected void Set(int value)
    {
        Value = value;
    }
    protected void Add(int value)
    {
        Value += value;
    }
    protected void Flip(int value)
    {
        Value = -Value;
    }
    protected void Randomize(int value)
    {
        Value = Random.Range(int.MinValue, int.MaxValue);
    }
    protected void RandomDirectionValue(int value)
    {
        int randomValue =  Random.Range(-Value, Value);
        Value = Mathf.Approximately(Value, randomValue) ? 0 : Value < randomValue ? 1 : -1;
    }
    public sealed override string ToString()
    {
#if UNITY_EDITOR
        return $@"
[Info]
    * Name                      : {Name}
    * Value                     : {Value.IntToString()}
";
#else
    return GetType().Name;
#endif
    }

    public sealed override bool Equals(object obj)
    {
        return obj is IntArg intArgItem && Name.Equals(intArgItem.Name);
    }

    public sealed override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}