using static Share.VisualElementEx;
using UnityEngine.UIElements;

namespace Diagram
{
    public class PropertySectionSubView : SectionViewItem
    {
        public Button declarationTypeRepeatButton;
        public TextField typeTextField;
        public TextField nameTextField;
        public Button removeButton;
        private GraphElementType _graphElementType;
        
        public void CreateNormalNodeSubview(
            string declarationTypeButtonLabel,
            string typeFieldLabel,
            string nameFieldLabel)
        {
            group = new VisualElement();
            
            declarationTypeRepeatButton = CreateButton(declarationTypeButtonLabel);
            typeTextField = CreateTextField(typeFieldLabel, 100);
            nameTextField = CreateTextField(nameFieldLabel);
            removeButton = CreateButton("-");
            
            group.AddElements(
                declarationTypeRepeatButton,
                typeTextField,
                nameTextField,
                removeButton);     
            group.SetFlexDirection(FlexDirection.Row);
            _graphElementType = GraphElementType.Normal;
        }
        public void CreateEventNodeSubview(string typeFieldLabel)
        {
            group = new VisualElement();
            
            typeTextField = CreateTextField(typeFieldLabel, 200);
            
            group.AddElements(typeTextField);
            group.SetFlexDirection(FlexDirection.Row);
            _graphElementType = GraphElementType.Event;
        }
        
        public bool IsMatch(PropertySectionSubView other)
        {
            return _graphElementType switch
            {
                GraphElementType.Normal => typeTextField.text.Equals(other.typeTextField.text) &&
                                   nameTextField.text.Equals(other.nameTextField.text),
                GraphElementType.Event => typeTextField.text.Equals(other.typeTextField.text),
                GraphElementType.Note => false,
                _ => false
            };
        }
        
    }
}