using Writer.Core;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} : number,
    /// {1} : typeName,
    /// {2} : enumValue
    /// </summary>
    public class EnumToNumberFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $"\t\t\t{{1}}.{{2}} => {{0}},\n";
        }
    }
}