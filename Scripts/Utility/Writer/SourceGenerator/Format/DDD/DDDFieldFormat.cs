using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Type
    /// {1} : Name
    /// </summary>
    public class DDDFieldFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return "\tprivate {0} {1};";
        }
    }
}