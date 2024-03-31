using System;
using System.Collections.Generic;
using System.Linq;
using Diagram;

namespace Writer.SourceGenerator.Format.Writer
{
    public class DDDValueObjectPropertyWriter : DDDContentWriterBase
    {
        private string propertyFormat;

        protected readonly List<string> commonConstructorProperties = new List<string>
        {
            "topCategory",
            "middleCategory",
            "bottomCategory",
            "objectName",
            "objectID",
            "displayName"
        };
        
        public DDDValueObjectPropertyWriter(DiagramNodeModel nodeModel) : base(nodeModel)
        {
            AddFormat(nameof(propertyFormat), new DDDValueObjectPropertyFormat());
        }
        
        public override string WriteContent()
        {
            IEnumerable<string> propertyLines = NodeModel.Header.Properties
                .Where(x => !commonConstructorProperties.Contains(x.Name, StringComparer.OrdinalIgnoreCase))
                .Select(item => GetFormat(nameof(propertyFormat))
                    .Replace("{0}", item.Type)
                    .Replace("{1}", item.Name));

            return string.Join('\n', propertyLines);
        }
    }
}