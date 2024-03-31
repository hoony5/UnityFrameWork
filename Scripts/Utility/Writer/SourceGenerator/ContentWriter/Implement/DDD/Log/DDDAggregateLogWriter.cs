using System.Collections.Generic;
using System.Linq;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class DDDAggregateLogWriter : DDDContentWriterBase
    {
        private string toStringFormat;
        public DDDAggregateLogWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            AddFormat(nameof(toStringFormat), new DDDAggregateToStringFormat(NodeModel.Header.Properties.Count != 0));
        }

        public override string WriteContent()
        {
            IEnumerable<string> propertyNames = NodeModel.Header.Properties.Select(item => $"{char.ToUpper(item.Name[0])}{item.Name[1..]}");
            return GetFormat(nameof(toStringFormat))
                .Replace("{0}", NodeModel.Header.Name)
                .Replace("{1}", string.Join(", ", propertyNames));
        }
    }

}