using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Name
    /// {1} : Value
    /// </summary>
    public class UxmlStyleItemFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return "{0}: {1};";

        }
    }
}