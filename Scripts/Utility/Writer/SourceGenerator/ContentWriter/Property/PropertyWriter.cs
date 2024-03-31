using System.Text;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class PropertyWriter : NormalContentWriterBase
    {
        public PropertyWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            AddFormat(nameof(propertyFormat), new PropertyFormat());
        }
        
        /// <summary>
        /// {0} : Attributes
        /// {1} : AccessModifier + ReturnType
        /// {2} : Type
        /// {3} : Name
        /// {4} : Getter Setter
        /// </summary>
        /// <returns></returns>
        public override string WriteContent()
        {
            if(nodeModel.Header.Properties.Count == 0) return string.Empty;
            
            StringBuilder sb = new StringBuilder();
            foreach (PropertyFieldInfo property in nodeModel.Header.Properties)
            {
                string declaration = property.DeclarationType == DeclarationType.NonModified
                    ? string.Empty
                    : $" {property.DeclarationType.ToString().ToLower()}";
                
                string content = GetFormat(nameof(propertyFormat))
                    .Replace("{0}", $"[field:SerializeField]")
                    .Replace("{1}", $"public{declaration}")
                    .Replace("{2}", property.Type)
                    .Replace("{3}", property.Name)
                    .Replace("{4}", "{ get; init; }");
            
                sb.AppendLine(content);
            }
            return sb.ToString();
        }
    }

}