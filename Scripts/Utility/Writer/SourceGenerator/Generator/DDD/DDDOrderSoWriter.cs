using Diagram;
using Writer.Core;
using Writer.SourceGenerator.Format;
using Writer.SourceGenerator.Format.Writer;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Writer.SourceGenerator
{
    public class DDDOrderSOWriter : DDDSourceExporter
    {
        private IWriterFormat orderSOFormat;
        
        public DDDOrderSOWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            string usingSpace = @"using System;
using UnityEngine;";
            orderSOFormat = new DDDOrderSOFormat(nodeModel.Header.Namespace, usingSpace);
        }

        protected override string BaseInheritanceName => "AggregateOrderSOBase";

        public async void Write(string path)
        {
            string contentFormat = orderSOFormat.GetFormat();
            string accessType = DiagramModelModifier.GetNodeViewTypeName<AccessType>();

            string content = contentFormat
                .Replace("{0}", accessType)
                .Replace("{1}", NodeModel.Header.Name);
            
            await base.WriteScript(path, content);
        }
    }
}