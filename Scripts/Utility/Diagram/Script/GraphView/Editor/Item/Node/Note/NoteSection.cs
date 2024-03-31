namespace Diagram
{
    public class NoteSection : ISectionFactory
    {
        public readonly DiagramNoteNode node;
        public NoteSectionView view;
        public ISubNote baseNote;
        
        public NoteSection(DiagramNoteNode node)
        {
            this.node = node;
            view = new NoteSectionView(node);
        }
        public void Setup()
        {
        }

        public void Load()
        {
            baseNote?.Load(node.NodeModel);
        }

        public void Reload()
        {
            baseNote?.Dispose();
        }

        public void Reset()
        {
            baseNote?.Dispose();
        }

        public void Dispose()
        {
            baseNote?.Dispose();
        }
    }
}