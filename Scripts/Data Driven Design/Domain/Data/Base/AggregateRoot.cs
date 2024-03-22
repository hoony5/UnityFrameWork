[System.Serializable]
public abstract class AggregateRoot : ObjectCategory
{
    protected AggregateRoot(){ }
    protected AggregateRoot(ObjectCategory objectCategory) : base(objectCategory.TopCategory ,objectCategory.MiddleCategory,
        objectCategory.BottomCategory, objectCategory.ObjectName, objectCategory.ObjectID)
    {
    }

  public override string ToString()
    {
#if UNITY_EDITOR
        return $@"
{base.ToString()}
";
#else
        return base.ToString();
#endif
    }
}
