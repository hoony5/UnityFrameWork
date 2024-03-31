using System.Collections.Generic;
using System.Linq;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class SubscribingEventWriter : ContentWriterBase
    {
        private readonly string subscribeFormat;
        
        protected readonly DiagramNodeModel nodeModel;
        
        public SubscribingEventWriter(DiagramNodeModel nodeModel)
        {
            this.nodeModel = nodeModel;
            
            AddFormat(nameof(subscribeFormat), new SubscribingEventFormat());
        }

        public override string WriteContent()
        {
            if(nodeModel.SubscribingEvents.Count == 0)
                return string.Empty;
            
            return string.Join('\n',
                nodeModel.SubscribingEvents.Select(x =>
                    string.Join('\n',
                        x.Methods.Select(y =>
                            $"    {GetFormat(nameof(subscribeFormat)).Replace("{0}", x.Properties[0].Type!).Replace("{1}", y.Name)}"))));
        }
    }
}