using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Diagram
{
    public interface IDiagramNode : IDisposable
    {
        string ID { get; }
        string name { get; set; }
        object userData { get; }
        DiagramGroup Group { get; }
        SectionFactory SectionFactory { get; }
        VisualElement MainContainer { get; }
        VisualElement titleContainer { get; }
        VisualElement extensionContainer { get; }
        VisualElement inputContainer { get; }
        VisualElement outputContainer { get; }
        
        DiagramNodeModel NodeModel { get; }
        DiagramModelModifier ModelModifier { get; }
        DiagramModelReflection ModelReflection { get; }
        DiagramGUI GUI { get; }
        void SetUp();
        void Load();
        Port InstantiatePort(Orientation orientation, Direction direction, Port.Capacity capacity, Type type);
        
        void RegisterOnGraphChanged(Func<GraphViewChange, GraphViewChange> changed);
        void UnregisterOnGraphChanged(Func<GraphViewChange, GraphViewChange> changed);
        void Reload();
        void MarkDirtyRepaint();
    }
}