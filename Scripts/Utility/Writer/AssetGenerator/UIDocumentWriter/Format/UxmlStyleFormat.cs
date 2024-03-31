using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : styles
    /// </summary>
    public class UxmlStyleFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $"\tstyle=\"{{0}}\"";

        }
    }
}