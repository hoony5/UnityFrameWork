using System;
using UnityEngine.UIElements;

namespace Diagram
{
    public class SummaryNoteSubViewBinder : IDisposable
    {
        private IDiagramNode node;
        private SummaryNoteSubView view;
        public SummaryNoteSubViewBinder(IDiagramNode node, SummaryNoteSubView view)
        {
            this.view = view;
            this.node = node;
        }

        private void BindSummary(FocusOutEvent evt)
        {
            node.NodeModel.Note.Summary = view.summary.value;
        }

        private void BindDetail(FocusOutEvent evt)
        {
            node.NodeModel.Note.Description = view.detail.value;
        }

        public void Bind()
        {
            view.summary.RegisterCallback<FocusOutEvent>(BindSummary);
            view.detail.RegisterCallback<FocusOutEvent>(BindDetail);
        }
        public void Dispose()
        {
            view.summary.UnregisterCallback<FocusOutEvent>(BindSummary);
            view.detail.UnregisterCallback<FocusOutEvent>(BindDetail);   
        }
    }
}