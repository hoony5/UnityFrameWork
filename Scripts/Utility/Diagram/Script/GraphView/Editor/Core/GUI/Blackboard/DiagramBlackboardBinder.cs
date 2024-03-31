using System;
using UnityEditor.Experimental.GraphView;

namespace Diagram
{
    public class DiagramBlackboardBinder : IDisposable
    {
        private BlackboardNormalNodeBinder normalNodeBinder;
        private BlackboardEventNodeBinder eventNodeBinder;
        private BlackboardNoteNodeBinder noteNodeBinder;
        private BlackboardMemoBinder memoBinder;
        private BlackboardGroupBinder groupBinder;

        
        public DiagramBlackboardBinder(DiagramBlackboard blackboard, DiagramBlackboardView view)
        {
            normalNodeBinder = new BlackboardNormalNodeBinder(blackboard, view.NormalNodeView);
            eventNodeBinder = new BlackboardEventNodeBinder(blackboard, view.EventNodeView);
            noteNodeBinder = new BlackboardNoteNodeBinder(blackboard, view.NoteNodeView);
            memoBinder = new BlackboardMemoBinder(view.MemoView);
            groupBinder = new BlackboardGroupBinder(view.GroupView);
        }
        
        public void Bind(ISelectable selectable)
        {
            Dispose();
            
            switch (selectable)
            {
                case DiagramNormalNode normalNode:
                    normalNodeBinder.SetUp(normalNode);
                    normalNodeBinder.Load();
                    normalNodeBinder.Bind();
                    break;
                case DiagramEventNode eventNode:
                    eventNodeBinder.SetUp(eventNode);
                    eventNodeBinder.Load();
                    eventNodeBinder.Bind();
                    break;
                case DiagramNoteNode noteNode:
                    noteNodeBinder.SetUp(noteNode);
                    noteNodeBinder.Load();
                    noteNodeBinder.Bind();
                    break;
                case DiagramMemo memo:
                    memoBinder.SetUp(memo);
                    memoBinder.Load();
                    memoBinder.Bind();
                    break;
                case DiagramGroup group:
                    groupBinder.SetUp(group);
                    groupBinder.Load();
                    groupBinder.Bind();
                    break;
            }
        }
        
        public void Dispose()
        {
            normalNodeBinder?.Dispose();
            eventNodeBinder?.Dispose();
            noteNodeBinder?.Dispose();
            memoBinder?.Dispose();
            groupBinder?.Dispose();    
        }
        
    }

}