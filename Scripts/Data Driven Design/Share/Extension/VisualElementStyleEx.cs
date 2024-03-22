using UnityEngine;
using UnityEngine.UIElements;

public static class VisualElementStyleEx
{
    public static VisualElement SetBorderWidth(this VisualElement visualElement, int width)
    {
        visualElement.style.borderBottomWidth = width;
        visualElement.style.borderLeftWidth = width;
        visualElement.style.borderRightWidth = width;
        visualElement.style.borderTopWidth = width;
        return visualElement;
    }
    
    public static VisualElement SetBorderBottomWidth(this VisualElement visualElement, int width)
    {
        visualElement.style.borderBottomWidth = width;
        return visualElement;
    }
    public static VisualElement SetBorderLeftWidth(this VisualElement visualElement, int width)
    {
        visualElement.style.borderLeftWidth = width;
        return visualElement;
    }
    public static VisualElement SetBorderRightWidth(this VisualElement visualElement, int width)
    {
        visualElement.style.borderRightWidth = width;
        return visualElement;
    }
    public static VisualElement SetBorderTopWidth(this VisualElement visualElement, int width)
    {
        visualElement.style.borderTopWidth = width;
        return visualElement;
    }
    
    public static VisualElement SetFlexDirection(this VisualElement visualElement, FlexDirection flexDirection)
    {
        visualElement.style.flexDirection = flexDirection;
        return visualElement;
    }
    
    public static VisualElement SetWidth(this VisualElement visualElement, float width)
    {
        visualElement.style.width = width;
        return visualElement;
    }
    public static VisualElement SetWidth(this VisualElement visualElement, float width, LengthUnit lengthUnit)
    {
        visualElement.style.width = new Length(width, lengthUnit);
        return visualElement;
    }
    public static VisualElement SetWidth(this VisualElement visualElement, StyleLength length)
    {
        visualElement.style.width = length;
        return visualElement;
    }
    
    public static VisualElement SetHeight(this VisualElement visualElement, float height)
    {
        visualElement.style.height = height;
        return visualElement;
    }
    public static VisualElement SetHeight(this VisualElement visualElement, int height, LengthUnit lengthUnit)
    {
        visualElement.style.height = new Length(height, lengthUnit);
        return visualElement;
    }
    public static VisualElement SetHeight(this VisualElement visualElement, StyleLength length)
    {
        visualElement.style.height = length;
        return visualElement;
    }
    public static VisualElement SetSize(this VisualElement visualElement, float width, float height)
    {
        visualElement.style.width = width;
        visualElement.style.height = height;
        return visualElement;
    }
    public static VisualElement SetSize(this VisualElement visualElement, StyleLength width, float height)
    {
        visualElement.style.width = width;
        visualElement.style.height = height;
        return visualElement;
    }
    public static VisualElement SetSize(this VisualElement visualElement, float width, StyleLength height)
    {
        visualElement.style.width = width;
        visualElement.style.height = height;
        return visualElement;
    }
    public static VisualElement SetSize(this VisualElement visualElement, StyleLength width, StyleLength height)
    {
        visualElement.style.width = width;
        visualElement.style.height = height;
        return visualElement;
    }
    public static VisualElement SetSize(this VisualElement visualElement, Vector2 size)
    {
        visualElement.style.width = size.x;
        visualElement.style.height = size.y;
        return visualElement;
    }
    
    public static VisualElement SetFlexGrow(this VisualElement visualElement, int flexGrow)
    {
        visualElement.style.flexGrow = flexGrow;
        return visualElement;
    }
    public static VisualElement SetFlexShrink(this VisualElement visualElement, int flexShrink)
    {
        visualElement.style.flexShrink = flexShrink;
        return visualElement;
    }
    public static VisualElement SetWhiteSpace(this VisualElement visualElement, WhiteSpace whiteSpace)
    {
        visualElement.style.whiteSpace = whiteSpace;
        return visualElement;
    }
    public static VisualElement SetDisplay(this VisualElement visualElement, bool visible)
    {
        visualElement.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        return visualElement;
    }
    public static VisualElement SetVisible(this VisualElement visualElement, bool visible)
    {
        visualElement.style.opacity = visible ? 1 : 0;
        visualElement.SetEnabled(visible); 
        return visualElement;
    }
    public static VisualElement SetFontSize(this VisualElement visualElement, int fontSize)
    {
        visualElement.style.fontSize = fontSize;
        return visualElement;
    }
    public static VisualElement SetFontStyle(this VisualElement visualElement, FontStyle fontStyle)
    {
        visualElement.style.unityFontStyleAndWeight = fontStyle;
        return visualElement;
    }
    public static VisualElement SetTextOverflow(this VisualElement visualElement, TextOverflow textOverflow)
    {
        visualElement.style.textOverflow = textOverflow;
        visualElement.style.whiteSpace = textOverflow == TextOverflow.Ellipsis ? WhiteSpace.NoWrap : WhiteSpace.Normal; 
        visualElement.style.overflow = textOverflow == TextOverflow.Ellipsis ? Overflow.Hidden : Overflow.Visible;
        return visualElement;
    }
    
    public static VisualElement SetBorderRadius(this VisualElement visualElement, int radius)
    {
        visualElement.style.borderBottomLeftRadius = radius;
        visualElement.style.borderBottomRightRadius = radius;
        visualElement.style.borderTopLeftRadius = radius;
        visualElement.style.borderTopRightRadius = radius;
        return visualElement;
    }
    
    public static VisualElement SetMargin(this VisualElement visualElement, int margin)
    {
        visualElement.style.marginBottom = margin;
        visualElement.style.marginLeft = margin;
        visualElement.style.marginRight = margin;
        visualElement.style.marginTop = margin;
        return visualElement;
    }
    
    public static VisualElement SetPadding(this VisualElement visualElement, int padding)
    {
        visualElement.style.paddingBottom = padding;
        visualElement.style.paddingLeft = padding;
        visualElement.style.paddingRight = padding;
        visualElement.style.paddingTop = padding;
        return visualElement;
    }
    
    public static VisualElement SetTextAlign(this VisualElement visualElement, TextAnchor textAnchor)
    {
        visualElement.style.unityTextAlign = textAnchor;
        return visualElement;
    }
    public static VisualElement SetAlignSelf(this VisualElement visualElement, Align alignSelf)
    {
        visualElement.style.alignSelf = alignSelf;
        return visualElement;
    }
    
    public static VisualElement SetBackgroundColor(this VisualElement visualElement, Color color)
    {
        visualElement.style.backgroundColor = color;
        return visualElement;
    }
    public static VisualElement SetBorderColor(this VisualElement visualElement, Color color)
    {
        visualElement.style.borderBottomColor = color;
        visualElement.style.borderLeftColor = color;
        visualElement.style.borderRightColor = color;
        visualElement.style.borderTopColor = color;
        return visualElement;
    }
    public static VisualElement SetBorderBottomColor(this VisualElement visualElement, Color color)
    {
        visualElement.style.borderBottomColor = color;
        return visualElement;
    }
    public static VisualElement SetBorderLeftColor(this VisualElement visualElement, Color color)
    {
        visualElement.style.borderLeftColor = color;
        return visualElement;
    }
    public static VisualElement SetBorderRightColor(this VisualElement visualElement, Color color)
    {
        visualElement.style.borderRightColor = color;
        return visualElement;
    }
    public static VisualElement SetBorderTopColor(this VisualElement visualElement, Color color)
    {
        visualElement.style.borderTopColor = color;
        return visualElement;
    }
    public static VisualElement SetFontColor(this VisualElement visualElement, Color color)
    {
        visualElement.style.color = color;
        return visualElement;
    }
    public static VisualElement SetSizeNotGrowButShrink(this VisualElement visualElement, float width, float height)
    {
        visualElement.SetSize(width, height);
        visualElement.SetFlexShrink(1);
        visualElement.SetFlexGrow(0);
        return visualElement;
    }
    public static VisualElement SetSizeNotGrowButShrink(this VisualElement visualElement, StyleLength width, StyleLength height)
    {
        visualElement.style.width = width;
        visualElement.style.height = height;
        visualElement.SetFlexShrink(1);
        visualElement.SetFlexGrow(0);
        return visualElement;
    }
    public static VisualElement SetWidthNotGrowButShrink(this VisualElement visualElement, StyleLength width)
    {
        visualElement.style.width = width;
        visualElement.SetFlexShrink(1);
        visualElement.SetFlexGrow(0);
        return visualElement;
    }
    public static VisualElement SetHeightNotGrowButShrink(this VisualElement visualElement, StyleLength height)
    {
        visualElement.style.height = height;
        visualElement.SetFlexShrink(1);
        visualElement.SetFlexGrow(0);
        return visualElement;
    }
    public static VisualElement SetSizeNotGrowButShrink(this VisualElement visualElement, Vector2 size)
    {
        visualElement.style.width = size.x;
        visualElement.style.height = size.y;
        visualElement.SetFlexShrink(1);
        visualElement.SetFlexGrow(0);
        return visualElement;
    }
    
    public static VisualElement SetRootContainerStyle(this VisualElement container, float width, float height, FlexDirection flexDirection = FlexDirection.Column)
    {
        container.SetWidth(width);
        container.SetHeight(height);
        container.SetFlexDirection(flexDirection);
        return container;
    }
    public static VisualElement StretchWidth(this VisualElement visualElement, float length)
    {
        visualElement.style.width = new StyleLength(new Length(length, LengthUnit.Percent));
        return visualElement;
    }
    public static VisualElement StretchHeight(this VisualElement visualElement, float length)
    {
        visualElement.style.height = new StyleLength(new Length(length, LengthUnit.Percent));
        return visualElement;
    }
    public static VisualElement SetPosition(this VisualElement visualElement, Position position)
    {
        visualElement.style.position = position;
        return visualElement;
    }
    public static VisualElement SetTop(this VisualElement visualElement, float top)
    {
        visualElement.style.top = top;
        return visualElement;
    }
    public static VisualElement SetBottom(this VisualElement visualElement, float bottom)
    {
        visualElement.style.bottom = bottom;
        return visualElement;
    }
    public static VisualElement SetLeft(this VisualElement visualElement, float left)
    {
        visualElement.style.left = left;
        return visualElement;
    }
    public static VisualElement SetRight(this VisualElement visualElement, float right)
    {
        visualElement.style.right = right;
        return visualElement;
    }
    public static VisualElement SetPosition(this VisualElement visualElement, float top, float bottom, float left, float right)
    {
        visualElement.style.top = top;
        visualElement.style.bottom = bottom;
        visualElement.style.left = left;
        visualElement.style.right = right;
        return visualElement;
    }
    public static VisualElement SetAlignItems(this VisualElement visualElement, Align alignItems)
    {
        visualElement.style.alignItems = alignItems;
        return visualElement;
    }
    public static VisualElement SetJustifyContent(this VisualElement visualElement, Justify justifyContent)
    {
        visualElement.style.justifyContent = justifyContent;
        return visualElement;
    }
    public static VisualElement SetWrapMode(this VisualElement visualElement, Wrap wrap)
    {
        visualElement.style.flexWrap = wrap;
        return visualElement;
    }
    public static VisualElement SetOpacity(this VisualElement visualElement, float opacity)
    {
        visualElement.style.opacity = opacity;
        return visualElement;
    }
    public static VisualElement SetOrigin(this VisualElement visualElement, float x, LengthUnit xUnit, float y, LengthUnit yUnit)
    {
        visualElement.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(new Length(x, xUnit), new Length(y, yUnit)));
        return visualElement;
    }
    public static VisualElement SetScale(this VisualElement visualElement, float x, float y)
    {
        visualElement.style.scale = new StyleScale(new Scale(new Vector2(x, y)));
        return visualElement;
    }
}
