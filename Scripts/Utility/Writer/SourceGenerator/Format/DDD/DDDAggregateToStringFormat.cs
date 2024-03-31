using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    public class DDDAggregateToStringFormat : IWriterFormat
    {
        private bool isAvailable;
        
        public DDDAggregateToStringFormat(bool isAvailable)
        {
            this.isAvailable = isAvailable;    
        }
        public string GetFormat()
        {
            return $@"{(isAvailable ? "" : "\nbase.ToString()\n")}[{{0}}]
{{1}}";
        }
    }
}