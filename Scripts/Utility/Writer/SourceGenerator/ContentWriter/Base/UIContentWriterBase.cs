using System.Collections.Generic;

namespace Writer.SourceGenerator.Format.Writer
{
    public abstract class UIContentWriterBase : ContentWriterBase
    {
        protected IList<(string type, string name)> elementInfos;
    }
}