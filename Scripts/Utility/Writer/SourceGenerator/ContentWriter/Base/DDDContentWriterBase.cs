using Diagram;
using Writer.SourceGenerator.Format;

namespace Writer.SourceGenerator
{
    public abstract class DDDContentWriterBase : ContentWriterBase
    {
        protected readonly string fieldFormat;
        protected readonly string methodFormat;
        
        protected DiagramNodeModel NodeModel;

        protected DDDContentWriterBase(DiagramNodeModel nodeModel)
        {
            NodeModel = nodeModel;
            
            AddFormat(nameof(fieldFormat), new DDDFieldFormat());
            AddFormat(nameof(methodFormat), new DDDMethodFormat());
        }
    }
}