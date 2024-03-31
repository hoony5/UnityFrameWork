using Writer.Core;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} : typeName,
    /// {1} : enumValue
    /// </summary>
    public class StringToEnumFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $"\t\t\t\"{{1}}\" => {{0}}.{{1}},\n";
        }
    }
}