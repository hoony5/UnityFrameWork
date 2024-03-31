using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : AccessModifier + ReturnType
    /// {1} : Type
    /// {2} : Name
    /// {3} : Parameters
    /// {4} : Method Implementations
    /// </summary>
    /// <returns></returns>
    public class MethodFormat : IWriterFormat
    {
        public virtual string GetFormat()
        {
            return @"    {0} {1} {2}({3})
{4}
";
        }
    }
}
    
    