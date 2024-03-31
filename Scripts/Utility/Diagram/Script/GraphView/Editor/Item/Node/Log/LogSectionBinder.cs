using System;
using System.Linq;
using UnityEngine.UIElements;

namespace Diagram
{
    public class LogSectionBinder : IDisposable
    {
        private LogSectionView view;
        private IDiagramNode node;
        private DiagramModelLogger modelLogger;
        private IVisualElementScheduledItem task;
        public LogSectionBinder(IDiagramNode node, LogSectionView view)
        {
            this.view = view;
            this.node = node;
            modelLogger = new DiagramModelLogger(node.NodeModel);
        } 
        
        public void Bind()
        {
            task = view.logLabel.schedule.Execute(() =>
            {
                if(node is DiagramNormalNode)
                    BindNormalNode();
                if (node is DiagramEventNode)
                    BindEventNode();
            }).Every(500);
            
            view.noteField.RegisterCallback<FocusOutEvent>(BindDescription);
        }
        
        private void BindNormalNode()
        {
            // when parent is changed, then update log
            if (node.NodeModel.Inheritances.Count != 0)
            {
                // but, only when header is dirty
                if (node.NodeModel.Inheritances.Any(inheritance => inheritance.Status.HeaderIsDirty))
                {
                    view.logLabel.text = modelLogger?.NormalNodeToString();    
                }
            }
                
            // when self is changed, then update log
            if (!node.NodeModel.Status.IsDirty) return;
            node.NodeModel.Status.IsDirty = false;
            view.logLabel.text = modelLogger?.NormalNodeToString();
        }

        private void BindEventNode()
        {
            if(!node.NodeModel.Status.IsDirty) return;
            node.NodeModel.Status.IsDirty = false;
            view.logLabel.text = modelLogger?.EventNodeToString();
        }

        private void BindDescription(FocusOutEvent evt)
        {
            node.NodeModel.Header.Description = view.noteField.value;
        }
        
        public void Reset()
        {
            Dispose();
            Bind();
        }
        
        public void Dispose()
        {
            task?.Pause();
            task = null;
            view.noteField.UnregisterCallback<FocusOutEvent>(BindDescription);
        }
    }
}