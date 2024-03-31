using System;

namespace Diagram
{
    public class BlackboardGroupBinder : IDisposable
    {
        private BlackboardGroupView view;
        public DiagramGroup group;
        
        public BlackboardGroupBinder(BlackboardGroupView view)
        {
            this.view = view;
        }

        public void SetUp(DiagramGroup group)
        {
            this.group = group;
        }
        
        private void BindGroupSummary()
        {
            if (group.NodeModel.Members.Count <= 0) return;
            //view.groupListField.value =
            int normalNodeCount = 0;
            int eventNodeCount = 0;
            int noteNodeCount = 0;
            string summary = "";
            group.NodeModel.Members.ForEach(member =>
            {
                if (member.Status.GraphElementType == GraphElementType.Normal)
                    normalNodeCount++;
                else if (member.Status.GraphElementType == GraphElementType.Event)
                    eventNodeCount++;
                else if (member.Status.GraphElementType == GraphElementType.Note) 
                    noteNodeCount++;
            });
            
            summary = $@"[Normal] : {normalNodeCount}
[Event] : {eventNodeCount}
[Note] : {noteNodeCount}";
            
            view.groupListField.value = summary;
        }
        public void Bind()
        {
             group.summaryTask = group.schedule
                 .Execute(BindGroupSummary)
                 .Every(500);
        }
        
        public void Dispose()
        {
            if(group == null) return;
            group.summaryTask?.Pause();
            group.summaryTask = null;
        }

        public void Load()
        {
            Dispose();
            Bind();
        }
    }
}