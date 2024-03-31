using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramBlackboardView
    {
        public VisualElement nodeContainer;
        public Label information;
        public BlackboardNormalNodeView NormalNodeView;
        public BlackboardEventNodeView EventNodeView;
        public BlackboardNoteNodeView NoteNodeView;
        public BlackboardMemoView MemoView;
        public BlackboardGroupView GroupView;
        
        public DiagramBlackboardView()
        {
            nodeContainer = new VisualElement();
            information = new Label("There is no selected item.");
            information.SetDisplay(false);
            NormalNodeView = new BlackboardNormalNodeView(string.Empty);
            EventNodeView = new BlackboardEventNodeView(string.Empty, string.Empty, string.Empty);
            NoteNodeView = new BlackboardNoteNodeView(string.Empty, string.Empty);
            MemoView = new BlackboardMemoView(string.Empty, string.Empty);
            GroupView = new BlackboardGroupView(string.Empty, string.Empty);
        }
        public void CreateView(ISelectable selectable)
        {
            nodeContainer.Clear();
            
            switch (selectable)
            {
                case DiagramNormalNode:
                    NormalNodeView.ClearValue();
                    nodeContainer.Add(NormalNodeView);
                    break;
                case DiagramEventNode:
                    EventNodeView.ClearValue();
                    nodeContainer.Add(EventNodeView);
                    break;
                case DiagramNoteNode:
                    NoteNodeView.ClearValue();
                    nodeContainer.Add(NoteNodeView);
                    break;
                case DiagramMemo:
                    MemoView.ClearValue();
                    nodeContainer.Add(MemoView);
                    break;
                case DiagramGroup:
                    GroupView.ClearValue();
                    nodeContainer.Add(GroupView);
                    break;
            }
        }

        public void Clear()
        {
            nodeContainer.Clear();
        }
    }

}