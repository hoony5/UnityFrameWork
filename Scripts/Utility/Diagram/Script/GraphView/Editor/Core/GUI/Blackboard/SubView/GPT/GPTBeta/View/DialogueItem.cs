using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;
using Writer.AssetGenerator.UIElement.Usecase;

namespace GPT
{
    public class DialogueItem : UxmlItem<VisualElement>
    {
        public readonly UxmlItem<VisualElement> headerContainer;
        public readonly UxmlItem<VisualElement> contentContainer;
        
        public readonly UxmlItem<Label> headerLabel;
        public readonly UxmlItem<Toggle> headerToggle;
        public readonly UxmlItem<Label> contentLabel;

        public DialogueItem(string title, string content)
        {
            this
                .SetName("dialogue-item")
                .StyleFlexDirection(FlexDirection.Column)
                .StyleWhiteSpace(WhiteSpace.Normal)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleBackgroundColor(new Color(0.5f, 0.5f, 0.5f, 0.5f))
                .StyleWidth(100, LengthUnit.Percent);
            
            headerContainer = new VisualElement()
                .SetUp()
                .SetName("dialogue-header")
                .StyleFlexDirection(FlexDirection.Row)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleHeight(30)
                .StyleWidth(100, LengthUnit.Percent);
            
            contentContainer = new VisualElement()
                .SetUp()
                .SetName("dialogue-content")
                .StyleFlexDirection(FlexDirection.Row)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(100, LengthUnit.Percent);
            
            headerLabel = new Label()
                .SetUp()
                .SetName("dialogue-header-label")
                .StyleFontSize(12)
                .StyleWidth(100, LengthUnit.Percent)
                .StyleHeight(25)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .SetText(title)
                .StyleWhiteSpace(WhiteSpace.Normal)
                .StyleTextAlign(TextAnchor.MiddleLeft);
             
            headerToggle = new Toggle()
                .SetUp()
                .SetName("dialogue-header-toggle")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(25)
                .StyleHeight(25);
            
            contentLabel = new Label()
                .SetUp()
                .SetName("dialogue-content-label")
                .SetEnableRichText(true)
                .StyleFontSize(12)
                .StyleWidth(100, LengthUnit.Percent)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWhiteSpace(WhiteSpace.Normal)
                .SetText(content)
                .StyleTextAlign(TextAnchor.MiddleLeft);
            
            contentLabel.element.selection.isSelectable = true;
            
            this.Add(
                headerContainer
                    .Add(headerToggle, headerLabel),
                UxmlGUI.CreateSpace(100, LengthUnit.Percent, 1, LengthUnit.Pixel, new Color(0, 0, 0, 1)),
                contentContainer
                    .Add(contentLabel));
        }
    }
}