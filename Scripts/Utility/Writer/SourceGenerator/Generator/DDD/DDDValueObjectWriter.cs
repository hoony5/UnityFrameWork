using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diagram;
using UnityEngine;
using Utility.ExcelReader;
using Writer.Core;
using Writer.Ex;
using Writer.SourceGenerator.Format;
using Writer.SourceGenerator.Format.Writer;

namespace Writer.SourceGenerator
{
    public class DDDValueObjectWriter : DDDSourceExporter
    {
        private IContentWriter propertyWriter;
        private IContentWriter logWriter;
        private IWriterFormat valueObjectFormat;
        
        public DDDValueObjectWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            logWriter = new DDDValueObjectLogWriter(nodeModel);
            propertyWriter = new DDDValueObjectPropertyWriter(nodeModel);
            
            string usingSpace = @"using Newtonsoft.Json;
using UnityEngine;
";
            
            valueObjectFormat = new DDDValueObjectFormat(nodeModel.Header.Namespace, usingSpace);
        }

        protected override string BaseInheritanceName => "ValueObject";

        protected override string GenerateInheritanceConstructorParameters()
        {
            return commonInheritanceProperties;
        }

        public async void Write(string path)
        {
            string accessType =
                $"{DiagramModelModifier.GetNodeViewTypeName<AccessType>()} {DiagramModelModifier.GetNodeViewTypeName<AccessKeyword>()}";
            string inheritances =
                $": {(NodeModel.Inheritances.Count == 0 ? BaseInheritanceName : string.Join(", ", NodeModel.Inheritances))}";
            string propertiesString = propertyWriter.WriteContent();
            string constructorParameters = WriteConstructorParameters();
            string constructor = WriteConstructor();
            string inheritanceParameters = GenerateInheritanceConstructorParameters();
            string baseLog = $"{(NodeModel.Inheritances.Count == 0 ? "" : "\nbase.ToString()\n")}";
            string log = logWriter.WriteContent();
                
            string contentFormat = valueObjectFormat.GetFormat();
            
            string content = contentFormat
                .Replace("{0}", accessType)
                .Replace("{1}", NodeModel.Header.Name)
                .Replace("{2}", inheritances)
                .Replace("{3}", propertiesString)
                .Replace("{4}", constructorParameters)
                .Replace("{5}", inheritanceParameters)
                .Replace("{6}", constructor)
                .Replace("{7}", baseLog)
                .Replace("{8}", log);

            await base.WriteScript(path, content);
        }
    }
}