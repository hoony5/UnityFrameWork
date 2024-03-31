using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Writer.Ex;
using Cursor = UnityEngine.UIElements.Cursor;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Writer.AssetGenerator.UIElement
{
    public static class StyleLineSetterEx
    {
        public static UxmlItem<T> SetUpSize<T>(this UxmlItem<T> uxmlItem) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.flexGrow, 0);
            uxmlItem.element.style.flexGrow = 0;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleAlignContent<T>(this UxmlItem<T> uxmlItem, Align content) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.alignContent, content.ToAlignString());
            uxmlItem.element.style.alignContent = content;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleAlignItems<T>(this UxmlItem<T> uxmlItem, Align items) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.alignItems, items.ToAlignString());
            uxmlItem.element.style.alignItems = items;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleAlignSelf<T>(this UxmlItem<T> uxmlItem, Align self) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.alignSelf, self.ToAlignString());
            uxmlItem.element.style.alignSelf = self;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            string colorRGBA = color.ToRGBA().ToLower();
            uxmlItem.SetStyle(StyleNames.backgroundColor, colorRGBA);
            uxmlItem.element.style.backgroundColor = color;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.backgroundColor, color);
            uxmlItem.element.style.backgroundColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }

        private static string GetPath<T>(T obj) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            return AssetDatabase.GetAssetPath(obj);
#else
            return "";
#endif
        }
        public static UxmlItem<T> StyleBackgroundImage<T>(this UxmlItem<T> uxmlItem, RenderTexture renderTexture) where T : VisualElement
        {
            string path = GetPath(renderTexture);
            uxmlItem.SetStyle(StyleNames.backgroundImage, path);
            // renderTexture to texture2D
            Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();
            RenderTexture.active = null;
            
            uxmlItem.element.style.backgroundImage = new StyleBackground(texture2D);
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBackgroundImage<T>(this UxmlItem<T> uxmlItem, Sprite sprite) where T : VisualElement
        {
            string path = GetPath(sprite);
            uxmlItem.SetStyle(StyleNames.backgroundImage, path);
            // path?
            uxmlItem.element.style.backgroundImage = new StyleBackground(sprite);
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBackgroundImage<T>(this UxmlItem<T> uxmlItem, Texture2D texture2D) where T : VisualElement
        {
            string path = GetPath(texture2D);
            uxmlItem.SetStyle(StyleNames.backgroundImage, path);
            // path?
            uxmlItem.element.style.backgroundImage = new StyleBackground(texture2D);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundPositionX<T>(this UxmlItem<T> uxmlItem, BackgroundPositionKeyword keyword, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string keywordValue = keyword.ToBackgroundPositionKeywordString();
            string lengthValue = $"{value}{unit.ToLengthUnitString()}";
            string positionValue = $"{(string.IsNullOrEmpty(keywordValue) ? "" : $"{keywordValue} ")}{lengthValue}";
            
            uxmlItem.SetStyle(StyleNames.backgroundPositionX, positionValue);
            uxmlItem.element.style.backgroundPositionX = new StyleBackgroundPosition(
                new BackgroundPosition(
                    keyword,
                    new Length(
                        value,
                        unit)
                    )
                );
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundPositionY<T>(this UxmlItem<T> uxmlItem, BackgroundPositionKeyword keyword, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string keywordValue = keyword.ToBackgroundPositionKeywordString();
            string lengthValue = $"{value}{unit.ToLengthUnitString()}";
            string positionValue = $"{(string.IsNullOrEmpty(keywordValue) ? "" : $"{keywordValue} ")}{lengthValue}";
            
            uxmlItem.SetStyle(StyleNames.backgroundPositionY, positionValue);
            uxmlItem.element.style.backgroundPositionX = new StyleBackgroundPosition(
                new BackgroundPosition(
                    keyword,
                    new Length(
                        value,
                        unit)
                    )
                );
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundRepeat<T>(this UxmlItem<T> uxmlItem, Repeat x, Repeat y) where T : VisualElement
        {
            string repeatValue = $"{x.ToRepeatString()} {y.ToRepeatString()}";
            uxmlItem.SetStyle(StyleNames.backgroundRepeat, repeatValue);
            uxmlItem.element.style.backgroundRepeat = new StyleBackgroundRepeat(new BackgroundRepeat(x, y));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundSize<T>(this UxmlItem<T> uxmlItem, BackgroundSizeType keyword) where T : VisualElement
        {
            string keywordValue = keyword.ToBackgroundSizeTypeString();
            uxmlItem.SetStyle(StyleNames.backgroundSize, keywordValue);
            uxmlItem.element.style.backgroundSize = new StyleBackgroundSize(new BackgroundSize(keyword));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundSize<T>(this UxmlItem<T> uxmlItem, float x, LengthUnit xUnit, float y, LengthUnit yUnit) where T : VisualElement
        {
            string xValue = $"{x}{xUnit.ToLengthUnitString()}";
            string yValue = $"{y}{yUnit.ToLengthUnitString()}";
            string sizeValue = $"{xValue} {yValue}";
            uxmlItem.SetStyle(StyleNames.backgroundSize, sizeValue);
            uxmlItem.element.style.backgroundSize = new StyleBackgroundSize(new BackgroundSize(new Length(x, xUnit), new Length(y, yUnit)));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderBottomColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            string colorValue = color.ToRGBA().ToLower();
            uxmlItem.SetStyle(StyleNames.borderBottomColor, colorValue);
            uxmlItem.element.style.borderBottomColor = color;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderLeftColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            string colorValue = color.ToRGBA().ToLower();
            uxmlItem.SetStyle(StyleNames.borderLeftColor, colorValue);
            uxmlItem.element.style.borderLeftColor = color;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderRightColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            string colorValue = color.ToRGBA().ToLower();
            uxmlItem.SetStyle(StyleNames.borderRightColor, colorValue);
            uxmlItem.element.style.borderRightColor = color;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderTopColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            string colorValue = color.ToRGBA().ToLower();
            uxmlItem.SetStyle(StyleNames.borderTopColor, colorValue);
            uxmlItem.element.style.borderTopColor = color;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            string colorValue = color.ToRGBA().ToLower();
            uxmlItem.SetStyle(StyleNames.borderColor, colorValue);
            uxmlItem.element.style.borderBottomColor = color;
            uxmlItem.element.style.borderLeftColor = color;
            uxmlItem.element.style.borderRightColor = color;
            uxmlItem.element.style.borderTopColor = color;
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderColor<T>(this UxmlItem<T> uxmlItem, Color left, Color right) where T : VisualElement
        {
            string leftValue = left.ToHtmlStringRGBA();
            string rightValue = right.ToHtmlStringRGBA();
            string colorValue = $"{leftValue} {rightValue}";
            
            uxmlItem.SetStyle(StyleNames.borderColor, colorValue);
            uxmlItem.element.style.borderBottomColor = left;
            uxmlItem.element.style.borderLeftColor = right;
            uxmlItem.element.style.borderRightColor = right;
            uxmlItem.element.style.borderTopColor = left;
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderBottomColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.borderBottomColor, color);
            uxmlItem.element.style.borderBottomColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);   
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderLeftColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.borderLeftColor, color);
            uxmlItem.element.style.borderLeftColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderRightColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.borderRightColor, color);
            uxmlItem.element.style.borderRightColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderTopColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.borderTopColor, color);
            uxmlItem.element.style.borderTopColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBorderColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.borderColor, color);
            StyleColor colorValue = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            uxmlItem.element.style.borderBottomColor = colorValue;
            uxmlItem.element.style.borderLeftColor = colorValue;
            uxmlItem.element.style.borderRightColor = colorValue;
            uxmlItem.element.style.borderTopColor = colorValue;
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderLeftRadius<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string radiusValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.borderBottomLeftRadius, radiusValue);
            uxmlItem.element.style.borderBottomLeftRadius = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderRightRadius<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string radiusValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.borderBottomRightRadius, radiusValue);
            uxmlItem.element.style.borderBottomRightRadius = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderTopRadius<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string radiusValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.borderTopleftRadius, radiusValue);
            uxmlItem.element.style.borderTopLeftRadius = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderBottomRadius<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string radiusValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.borderTopRightRadius, radiusValue);
            uxmlItem.element.style.borderTopRightRadius = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderRadius<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string radiusValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.borderRadius, radiusValue);
            uxmlItem.element.style.borderBottomLeftRadius = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.borderBottomRightRadius = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.borderTopLeftRadius = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.borderTopRightRadius = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderLeftWidth<T>(this UxmlItem<T> uxmlItem, float value) where T : VisualElement
        {
            string widthValue = $"{value}px";
            uxmlItem.SetStyle(StyleNames.borderLeftWidth, widthValue);
            uxmlItem.element.style.borderLeftWidth = new StyleFloat(value);
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderRightWidth<T>(this UxmlItem<T> uxmlItem, float value) where T : VisualElement
        {
            string widthValue = $"{value}px";
            uxmlItem.SetStyle(StyleNames.borderRightWidth, widthValue);
            uxmlItem.element.style.borderRightWidth = new StyleFloat(value);
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderTopWidth<T>(this UxmlItem<T> uxmlItem, float value) where T : VisualElement
        {
            string widthValue = $"{value}px";
            uxmlItem.SetStyle(StyleNames.borderTopWidth, widthValue);
            uxmlItem.element.style.borderTopWidth = new StyleFloat(value);
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderBottomWidth<T>(this UxmlItem<T> uxmlItem, float value) where T : VisualElement
        {
            string widthValue = $"{value}px";
            uxmlItem.SetStyle(StyleNames.borderBottomWidth, widthValue);
            uxmlItem.element.style.borderBottomWidth = new StyleFloat(value);
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBorderWidth<T>(this UxmlItem<T> uxmlItem, float value) where T : VisualElement
        {
            string widthValue = $"{value}px";
            uxmlItem.SetStyle(StyleNames.borderWidth, widthValue);
            uxmlItem.element.style.borderBottomWidth = new StyleFloat(value);
            uxmlItem.element.style.borderLeftWidth = new StyleFloat(value);
            uxmlItem.element.style.borderRightWidth = new StyleFloat(value);
            uxmlItem.element.style.borderTopWidth = new StyleFloat(value);
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBottom<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string bottomValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.bottom, bottomValue);
            uxmlItem.element.style.bottom = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleTop<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string topValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.top, topValue);
            uxmlItem.element.style.top = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleLeft<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string leftValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.left, leftValue);
            uxmlItem.element.style.left = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        public static UxmlItem<T> StyleRight<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string rightValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.right, rightValue);
            uxmlItem.element.style.right = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        //  [ [ <resource> | <url> ] [ <integer> <integer>]? , ] [ arrow | text | resize-vertical | resize-horizontal | link | slide-arrow | resize-up-right | resize-up-left | move-arrow | rotate-arrow | scale-arrow | arrow-plus | arrow-minus | pan | orbit | zoom | fps | split-resize-up-down | split-resize-left-right ]
        public static UxmlItem<T> StyleColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            string colorValue = color.ToRGBA().ToLower();
            uxmlItem.SetStyle(StyleNames.color, colorValue);
            uxmlItem.element.style.color = color;
            return uxmlItem;
        }
        public static UxmlItem<T> StyleColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.color, color);
            uxmlItem.element.style.color = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleCursor<T>(this UxmlItem<T> uxmlItem, string path) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.cursor, path);
            Cursor cursor = new Cursor();
            cursor.texture = 
#if UNITY_EDITOR 
            AssetDatabase.LoadAssetAtPath<Texture2D>(path);
#else
            null;          
#endif
            uxmlItem.element.style.cursor = new StyleCursor(cursor);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleDisplay<T>(this UxmlItem<T> uxmlItem, DisplayStyle display) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.display, display.ToDisplayStyleString());
            uxmlItem.element.style.display = display;
            return uxmlItem;
        }
        
        /* Three values: flex-grow | flex-shrink | flex-basis */
        public static UxmlItem<T> StyleFlex<T>(this UxmlItem<T> uxmlItem, float grow, float shrink, float basis) where T : VisualElement
        {
            string flexValue = $"{grow} {shrink} {basis}";
            uxmlItem.SetStyle(StyleNames.flex, flexValue);
            uxmlItem.element.style.flexGrow = grow;
            uxmlItem.element.style.flexShrink = shrink;
            uxmlItem.element.style.flexBasis = basis;
            return uxmlItem;
        }
        public static UxmlItem<T> StyleFlexGrowShrink<T>(this UxmlItem<T> uxmlItem, float grow, float shrink) where T : VisualElement
        {
            string flexValue = $"{grow} {shrink}";
            uxmlItem.SetStyle(StyleNames.flex, flexValue);
            uxmlItem.element.style.flexGrow = grow;
            uxmlItem.element.style.flexShrink = shrink;
            return uxmlItem;
        }
        public static UxmlItem<T> StyleFlexGrowBasis<T>(this UxmlItem<T> uxmlItem, float grow, float basis) where T : VisualElement
        {
            string flexValue = $"{grow} {basis}";
            uxmlItem.SetStyle(StyleNames.flex, flexValue);
            uxmlItem.element.style.flexGrow = grow;
            uxmlItem.element.style.flexBasis = basis;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFlexGrow<T>(this UxmlItem<T> uxmlItem, float grow) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.flexGrow, grow);
            uxmlItem.element.style.flexGrow = grow;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFlexShrink<T>(this UxmlItem<T> uxmlItem, float shrink) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.flexShrink, shrink);
            uxmlItem.element.style.flexShrink = shrink;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFlexBasis<T>(this UxmlItem<T> uxmlItem, float basis) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.flexBasis, basis);
            uxmlItem.element.style.flexBasis = basis;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFlexDirection<T>(this UxmlItem<T> uxmlItem, FlexDirection direction) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.flexDirection, direction.ToFlexDirectionString());
            uxmlItem.element.style.flexDirection = direction;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFlexWrap<T>(this UxmlItem<T> uxmlItem, Wrap wrap) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.flexWrap, wrap.ToWrapString());
            uxmlItem.element.style.flexWrap = wrap;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFontSize<T>(this UxmlItem<T> uxmlItem, int size) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.fontSize, size);
            uxmlItem.element.style.fontSize = size;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleHeight<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string heightValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.height, heightValue);
            uxmlItem.element.style.height = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleJustifyContent<T>(this UxmlItem<T> uxmlItem, Justify justify) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.justifyContent, justify.ToJustifyString());
            uxmlItem.element.style.justifyContent = justify;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleLetterSpacing<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string spacingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.letterSpacing, spacingValue);
            uxmlItem.element.style.letterSpacing = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMarginLeft<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string marginValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.marginLeft, marginValue);
            uxmlItem.element.style.marginLeft = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMarginRight<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string marginValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.marginRight, marginValue);
            uxmlItem.element.style.marginRight = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMarginTop<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string marginValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.marginTop, marginValue);
            uxmlItem.element.style.marginTop = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMarginBottom<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string marginValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.marginBottom, marginValue);
            uxmlItem.element.style.marginBottom = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMargin<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string marginValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.margin, marginValue);
            uxmlItem.element.style.marginLeft = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.marginRight = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.marginTop = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.marginBottom = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMaxHeight<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string heightValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.maxHeight, heightValue);
            uxmlItem.element.style.maxHeight = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMaxWidth<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string widthValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.maxWidth, widthValue);
            uxmlItem.element.style.maxWidth = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMinHeight<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string heightValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.minHeight, heightValue);
            uxmlItem.element.style.minHeight = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleMinWidth<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string widthValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.minWidth, widthValue);
            uxmlItem.element.style.minWidth = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleOpacity<T>(this UxmlItem<T> uxmlItem, float value) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.opacity, value);
            uxmlItem.element.style.opacity = value;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleOverflow<T>(this UxmlItem<T> uxmlItem, Overflow overflow) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.overflow, overflow.ToOverflowString());
            uxmlItem.element.style.overflow = overflow;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StylePaddingLeft<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string paddingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.paddingLeft, paddingValue);
            uxmlItem.element.style.paddingLeft = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StylePaddingRight<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string paddingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.paddingRight, paddingValue);
            uxmlItem.element.style.paddingRight = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StylePaddingTop<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string paddingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.paddingTop, paddingValue);
            uxmlItem.element.style.paddingTop = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StylePaddingBottom<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string paddingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.paddingBottom, paddingValue);
            uxmlItem.element.style.paddingBottom = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StylePadding<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string paddingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.padding, paddingValue);
            uxmlItem.element.style.paddingLeft = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.paddingRight = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.paddingTop = new StyleLength(new Length(value, unit));
            uxmlItem.element.style.paddingBottom = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StylePosition<T>(this UxmlItem<T> uxmlItem, Position position) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.position, position.ToPositionString());
            uxmlItem.element.style.position = position;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleRotate<T>(this UxmlItem<T> uxmlItem, float value, AngleUnit unit = AngleUnit.Degree) where T : VisualElement
        {
            string rotateValue = $"{value}{unit.ToAngleUnitString()}";
            uxmlItem.SetStyle(StyleNames.rotate, rotateValue);
            uxmlItem.element.style.rotate = new StyleRotate(new Rotate(new Angle(value, unit)));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleScale<T>(this UxmlItem<T> uxmlItem, float x, float y) where T : VisualElement
        {
            string scaleValue = $"{x} {y}";
            uxmlItem.SetStyle(StyleNames.scale, scaleValue);
            uxmlItem.element.style.scale = new StyleScale(new Scale(new Vector2(x, y)));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTextOverflow<T>(this UxmlItem<T> uxmlItem, TextOverflow overflow) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.textOverFlow, overflow.ToTextOverflowString());
            uxmlItem.element.style.textOverflow = overflow;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTextShadow<T>(this UxmlItem<T> uxmlItem, float x, float y, float blur, Color color) where T : VisualElement
        {
            string shadowValue = $"{x}px {y}px {blur}px {color.ToRGBA().ToLower()}";
            uxmlItem.SetStyle(StyleNames.textShadow, shadowValue);
            uxmlItem.element.style.textShadow = new StyleTextShadow(new TextShadow
            {
                blurRadius = blur,
                color = color,
                offset = new Vector2(x, y)
            });
            return uxmlItem;
        }
        public static UxmlItem<T> StyleTextShadow<T>(this UxmlItem<T> uxmlItem, float x, float y, float blur, string color) where T : VisualElement
        {
            string shadowValue = $"{x}px {y}px {blur}px {color}";
            uxmlItem.SetStyle(StyleNames.textShadow, shadowValue);
            uxmlItem.element.style.textShadow = new StyleTextShadow(new TextShadow
            {
                blurRadius = blur,
                color = ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear,
                offset = new Vector2(x, y)
            });
            return uxmlItem;
        }
        public static UxmlItem<T> StyleTransformOrigin<T>(this UxmlItem<T> uxmlItem, float x, float y, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string originValue = $"{x}{unit.ToLengthUnitString()} {y}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.transformOrigin, originValue);
            uxmlItem.element.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(new Length(x, unit), new Length(y, unit)));
            return uxmlItem;
        }
        
        /// <summary>
        /// only on the uss file
        /// </summary>
        /// <param name="uxmlItem"></param>
        /// <param name="property"></param>
        /// <param name="duration"></param>
        /// <param name="postProcess"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static UxmlItem<T> StyleTransition<T>(this UxmlItem<T> uxmlItem, string property, float duration, string postProcess) where T : VisualElement
        {
            string transitionValue = $"{property} {duration}s {postProcess}";
            uxmlItem.SetStyle(StyleNames.transition, transitionValue);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTransitionDelay<T>(this UxmlItem<T> uxmlItem, params (float seconds, TimeUnit unit)[] times) where T : VisualElement
        {
            List<TimeValue> values = new List<TimeValue>();
            foreach (var time in times)
            {
                values.Add(new TimeValue(time.seconds, time.unit));
            }
            
            string delayValue = string.Join(", ", values.Select(item => $"{item.value}{item.unit.ToTimeUnitString()}"));
            uxmlItem.SetStyle(StyleNames.transitionDelay, delayValue);
            uxmlItem.element.style.transitionDelay = new StyleList<TimeValue>(new List<TimeValue>(values));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTransitionDuration<T>(this UxmlItem<T> uxmlItem, params (float seconds, TimeUnit unit)[] times) where T : VisualElement
        {
            List<TimeValue> values = new List<TimeValue>();
            foreach (var time in times)
            {
                values.Add(new TimeValue(time.seconds, time.unit));
            }
            
            string durationValue = string.Join(", ", values.Select(item => $"{item.value}{item.unit.ToTimeUnitString()}"));
            uxmlItem.SetStyle(StyleNames.transitionDuration, durationValue);
            uxmlItem.element.style.transitionDuration = new StyleList<TimeValue>(new List<TimeValue>(values));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTransitionProperty<T>(this UxmlItem<T> uxmlItem, params string[] properties) where T : VisualElement
        {
            List<StylePropertyName> values = new List<StylePropertyName>();
            string propertyValue = string.Join(", ", properties);
            foreach (var property in properties)
            {
                values.Add(new StylePropertyName(property));
            }
            
            uxmlItem.SetStyle(StyleNames.transitionProperty, propertyValue);
            uxmlItem.element.style.transitionProperty = new StyleList<StylePropertyName>(new List<StylePropertyName>(values));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTransitionTimingFunction<T>(this UxmlItem<T> uxmlItem, params EasingMode[] functions) where T : VisualElement
        {
            List<EasingFunction> values = new List<EasingFunction>();
            foreach (var function in functions)
            {
                values.Add(new EasingFunction(function));
            }
            
            string functionValue = string.Join(", ", values.Select(item => item.mode.ToEasingModeString()));
            uxmlItem.SetStyle(StyleNames.transitionTimingFunction, functionValue);
            uxmlItem.element.style.transitionTimingFunction = new StyleList<EasingFunction>(new List<EasingFunction>(values));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTranslate<T>(this UxmlItem<T> uxmlItem, float x, float y, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string translateValue = $"{x}{unit.ToLengthUnitString()} {y}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.translate, translateValue);
            uxmlItem.element.style.translate = new StyleTranslate(new Translate(new Length(x, unit), new Length(y, unit)));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleBackgroundImageTintColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityBackgroundImageTintColor, color.ToRGBA().ToLower());
            uxmlItem.element.style.unityBackgroundImageTintColor = color;
            return uxmlItem;
        }
        public static UxmlItem<T> StyleBackgroundImageTintColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityBackgroundImageTintColor, color);
            uxmlItem.element.style.unityBackgroundImageTintColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFont<T>(this UxmlItem<T> uxmlItem, Font font) where T : VisualElement
        {
            string path = GetPath(font);
            uxmlItem.SetStyle(StyleNames.unityFont, path);
            uxmlItem.element.style.unityFont = font;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFontDefinition<T>(this UxmlItem<T> uxmlItem, Font font) where T : VisualElement
        {
            string path = GetPath(font);
            uxmlItem.SetStyle(StyleNames.unityFontDefinition, path);
            uxmlItem.element.style.unityFontDefinition = new StyleFontDefinition(font);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFontDefinition<T>(this UxmlItem<T> uxmlItem, FontAsset fontAsset) where T : VisualElement
        {
            string path = GetPath(fontAsset);
            uxmlItem.SetStyle(StyleNames.unityFontDefinition, path);
            uxmlItem.element.style.unityFontDefinition = new StyleFontDefinition(fontAsset);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFontStyle<T>(this UxmlItem<T> uxmlItem, FontStyle style) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityFontStyle, style.ToFontStyleString());
            uxmlItem.element.style.unityFontStyleAndWeight = style;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleFontOverflowClipBox<T>(this UxmlItem<T> uxmlItem, OverflowClipBox clipBox) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityOverflowClipBox, clipBox.ToOverflowClipBoxString());
            uxmlItem.element.style.unityOverflowClipBox = new StyleEnum<OverflowClipBox>(clipBox);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleParagraphSpacing<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string spacingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.unityParagraphSpacing, spacingValue);
            uxmlItem.element.style.unityParagraphSpacing = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleSliceBottom<T>(this UxmlItem<T> uxmlItem, int value) where T : VisualElement
        {
            string sliceValue = $"{value}";
            uxmlItem.SetStyle(StyleNames.unitySliceBottom, sliceValue);
            uxmlItem.element.style.unitySliceBottom = new StyleInt(value);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleSliceLeft<T>(this UxmlItem<T> uxmlItem, int value) where T : VisualElement
        {
            string sliceValue = $"{value}";
            uxmlItem.SetStyle(StyleNames.unitySliceLeft, sliceValue);
            uxmlItem.element.style.unitySliceLeft = new StyleInt(value);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleSliceRight<T>(this UxmlItem<T> uxmlItem, int value) where T : VisualElement
        {
            string sliceValue = $"{value}";
            uxmlItem.SetStyle(StyleNames.unitySliceRight, sliceValue);
            uxmlItem.element.style.unitySliceRight = new StyleInt(value);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleSliceTop<T>(this UxmlItem<T> uxmlItem, int value) where T : VisualElement
        {
            string sliceValue = $"{value}";
            uxmlItem.SetStyle(StyleNames.unitySliceTop, sliceValue);
            uxmlItem.element.style.unitySliceTop = new StyleInt(value);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTextOverflowPosition<T>(this UxmlItem<T> uxmlItem, TextOverflowPosition position) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityTextOverflowPosition, position.ToTextOverflowPositionString());
            uxmlItem.element.style.unityTextOverflowPosition = position;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleSliceScale<T>(this UxmlItem<T> uxmlItem, float value) where T : VisualElement
        {
            string sliceValue = $"{value}";
            uxmlItem.SetStyle(StyleNames.unitySliceScale, sliceValue);
            uxmlItem.element.style.unitySliceScale = new StyleFloat(value);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleTextAlign<T>(this UxmlItem<T> uxmlItem, TextAnchor align) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityTextAlign, align.ToTextAnchorString());
            uxmlItem.element.style.unityTextAlign = align;
            return uxmlItem;
        }
        
        
        public static UxmlItem<T> StyleWidth<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string widthValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.width, widthValue);
            uxmlItem.element.style.width = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleOutlineWidth<T>(this UxmlItem<T> uxmlItem, float width) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityTextOutLineWidth, width);
            uxmlItem.element.style.unityTextOutlineWidth = new StyleFloat(width);
            return uxmlItem;
        }

        public static UxmlItem<T> StyleOutlineColor<T>(this UxmlItem<T> uxmlItem, Color color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityTextOutLineColor, color.ToRGBA().ToLower());
            uxmlItem.element.style.unityTextOutlineColor = color;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleOutline<T>(this UxmlItem<T> uxmlItem, float width, Color color) where T : VisualElement
        {
            string outlineValue = $"{width} {color.ToRGBA().ToLower()}";
            uxmlItem.SetStyle(StyleNames.unityTextOutline, outlineValue);
            uxmlItem.element.style.unityTextOutlineWidth = new StyleFloat(width);
            uxmlItem.element.style.unityTextOutlineColor = color;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleOutlineColor<T>(this UxmlItem<T> uxmlItem, string color) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.unityTextOutLineColor, color);
            uxmlItem.element.style.unityTextOutlineColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleOutline<T>(this UxmlItem<T> uxmlItem, float width, string color) where T : VisualElement
        {
            string outlineValue = $"{width} {color}";
            uxmlItem.SetStyle(StyleNames.unityTextOutline, outlineValue);
            uxmlItem.element.style.unityTextOutlineWidth = new StyleFloat(width);
            uxmlItem.element.style.unityTextOutlineColor = new StyleColor(ColorUtility.TryParseHtmlString(color, out Color c) ? c : Color.clear);
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleVisibility<T>(this UxmlItem<T> uxmlItem, Visibility visibility) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.visibility, visibility.ToVisibilityString());
            uxmlItem.element.style.visibility = visibility;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleWhiteSpace<T>(this UxmlItem<T> uxmlItem, WhiteSpace whiteSpace) where T : VisualElement
        {
            uxmlItem.SetStyle(StyleNames.whiteSpace, whiteSpace.ToWhiteSpaceString());
            uxmlItem.element.style.whiteSpace = whiteSpace;
            return uxmlItem;
        }
        
        public static UxmlItem<T> StyleWordSpacing<T>(this UxmlItem<T> uxmlItem, float value, LengthUnit unit = LengthUnit.Pixel) where T : VisualElement
        {
            string spacingValue = $"{value}{unit.ToLengthUnitString()}";
            uxmlItem.SetStyle(StyleNames.wordSpacing, spacingValue);
            uxmlItem.element.style.wordSpacing = new StyleLength(new Length(value, unit));
            return uxmlItem;
        }
    }
}