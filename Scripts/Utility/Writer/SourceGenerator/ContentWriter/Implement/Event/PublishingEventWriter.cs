using System.Collections.Generic;
using System.Linq;
using Diagram;
using UnityEngine;
using Writer.Ex;

namespace Writer.SourceGenerator.Format.Writer
{
    public class PublishingEventWriter : ContentWriterBase
    {
        private readonly string publishFormat;
        
        private readonly DiagramNodeModel nodeModel;
        
        public PublishingEventWriter(DiagramNodeModel nodeModel)
        {
            this.nodeModel = nodeModel;
            
            AddFormat(nameof(publishFormat), new PublishingEventFormat());
        }

        public override string WriteContent()
        {
            if(nodeModel.PublishingEvents.Count == 0)
                return string.Empty;
            
            return string.Join('\n',
                nodeModel.PublishingEvents.Select(x =>
                    string.Join('\n',
                        x.Methods.Select(y =>
                            $"    {GetFormat(nameof(publishFormat)).Replace("{0}", y.Name).Replace("{1}", x.Properties[0].Type!)}"))));
        }
    }
}