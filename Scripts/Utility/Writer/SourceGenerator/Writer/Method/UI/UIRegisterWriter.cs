using System.Collections.Generic;
using System.Text;


namespace Writer.SourceGenerator.Format.Writer
{
    public class UIRegisterWriter : UIWriterBase
    {
        public string modelName;
        private RegisterType @operator;
        
        public UIRegisterWriter(IList<(string type, string name)> elementInfos, RegisterType @operator)
        {
            this.@operator = @operator;
            this.elementInfos = elementInfos;
        }
        
        public override string WriteContent()
        {
            contentBuilder.Clear();

            // visualElement Query
            for (int i = 0; i < elementInfos.Count; i++)
            {
                (string type, string name) element = elementInfos[i];
                string contentFormat = new UIRegisterCallbackFormat(element.type, element.name, @operator).GetFormat();
                string content = string.Format(contentFormat, modelName);
                
                if (string.IsNullOrEmpty(content)) 
                    continue;
                
                if (i == elementInfos.Count - 1)
                {
                    contentBuilder.Append($"\t\t{content}");
                }
                else
                {
                    contentBuilder.AppendLine($"\t\t{content}\n");
                }
            }

            return contentBuilder.ToString();
        }
    }
}