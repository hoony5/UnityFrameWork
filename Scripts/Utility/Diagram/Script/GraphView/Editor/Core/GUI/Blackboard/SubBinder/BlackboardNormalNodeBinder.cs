using System;
using System.Linq;
using Share;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public class BlackboardNormalNodeBinder : IDisposable
    {
        private DiagramBlackboard blackboard;
        private BlackboardNormalNodeView view;
        public DiagramNormalNode node;
        
        public BlackboardNormalNodeBinder(DiagramBlackboard blackboard, BlackboardNormalNodeView view)
        {
            this.blackboard = blackboard;
            this.view = view;
        }
        public void SetUp(DiagramNormalNode normalNode)
        {
            node = normalNode;
        }
        private void BindScriptPayload(FocusOutEvent evt)
        {
            Debug.Log("BindScriptPayload");
            node.NodeModel.Blackboard.ExportExampleCode = view.scriptExampleField.value;
        }
        // from source generator
        private void BindScriptPayload()
        {
            DiagramModelModifier modelModifier = node.ModelModifier;
            string accessType = modelModifier.GetNodeViewTypeName<AccessType>();
            string accessKeyword = modelModifier.GetNodeViewTypeName<AccessKeyword>();
            string inheritance = node.NodeModel.Inheritances.Count > 0 ? $" : {string.Join(", ", node.NodeModel.Inheritances.OrderBy(i => i.AccessKeyword).Select(i => i.Name))}" : "";

            string serializedFieldLine = "[field:SerializeField]";
            string configPropertySyntaxFormat = "public {0} {1} {{get; init;}}";
            string configMethodSyntaxFormat = @"{0} {1} {2}({3})
    {{
        new NotImplementedException();
    }}";

            string properties = node.NodeModel.Header.Properties.Count > 0
                ? string.Join("\n",
                    node.NodeModel
                        .Header
                        .Properties
                        .Select(p => $@"    {serializedFieldLine} {string.Format(configPropertySyntaxFormat,
                            p.DeclarationType switch
                            {
                                DeclarationType.Static => $"static {p.Type}",
                                DeclarationType.Abstract => $"abstract {p.Type}",
                                DeclarationType.Virtual => $"virtual {p.Type}",
                                _ => p.Type
                            },
                            p.Name)}"))
                : "";
            
            string methods = node.NodeModel.Header.Methods.Count > 0
                ? string.Join("\n",
                    node.NodeModel
                        .Header
                        .Methods
                        .Select(m => $@"    {string.Format(configMethodSyntaxFormat,
                            m.AccessType,
                            m.DeclarationType switch 
                            {
                                DeclarationType.Static => $"static {m.Type}",
                                DeclarationType.Abstract => $"abstract {m.Type}",
                                DeclarationType.Virtual => $"virtual {m.Type}",
                                _ => m.Type
                            },
                            m.Name,
                            string.Join(", ", m.Parameters.Select(p => $"{p.type} {p.name.IsNullOrEmptyThen(p.type.ToLowerAt(0))}")))}"))
                : "";
 
            string methodLine = $"{(methods.IsNullOrEmpty() ? "": methods)}";
            string payload = $"{(properties.IsNullOrEmpty() ? $"{methodLine}" : methodLine.IsNullOrEmpty() ? $"{properties}" : $"{properties}\n\n{methodLine}")}";
            view.scriptExampleField.value = $@"{accessType} {accessKeyword} {node.NodeModel.Header.Name}{inheritance}
{{
{payload}
}}";
        }
        private void BindNodeDescription(FocusOutEvent evt)
        {
            node.NodeModel.Blackboard.Note = view.textAreaField.value;
        }

        private void BindNodeName(FocusOutEvent evt)
        {
            view.nameLabelField.text = node.NodeModel.Header.Name;
        }
        public void Bind()
        {
            if(node == null) return;
            
            view.scriptExampleField.RegisterCallback<FocusOutEvent>(BindScriptPayload);
            if(node.SectionFactory.TryGetSection(out HeaderSection headerSection))
                headerSection.view.nameTextField.RegisterCallback<FocusOutEvent>(BindNodeName);
            blackboard.scriptGenerateTask = blackboard.schedule
                .Execute(BindScriptPayload)
                .Every(250);
            view.textAreaField.RegisterCallback<FocusOutEvent>(BindNodeDescription);
        }

        public void Dispose()
        {
            blackboard.scriptGenerateTask?.Pause();
            if(node == null) return;

            if(node.SectionFactory.TryGetSection(out HeaderSection headerSection))
                headerSection.view.nameTextField.UnregisterCallback<FocusOutEvent>(BindNodeName);
            
            view.textAreaField.UnregisterCallback<FocusOutEvent>(BindNodeDescription);
            // 
        }

        public void Load()
        {
            view.nameLabelField.text = node.NodeModel.Header.Name;
            view.textAreaField.value = node.NodeModel.Blackboard.Note;
            node.NodeModel.Header.SummarizeDescription = node.GetDescription();
        }
    }
}