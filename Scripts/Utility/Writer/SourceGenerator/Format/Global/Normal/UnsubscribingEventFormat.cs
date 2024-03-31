using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : type
    /// {1} : Name
    /// </summary>
    public class UnsubscribingEventFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return @"    Messenger.Default.Unsubscribe<{0}>({1});
";
        }
    }
}