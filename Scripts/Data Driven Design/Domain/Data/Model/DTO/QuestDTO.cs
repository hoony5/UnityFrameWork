using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class QuestDTO : DTOBase
{
    [field:SerializeField, JsonProperty("QuestState")] public int QuestState { get; private set; }
    
    [JsonConstructor]
    public QuestDTO(string name, int level, int questState) : base(name, level)
    {
        QuestState = questState;
    } 
}