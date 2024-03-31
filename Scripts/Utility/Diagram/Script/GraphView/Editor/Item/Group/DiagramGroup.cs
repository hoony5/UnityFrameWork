using System;
using static Share.VisualElementEx;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Diagram
{
    public sealed class DiagramGroup : Group , IDisposable
    {
        private DiagramGroupView view;
        private DiagramGroupBinder binder;
        public string ID { get; set; }
        public bool IsFixed { get; set; }
        public Vector2 OriginalPosition { get; private set; }
        public DiagramGUI GUI { get; private set; }
        public DiagramNodeModel NodeModel { get; private set; }
        private IVisualElementScheduledItem positionTask;
        public IVisualElementScheduledItem summaryTask;
        public DiagramGroup (DiagramGUI gui, DiagramNodeModel nodeModel)
        {
            GUI = gui;
            NodeModel = new DiagramNodeModel(nodeModel);
            title = nodeModel.Note.Title;
            ID = nodeModel.Header.ID;
            view = new DiagramGroupView();
            binder = new DiagramGroupBinder(this, view);
            
            OriginalPosition = nodeModel.Header.Position;
            NodeModel.Status.GraphElementType = GraphElementType.Group;
            
            AddToClassList("group");
            SetPosition(new Rect(NodeModel.Header.Position, NodeModel.Header.Size));
            
            // remove nodes
            headerContainer.SetFontColor(Color.white);
            headerContainer.SetFontSize(50);
            headerContainer.SetFlexGrow(0);
            
            headerContainer.SetFlexDirection(FlexDirection.Row);
            headerContainer.Add(CreateButton("-"));
        }
        
        public void SetUp()
        {
            binder.Bind();
        }
        // TODO :: add member, gui.view.diagramNodes using this
        public void Load()
        {
            title = NodeModel.Note.Title;
            if(NodeModel.Members.Count == 0) return;
            NodeModel.Members.ForEach(member =>
            {
                CustomNode loadedMember = GUI.view.diagramNodes[member.ID];
                AddElement(loadedMember);
                loadedMember.Group = this;
                loadedMember.NodeModel.Header.Group = ID;
                loadedMember.NodeModel.Header.Namespace = NodeModel.Note.Title;
                member.Group = ID;
                member.Namespace = NodeModel.Note.Title;
            });
            SetPosition(new Rect(NodeModel.Header.Position, NodeModel.Header.Size));
        }
        public void Dispose()
        {
            positionTask?.Pause();
            positionTask = null;
        }
    }
}