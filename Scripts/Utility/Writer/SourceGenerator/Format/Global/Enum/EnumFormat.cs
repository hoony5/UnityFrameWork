using Writer.SourceGenerator.Format;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} typeName
    /// {1} content
    /// </summary>
    public class EnumFormat : FrameFormatBase
    {
        protected override string GetFormatWithoutNamespace(string usingSpace)
        {
            return $@"{usingSpace}
[JsonConverter(typeof({{0}}JsonConverter))]
public enum {{0}}
{{
    {{1}}
}}";
        }

        public EnumFormat(
            string nameSpace,
            string usingSpace) 
            : base(nameSpace, usingSpace)
        {
            
        }
    }
}