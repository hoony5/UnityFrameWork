using System;
using Newtonsoft.Json;

[Serializable]
public abstract class ValueObject : ObjectCategory
{
    protected ValueObject() { }
    [JsonConstructor]
    protected ValueObject(string topCategory, string middleCategory, string bottomCategory, string objectName, int objectID)
        : base(topCategory, middleCategory, bottomCategory, objectName, objectID)
    {
    }
    public override string ToString()
    {
#if UNITY_EDITOR
        return $@"
[Info]
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