using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    /// <summary>
    /// {0} : Type
    /// {1} : Name
    /// </summary>
    public class DDDMethodFormat : MethodFormat
    {
        /// <summary>
        /// {0} : AccessModifier + ReturnType
        /// {1} : Name
        /// {2} : Parameters
        /// {3} : Body
        /// </summary>
        /// <returns></returns>
        public override string GetFormat()
        {
            return base.GetFormat()
                .Replace("{0}", "public")
                .Replace("{3}",
                    @"{
    new NotImplementedException();
}");
        }
    }
}