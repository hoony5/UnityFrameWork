using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Name
    /// {1} : Value
    /// </summary>
    public class UssBlackContentFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $"\t{{0}}: {{1}};";
        }
    }
}