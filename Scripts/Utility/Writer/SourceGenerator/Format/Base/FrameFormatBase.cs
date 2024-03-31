using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    public abstract class FrameFormatBase : IWriterFormat
    {
        private string nameSpace;
        protected string usingSpace;
        
        protected FrameFormatBase(string nameSpace, string usingSpace)
        {
            this.nameSpace = nameSpace;
            this.usingSpace = usingSpace;
        }

        public virtual string GetFormat()
        {
            string content = GetFormatWithoutNamespace(usingSpace);
            return HasNamespace() ? GetFormatWithNamespace(usingSpace , content) : $"{content}";
        }

        protected abstract string GetFormatWithoutNamespace(string usingSpace);
        private string GetFormatWithNamespace(string usingSpace, string content)
        {
            return $@"{usingSpace}
namespace {nameSpace}
{{{content}}}";
        }

        private bool HasNamespace()
        {
            return !string.IsNullOrEmpty(nameSpace);
        }

    }
}