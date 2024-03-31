using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Diagram
{
    public class BlackboardEventNodeBinder : IDisposable
    {
        private DiagramBlackboard blackboard;
        private BlackboardEventNodeView view;
        public DiagramEventNode node;
        private DiagramNodeBase[] messageType;
        
        public BlackboardEventNodeBinder(DiagramBlackboard blackboard, BlackboardEventNodeView view)
        {
            this.blackboard = blackboard;
            this.view = view;
        }

        public void SetUp(DiagramEventNode eventNode)
        {
            node = eventNode;
        }

        private void BindMessageType(FocusOutEvent evt)
        {
            if (node.NodeModel.Header.Properties.Count == 0) return;
            view.messageTypeField.text = node.NodeModel.Header.Properties.First().Type;
        }

        private void FindMessageType()
        {
            if (blackboard.gui.view.diagramNodes.Count == 0) return;
            messageType = blackboard.gui.view.diagramNodes.Values
                .Where(n => n is DiagramEventNode)
                .Where(other => other.NodeModel.Header.Properties.FirstOrDefault()?.Type != string.Empty)
                .Where(other =>
                    other.NodeModel.Header.Properties.FirstOrDefault()?.Type ==
                    node.NodeModel.Header.Properties.FirstOrDefault()?.Type)
                .ToArray();
        }
        private void BindPublishList()
        {
            if (blackboard.gui.view.diagramNodes.Count == 0) return;
            if (messageType.Length == 0) return;

            IEnumerable<string> publishers = messageType
                .Select(n => string.Join("\n", n.NodeModel.PublishingEvents?.Select(s => s.Name) ?? Array.Empty<string>()))
                .Distinct();
                
            string publishList = string.Join("\n", publishers);
            view.publishListField.value = publishList;
        }
        private void BindSubscribeList()
        {
            if (blackboard.gui.view.diagramNodes.Count == 0) return;
            if (messageType.Length == 0) return;
                
            IEnumerable<string> subscribers = messageType
                .Select(n => string.Join("\n", n.NodeModel.SubscribingEvents?.Select(s => s.Name) ?? Array.Empty<string>()))
                .Distinct();
                
            string subscribeList = string.Join("\n", subscribers);
            view.subscribeListField.value = subscribeList;
        }
        
        public void Bind()
        {
            if(node == null) return;
            node.messageTypeFieldSection
                .subviewItems?.FirstOrDefault()?
                .typeTextField.RegisterCallback<FocusOutEvent>(BindMessageType);
            blackboard.eventFindTask = blackboard.schedule.Execute(FindMessageType).Every(200);
            blackboard.publisherFindTask = blackboard.schedule.Execute(BindPublishList).Every(250);
            blackboard.subscribeFindTask = blackboard.schedule.Execute(BindSubscribeList).Every(250);
            // 
        }

        private void DisposeTask(IVisualElementScheduledItem task)
        {
            task?.Pause();
        }
        public void Dispose()
        {
            DisposeTask(blackboard.eventFindTask);
            DisposeTask(blackboard.publisherFindTask);
            DisposeTask(blackboard.subscribeFindTask);

            node?.messageTypeFieldSection
                .subviewItems?.FirstOrDefault()?
                .typeTextField.UnregisterCallback<FocusOutEvent>(BindMessageType);
            // 
        }

        public void Load()
        {
            view.messageTypeField.text = node.NodeModel.Header.Properties.Count > 0
                ? node.NodeModel.Header.Properties.First().Type
                : "";
            node.NodeModel.Header.SummarizeDescription = node.GetDescription();
        }
    }
}