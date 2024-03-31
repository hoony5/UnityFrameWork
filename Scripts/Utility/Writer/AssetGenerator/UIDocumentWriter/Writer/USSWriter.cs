using System.Collections.Generic;
using System.Linq;
using System.Text;
using Writer.SourceGenerator.Format;

namespace Writer.AssetGenerator.UIElement
{
    public class USSWriter : UIDocumentWriterBase
    {
        public string StyleName { get; set; }
        public string Selectors { get; set; }
        
        public override string WriteContent()
        {
            return GetUssBlock(StyleName, Selectors, stylePairs.Select(pair => (pair.Key, pair.Value)).ToArray());
        }

        private string GetUssBlock(string styleName, string selectors ,params (string name, object value)[] styles)
        {
            StringBuilder sb = new StringBuilder();
            string contentFormat = GetFormat(nameof(ussBlackContetnFormat));
            
            for (var index = 0; index < styles.Length; index++)
            {
                (string name, object value) = styles[index];
                sb.AppendFormat(contentFormat, name, value);
            }
            
            string blockFormat = GetFormat(nameof(blockFormat));
            return string.Format(blockFormat, styleName, selectors, sb);
        }
    }
}