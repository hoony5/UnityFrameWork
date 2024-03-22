using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class ObjectCategory : ICategory
{
    [field: SerializeField, JsonProperty(nameof(TopCategory))] public string TopCategory { get; protected set; }
    [field: SerializeField, JsonProperty(nameof(MiddleCategory))] public string MiddleCategory { get; protected set; }
    [field: SerializeField, JsonProperty(nameof(BottomCategory))] public string BottomCategory { get; protected set; }
    [field: SerializeField, JsonProperty(nameof(ObjectName))] public string ObjectName { get; protected set; }
    [field: SerializeField, JsonProperty(nameof(ObjectID))] public int ObjectID { get; protected set; }
    [field: SerializeField] public string DisplayName { get; protected  set; }
    
    public ObjectCategory(){ }
    [JsonConstructor]
    public ObjectCategory(string topCategory, string middleCategory, string bottomCategory, string objectName, int objectID)
    {
        TopCategory = topCategory;
        MiddleCategory = middleCategory;
        BottomCategory = bottomCategory;
        ObjectName = objectName;
        ObjectID = objectID;
        DisplayName = ObjectName;
    }
    public void SetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }
    public bool Equals(ICategory category)
    {
        return TopCategory == category.TopCategory &&
               MiddleCategory == category.MiddleCategory &&
               BottomCategory == category.BottomCategory &&
               ObjectName == category.ObjectName;
    }

  public override string ToString()
  {
#if UNITY_EDITOR
        return $@"
[DataModel]
    * TopCategory: {TopCategory}
    * MiddleCategory: {MiddleCategory}
    * BottomCategory: {BottomCategory}
    * ObjectName: {ObjectName}
    * ObjectID: {ObjectID}
    * DisplayName: {DisplayName}
";
#else
        return base.ToString();
#endif
    }
}