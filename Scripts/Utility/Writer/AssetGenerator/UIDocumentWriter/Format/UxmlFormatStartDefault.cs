using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Type
    /// {1} : Attributes
    /// {2} : Styles
    /// </summary>
    public class UxmlFormatStartDefault: IWriterFormat
    {
        public string GetFormat()
        {
            return "<ui:{0}{1}{2} />";
        }
    }
}