using UnityEngine;
using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement.Usecase
{
    public static class InlineStyle
    {
        public static UxmlItem<Label> SetLabelStyle(this Label label, float width, float height, int fontSize, TextAnchor anchor)
        {
            return label
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height)
                .StyleFontSize(fontSize)
                .StyleTextAlign(anchor);
        }
        
        public static UxmlItem<TextField> SetTextFieldStyle(this TextField textField, float width, float height)
        {
            return textField
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        
        public static UxmlItem<IntegerField> SetIntegerFieldStyle(this IntegerField integerField, float width, float height)
        {
            return integerField
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        
        public static UxmlItem<FloatField> SetFloatFieldStyle(this FloatField floatField, float width, float height)
        {
            return floatField
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        
        public static UxmlItem<EnumField> SetEnumFieldStyle(this EnumField enumField, float width, float height)
        {
            return enumField
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        public static UxmlItem<DropdownField> SetDropdownFieldStyle(this DropdownField dropdownField, float width, float height)
        {
            return dropdownField
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        public static UxmlItem<DoubleField> SetDoubleFieldStyle(this DoubleField doubleField, float width, float height)
        {
            return doubleField
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        public static UxmlItem<LongField> SetLongFieldStyle(this LongField longField, float width, float height)
        {
            return longField
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        public static UxmlItem<Button> SetButtonStyle(this Button button, float width, float height, int fontSize)
        {
            return button
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height)
                .StyleFontSize(fontSize);
        }
        public static UxmlItem<Toggle> SetToggleStyle(this Toggle toggle, float width, float height)
        {
            return toggle
                .SetUp()
                .StyleFlexGrow(0)
                .StyleFlexShrink(1)
                .StyleWidth(width)
                .StyleHeight(height);
        }
        
    }
}