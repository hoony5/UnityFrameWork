using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    public class UIElementNameFieldFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return "public readonly string {0}Name = \"{0}\";";
        }
    }

}