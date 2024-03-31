using UnityEngine.UIElements;

namespace Writer.AssetGenerator.UIElement
{
    public static class AttributeNames
    {
        public class VisualElementAttribute
        {
           public const string name = "name";
           public const string viewDataKey = "view-data-key";
           public const string pickingMode = "picking-mode";
           public const string tooltip = "tooltip";
           public const string usageHints = "usage-hints";
           public const string tabIndex = "tabindex";
           public const string focusable = "focusable";
        }
        public class BindableElementAttribute : VisualElementAttribute
        {
            public const string bindingPath = "binding-path";
        }

        public class ScrollViewAttribute : VisualElementAttribute
        {
           public const string mode = "mode";
           public const string nestedInteractionKind = "nested-interaction-kind";
           public const string horizontalScrollerVisibility = "horizontal-scroller-visibility";
           public const string verticalScrollerVisibility = "vertical-scroller-visibility";
           public const string horizontalPageSize = "horizontal-page-size";
           public const string verticalPageSize = "vertical-page-size";
           public const string mouseWheelScrollSize = "mouse-wheel-scroll-size";
           public const string touchScrollType = "touch-scroll-type";
           public const string scrollDecelerationRate = "scroll-deceleration-rate";
           public const string elasticity = "elasticity";
        }

        public class ViewAttributeBase : BindableElementAttribute
        {
            public const string fixedItemHeight = "fixed-item-height";
            public const string virtualizationMethod = "virtualization-method";
            public const string showBorder = "show-border";
            public const string selectionType = "selection-type";
            public const string showAlternatingRowBackgrounds = "show-alternating-row-backgrounds";
            public const string reorderable = "reorderable";
            public const string horizontalScrolling = "horizontal-scrolling";
        }
        public class ListViewAttribute : ViewAttributeBase
        {
            public const string showFoldoutHeader = "show-foldout-header";
            public const string headerTitle = "header-title";
            public const string showAddRemoveFooter = "show-add-remove-footer";
            public const string reorderMode = "reorder-mode";
            public const string showBoundCollectionSize = "show-bound-collection-size";
        }
        
        public class TreeViewAttribute : ViewAttributeBase
        {
            public const string autoExpand = "auto-expand"; 
        }
        
        public class BindableTextElementAttribute : BindableElementAttribute
        {
            public const string text = "text"; 
        }
        public class BindableLabelElementAttribute : BindableElementAttribute
        {
            public const string label = "label";
        }
        public class BindableLabelWithValueElementAttribute : BindableLabelElementAttribute
        {
            public const string value = "value";
        }
        
        public class GroupBoxAttribute : BindableTextElementAttribute
        {
        }
        
        public class LabelAttribute : BindableTextElementAttribute
        {
            public const string enableRichText = "enable-rich-text";
            public const string displayTooltipWhenElided = "display-tooltip-when-elided";
        }
        
        public class ButtonAttribute : Label
        {
            
        }
        
        public class ToggleAttribute : BindableTextElementAttribute
        {
        }
        
        public class ScrollerAttribute : BindableElementAttribute
        {
            public const string lowValue = "low-value";
            public const string highValue = "high-value";
            public const string value = "value";
            public const string direction = "direction";
        }
        
        public class TextFieldAttribute : BindableLabelWithValueElementAttribute
        {
            public const string maxLength = "max-length";
            public const string password = "password";
            public const string maskCharacter = "mask-character";
            public const string readOnly = "readonly";
            public const string isDelayed = "is-delayed";
            public const string hideMobileInput = "hide-mobile-input";
            public const string keyboardType = "keyboard-type";
            public const string autoCorrection = "auto-correction";
            public const string multiline = "multiline";
        }
        
        public class FoldoutAttribute : BindableTextElementAttribute
        {
            public const string value = "value";
        }
        
        public class RangeElementAttribute : BindableElementAttribute
        {
            public const string lowValue = "low-value";
            public const string highValue = "high-value";
        }
        
        public class SliderAttribute : RangeElementAttribute
        {
            public const string label = "label";
            public const string value = "value";
            public const string pageSize = "page-size";
            public const string showInputField = "show-input-field";
            public const string direction = "direction";
            public const string inverted = "inverted";
        }
        
        public class SliderIntAttribute : Slider
        {
            
        }
        
        public class MinMaxSliderAttribute : RangeElementAttribute
        {
            public const string label = "label";
            public const string lowLimit = "low-limit";
            public const string highLimit = "high-limit";
        }
        
        public class ProgressBarAttribute : RangeElementAttribute
        {
            public const string value = "value";
            public const string title = "title";
        }
        
        public class DropdownAttribute : BindableLabelElementAttribute
        {
            public const string index = "index";
            public const string choices = "choices"; // a,b,c,d,
        }
        
        public class EnumFieldAttribute : BindableLabelWithValueElementAttribute
        {
            public const string type = "type"; // namespcae.type, Assembly-CSharp-Editor
            public const string includeObsoleteValues = "include-obsolete-values";
        }
        
        public class RadioButtonAttribute : BindableLabelWithValueElementAttribute
        {
            public const string text = "text";
        }
        
        public class RadioButtonGroupAttribute : BindableLabelWithValueElementAttribute
        {
            public const string choices = "choices"; // a,b,c,d,
        }
        
        public class NumberFieldAttribute : BindableLabelWithValueElementAttribute
        {
            public const string readOnly = "readonly";
            public const string isDelayed = "is-delayed";
        }
        public class IntegerFieldAttribute : NumberFieldAttribute
        {
        }
        
        public class FloatFieldAttribute : NumberFieldAttribute
        {
        }
        
        public class DoubleFieldAttribute : NumberFieldAttribute
        {
        }
        
        public class LongFieldAttribute : NumberFieldAttribute
        {
        }
        public class Hash128FieldAttribute : NumberFieldAttribute
        {
            
        }
        
        public class Vector2FieldAttribute : BindableLabelElementAttribute
        {
            public const string x = "x";
            public const string y = "y";
        }
        
        public class Vector3FieldAttribute : Vector2FieldAttribute
        {
            public const string z = "z";
        }
        
        public class Vector4FieldAttribute : Vector3FieldAttribute
        {
            public const string w = "w";
        }
        
        public class RectFieldAttribute : Vector2FieldAttribute
        {
            public const string width = "w";
            public const string height = "h";
        }
        
        public class BoundsFieldAttribute : BindableLabelElementAttribute
        {
            public const string centerX = "cx";
            public const string centerY = "cy";
            public const string centerZ = "cz";
            
            public const string sizeX = "ex";
            public const string sizeY = "ey";
            public const string sizeZ = "ez";
        }
        
        public class UnsignedIntegerFieldAttribute : NumberFieldAttribute
        {
        }
        
        public class UnsigendLongFieldAttribute : NumberFieldAttribute
        {
        }
        
        public class Vector2IntFieldAttribute : Vector2FieldAttribute
        {
        }
        
        public class Vector3IntFieldAttribute : Vector3FieldAttribute
        {
        }
        
        public class RectIntFieldAttribute : RectFieldAttribute
        {
        }
        
        public class BoundsIntFieldAttribute : BindableLabelElementAttribute
        {
            public const string positionX = "px";
            public const string positionY = "py";
            public const string positionZ = "pz";
            
            public const string sizeX = "sx";
            public const string sizeY = "sy";
            public const string sizeZ = "sz";
        }
        
        public class Editor
        {
            public class IMGUIContainerAttribute : VisualElementAttribute
            {
                
            }
            public class ColorFieldAttribute : BindableLabelWithValueElementAttribute
            {
                public const string showEyeDropper = "show-eye-dropper";
                public const string showAlpha = "show-alpha";
                public const string hdr = "hdr";
            }
            
            public class CurveFieldAttribute : BindableLabelElementAttribute
            {
                
            }
            
            public class GradientFieldAttribute : BindableLabelElementAttribute
            {
            }
            
            public class TagFieldAttribute : BindableLabelWithValueElementAttribute
            {
            }
            
            public class LayerFiledAttribute : BindableLabelWithValueElementAttribute
            {
            }
            
            public class MaskFieldAttribute : BindableLabelWithValueElementAttribute
            {
                public const string choices = "choices"; // a,b,c,d, bit mask
            }
            
            public class LayerMaskFiledAttribute : BindableLabelWithValueElementAttribute
            {
            }
            
            public class EnumFlagsFiledAttribute : EnumField
            {
            }

            public class ToolbarAttribute : VisualElementAttribute
            {
            }

            public class ToolbarMenuAttribute : BindableTextElementAttribute
            {
                public const string enableRichText = "enable-rich-text";
                public const string displayTooltipWhenElided = "display-tooltip-when-elided";
            }
            
            public class ToolbarButtonAttribute : ToolbarMenuAttribute
            {
            }
            public class ToolbarSpacerAttribute : VisualElementAttribute
            {
            }

            public class ToolbarToggleAttribute : BindableLabelWithValueElementAttribute
            {
                public const string text = "text";
            }
            public class ToolbarBreadcrumbsAttribute : VisualElementAttribute
            {
            }
            public class ToolbarSerachFieldAttribute : VisualElementAttribute
            {
            }
            public class ToolbarPopupSearchFieldAttribute : VisualElementAttribute
            {
            }
            public class ObjectFieldAttribute : BindableLabelElementAttribute
            {
                public const string allowSceneObjects = "allow-scene-objects";
                public const string type = "type"; // namespcae.type, Assembly-CSharp-Editor
            }
            
            public class PropertyFieldAttribute : BindableLabelElementAttribute
            {
                
            }
        }
        
        
        
    }

}