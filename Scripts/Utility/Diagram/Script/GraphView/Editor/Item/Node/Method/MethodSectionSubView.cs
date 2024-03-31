using Share;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class MethodSectionSubView : SectionViewItem
    {
        public Button accessTypeRepeatButton;
        public Button declarationTypeRepeatButton;
        public TextField typeTextField;
        public TextField nameTextField;
        public TextField eventPredicateTextField;
        public TextField parameterTextField;
        public Button removeButton;
        private GraphElementType _graphElementType;

        public void CreateNormalNodeSubview(
            string accessTypeButtonLabel,
            string declarationTypeButtonLabel,
            string typeFieldLabel,
            string nameFieldLabel,
            string parameterFieldLabel)
        {
            group = new VisualElement();

            accessTypeRepeatButton = CreateButton(accessTypeButtonLabel);
            declarationTypeRepeatButton = CreateButton(declarationTypeButtonLabel);
            typeTextField = CreateTextField(typeFieldLabel, 100);
            nameTextField = CreateTextField(nameFieldLabel);
            parameterTextField = CreateTextField(parameterFieldLabel);
            removeButton = CreateButton("-");

            group.AddElements(
                accessTypeRepeatButton,
                declarationTypeRepeatButton,
                typeTextField,
                nameTextField,
                parameterTextField,
                removeButton);

            group.SetFlexDirection(FlexDirection.Row);
            accessTypeRepeatButton.tooltip =
                "'+' means public, '-' means protected, '#' means internal, '~' means private";
            parameterTextField.tooltip =
                "Separate each parameter type with a comma(,). you can write 'type-name' or 'type'";
            _graphElementType = GraphElementType.Normal;
        }

        public void CreateEventNodeView(string nameFieldLabel, string predicateFieldLabel)
        {
            group = new VisualElement();

            nameTextField = CreateTextField(nameFieldLabel);
            eventPredicateTextField = CreateTextField(predicateFieldLabel);
            removeButton = CreateButton("-");

            group.AddElements(nameTextField, eventPredicateTextField, removeButton);

            group.SetFlexDirection(FlexDirection.Row);
            _graphElementType = GraphElementType.Event;
        }

        public bool IsMatch(MethodSectionSubView other)
        {
            return _graphElementType switch
            {
                GraphElementType.Normal => nameTextField.text.Equals(other.nameTextField.text) &&
                                   parameterTextField.text.Equals(other.parameterTextField.text),
                GraphElementType.Event => nameTextField.text.Equals(other.nameTextField.text) &&
                                  eventPredicateTextField.text.Equals(other.eventPredicateTextField.text),
                GraphElementType.Note => false,
                _ => false
            };
        }
    }
}