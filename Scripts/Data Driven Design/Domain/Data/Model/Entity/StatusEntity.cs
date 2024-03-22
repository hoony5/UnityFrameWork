using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[System.Serializable]
public class StatusEntity : Entity
{
    [field:SerializeField] public SerializedDictionary<string, int> Map { get; private set; }
    public StatusEntity(string id, string auth, IDictionary<string, int> map) : base(id, auth)
    {
        Map = new SerializedDictionary<string, int>(map);
    }

    public void Reset()
    {
        foreach (string name in Map.Keys)
        {
            Map[name] = 0;
        }
    }

    public void Modify(string name, int value, ModificationType modificationType)
    {
        if (Map is null) throw new Exception($"{nameof(Map)} is null.");
        if (!Map.ContainsKey(name))
            throw new Exception($"{nameof(name)} | {name} is not found.");

        switch (modificationType)
        {
            case ModificationType.Set:
                Map[name] = value;
                break;
            case ModificationType.Add:
                Map[name] += value;
                break;
            case ModificationType.Randomize:
                Map[name] = UnityEngine.Random.Range(-value, value);
                break;
            case ModificationType.Round:
            case ModificationType.Flip:
            case ModificationType.RandomDirectionValue:
            default:
                throw new Exception(
                    $"{nameof(modificationType)} | {modificationType} is not supported.");
        }
    }
    public override string ToString()
    {
#if UNITY_EDITOR
        return $@"
[Status]
    *   Map                           : {string.Join(",\n                                    ", Map.Select(x => $"{x.Key} : {x.Value}"))}
";
#else
        return GetType().ToString();
#endif
    }
}