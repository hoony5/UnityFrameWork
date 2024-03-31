using System;
using System.Linq;
using Diagram;
using UnityEngine;
using Writer.Core;
using Writer.Ex;
using Writer.SourceGenerator.Format;
using Writer.SourceGenerator.Format.Writer;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Writer.SourceGenerator
{
    public class DDDAggregateWriter : DDDSourceExporter
    {
        private IWriterFormat aggregateFormat;
        private IContentWriter propertyWriter;
        private IContentWriter logWriter;

        public DDDAggregateWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            propertyWriter = new DDDAggregatePropertyWriter(nodeModel);
            logWriter = new DDDAggregateLogWriter(nodeModel);
            
            string usingSpace = @"using System;
using UnityEngine;
using Newtonsoft.Json;
";
            aggregateFormat = new DDDAggregateFormat(nodeModel.Header.Namespace, usingSpace);
        }
        protected override string BaseInheritanceName => "AggregateRoot";

        protected override string GenerateInheritanceConstructorParameters()
        {
            return NodeModel.Inheritances.Count == 0 ? "default" : NodeModel.Header.Properties[0].Name;
        }

        public async void Write(string path)
        {
            if (validateAccessKeywords.All(AccessKeyword => !AccessKeyword.Equals(NodeModel.Header.AccessKeyword.ToString(), StringComparison.OrdinalIgnoreCase)))
            {
                Debug.LogError($"Invalidate AccessKeyword -> {NodeModel.Header.AccessKeyword.ToString().ToLower()} | class, struct ,record");
                return;
            }

            string contentFormat = aggregateFormat.GetFormat();
            string accessModifier =
                $"{DiagramModelModifier.GetNodeViewTypeName<AccessType>()} {DiagramModelModifier.GetNodeViewTypeName<AccessKeyword>()}";
            string inheritance =
                $": {(NodeModel.Inheritances.Count == 0 ? BaseInheritanceName : string.Join(", ", NodeModel.Inheritances))}";

            string properties = propertyWriter.WriteContent();
            string constructorParameters = WriteConstructorParameters();
            string constructorVariables = WriteConstructor();
            string log = logWriter.WriteContent();
            
            string content = contentFormat
                .Replace("{0}", accessModifier)
                .Replace("{1}", NodeModel.Header.Name)
                .Replace("{2}", inheritance)
                .Replace("{3}", properties)
                .Replace("{4}", constructorParameters)
                .Replace("{5}", constructorVariables)
                .Replace("{6}", log);
            
            await base.WriteScript(path, content);
        }
    }
}