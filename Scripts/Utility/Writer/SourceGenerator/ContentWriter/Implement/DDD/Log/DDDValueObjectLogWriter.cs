using System.Collections.Generic;
using System.Linq;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class DDDValueObjectLogWriter : DDDContentWriterBase
    {
        private string toStringFormat;
        public DDDValueObjectLogWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            AddFormat(nameof(toStringFormat), new DDDValueObjectToStringFormat());
        }

        public override string WriteContent()
        {
            IEnumerable<string> logLines = NodeModel.Header
                .Properties.Select(item => GetFormat(nameof(toStringFormat)).Replace("{0}", item.Name));
            return string.Join('\n', logLines);
        }
    }
}