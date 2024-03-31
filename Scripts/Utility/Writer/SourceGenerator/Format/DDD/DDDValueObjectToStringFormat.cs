using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Name
    /// </summary>
    public class DDDValueObjectToStringFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return "\t*   {0}\t\t\t\t\t\t\t\t\t: {{0}}";
        }
    }
}