using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;
using Writer.AssetGenerator.UIElement.Usecase;

namespace GPT
{
    public class LineElement : UxmlItem<VisualElement>
    {
        public readonly UxmlItem<VisualElement> space;
        public readonly UxmlItem<Label> label;
            
        public LineElement(string title)
        {
            this
                .SetName("config-item")
                .StyleFlexDirection(FlexDirection.Row)
                .StyleJustifyContent(Justify.SpaceAround)
                .StyleMarginBottom(5)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleHeight(40)
                .StyleWidth(100, LengthUnit.Percent);
                
            space = UxmlGUI.CreateSpace(10, LengthUnit.Pixel, 100, LengthUnit.Percent, Color.clear);

            label = new Label()
                .SetUp()
                .SetName("config-label")
                .StyleFontSize(12)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(100)
                .StyleHeight(25)
                .SetText(title)
                .StyleTextAlign(TextAnchor.MiddleLeft);
                
            this.Add(label, space);
        }
    }
}