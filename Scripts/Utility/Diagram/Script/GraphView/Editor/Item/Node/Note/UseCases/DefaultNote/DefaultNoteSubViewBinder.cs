using System;
using UnityEngine.UIElements;

namespace Diagram
{
    public class DefaultNoteSubViewBinder : IDisposable
    {
        private IDiagramNode node;
        private DefaultNoteSubView view;
        
        public DefaultNoteSubViewBinder(IDiagramNode node, DefaultNoteSubView view)
        {
            this.node = node;
            this.view = view;
        }
        
        private void BindSubTitle(FocusOutEvent evt)
        {
            node.NodeModel.Note.SubTitle = view.subtitle.value;
        }
        private void BindDescription(FocusOutEvent evt)
        {
            node.NodeModel.Note.Description = view.description.value;
        }
        
        public void Bind()
        {
            view.subtitle.RegisterCallback<FocusOutEvent>(BindSubTitle);
            view.description.RegisterCallback<FocusOutEvent>(BindDescription);
        }
        public void Dispose()
        {
            view.subtitle.UnregisterCallback<FocusOutEvent>(BindSubTitle);
            view.description.UnregisterCallback<FocusOutEvent>(BindDescription);
        }
    }
}