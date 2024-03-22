using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class LevelDTO : DTOBase
{
    [field: SerializeField, JsonProperty("Exp")] public int Exp { get; private set; }

    [JsonConstructor]
    public LevelDTO (string name, int level, int exp) : base(name, level)
    {
        Exp = exp;
    }
}
