using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class CheckList : VisualElement
    {
        public int index;
        private CheckList note;
        public Toggle toggle;
        public TextField descriptionField;
        public Button removeButton;

        public CheckList(bool value = false, string description = "description...") 
        {
            toggle = CreateToggle(value);
            descriptionField = CreateTextField(description);
            removeButton = CreateButton("-");
            
            Add(toggle);
            this.CreateHorizontalSpace(20);
            Add(descriptionField);
            this.CreateHorizontalSpace(20);
            Add(removeButton);
            
            // Style
            this.SetBorderLeftWidth(5);
            this.SetFlexDirection(FlexDirection.Row);
            this.SetFlexGrow(0);
            this.SetTextAlign(TextAnchor.UpperLeft);
            
            // toggle - description style
            toggle.SetSize(20, 20);
            descriptionField.SetSize(180, 20);
            descriptionField.SetWhiteSpace(WhiteSpace.Normal);
            descriptionField.SetFlexGrow(0);
            removeButton.SetSize(20, 20);
            removeButton.SetFlexGrow(0);
        }
    }
}