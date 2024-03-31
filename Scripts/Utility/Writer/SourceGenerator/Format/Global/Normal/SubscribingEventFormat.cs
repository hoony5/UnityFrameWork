using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : type
    /// {1} : Name
    /// </summary>
    public class SubscribingEventFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return @"    Messenger.Default.Subscribe<{0}>({1});
";
        }
    }
}