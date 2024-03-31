using System.Collections.Generic;
using System.Linq;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class DDDAggregatePropertyWriter : DDDContentWriterBase
    {
        private string propertyFormat;

        public DDDAggregatePropertyWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            AddFormat(nameof(propertyFormat), new DDDAggregatePropertyFormat());
        }
        
        public override string WriteContent()
        {
            IEnumerable<string> propertyLines = NodeModel.Header.Properties.Select(item =>
                GetFormat(nameof(propertyFormat))
                    .Replace("{0}", item.Type)
                    .Replace("{1}", item.Name));

            return string.Join('\n', propertyLines);
        }
    }
}