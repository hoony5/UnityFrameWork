using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Diagram;
using Share;
using UnityEngine;
using Writer.Ex;

namespace Writer.SourceGenerator
{
    public abstract class DDDSourceExporter : SourceExporterBase
    {
        protected DiagramNodeModel NodeModel { get; private set; }
         protected DiagramModelModifier DiagramModelModifier { get; private set; }
        
         protected string[] validateAccessKeywords = { "class", "struct", "record" };
         protected readonly string commonInheritanceProperties =
             ": base(topCategory, middleCategory,bottomCategory, objectName, objectID)";
        protected DDDSourceExporter(DiagramNodeModel nodeModel)
        {
            NodeModel = nodeModel;
            DiagramModelModifier = new DiagramModelModifier(nodeModel);
        }

        protected virtual string BaseInheritanceName { get; }

        protected virtual string GenerateInheritanceConstructorParameters() { return string.Empty; }

        protected virtual string WriteConstructorParameters()
        {
            string constructorParameters = NodeModel.Header.Properties.Count == 0
                ? string.Empty
                : string.Join(", ",
                    NodeModel.Header.Properties
                        .Select(item => $"{item.Type} {item.Name.ToLowerAt(0)}"));
            
            StringBuilder sb = new StringBuilder();
            for(int i = 0 ; i < constructorParameters.Length; i++)
            {
                sb.Append(constructorParameters[i]);
                if (i >= 1 &&
                    constructorParameters[i - 1] == ',' &&
                    i != constructorParameters.Length - 1)
                {
                    sb.Append("\n\t\t");
                }
            }
            return sb.ToString();
        }

        protected virtual string WriteConstructor()
        {
            return NodeModel.Header.Properties.Count == 0
                ? string.Empty
                : string.Join("\n",
                    NodeModel.Header.Properties
                        .Select(item => $"\t\t{item.Name} = {item.Name.ToLowerAt(0)};"));
        }
    }

}