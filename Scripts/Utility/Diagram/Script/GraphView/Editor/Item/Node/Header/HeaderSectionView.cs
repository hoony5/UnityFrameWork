using UnityEngine.UIElements;
using static Share.VisualElementEx;
namespace Diagram
{
    public class HeaderSectionView : SectionView
    {
        // common
        public TextField nameTextField;
        public VisualElement section;
        
        // normal node
        public Button accessTypeButton;
        public Button dataAccessKeywordButton;
        public Button entityTypeButton;
        
        // event node
        public Button eventTypeButton;
        
        // note node
        public Button noteExportFileTypeButton;
        public Button descriptionTypeButton;
        public Button clearButton;
        
        public HeaderSectionView(IDiagramNode node)
        {
            section = new VisualElement();
            
            nameTextField = CreateTextField( node switch
            {
                DiagramNormalNode => node.NodeModel.Header.Name,
                DiagramEventNode => node.NodeModel.Header.Name,
                DiagramNoteNode => node.NodeModel.Note.Title,
                _ => "new Node"
            });
            
            node.titleContainer.Insert(0, nameTextField);
            node.titleContainer.Add(section);
            node.name = nameTextField.value;
            
            section.SetFlexDirection(FlexDirection.Row);
            nameTextField.SetWidth(100);
        }

        public void CreateNormalNodeSubview(
            string accessType,
            string dataAccessKeyword,
            string entityType)
        {
            accessTypeButton = CreateButton(accessType);
            dataAccessKeywordButton = CreateButton(dataAccessKeyword);
            entityTypeButton = CreateButton(entityType);
            
            section.AddElements(accessTypeButton, dataAccessKeywordButton, entityTypeButton);
            
            entityTypeButton.tooltip = @"The entity type are none, value object, aggregate, orderSO, repositorySO.
none which means no inheritance, value object which means inheritance from ValueObject, aggregate which means inheritance from AggregateRoot, orderSO which means inheritance from OrderSO, repositorySO which means inheritance from RepositorySO.";
        }
        public void CreateEventNodeSubview(string eventType)
        {
            eventTypeButton = CreateButton(eventType);
            
            section.AddElements(eventTypeButton);
        }

        public void CreateNoteNodeSubview(string exportType, string noteType)
        {
            noteExportFileTypeButton = CreateButton(exportType);
            descriptionTypeButton = CreateButton(noteType);
            clearButton = CreateButton("Clear");

            section.AddElements(noteExportFileTypeButton, descriptionTypeButton, clearButton);
            
        }
        public void RemoveElements()
        {
            section.Clear();
        }
    }

}