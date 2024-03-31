using System.Collections.Generic;
using System.Text;

using Writer.Core;

namespace Writer
{
    public abstract class ContentWriterBase : IContentWriter
    {
        protected readonly StringBuilder contentBuilder;
        private readonly Dictionary<string, string> formatMap;
        
        protected ContentWriterBase(params (string key, string format)[] list)
        {
            contentBuilder = new StringBuilder();
            formatMap = new Dictionary<string, string>();
            
            if (list == null || list.Length == 0) return;
            foreach ((string key, string value) in list)
            {
                formatMap.Add(key, value);
            }
        }
        
        protected void AddFormat(string key, IWriterFormat formatProvider)
        {
            string format = formatProvider.GetFormat();
            formatMap.Add(key, format);
        }
        protected string GetFormat(string key)
        {
            return formatMap[key];
        }

        public virtual string WriteContent()
        {
            return string.Empty;
        }
    }
}