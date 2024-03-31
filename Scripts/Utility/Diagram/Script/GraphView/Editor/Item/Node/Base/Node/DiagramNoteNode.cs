using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public sealed class DiagramNoteNode : DiagramNodeBase
    {
        public override SectionFactory SectionFactory { get; protected set; }
        public NoteSection noteSection;
        private VisualElement extensionContainerVerticalSpace20;
        private VisualElement mainContainerVerticalSpace20;
        
        public DiagramNoteNode(DiagramGUI gui, DiagramNodeModel nodeModel) 
            : base(gui, nodeModel)
        {
            // Create Factories
            NodeModel.Status.GraphElementType = GraphElementType.Note;
            NodeModel.Note.DescriptionType = NodeModel.Note.DescriptionType == DescriptionType.Not_Used 
                ? DescriptionType.Default 
                : NodeModel.Note.DescriptionType;; 
            name = NodeModel.Note.Title;
            
            SetupFactory();
            noteSection.baseNote = noteSection.view.CreateNote();
        }

        private void SetupFactory()
        {
            SectionFactory = new SectionFactory();
            SectionFactory.AddSection(
                new PortSection(this,
                    ("baseNote", "note")));
            SectionFactory.AddSection(new HeaderSection(this));
            SectionFactory.AddSection(noteSection = new NoteSection(this));
        }

        public void SetUpFoldOutHierarchy()
        {
            extensionContainerVerticalSpace20 = extensionContainer.CreateVerticalSpace(20);
            extensionContainer.Add(noteSection.view.foldout);
            extensionContainer.Add(extensionContainerVerticalSpace20);
        }
        
        public void SetUpPlainHierarchy()
        {
            mainContainer.Add(noteSection.view.plain);
            mainContainer.Add(mainContainerVerticalSpace20 = mainContainer.CreateVerticalSpace(20));
        }

        public void Reset()
        {
            extensionContainer.SafeRemove(extensionContainerVerticalSpace20);
            extensionContainer.SafeRemove(noteSection.view.foldout);
            extensionContainer.SafeRemove(extensionContainerVerticalSpace20);
            mainContainer.SafeRemove(noteSection.view.plain);
            mainContainer.SafeRemove(mainContainerVerticalSpace20);
        }
        public override void RepaintAllSections()
        {
            RefreshExpandedState();
            MarkDirtyRepaint(); 
        }

        public override void Dispose()
        {
            SectionFactory.Dispose();
            base.Dispose();
        }
    }

}