using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : callbackName
    /// </summary>
    public class PublishingEventFormat : IWriterFormat
    {
        public string GetFormat()
        {
            return @"    Messenger.Default.Publish<{1}>({0}());
";
        }
    }
}