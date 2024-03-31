using System.Collections.Generic;
using System.Linq;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class UnsubscribingEventWriter : ContentWriterBase
    {
        private readonly string unsubscribeModel;
        
        protected readonly DiagramNodeModel nodeModel;
        
        public UnsubscribingEventWriter(DiagramNodeModel nodeModel)
        {
            this.nodeModel = nodeModel;
            
            AddFormat(nameof(unsubscribeModel), new UnsubscribingEventFormat());
        }
        public override string WriteContent()
        {
            if(nodeModel.SubscribingEvents.Count == 0)
                return string.Empty;
            
            return string.Join('\n',
                nodeModel.SubscribingEvents.Select(x =>
                    string.Join('\n',
                        x.Methods.Select(y =>
                            $"    {GetFormat(nameof(unsubscribeModel)).Replace("{0}", x.Properties[0].Type!).Replace("{1}", y.Name)}"))));
        }
    }
}