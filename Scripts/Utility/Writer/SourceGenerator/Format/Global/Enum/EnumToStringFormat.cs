using Writer.Core;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} : typeName,
    /// {1} : enumValue,
    /// </summary>
    public class EnumToStringFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $"\t\t\t{{0}}.{{0}} => nameof({{0}}.{{1}}),\n";
        }
    }
}