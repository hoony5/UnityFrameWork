using System.Collections.Generic;

namespace Writer.SourceGenerator.Format.Writer
{
    public class UIFieldWriter : UIWriterBase
    {
        private readonly string elementFieldFormat;
        private readonly string elementNameFieldFormat;
        public UIFieldWriter(IList<(string type, string name)> elementInfos) 
        {
            AddFormat(nameof(elementFieldFormat), new UIElementFieldFormat());
            AddFormat(nameof(elementNameFieldFormat), new UIElementNameFieldFormat());
            
            this.elementInfos = elementInfos;
        }
        
        public override string WriteContent()
        {
            contentBuilder.Clear();

            for (int i = 0; i < elementInfos.Count; i++)
            {
                (string type, string name) element = elementInfos[i];
                // visualElement field
                AppendField(element);
                
                // visualElement's name field
                AppendNameField(element);
            }

            return contentBuilder.ToString();
        }

        private void AppendContent(string key, (string type, string name) element)
        {
            string content = string.Format(GetFormat(key), element.type, element.name);
            contentBuilder.Append($"{content}\n\t");
        }
        private void AppendField((string type, string name) element)
        {
            AppendContent(nameof(elementFieldFormat), element);
        }
        
        private void AppendNameField((string type, string name) element)
        {
            AppendContent(nameof(elementNameFieldFormat), element);
        }
    }

}