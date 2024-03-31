using Share;
using UnityEngine;
using UnityEngine.UIElements;
using static Share.VisualElementEx;

namespace Diagram
{
    public class BlackboardNormalNodeView : VisualElement
    {
        public VisualElement container;
        public VisualElement headerContainer;
        public Label nameLabelField;            // node name
        public VisualElement scriptExampleSection;
        public TextField scriptExampleField;        // source code ex
        public TextField textAreaField;      // description

        public BlackboardNormalNodeView(string name)
        {
             container = new VisualElement();
             headerContainer = new VisualElement();
             scriptExampleSection = new VisualElement();
             nameLabelField = CreateLabel(name.IsNullOrEmpty() ? "Name" : name);
             Label scriptExampleLabel = CreateLabel("Example (Read Only)");
             scriptExampleField = CreateTextField(@"public class OnExample 
{
 ... 
}");
             Label textAreaLabel = new Label("Note..");
             textAreaField = CreateTextArea("Description");
             
             headerContainer.Add(nameLabelField);
             container.Add(headerContainer);
             container.CreateVerticalSpace(15);
             container.CreateHorizontalBar(5, Color.yellow);
             container.CreateVerticalSpace(15);
             container.Add(scriptExampleLabel);
             scriptExampleSection.Add(scriptExampleField);
             container.Add(scriptExampleSection);
             container.CreateVerticalSpace(30);
             container.Add(textAreaLabel);
             container.Add(textAreaField);
             
             container.SetFlexDirection(FlexDirection.Column);
             headerContainer.SetFlexDirection(FlexDirection.Row);
             
             nameLabelField.SetFlexGrow(1);
             nameLabelField.SetHeight(25);
             nameLabelField.SetFontSize(18);
             nameLabelField.SetFontStyle(FontStyle.Bold);
             nameLabelField.SetTextAlign(TextAnchor.MiddleLeft);
             
             scriptExampleLabel.SetFlexGrow(1);
             scriptExampleLabel.SetHeight(25);
             scriptExampleLabel.SetFontSize(12);
             scriptExampleLabel.SetFontStyle(FontStyle.Bold);
             scriptExampleLabel.SetTextAlign(TextAnchor.MiddleLeft);
             
             textAreaLabel.SetFlexGrow(1);
             textAreaLabel.SetHeight(25);
             textAreaLabel.SetFontSize(12);
             textAreaLabel.SetFontStyle(FontStyle.Bold);
             textAreaLabel.SetTextAlign(TextAnchor.MiddleLeft);
             
             scriptExampleField.SetWhiteSpace(WhiteSpace.Normal);
             scriptExampleField.SetFlexGrow(0);
             scriptExampleField.SetFlexShrink(1);
             scriptExampleField.style.width = new StyleLength(new Length(95, LengthUnit.Percent));
             scriptExampleField.SetHeight(430);
             scriptExampleField.multiline = true;
             scriptExampleField.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);
             
             scriptExampleSection.SetFlexGrow(1);
             scriptExampleSection.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
             scriptExampleSection.SetHeight(450);
             scriptExampleSection.SetBackgroundColor(new Color(0.12f,0.12f,0.12f,1));
             scriptExampleSection.style.alignItems = Align.Center;
             scriptExampleSection.style.justifyContent = Justify.Center;
             
             textAreaField.SetFlexGrow(1);
             textAreaField.style.width = new StyleLength(new Length(95, LengthUnit.Percent));
             textAreaField.SetHeight(450);
             textAreaField.SetWhiteSpace(WhiteSpace.Normal);
            
             Add(container);
        }

        public void ClearValue()
        {
            nameLabelField.text = string.Empty;
            scriptExampleField.value = string.Empty;
            textAreaField.value = string.Empty;
        }
    }
}