
using Diagram;
using Writer.SourceGenerator.Format;

namespace Writer.SourceGenerator
{
    public class NormalContentWriterBase : ContentWriterBase
    {
        protected readonly string propertyFormat;
        protected readonly string methodFormat;

        protected readonly DiagramNodeModel nodeModel;

        protected NormalContentWriterBase(DiagramNodeModel nodeModel)
        {
            this.nodeModel = nodeModel;
        }
    }
}