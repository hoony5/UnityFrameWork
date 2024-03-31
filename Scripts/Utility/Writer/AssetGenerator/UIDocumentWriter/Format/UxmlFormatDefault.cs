using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : indent
    /// {1} : content
    /// </summary>
    public class UxmlFormatDefault : IWriterFormat
    {
        public string GetFormat()
        {
            return "{0}{1}";
        }
    }
}