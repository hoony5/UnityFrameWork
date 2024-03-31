using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Diagram
{
    public sealed class DiagramMemo : StickyNote, IDisposable
    {
        public string ID { get; set; }
        public DiagramNodeModel NodeModel { get; }
        public DiagramGUI GUI { get; }
        private DiagramMemoView view;
        private DiagramMemoBinder binder;
        public DiagramMemo(DiagramGUI gui, DiagramNodeModel nodeModel)
        {
            NodeModel = new DiagramNodeModel(nodeModel);
            GUI = gui;
            ID = nodeModel.Header.ID;
            NodeModel.Status.GraphElementType = GraphElementType.Memo;
            NodeModel.Note.DescriptionType = DescriptionType.Not_Used;
            title = NodeModel.Note.Title;
            view = new DiagramMemoView(this);
            binder = new DiagramMemoBinder(this, view);
        }
        public void SetUp()
        {
            binder.Bind();
        }

        public void ChangeFontColor(Color color)
        {
            contentContainer.SetFontColor(color);
            contentContainer.Q("title").SetFontColor(color);
            contentContainer.Q("contents").SetFontColor(color);
        }
        public void Load()
        {
            title = NodeModel.Note.Title;
            contents = NodeModel.Note.Description;
            SetPosition(new Rect(NodeModel.Header.Position, NodeModel.Header.Size));
        }

        public void Dispose()
        {
            binder?.Dispose();
        }
    }
}