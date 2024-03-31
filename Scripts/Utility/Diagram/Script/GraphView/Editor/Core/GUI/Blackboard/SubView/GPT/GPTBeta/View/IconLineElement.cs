using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace GPT
{
    public class IconLineElement : UxmlItem<VisualElement>
    {
        public readonly UxmlItem<Toggle> toggle;
        public readonly UxmlItem<Label> label;
        public readonly UxmlItem<VisualElement> space;
        public readonly UxmlItem<VisualElement> icon;
        public readonly UxmlItem<Button> removeButton;
        
        /// <summary>
        /// path i.e "d_Collab.FileAdded"
        /// </summary>
        /// <param name="iconPath"></param>
        public IconLineElement(string iconPath, string fileName)
        {
            this
                .SetName("icon-line")
                .StyleFlexDirection(FlexDirection.Row)
                .StyleJustifyContent(Justify.SpaceAround)
                .StyleMargin(10)
                .StyleMarginBottom(5)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleHeight(25)
                .StyleWidth(95, LengthUnit.Percent);
            
            toggle = new Toggle()
                .SetUp()
                .SetName("icon-toggle")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(25)
                .StyleHeight(25);
            
            space = new UxmlItem<VisualElement>()
                .SetName("icon-space")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(5);
            
            label = new Label()
                .SetUp()
                .SetName("icon-label")
                .SetText(fileName)
                .StyleFontSize(12)
                .StyleFlexShrink(1)
                .StyleWidth(100)
                .StyleTextOverflow(TextOverflow.Ellipsis)
                .StyleOverflow(Overflow.Hidden)
                .StyleHeight(25)
                .StyleTextAlign(TextAnchor.MiddleLeft);
            
            removeButton = new Button()
                .SetUp()
                .SetName("icon-remove-button")
                .SetText("-")
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(25)
                .StyleHeight(25);
            
            // unity icon image path by extension?
            Texture2D iconTexture = UnityEditor.EditorGUIUtility.FindTexture(iconPath);
            icon = new VisualElement()
                .SetUp()
                .SetName("icon")
                .StyleBackgroundImage(iconTexture)
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(25)
                .StyleHeight(25);
            
            this.Add(space, toggle, icon, label, removeButton);
        }
    }
}