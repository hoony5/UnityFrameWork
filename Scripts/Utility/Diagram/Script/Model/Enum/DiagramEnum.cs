namespace Diagram
{
    public enum CopyType
    {
        Default,
        Cut,
        Duplicate
    }
    public enum AccessType
    {
        Public,
        Public_Partial,
        Public_ReadOnly,
        Public_Abstract,
        Public_Static,
        Public_Sealed,
        Internal,
        Internal_Partial,
        Internal_ReadOnly,
        Internal_Abstract,
        Internal_Static,
        Internal_Sealed,
    }
    public enum DeclarationType
    {
        NonModified,
        Abstract,
        Virtual,
        Static
    }
    public enum EntityType
    {
        None,
        ValueObject,
        Aggregate
    }

    public enum EventType
    {
        Not_Used,
        Publish,
        Subscribe,
        Both
    }

    public enum ExportFileType
    {
        Not_Used,
        Text,
        CSV,
        MarkDown,
        Confluence,
        Slack
    }
    public enum DescriptionType
    {
        Not_Used,
        Default,
        Table,
        Summary,
        CheckList,
    }
    public enum AccessKeyword
    {
        Class,
        Interface,
        Enum,
        Struct,
        Record
    }
    public enum GraphElementType
    {
        Normal,
        Event,
        Note,
        Memo,
        Group
    }
}