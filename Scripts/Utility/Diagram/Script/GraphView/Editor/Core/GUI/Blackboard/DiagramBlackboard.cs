using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DiagramBlackboard : Blackboard, IDisposable
    {
        public readonly DiagramGUI gui;
        private DiagramBlackboardView view;
        private DiagramBlackboardBinder binder;
        private ISelectable currentSelection;
        
        public IVisualElementScheduledItem scriptGenerateTask;
        public IVisualElementScheduledItem eventFindTask;
        public IVisualElementScheduledItem publisherFindTask;
        public IVisualElementScheduledItem subscribeFindTask;
        public IVisualElementScheduledItem exportExampleTask;
        public IVisualElementScheduledItem getConfluenceTitleTask;
        
        public DiagramBlackboard(DiagramGUI gui)
        {
            this.gui = gui;
            view = new DiagramBlackboardView();
            binder = new DiagramBlackboardBinder(this, view);
            
            Add(view.nodeContainer);
            Add(view.information);
            
            this.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
            this.style.width = new StyleLength(new Length(40, LengthUnit.Percent));
        }
        public void OnUpdate(ISelectable selectable)
        {
            switch (currentSelection)
            {
                case DiagramMemo memo when
                    selectable is DiagramMemo otherMemo && memo.ID.Equals(otherMemo.ID):
                case DiagramGroup group when
                    selectable is DiagramGroup otherGroup && group.ID.Equals(otherGroup.ID):
                    return;
                default:
                    currentSelection = selectable;
                    view.CreateView(selectable);
                    binder?.Bind(selectable);
                    break;
            }
        }

        public void DisplaySimpleInfo(bool active)
        {
            view.information.SetDisplay(active);
        }
        
        public void Dispose()
        {
            binder.Dispose();
            scriptGenerateTask?.Pause();
            scriptGenerateTask = null;
            eventFindTask?.Pause();
            eventFindTask = null;
            publisherFindTask?.Pause();
            publisherFindTask = null;
            subscribeFindTask?.Pause();
            subscribeFindTask = null;
            exportExampleTask?.Pause();
            exportExampleTask = null;
            getConfluenceTitleTask?.Pause();
            getConfluenceTitleTask = null;
        }
    }
}