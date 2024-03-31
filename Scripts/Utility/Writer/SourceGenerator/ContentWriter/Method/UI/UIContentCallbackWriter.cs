using System.Collections.Generic;


namespace Writer.SourceGenerator.Format.Writer
{
    public class UIContentCallbackWriter : UIContentWriterBase
    {
        public UIContentCallbackWriter(IList<(string type, string name)> elementInfos)
        {
            this.elementInfos = elementInfos;
        }

        public override string WriteContent()
        {
            contentBuilder.Clear();

            // visualElement Query
            for (int i = 0; i < elementInfos.Count; i++)
            {
                (string type, string name) element = elementInfos[i];
                string content = new UICallbackFormat(element.type, element.name).GetFormat();
                
                if (string.IsNullOrEmpty(content)) 
                    continue;
                
                if (i == 0 || i == elementInfos.Count - 1)
                {
                    contentBuilder.Append($"\t{content}");
                }
                else
                {
                    contentBuilder.AppendLine($"\t{content}");
                }
            }

            return contentBuilder.ToString();
        }
    }
}