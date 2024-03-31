using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Attributes
    /// {1} : AccessModifier + ReturnType
    /// {2} : Type
    /// {3} : Name
    /// {4} : Getter Setter
    /// </summary>
    /// <returns></returns>
    public class PropertyFormat : IWriterFormat
    {
        public virtual string GetFormat()
        {
            return "   {0} {1} {2} {3} {4}";
        }
    }
}