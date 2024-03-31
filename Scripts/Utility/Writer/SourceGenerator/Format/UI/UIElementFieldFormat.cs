using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    public class UIElementFieldFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return "public {0} {1};";
        }
    }

}