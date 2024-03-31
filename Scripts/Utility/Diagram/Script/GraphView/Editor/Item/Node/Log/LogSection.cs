using UnityEngine.UIElements;

namespace Diagram
{
    public class LogSection : ISectionFactory
    {
        private IDiagramNode node;
        public readonly LogSectionView view;
        private LogSectionBinder binder;
        private IVisualElementScheduledItem task;
        
        public LogSection(IDiagramNode node, string title)
        {
            this.node = node;
            view = new LogSectionView(title);
        }
        public void Setup()
        {
            // bind to Model
            binder = new LogSectionBinder(node, view);
            binder.Bind();
        }

        public void Load()
        {
            view.noteField.value = node.NodeModel.Header.Description;
        }

        public void Reload()
        {
            binder.Reset();
        }

        public void Reset()
        {
            binder.Reset();
        }

        public void Dispose()
        {
            binder.Dispose();
        }
    }
}