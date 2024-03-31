using System.Collections.Generic;
using Writer.SourceGenerator.Format;
using static Share.CollectionEx;
namespace Writer.AssetGenerator
{
    public abstract class UIDocumentWriterBase : ContentWriterBase
    {
        protected readonly string uxmlFormatStartDefault;
        protected readonly string uxmlFormatStartWithChildren;
        protected readonly string uxmlAttributesFormat;
        protected readonly string uxmlStyleFormat;
        protected readonly string uxmlStyleItemFormat;
        protected readonly string uxmlForamtDefulat;
        
        protected readonly string ussBlackFormat;
        protected readonly string ussBlackContetnFormat;
        
        protected readonly string space = " ";
        protected readonly string tab = "    ";
        /// <summary>
        /// style name with value. i.e {"background-color", "rgba(0,0,0,0.5)"}
        /// </summary>
        protected Dictionary<string, object> stylePairs;
        
        protected UIDocumentWriterBase(params (string key, string format)[] list) : base(list)
        {
            stylePairs = new Dictionary<string, object>();
            
            AddFormat(nameof(uxmlFormatStartDefault), new UxmlFormatStartDefault());
            AddFormat(nameof(uxmlFormatStartWithChildren), new UxmlFormatStartWithChildren());
            AddFormat(nameof(uxmlAttributesFormat), new UxmlAttributeFormat());
            AddFormat(nameof(uxmlStyleFormat), new UxmlStyleFormat());
            AddFormat(nameof(uxmlStyleItemFormat), new UxmlStyleItemFormat());
            AddFormat(nameof(uxmlForamtDefulat), new UxmlFormatDefault());
            AddFormat(nameof(ussBlackFormat), new UssBlackFormat());
            AddFormat(nameof(ussBlackContetnFormat), new UssBlackContentFormat());
        }
        
        
        public void SetStyle(string name, object value)
        {
            stylePairs.AddOrUpdate(name, value);
        }
        
        public virtual void Clear()
        {
            stylePairs.Clear();
        }


    }

}