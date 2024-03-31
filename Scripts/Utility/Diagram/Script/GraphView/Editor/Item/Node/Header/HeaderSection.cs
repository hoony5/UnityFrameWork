namespace Diagram
{

    public class HeaderSection : ISectionFactory
    {
        public readonly IDiagramNode node;
        public readonly HeaderSectionView view;
        private HeaderSectionBinder binder;

        public HeaderSection(IDiagramNode node)
        {
            this.node = node;
            view = new HeaderSectionView(node);
        }

        public void Setup()
        {
            view.RemoveElements();
            
            switch (node)
            {
                case DiagramNormalNode:
                    view.CreateNormalNodeSubview("public", "class", "None");
                    break;
                case DiagramEventNode:
                    view.CreateEventNodeSubview("publish/subscribe");
                    break;
                case DiagramNoteNode:
                    view.CreateNoteNodeSubview("Text", "Default");
                    break;
            }
            
            binder = new HeaderSectionBinder(node, view);
            binder.Bind();
        }

        public void Load()
        {
            switch (node)
            {
                case DiagramNormalNode:
                    LoadNormalNode();
                    break;
                case DiagramEventNode:
                    LoadEventNode();
                    break;
                case DiagramNoteNode:
                    LoadNoteNode();
                    break;
            }
        }

        public void Reload()
        {
            binder.Dispose();
            Setup();
        }

        private void LoadNormalNode()
        {
            view.accessTypeButton.text = node.ModelModifier.GetNodeViewTypeName<AccessType>();
            view.dataAccessKeywordButton.text = node.ModelModifier.GetNodeViewTypeName<AccessKeyword>();
            view.entityTypeButton.text = node.NodeModel.Header.EntityType.ToString().ToLower();
            view.nameTextField.value = node.NodeModel.Header.Name;
        }
        
        private void LoadEventNode()
        {
            view.eventTypeButton.text = node.ModelModifier.GetNodeViewTypeName<EventType>();
            view.nameTextField.value = node.NodeModel.Header.Name;
        }
        
        private void LoadNoteNode()
        {
            view.noteExportFileTypeButton.text = node.ModelModifier.GetNodeViewTypeName<ExportFileType>();
            view.descriptionTypeButton.text = node.ModelModifier.GetNodeViewTypeName<DescriptionType>();
            view.nameTextField.value = node.NodeModel.Note.Title;
        }

        public void Dispose()
        {
            binder.Dispose();
        }

    }
}