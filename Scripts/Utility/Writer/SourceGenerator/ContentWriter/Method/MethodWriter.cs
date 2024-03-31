using System.Linq;
using System.Text;
using Diagram;
using Writer.Ex;

namespace Writer.SourceGenerator.Format.Writer
{
    public class MethodWriter : NormalContentWriterBase
    {
        public MethodWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            AddFormat(nameof(methodFormat), new MethodFormat());
        }
        
        /// {0} : accessType,
        /// {1} : type,
        /// {2} : name,
        /// {3} : parameters,
        /// {4} : body
        public override string WriteContent()
        {
            if(nodeModel.Header.Methods.Count == 0) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (MethodInfo method in nodeModel.Header.Methods)
            {
                string declaration =
                    $"{(method.DeclarationType == DeclarationType.NonModified ? string.Empty : $" {method.DeclarationType.ToString().ToLower()}")}";
                string parameters = method.Parameters.Count == 0
                    ? ""
                    : string.Join(", ", method.Parameters.Select(p => $"{p.type} {p.name}"));
                string body = method.DeclarationType == DeclarationType.Abstract 
                    ? ";" 
                    : method.DeclarationType == DeclarationType.Virtual 
                        ? " => { }" 
                        : " { new NotImplementedException(); }";
                
                string content = GetFormat(nameof(methodFormat))
                    .Replace("{0}", $"{method.AccessType}{declaration}")
                    .Replace("{1}", method.Type)
                    .Replace("{2}", method.Name)
                    .Replace("{3}", $"{parameters}")
                    .Replace("{4}", $"{body}");
                
                sb.AppendLine(content);   
            }
            return sb.ToString();   
        }
    }

}