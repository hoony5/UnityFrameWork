

// TODO :: Basically note content is text using text overflow Ellipsis.
namespace Diagram
{
    public class NoteSectionView : SectionView
    {
        private DiagramNoteNode noteNode;
        
        private DefaultNote defaultNote;
        private SummaryNote summaryNote;
        private TableNote tableNote;
        private CheckListNote checkListNote;

        public NoteSectionView(DiagramNoteNode noteNode)
        {
            this.noteNode = noteNode;
        }
        
        public ISubNote CreateNote()
        {
            noteNode.Reset();
            
            defaultNote?.Dispose();  
            summaryNote?.Dispose();
            tableNote?.Dispose();
            checkListNote?.Dispose();
            
            switch (noteNode.NodeModel.Note.DescriptionType)
            {
                case DescriptionType.Default:
                    defaultNote = new DefaultNote(noteNode);
                    noteNode.SetUpPlainHierarchy();
                    noteNode.RepaintAllSections();
                    return defaultNote;
                case DescriptionType.Summary:
                    summaryNote = new SummaryNote(noteNode);
                    noteNode.SetUpPlainHierarchy();
                    noteNode.SetUpFoldOutHierarchy();
                    noteNode.RepaintAllSections();
                    return summaryNote;
                case DescriptionType.Table:
                    tableNote = new TableNote(noteNode);
                    noteNode.SetUpPlainHierarchy();
                    noteNode.RepaintAllSections();
                    return tableNote;
                case DescriptionType.CheckList:
                    checkListNote = new CheckListNote(noteNode);
                    noteNode.SetUpFoldOutHierarchy();
                    noteNode.RepaintAllSections();
                    return checkListNote;
                default:
                case DescriptionType.Not_Used:
                    return default;
            }
        }
    }
}