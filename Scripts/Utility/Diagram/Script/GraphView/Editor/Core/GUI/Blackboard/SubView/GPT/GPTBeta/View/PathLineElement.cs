using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace GPT
{
    public class PathLineElement : LineElement
    {
        public readonly UxmlItem<TextField> textField;
        public readonly UxmlItem<Button> button;

        public PathLineElement(string title, bool useMask) : base(title)
        {
            textField = new TextField()
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(250)
                .StyleHeight(25);

            if (useMask)
                textField.element.maskChar = '*';

            button = new Button()
                .SetUp()
                .SetName("config-button")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(35)
                .StyleHeight(25)
                .SetText("...");

            this.Add(textField, button);
        }

    }
}