using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace Writer.CommandConsole
{
    public class LineElement : UxmlItem<VisualElement>
    {
        public readonly UxmlItem<VisualElement> mainContainer;
        public readonly UxmlItem<VisualElement> headerContainer;
        public readonly UxmlItem<Label> headerLabel;
        public readonly UxmlItem<Label> contentLabel;
        public LineElement()
        {
            mainContainer = new VisualElement().SetUp();
            headerContainer = new VisualElement().SetUp();
            headerLabel = new Label().SetUp();
            contentLabel = new Label().SetUp();
            
            mainContainer
                .StyleFlexDirection(FlexDirection.Column)
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);
            
            headerContainer
                .StyleMargin(10)
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);
            
            headerLabel
                .StyleMarginBottom(20)
                .StyleFlexGrow(1)
                .StyleFlexShrink(1)
                .StyleWhiteSpace(WhiteSpace.Normal)
                .StyleFontStyle(FontStyle.BoldAndItalic);
            
            contentLabel
                .StyleWhiteSpace(WhiteSpace.Normal)
                .StyleFlexGrow(1)
                .StyleFlexShrink(1);
            
            headerContainer.Add(headerLabel);
            mainContainer.Add(headerContainer, contentLabel);
        }
    }
}