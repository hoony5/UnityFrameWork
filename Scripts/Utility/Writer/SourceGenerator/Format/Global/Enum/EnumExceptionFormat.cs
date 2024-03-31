using Writer.Core;

namespace Writer.SourceGenerator
{
    /// <summary>
    /// {0} : typeName,
    /// {1} : inputValueName,
    /// </summary>
    public class EnumExceptionFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return $"\t\t\t_ => throw new {{0}}JsonConverterException( $\"Not supported {{0}}: {{1}}\")";
        }
    }
}