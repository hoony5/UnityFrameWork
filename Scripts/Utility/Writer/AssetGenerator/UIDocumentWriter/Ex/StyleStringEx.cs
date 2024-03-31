using UnityEngine;
using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement
{
    public static class StyleStringEx
    {
        public static string ToLengthUnitString(this LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Pixel:
                    return "px";
                case LengthUnit.Percent:
                    return "%";
                default:
                    return "auto";
            }
        }
        
        public static string ToTimeUnitString(this TimeUnit unit)
        {
            switch (unit)
            {
                case TimeUnit.Millisecond:
                    return "ms";
                case TimeUnit.Second:
                    return "s";
                default:
                    return "s";
            }
        }

        public static string ToAlignString(this Align align)
        {
            switch (align)
            {
                case Align.Auto:
                    return "auto";
                case Align.FlexStart:
                    return "flex-start";
                case Align.FlexEnd:
                    return "flex-end";
                case Align.Center:
                    return "center";
                case Align.Stretch:
                    return "stretch";
                default:
                    return "auto";
            }
        }
        
        public static string ToBackgroundPositionKeywordString(this BackgroundPositionKeyword keyword)
        {
            switch (keyword)
            {
                case BackgroundPositionKeyword.Top:
                    return "top";
                case BackgroundPositionKeyword.Bottom:
                    return "bottom";
                case BackgroundPositionKeyword.Left:
                    return "left";
                case BackgroundPositionKeyword.Right:
                    return "right";
                case BackgroundPositionKeyword.Center:
                    return "center";
                default:
                    return "center";
            }
        }
        
        public static string ToRepeatString(this Repeat repeat)
        {
            switch (repeat)
            {
                case Repeat.NoRepeat:
                    return "no-repeat";
                case Repeat.Repeat:
                    return "repeat";
                case Repeat.Round:
                    return "round";
                case Repeat.Space:
                    return "space";
                default:
                    return "no-repeat";
            }
        }
        
        public static string ToBackgroundSizeTypeString(this BackgroundSizeType type)
        {
            switch (type)
            {
                case BackgroundSizeType.Contain:
                    return "contain";
                case BackgroundSizeType.Cover:
                    return "cover";
                case BackgroundSizeType.Length:
                    return "length";
                default:
                    return "auto";
            }
        }
        
        public static string ToDisplayStyleString(this DisplayStyle display)
        {
            switch (display)
            {
                case DisplayStyle.None:
                    return "none";
                case DisplayStyle.Flex:
                    return "flex";
                default:
                    return "none";
            }
        }
        
        public static string ToFlexDirectionString(this FlexDirection direction)
        {
            switch (direction)
            {
                case FlexDirection.Row:
                    return "row";
                case FlexDirection.RowReverse:
                    return "row-reverse";
                case FlexDirection.Column:
                    return "column";
                case FlexDirection.ColumnReverse:
                    return "column-reverse";
                default:
                    return "row";
            }
        }
        
        public static string ToWrapString(this Wrap wrap)
        {
            switch (wrap)
            {
                case Wrap.NoWrap:
                    return "nowrap";
                case Wrap.Wrap:
                    return "wrap";
                case Wrap.WrapReverse:
                    return "wrap-reverse";
                default:
                    return "nowrap";
            }
        }
        
        public static string ToJustifyString(this Justify justify)
        {
            switch (justify)
            {
                case Justify.FlexStart:
                    return "flex-start";
                case Justify.FlexEnd:
                    return "flex-end";
                case Justify.Center:
                    return "center";
                case Justify.SpaceBetween:
                    return "space-between";
                case Justify.SpaceAround:
                    return "space-around";
                default:
                    return "flex-start";
            }
        }
        
        public static string ToOverflowString(this Overflow overflow)
        {
            switch (overflow)
            {
                case Overflow.Visible:
                    return "visible";
                case Overflow.Hidden:
                    return "hidden";
                default:
                    return "visible";
            }
        }
        
        public static string ToPositionString(this Position position)
        {
            switch (position)
            {
                case Position.Relative:
                    return "relative";
                case Position.Absolute:
                    return "absolute";
                default:
                    return "absolute";
            }
        }
        
        public static string ToAngleUnitString(this AngleUnit unit)
        {
            switch (unit)
            {
                case AngleUnit.Degree:
                    return "deg";
                case AngleUnit.Radian:
                    return "rad";
                case AngleUnit.Turn:
                    return "turn";
                default:
                    return "deg";
            }
        }
        
        public static string ToTextOverflowString(this TextOverflow overflow)
        {
            switch (overflow)
            {
                case TextOverflow.Clip:
                    return "clip";
                case TextOverflow.Ellipsis:
                    return "ellipsis";
                default:
                    return "clip";
            }
        }

        public static string ToEasingModeString(this EasingMode mode)
        {
            switch (mode)
            {
                case EasingMode.Ease:
                    return "ease";
                case EasingMode.Linear:
                    return "linear";
                case EasingMode.EaseIn:
                    return "ease-in";
                case EasingMode.EaseOut:
                    return "ease-out";
                case EasingMode.EaseInOut:
                    return "ease-in-out";
                default:
                    return "ease";
            }
        }
        
        public static string ToFontStyleString(this FontStyle style)
        {
            switch (style)
            {
                case FontStyle.Normal:
                    return "normal";
                case FontStyle.Italic:
                    return "italic";
                case FontStyle.Bold:
                    return "bold";
                case FontStyle.BoldAndItalic:
                    return "bold-and-italic";
                default:
                    return "normal";
            }
        }
        public static string ToOverflowClipBoxString(this OverflowClipBox box)
        {
            switch (box)
            {
                case OverflowClipBox.PaddingBox:
                    return "padding-box";
                case OverflowClipBox.ContentBox:
                    return "content-box";
                default:
                    return "padding-box";
            }
        }
        
        public static string ToTextOverflowPositionString(this TextOverflowPosition position)
        {
            switch (position)
            {
                case TextOverflowPosition.End:
                    return "end";
                case TextOverflowPosition.Start:
                    return "start";
                default:
                    return "end";
            }
        }
        
        public static string ToTextAnchorString(this TextAnchor anchor)
        {
            switch (anchor)
            {
                case TextAnchor.UpperLeft:
                    return "upper-left";
                case TextAnchor.UpperCenter:
                    return "upper-center";
                case TextAnchor.UpperRight:
                    return "upper-right";
                case TextAnchor.MiddleLeft:
                    return "middle-left";
                case TextAnchor.MiddleCenter:
                    return "middle-center";
                case TextAnchor.MiddleRight:
                    return "middle-right";
                case TextAnchor.LowerLeft:
                    return "lower-left";
                case TextAnchor.LowerCenter:
                    return "lower-center";
                case TextAnchor.LowerRight:
                    return "lower-right";
                default:
                    return "upper-left";
            }
        }
        
        public static string ToVisibilityString(this Visibility visibility)
        {
            switch (visibility)
            {
                case Visibility.Visible:
                    return "visible";
                case Visibility.Hidden:
                    return "hidden";
                default:
                    return "visible";
            }
        }
        public static string ToWhiteSpaceString(this WhiteSpace whiteSpace)
        {
            switch (whiteSpace)
            {
                case WhiteSpace.Normal:
                    return "normal";
                case WhiteSpace.NoWrap:
                    return "nowrap";
                default:
                    return "normal";
            }
        }
    }

}