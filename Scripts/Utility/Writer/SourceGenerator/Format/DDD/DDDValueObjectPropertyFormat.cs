using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Type
    /// {1} : Name
    /// </summary>
    public class DDDValueObjectPropertyFormat : PropertyFormat
    {
        public override string GetFormat()
        {
            return base.GetFormat()
                .Replace("{1}", "public")
                .Replace("{0}", "[field:SerializeField, JsonProperty(nameof({1}))]")
                .Replace("{2}", "{0}")
                .Replace("{3}", "{1}")
                .Replace("{4}", "{ get; init; }");
        }
    }
}