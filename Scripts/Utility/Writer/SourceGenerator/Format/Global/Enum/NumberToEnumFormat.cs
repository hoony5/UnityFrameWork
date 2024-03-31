using Writer.Core;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} : number,
    /// {1} : typeName,
    /// {2} : enumValue
    /// </summary>
    public class NumberToEnumFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $"\t\t\t{{0}} => {{1}}.{{2}},\n";
        }
    }
}