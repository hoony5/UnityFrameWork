using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Name
    /// {1} : Selector
    /// {2} : Content
    /// </summary>
    public class UssBlackFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $@".{{0}}{{1}}
{{
{{2}}
}}"
                ;
        }
    }
}