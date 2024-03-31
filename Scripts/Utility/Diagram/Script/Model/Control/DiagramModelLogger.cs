using System.Collections.Generic;
using System.Linq;
using Share;
using Writer.SourceGenerator.Format.Writer;

namespace Diagram
{
    public class DiagramModelLogger
    {
        private DiagramNodeModel nodeModel;
        private DiagramModelModifier modifier;
        
        public DiagramModelLogger(DiagramNodeModel nodeModel)
        {
            this.nodeModel = nodeModel;
            modifier = new DiagramModelModifier(nodeModel);
        }
        
        #region Debug

        private IEnumerable<string> GetEnumMembers()
        {
            return nodeModel.Header.Properties.Select(p => $"{p.Type} = {p.Name}");
        }
        private string GetEnumInformationLine(IEnumerable<string> enumMemberLines)
        {
            string enumMembersLine = string.Join("\n                                      ", enumMemberLines);
            return $"- Values                       {(nodeModel.Header.Properties.Count == 0 ? "None" : enumMembersLine)}";
        }
        private IEnumerable<string> GetProperties()
        {
            return nodeModel.Header.Properties.Select(p =>
                $"public{(p.DeclarationType == DeclarationType.NonModified ? string.Empty : $" {p.DeclarationType.ToString().ToLower()}")} {p.Type} {p.Name} {{get; init;}}");
        }
        private string GetPropertyInformationLine(IEnumerable<string> propertyLines)
        {
            string propertiesLine = string.Join("\n                                      ", propertyLines);
            return $"- Properties                  {(nodeModel.Header.Properties.Count == 0 ? "None" : propertiesLine)}";
        }
        private IEnumerable<string> GetNormalMethods()
        {
            return nodeModel.Header.Methods.Select(m =>
                $"{m.AccessType}{(m.DeclarationType == DeclarationType.NonModified ? string.Empty : $" {m.DeclarationType.ToString().ToLower()}")} {m.Type} {m.Name}({(m.Parameters.Count == 0 ? "" : string.Join(", ", m.Parameters.Select(p => $"{p.type} {p.name}")))})");
        }
        private IEnumerable<string> GetEventMethods()
        {
            return nodeModel.Header.Methods.Select(m => $"private void {m.Name}({nodeModel.Header.Name} message){(m.Predicate.IsNullOrEmpty() ? string.Empty : $" | Predicate<{nodeModel.Header.Name}> {m.Predicate}")}");
        }
        private string GetMethodInformationLine(string title, IEnumerable<string> methodLines)
        {
            string methodLine = string.Join("\n                                      ", methodLines);
            return $"- {title}                     {(nodeModel.Header.Methods.Count == 0 ? "None" : methodLine)}";
        }

        private IEnumerable<string> GetValidatedInstances()
        {
            return nodeModel.Inheritances
                .Where(item => item.Status.GraphElementType is GraphElementType.Normal)
                .OrderBy(item => item.AccessKeyword)
                .Select(item => $"{string.Join(", ", item.Name)}");
        }
        private string GetInstancesLines(IEnumerable<string> validatedInheritances)
        {
            return nodeModel.Inheritances.Count == 0
                ? "None"
                : $"{modifier.GetNodeViewTypeName<AccessType>()} {nodeModel.Header.Name} : {string.Join(", ", validatedInheritances)}";
        }

        public string NormalNodeToString()
        {
            bool isEnum = nodeModel.Header.AccessKeyword == AccessKeyword.Enum;

            IEnumerable<string> enumMembers = GetEnumMembers();
            string enumInformationLine = GetEnumInformationLine(enumMembers);
            
            IEnumerable<string> properties = GetProperties();
            string propertyInformationLine = GetPropertyInformationLine(properties);
            
            IEnumerable<string> methods = GetNormalMethods();
            string methodInformationLine = GetMethodInformationLine("Methods" , methods);
            
            IEnumerable<string> validatedInheritances = GetValidatedInstances();
            
            return $@"[Debug]
- Namespace               {nodeModel.Header.Namespace.IsNullOrEmptyThen("None")}
- AccessType              {modifier.GetNodeViewTypeName<AccessType>()}
- DataKeywordType    {modifier.GetNodeViewTypeName<AccessKeyword>()}
- Name                         {nodeModel.Header.Name.IsNullOrEmptyThen("None")}
- EntityType                 {nodeModel.Header.EntityType}
- Inheritances               {GetInstancesLines(validatedInheritances)}

{(isEnum ? enumInformationLine : propertyInformationLine)}

{(isEnum ? "" : methodInformationLine)}

";
        }

        public string EventNodeToString()
        {
            IEnumerable<string> methods = GetEventMethods();
            string methodInformationLine = GetMethodInformationLine("Callbacks", methods);
            
            return $@"[Debug]
- Group                        {nodeModel.Header.Group.IsNullOrEmptyThen("None")}
- Namespace               {nodeModel.Header.Namespace.IsNullOrEmptyThen("None")}
- EventType                 {modifier.GetNodeViewTypeName<EventType>()}
- MessageType            {nodeModel.Header.Name}

{methodInformationLine}
";
        }

        #endregion
    }

}