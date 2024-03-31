using System;
using System.Collections.Generic;
using System.Linq;
using UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static Writer.AssetGenerator.UIElement.AttributeNames;

#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace Writer.AssetGenerator.UIElement
{
    public static class UxmlLineStterEx
    {
        public static UxmlItem<T> SetUp<T>(this T visualElement) where T : VisualElement
        {
            UxmlItem<T> uxmlItem = new UxmlItem<T>(visualElement);
            return uxmlItem;
        }
        
        public static Validation<UxmlItem<T>> If<T>(this UxmlItem<T> uxmlItem, Func<bool> predicate) where T : VisualElement
        {
            Validation<UxmlItem<T>> validation = new Validation<UxmlItem<T>>(uxmlItem, predicate);
            return validation;
        }
        
        public static UxmlItem<T> SetName<T>(this UxmlItem<T> uxmlItem, string name) where T : VisualElement
        {
            uxmlItem.element.name = name;
            uxmlItem.SetAttribute(VisualElementAttribute.name, name);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetViewDataKey<T>(this UxmlItem<T> uxmlItem, string key) where T : VisualElement
        {
            uxmlItem.element.viewDataKey = key;
            uxmlItem.SetAttribute(VisualElementAttribute.viewDataKey, key);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetPickingMode<T>(this UxmlItem<T> uxmlItem, PickingMode mode) where T : VisualElement
        {
            uxmlItem.element.pickingMode = mode;
            uxmlItem.SetAttribute(VisualElementAttribute.pickingMode, mode);
            return uxmlItem;
        }
        public static UxmlItem<T> SetTooltip<T>(this UxmlItem<T> uxmlItem, string tooltip) where T : VisualElement
        {
            uxmlItem.element.tooltip = tooltip;
            uxmlItem.SetAttribute(VisualElementAttribute.tooltip, tooltip);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetUsageHints<T>(this UxmlItem<T> uxmlItem, UsageHints hints) where T : VisualElement
        {
            uxmlItem.element.usageHints = hints;
            uxmlItem.SetAttribute(VisualElementAttribute.usageHints, hints);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetTabIndex<T>(this UxmlItem<T> uxmlItem, int index) where T : VisualElement
        {
            uxmlItem.element.tabIndex = index;
            uxmlItem.SetAttribute(VisualElementAttribute.tabIndex, index);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetFocusable<T>(this UxmlItem<T> uxmlItem, bool focusable) where T : VisualElement
        {
            uxmlItem.element.focusable = focusable;
            uxmlItem.SetAttribute(VisualElementAttribute.focusable, focusable);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBindingPath<T>(this UxmlItem<T> uxmlItem, string path) where T : BindableElement 
        {
            if (uxmlItem.element is not BindableElement bindableElement) return uxmlItem;
            bindableElement.bindingPath = path;
            uxmlItem.SetAttribute(BindableElementAttribute.bindingPath, path);
            return uxmlItem;
        }
        
        // scrollView
        public static UxmlItem<T> SetMode<T>(this UxmlItem<T> uxmlItem, ScrollViewMode mode) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.mode = mode;
            uxmlItem.SetAttribute(ScrollViewAttribute.mode, mode);
            return uxmlItem;
        }
        
        public static UxmlItem<T> NestedInteractionKind<T>(this UxmlItem<T> uxmlItem, ScrollView.NestedInteractionKind kind) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.nestedInteractionKind = kind;
            uxmlItem.SetAttribute(ScrollViewAttribute.nestedInteractionKind, kind);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowHorizontalVisibility<T>(this UxmlItem<T> uxmlItem, ScrollerVisibility visibility) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.horizontalScrollerVisibility = visibility;
            uxmlItem.SetAttribute(ScrollViewAttribute.horizontalScrollerVisibility, visibility);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowVerticalVisibility<T>(this UxmlItem<T> uxmlItem, ScrollerVisibility visibility) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.verticalScrollerVisibility = visibility;
            uxmlItem.SetAttribute(ScrollViewAttribute.verticalScrollerVisibility, visibility);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetHorizontalPageSize<T>(this UxmlItem<T> uxmlItem, float pageSize) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.horizontalPageSize = pageSize;
            uxmlItem.SetAttribute(ScrollViewAttribute.horizontalPageSize, pageSize);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetVerticalPageSize<T>(this UxmlItem<T> uxmlItem, float pageSize) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.verticalPageSize = pageSize;
            uxmlItem.SetAttribute(ScrollViewAttribute.verticalPageSize, pageSize);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetMouseWheelScrollSize<T>(this UxmlItem<T> uxmlItem, float scrollSize) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.mouseWheelScrollSize = scrollSize;
            uxmlItem.SetAttribute(ScrollViewAttribute.mouseWheelScrollSize, scrollSize);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetTouchScrollType<T>(this UxmlItem<T> uxmlItem, ScrollView.TouchScrollBehavior type) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.touchScrollBehavior = type;
            uxmlItem.SetAttribute(ScrollViewAttribute.touchScrollType, type);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetScrollDecelerationRate<T>(this UxmlItem<T> uxmlItem, float rate) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.scrollDecelerationRate = rate;
            uxmlItem.SetAttribute(ScrollViewAttribute.scrollDecelerationRate, rate);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetElasticity<T>(this UxmlItem<T> uxmlItem, float elasticity) where T : ScrollView
        {
            if (uxmlItem.element is not ScrollView scrollView) return uxmlItem;
            scrollView.elasticity = elasticity;
            uxmlItem.SetAttribute(ScrollViewAttribute.elasticity, elasticity);
            return uxmlItem;
        }
        
        // ListView
        
        public static UxmlItem<T> SetFixedItemHeight<T>(this UxmlItem<T> uxmlItem, float height) where T : BaseVerticalCollectionView
        {
            if (uxmlItem.element is not BaseVerticalCollectionView view) return uxmlItem;
            view.fixedItemHeight = height;
            uxmlItem.SetAttribute(ViewAttributeBase.fixedItemHeight, height);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetVirtualizationMethod<T>(this UxmlItem<T> uxmlItem, CollectionVirtualizationMethod type) where T : BaseVerticalCollectionView
        {
            if (uxmlItem.element is not BaseVerticalCollectionView view) return uxmlItem;
            view.virtualizationMethod = type;
            uxmlItem.SetAttribute(ViewAttributeBase.virtualizationMethod, type);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowBorder<T>(this UxmlItem<T> uxmlItem, bool show) where T : BaseVerticalCollectionView
        {
            if (uxmlItem.element is not BaseVerticalCollectionView view) return uxmlItem;
            view.showBorder = show;
            uxmlItem.SetAttribute(ViewAttributeBase.showBorder, show);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetSelectionType<T>(this UxmlItem<T> uxmlItem, SelectionType type) where T : BaseVerticalCollectionView
        {
            if (uxmlItem.element is not BaseVerticalCollectionView view) return uxmlItem;
            view.selectionType = type;
            uxmlItem.SetAttribute(ViewAttributeBase.selectionType, type);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowAlternatingRowBackgrounds<T>(this UxmlItem<T> uxmlItem, AlternatingRowBackground type) where T : BaseVerticalCollectionView
        {
            if (uxmlItem.element is not BaseVerticalCollectionView view) return uxmlItem;
            view.showAlternatingRowBackgrounds = type;
            uxmlItem.SetAttribute(ViewAttributeBase.showAlternatingRowBackgrounds, type);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetReorderable<T>(this UxmlItem<T> uxmlItem, bool reorderable) where T : BaseVerticalCollectionView
        {
            if (uxmlItem.element is not BaseVerticalCollectionView view) return uxmlItem;
            view.reorderable = reorderable;
            uxmlItem.SetAttribute(ViewAttributeBase.reorderable, reorderable);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetHorizontalScrolling<T>(this UxmlItem<T> uxmlItem, bool scrolling) where T : BaseVerticalCollectionView
        {
            if (uxmlItem.element is not BaseVerticalCollectionView view) return uxmlItem;
            view.horizontalScrollingEnabled = scrolling;
            uxmlItem.SetAttribute(ViewAttributeBase.horizontalScrolling, scrolling);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowFoldoutHeader<T>(this UxmlItem<T> uxmlItem, bool show) where T : ListView
        {
            if (uxmlItem.element is not ListView listView) return uxmlItem;
            listView.showFoldoutHeader = show;
            uxmlItem.SetAttribute(ListViewAttribute.showFoldoutHeader, show);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetHeaderTitle<T>(this UxmlItem<T> uxmlItem, string title) where T : ListView
        {
            if (uxmlItem.element is not ListView listView) return uxmlItem;
            listView.headerTitle = title;
            uxmlItem.SetAttribute(ListViewAttribute.headerTitle, title);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowAddRemoveFooter<T>(this UxmlItem<T> uxmlItem, bool show) where T : ListView
        {
            if (uxmlItem.element is not ListView listView) return uxmlItem;
            listView.showAddRemoveFooter = show;
            uxmlItem.SetAttribute(ListViewAttribute.showAddRemoveFooter, show);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetReorderMode<T>(this UxmlItem<T> uxmlItem, ListViewReorderMode mode) where T : ListView
        {
            if (uxmlItem.element is not ListView listView) return uxmlItem;
            listView.reorderMode = mode;
            uxmlItem.SetAttribute(ListViewAttribute.reorderMode, mode);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowBoundCollectionSize<T> (this UxmlItem<T> uxmlItem, bool show) where T : ListView
        {
            if (uxmlItem.element is not ListView listView) return uxmlItem;
            listView.showBoundCollectionSize = show;
            uxmlItem.SetAttribute(ListViewAttribute.showBoundCollectionSize, show);
            return uxmlItem;
        }
        // treeView
        
        public static UxmlItem<T> SetAutoExpand<T>(this UxmlItem<T> uxmlItem, bool expand) where T : TreeView
        {
            if (uxmlItem.element is not TreeView view) return uxmlItem;
            view.autoExpand = expand;
            uxmlItem.SetAttribute(TreeViewAttribute.autoExpand, expand);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetText<T>(this UxmlItem<T> uxmlItem, string text) where T : TextElement
        {
            if (uxmlItem.element is not TextElement element) return uxmlItem;
            element.text = text;
            uxmlItem.SetAttribute(BindableTextElementAttribute.text, text);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetLabel<T>(this UxmlItem<T> uxmlItem, string label) where T : VisualElement
        {
            bool labelIsSet = false;
            if (uxmlItem.element is Label ownedLabel)
            {
                ownedLabel.text = label;
                labelIsSet = true;
            }
            
            Label childIsLabel = uxmlItem.element.Q<Label>("Label");
            if (childIsLabel != null)
            {
                childIsLabel.text = label;
                labelIsSet = true;
            }
            
            if (!labelIsSet)
            {
                return uxmlItem;
            }

            uxmlItem.SetAttribute(BindableLabelElementAttribute.label, label);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetValue<T>(this UxmlItem<T> uxmlItem, object value) where T : VisualElement
        {
            bool valueIsSet = false;
            if (uxmlItem.element is Foldout foldout)
            {
                foldout.value = (bool)value;
                valueIsSet = true;
            }
            Type elementType = uxmlItem.element.GetType();
            if (elementType.IsSubclassOf(typeof(BaseField<>)))
            {
                elementType.GetProperties().First(p => p.Name == "value").SetValue(uxmlItem.element, value);
                valueIsSet = true;
            }
            
            if(!valueIsSet) return uxmlItem; 
            
            uxmlItem.SetAttribute(BindableLabelWithValueElementAttribute.value, value);
            return uxmlItem;
        }
        
        // label 
        public static UxmlItem<T> SetEnableRichText<T>(this UxmlItem<T> uxmlItem, bool enable) where T : TextElement
        {
            if (uxmlItem.element is not TextElement element) return uxmlItem;
            element.enableRichText = enable;
            uxmlItem.SetAttribute(LabelAttribute.enableRichText, enable);
            return uxmlItem;
        } 
        
        public static UxmlItem<T> SetDisplayTooltipWhenElided<T>(this UxmlItem<T> uxmlItem, bool enable) where T : TextElement
        {
            if (uxmlItem.element is not TextElement element) return uxmlItem;
            element.displayTooltipWhenElided = enable;
            uxmlItem.SetAttribute(LabelAttribute.displayTooltipWhenElided, enable);
            return uxmlItem;
        }
        
        // scroller
        public static UxmlItem<T> SetLowValue<T>(this UxmlItem<T> uxmlItem, float lowValue) where T : Scroller
        {
            if (uxmlItem.element is not Scroller scroller) return uxmlItem;
            scroller.lowValue = lowValue;
            uxmlItem.SetAttribute(ScrollerAttribute.lowValue, lowValue);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetHighValue<T>(this UxmlItem<T> uxmlItem, float highValue) where T : Scroller
        {
            if (uxmlItem.element is not Scroller scroller) return uxmlItem;
            scroller.highValue = highValue;
            uxmlItem.SetAttribute(ScrollerAttribute.highValue, highValue);
            return uxmlItem;
        }
        public static UxmlItem<T> SetScrollerValue<T>(this UxmlItem<T> uxmlItem, float value) where T : Scroller
        {
            if (uxmlItem.element is not Scroller scroller) return uxmlItem;
            scroller.value = value;
            uxmlItem.SetAttribute(ScrollerAttribute.value, value);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetDirection<T>(this UxmlItem<T> uxmlItem, SliderDirection direction) where T : Scroller
        {
            if (uxmlItem.element is not Scroller scroller) return uxmlItem;
            scroller.direction = direction;
            uxmlItem.SetAttribute(ScrollerAttribute.direction, direction);
            return uxmlItem;
        } 
        
        // textField
        public static UxmlItem<T> SetMaxLength<T>(this UxmlItem<T> uxmlItem, int length) where T : TextField
        {
            if (uxmlItem.element is not TextField textField) return uxmlItem;
            textField.maxLength = length;
            uxmlItem.SetAttribute(TextFieldAttribute.maxLength, length);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetMultiline<T>(this UxmlItem<T> uxmlItem, bool multiline) where T : TextField
        {
            if (uxmlItem.element is not TextField textField) return uxmlItem;
            textField.multiline = multiline;
            uxmlItem.SetAttribute(TextFieldAttribute.multiline, multiline);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetIsPasswordField<T>(this UxmlItem<T> uxmlItem, bool password) where T : TextField
        {
            if (uxmlItem.element is not TextField textField) return uxmlItem;
            textField.isPasswordField = password;
            uxmlItem.SetAttribute(TextFieldAttribute.password, password);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetMaskCharacter<T>(this UxmlItem<T> uxmlItem, char mask) where T : TextField
        {
            if (uxmlItem.element is not TextField textField) return uxmlItem;
            textField.maskChar = mask;
            uxmlItem.SetAttribute(TextFieldAttribute.maskCharacter, mask);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetReadOnly<T>(this UxmlItem<T> uxmlItem, bool readOnly) where T : VisualElement
        {
            bool readOnlyIsSet = false;
            if (uxmlItem.element is TextField textField)
            {
                textField.isReadOnly = readOnly;
                readOnlyIsSet = true;
            }

            Type elementType = uxmlItem.element.GetType();
            if (elementType.IsSubclassOf(typeof(TextInputBaseField<>)))
            {
                elementType.GetProperties().First(p => p.Name == "isReadOnly").SetValue(uxmlItem.element, readOnly);
                readOnlyIsSet = true;
            }
            
            if(!readOnlyIsSet) return uxmlItem;
            
            uxmlItem.SetAttribute(TextFieldAttribute.readOnly, readOnly);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetDelay<T>(this UxmlItem<T> uxmlItem, bool delay) where T : VisualElement
        {
            Type elementType = uxmlItem.element.GetType();
            if (elementType.IsSubclassOf(typeof(TextInputBaseField<>)))
            {
                elementType.GetProperties().First(p => p.Name == "isDelayed").SetValue(uxmlItem.element, delay);   
            }
            else
                return uxmlItem;
            
            uxmlItem.SetAttribute(TextFieldAttribute.isDelayed, delay);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetHideMobileInput<T>(this UxmlItem<T> uxmlItem, bool hide) where T : TextField
        {
            if (uxmlItem.element is not TextField textField) return uxmlItem;
            textField.hideMobileInput = hide;
            uxmlItem.SetAttribute(TextFieldAttribute.hideMobileInput, hide);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetKeyboardType<T>(this UxmlItem<T> uxmlItem, TouchScreenKeyboardType type) where T : TextField
        {
            if (uxmlItem.element is not TextField textField) return uxmlItem;
            textField.keyboardType = type;
            uxmlItem.SetAttribute(TextFieldAttribute.keyboardType, type);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetAutoCorrection<T>(this UxmlItem<T> uxmlItem, bool correction) where T : TextField
        {
            if (uxmlItem.element is not TextField textField) return uxmlItem;
            textField.autoCorrection = correction;
            uxmlItem.SetAttribute(TextFieldAttribute.autoCorrection, correction);
            return uxmlItem;
        }
        
        // foldout
        
        public static UxmlItem<T> SetFoldout<T>(this UxmlItem<T> uxmlItem, bool value) where T : Foldout
        {
            if (uxmlItem.element is not Foldout foldout) return uxmlItem;
            foldout.value = value;
            uxmlItem.SetAttribute(FoldoutAttribute.value, value);
            return uxmlItem;
        }
        
        // slider
        
        public static UxmlItem<T> SetSliderPageSize<T>(this UxmlItem<T> uxmlItem, float pageSize) where T : Slider
        {
            if (uxmlItem.element is not Slider slider) return uxmlItem;
            slider.pageSize = pageSize;
            uxmlItem.SetAttribute(SliderAttribute.pageSize, pageSize);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetSliderLowValue<T>(this UxmlItem<T> uxmlItem, float lowValue) where T : Slider
        {
            if (uxmlItem.element is not Slider slider) return uxmlItem;
            slider.lowValue = lowValue;
            uxmlItem.SetAttribute(RangeElementAttribute.lowValue, lowValue);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetSliderHighValue<T>(this UxmlItem<T> uxmlItem, float highValue) where T : Slider
        {
            if (uxmlItem.element is not Slider slider) return uxmlItem;
            slider.highValue = highValue;
            uxmlItem.SetAttribute(RangeElementAttribute.highValue, highValue);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetSliderDirection<T>(this UxmlItem<T> uxmlItem, SliderDirection direction) where T : Slider
        {
            if (uxmlItem.element is not Slider slider) return uxmlItem;
            slider.direction = direction;
            uxmlItem.SetAttribute(SliderAttribute.direction, direction);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetSliderShowInputField<T>(this UxmlItem<T> uxmlItem, bool show) where T : Slider
        {
            if (uxmlItem.element is not Slider slider) return uxmlItem;
            slider.showInputField = show;
            uxmlItem.SetAttribute(SliderAttribute.showInputField, show);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetInverted<T>(this UxmlItem<T> uxmlItem, bool inverted) where T : Slider
        {
            if (uxmlItem.element is not Slider slider) return uxmlItem;
            slider.inverted = inverted;
            uxmlItem.SetAttribute(SliderAttribute.inverted, inverted);
            return uxmlItem;
        }
        
        // MinMaxSlider
        
        public static UxmlItem<T> SetLowLimt<T>(this UxmlItem<T> uxmlItem, float lowLimit) where T : MinMaxSlider
        {
            if (uxmlItem.element is not MinMaxSlider slider) return uxmlItem;
            slider.lowLimit = lowLimit;
            uxmlItem.SetAttribute(MinMaxSliderAttribute.lowLimit, lowLimit);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetHighLimit<T>(this UxmlItem<T> uxmlItem, float highLimit) where T : MinMaxSlider
        {
            if (uxmlItem.element is not MinMaxSlider slider) return uxmlItem;
            slider.highLimit = highLimit;
            uxmlItem.SetAttribute(MinMaxSliderAttribute.highLimit, highLimit);
            return uxmlItem;
        }
        
        // progressbar
        
        public static UxmlItem<T> SetTitle<T> (this UxmlItem<T> uxmlItem, string title) where T : ProgressBar
        {
            if (uxmlItem.element is not ProgressBar progressBar) return uxmlItem;
            progressBar.title = title;
            uxmlItem.SetAttribute(ProgressBarAttribute.title, title);
            return uxmlItem;
        }
        
        // dropdown
        
        public static UxmlItem<T> SetIndex<T>(this UxmlItem<T> uxmlItem, int index) where T : DropdownField
        {
            if (uxmlItem.element is not DropdownField dropdown) return uxmlItem;
            dropdown.index = index;
            uxmlItem.SetAttribute(DropdownAttribute.index, index);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetChoices<T>(this UxmlItem<T> uxmlItem, List<string> choices) where T : DropdownField
        {
            if (uxmlItem.element is not DropdownField dropdown) return uxmlItem;
            dropdown.choices = choices;
            uxmlItem.SetAttribute(DropdownAttribute.choices, choices);
            return uxmlItem;
        }
        
        // EnumField
        
        public static UxmlItem<T> SetEnumType<T>(this UxmlItem<T> uxmlItem, Type type, Enum @enum) where T : EnumField
        {
            if (uxmlItem.element is not EnumField enumField) return uxmlItem;
            enumField.value = @enum;
            uxmlItem.SetAttribute(EnumFieldAttribute.type, type);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetIncludeobsoleteValues<T>(this UxmlItem<T> uxmlItem, bool include) where T : EnumField
        {
            uxmlItem.SetAttribute(EnumFieldAttribute.includeObsoleteValues, include);
            return uxmlItem;
        }
        
        // radio Button
        public static UxmlItem<T> SetRadioButtonText<T>(this UxmlItem<T> uxmlItem, string text) where T : RadioButton
        {
            if (uxmlItem.element is not RadioButton radioButton) return uxmlItem;
            radioButton.text = text;
            uxmlItem.SetAttribute(RadioButtonAttribute.text, text);
            return uxmlItem;
        }
        
        // radioButton Group
        public static UxmlItem<T> SetRadioButtonGroupChoices<T>(this UxmlItem<T> uxmlItem, List<string> choices) where T : RadioButtonGroup
        {
            if (uxmlItem.element is not RadioButtonGroup radioButtonGroup) return uxmlItem;
            radioButtonGroup.choices = choices;
            uxmlItem.SetAttribute(RadioButtonGroupAttribute.choices, choices);
            return uxmlItem;
        }
        
        // Vector Fields
        
        public static UxmlItem<T> SetVector2Value<T>(this UxmlItem<T> uxmlItem, Vector2 value) where T : Vector2Field
        {
            if (uxmlItem.element is not Vector2Field vector2Field) return uxmlItem;
            vector2Field.value = value;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, value.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, value.y);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetVector3Value<T>(this UxmlItem<T> uxmlItem, Vector3 value) where T : Vector3Field
        {
            if (uxmlItem.element is not Vector3Field vector3Field) return uxmlItem;
            vector3Field.value = value;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, value.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, value.y);
            uxmlItem.SetAttribute(Vector3FieldAttribute.z, value.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetVector4Value<T>(this UxmlItem<T> uxmlItem, Vector4 value) where T : Vector4Field
        {
            if (uxmlItem.element is not Vector4Field vector4Field) return uxmlItem;
            vector4Field.value = value;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, value.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, value.y);
            uxmlItem.SetAttribute(Vector3FieldAttribute.z, value.z);
            uxmlItem.SetAttribute(Vector4FieldAttribute.w, value.w);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetVector2IntValue<T>(this UxmlItem<T> uxmlItem, Vector2Int value) where T : Vector2IntField
        {
            if (uxmlItem.element is not Vector2IntField vector2Field) return uxmlItem;
            vector2Field.value = value;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, value.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, value.y);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetVector3IntValue<T>(this UxmlItem<T> uxmlItem, Vector3Int value) where T : Vector3IntField
        {
            if (uxmlItem.element is not Vector3IntField vector3Field) return uxmlItem;
            vector3Field.value = value;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, value.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, value.y);
            uxmlItem.SetAttribute(Vector3FieldAttribute.z, value.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsPosition<T>(this UxmlItem<T> uxmlItem, Vector3 value) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            boundsField.value = new Bounds(value, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerX, value.x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerY, value.y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerZ, value.z);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsPositionX<T>(this UxmlItem<T> uxmlItem, float x) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            Vector3 center = boundsField.value.center;
            center.x = x;
            boundsField.value = new Bounds(center, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerX, x);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsPositionY<T>(this UxmlItem<T> uxmlItem, float y) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            Vector3 center = boundsField.value.center;
            center.y = y;
            boundsField.value = new Bounds(center, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerY, y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsPositionZ<T>(this UxmlItem<T> uxmlItem, float z) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            Vector3 center = boundsField.value.center;
            center.z = z;
            boundsField.value = new Bounds(center, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerZ, z);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsPosition<T>(this UxmlItem<T> uxmlItem, float x, float y, float z) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            boundsField.value = new Bounds(new Vector3(x,y,z), boundsField.value.size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerX, x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerY, y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerZ, z);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsSize<T>(this UxmlItem<T> uxmlItem, Vector3 value) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            boundsField.value = new Bounds(boundsField.value.center, value);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeX, value.x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeY, value.y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeZ, value.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsSizeX<T>(this UxmlItem<T> uxmlItem, float x) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            Vector3 size = boundsField.value.size;
            size.x = x;
            boundsField.value = new Bounds(boundsField.value.center, size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeX, x);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsSizeY<T>(this UxmlItem<T> uxmlItem, float y) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            Vector3 size = boundsField.value.size;
            size.y = y;
            boundsField.value = new Bounds(boundsField.value.center, size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeY, y);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsSizeZ<T>(this UxmlItem<T> uxmlItem, float z) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            Vector3 size = boundsField.value.size;
            size.z = z;
            boundsField.value = new Bounds(boundsField.value.center, size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeZ, z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsSize<T>(this UxmlItem<T> uxmlItem, float x, float y, float z) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            boundsField.value = new Bounds(boundsField.value.center, new Vector3(x,y,z));
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeX, x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeY, y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeZ, z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBounds<T>(this UxmlItem<T> uxmlItem, Vector3 center, Vector3 size) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            boundsField.value = new Bounds(center, size);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerX, center.x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerY, center.y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerZ, center.z);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeX, size.x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeY, size.y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeZ, size.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBounds<T>(this UxmlItem<T> uxmlItem, Bounds bounds) where T : BoundsField
        {
            if (uxmlItem.element is not BoundsField boundsField) return uxmlItem;
            boundsField.value = bounds;
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerX, bounds.center.x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerY, bounds.center.y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.centerZ, bounds.center.z);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeX, bounds.size.x);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeY, bounds.size.y);
            uxmlItem.SetAttribute(BoundsFieldAttribute.sizeZ, bounds.size.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetRect<T>(this UxmlItem<T> uxmlItem, Rect rect) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            rectField.value = rect;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, rect.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, rect.y);
            uxmlItem.SetAttribute(RectFieldAttribute.width, rect.width);
            uxmlItem.SetAttribute(RectFieldAttribute.height, rect.height);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRect<T>(this UxmlItem<T> uxmlItem, float x, float y, float width, float height) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            rectField.value = new Rect(x, y, width, height);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, y);
            uxmlItem.SetAttribute(RectFieldAttribute.width, width);
            uxmlItem.SetAttribute(RectFieldAttribute.height, height);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRect<T>(this UxmlItem<T> uxmlItem, Vector2 position, Vector2 size) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            rectField.value = new Rect(position, size);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, position.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, position.y);
            uxmlItem.SetAttribute(RectFieldAttribute.width, size.x);
            uxmlItem.SetAttribute(RectFieldAttribute.height, size.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectPostion<T>(this UxmlItem<T> uxmlItem, float x, float y) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            rectField.value = new Rect(new Vector2(x, y), rectField.value.size);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectSize<T>(this UxmlItem<T> uxmlItem, float x, float y) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            rectField.value = new Rect(rectField.value.position, new Vector2(x, y));
            uxmlItem.SetAttribute(RectFieldAttribute.width, x);
            uxmlItem.SetAttribute(RectFieldAttribute.height, y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectPostion<T>(this UxmlItem<T> uxmlItem, Vector2 position) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            rectField.value = new Rect(position, rectField.value.size);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, position.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, position.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectSize<T>(this UxmlItem<T> uxmlItem, Vector2 size) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            rectField.value = new Rect(rectField.value.position, size);
            uxmlItem.SetAttribute(RectFieldAttribute.width, size.x);
            uxmlItem.SetAttribute(RectFieldAttribute.height, size.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectPositionX<T>(this UxmlItem<T> uxmlItem, float x) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            Rect rect = rectField.value;
            rect.x = x;
            rectField.value = rect;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, rect.x);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectPositionY<T>(this UxmlItem<T> uxmlItem, float y) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            Rect rect = rectField.value;
            rect.y = y;
            rectField.value = rect;
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, rect.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectSizeX<T>(this UxmlItem<T> uxmlItem, float x) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            Rect rect = rectField.value;
            rect.width = x;
            rectField.value = rect;
            uxmlItem.SetAttribute(RectFieldAttribute.width, rect.width);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectSizeY<T>(this UxmlItem<T> uxmlItem, float y) where T : RectField
        {
            if (uxmlItem.element is not RectField rectField) return uxmlItem;
            Rect rect = rectField.value;
            rect.height = y;
            rectField.value = rect;
            uxmlItem.SetAttribute(RectFieldAttribute.height, rect.height);
            return uxmlItem;
        }
        
        
        
        public static UxmlItem<T> SetBoundsIntPosition<T>(this UxmlItem<T> uxmlItem, Vector3Int value) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            boundsField.value = new BoundsInt(value, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionX, value.x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionY, value.y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionZ, value.z);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsIntPositionX<T>(this UxmlItem<T> uxmlItem, int x) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            Vector3Int center = boundsField.value.position;
            center.x = x;
            boundsField.value = new BoundsInt(center, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionX, x);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsIntPositionY<T>(this UxmlItem<T> uxmlItem, int y) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            Vector3Int center = boundsField.value.position;
            center.y = y;
            boundsField.value = new BoundsInt(center, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionY, y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsIntPositionZ<T>(this UxmlItem<T> uxmlItem, int z) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            Vector3Int center = boundsField.value.position;
            center.z = z;
            boundsField.value = new BoundsInt(center, boundsField.value.size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionZ, z);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsIntPosition<T>(this UxmlItem<T> uxmlItem, int x, int y, int z) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            boundsField.value = new BoundsInt(new Vector3Int(x,y,z), boundsField.value.size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionX, x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionY, y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionZ, z);
            return uxmlItem;
        }
        public static UxmlItem<T> SetBoundsIntSize<T>(this UxmlItem<T> uxmlItem, Vector3Int value) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            boundsField.value = new BoundsInt(boundsField.value.position, value);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeX, value.x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeY, value.y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeZ, value.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsIntSizeX<T>(this UxmlItem<T> uxmlItem, int x) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            Vector3Int size = boundsField.value.size;
            size.x = x;
            boundsField.value = new BoundsInt(boundsField.value.position, size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeX, x);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsIntSizeY<T>(this UxmlItem<T> uxmlItem, int y) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            Vector3Int size = boundsField.value.size;
            size.y = y;
            boundsField.value = new BoundsInt(boundsField.value.position, size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeY, y);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsIntSizeZ<T>(this UxmlItem<T> uxmlItem, int z) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            Vector3Int size = boundsField.value.size;
            size.z = z;
            boundsField.value = new BoundsInt(boundsField.value.position, size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeZ, z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsIntSize<T>(this UxmlItem<T> uxmlItem, int x, int y, int z) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            boundsField.value = new BoundsInt(boundsField.value.position, new Vector3Int(x,y,z));
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeX, x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeY, y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeZ, z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsInt<T>(this UxmlItem<T> uxmlItem, Vector3Int position, Vector3Int size) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            boundsField.value = new BoundsInt(position, size);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionX, position.x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionY, position.y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionZ, position.z);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeX, size.x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeY, size.y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeZ, size.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetBoundsInt<T>(this UxmlItem<T> uxmlItem, BoundsInt bounds) where T : BoundsIntField
        {
            if (uxmlItem.element is not BoundsIntField boundsField) return uxmlItem;
            boundsField.value = bounds;
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionX, bounds.center.x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionY, bounds.center.y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.positionZ, bounds.center.z);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeX, bounds.size.x);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeY, bounds.size.y);
            uxmlItem.SetAttribute(BoundsIntFieldAttribute.sizeZ, bounds.size.z);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetRectInt<T>(this UxmlItem<T> uxmlItem, RectInt rect) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            rectField.value = rect;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, rect.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, rect.y);
            uxmlItem.SetAttribute(RectFieldAttribute.width, rect.width);
            uxmlItem.SetAttribute(RectFieldAttribute.height, rect.height);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectInt<T>(this UxmlItem<T> uxmlItem, int x, int y, int width, int height) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            rectField.value = new RectInt(x, y, width, height);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, y);
            uxmlItem.SetAttribute(RectFieldAttribute.width, width);
            uxmlItem.SetAttribute(RectFieldAttribute.height, height);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectInt<T>(this UxmlItem<T> uxmlItem, Vector2Int position, Vector2Int size) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            rectField.value = new RectInt(position, size);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, position.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, position.y);
            uxmlItem.SetAttribute(RectFieldAttribute.width, size.x);
            uxmlItem.SetAttribute(RectFieldAttribute.height, size.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntPostion<T>(this UxmlItem<T> uxmlItem, int x, int y) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            rectField.value = new RectInt(new Vector2Int(x, y), rectField.value.size);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntSize<T>(this UxmlItem<T> uxmlItem, int x, int y) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            rectField.value = new RectInt(rectField.value.position, new Vector2Int(x, y));
            uxmlItem.SetAttribute(RectFieldAttribute.width, x);
            uxmlItem.SetAttribute(RectFieldAttribute.height, y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntPostion<T>(this UxmlItem<T> uxmlItem, Vector2Int position) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            rectField.value = new RectInt(position, rectField.value.size);
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, position.x);
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, position.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntSize<T>(this UxmlItem<T> uxmlItem, Vector2Int size) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            rectField.value = new RectInt(rectField.value.position, size);
            uxmlItem.SetAttribute(RectFieldAttribute.width, size.x);
            uxmlItem.SetAttribute(RectFieldAttribute.height, size.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntPositionX<T>(this UxmlItem<T> uxmlItem, int x) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            RectInt rect = rectField.value;
            rect.x = x;
            rectField.value = rect;
            uxmlItem.SetAttribute(Vector2FieldAttribute.x, rect.x);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntPositionY<T>(this UxmlItem<T> uxmlItem, int y) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            RectInt rect = rectField.value;
            rect.y = y;
            rectField.value = rect;
            uxmlItem.SetAttribute(Vector2FieldAttribute.y, rect.y);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntSizeX<T>(this UxmlItem<T> uxmlItem, int x) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            RectInt rect = rectField.value;
            rect.width = x;
            rectField.value = rect;
            uxmlItem.SetAttribute(RectFieldAttribute.width, rect.width);
            return uxmlItem;
        }
        public static UxmlItem<T> SetRectIntSizeY<T>(this UxmlItem<T> uxmlItem, int y) where T : RectIntField
        {
            if (uxmlItem.element is not RectIntField rectField) return uxmlItem;
            RectInt rect = rectField.value;
            rect.height = y;
            rectField.value = rect;
            uxmlItem.SetAttribute(RectFieldAttribute.height, rect.height);
            return uxmlItem;
        }

#if UNITY_EDITOR
        public static UxmlItem<T> SetShowEyeDropper<T>(this UxmlItem<T> uxmlItem, bool show) where T : ColorField
        {
            if (uxmlItem.element is not ColorField colorField) return uxmlItem;
            colorField.showEyeDropper = show;
            uxmlItem.SetAttribute(Editor.ColorFieldAttribute.showEyeDropper, show);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetShowAlpha<T>(this UxmlItem<T> uxmlItem, bool show) where T : ColorField
        {
            if (uxmlItem.element is not ColorField colorField) return uxmlItem;
            colorField.showAlpha = show;
            uxmlItem.SetAttribute(Editor.ColorFieldAttribute.showAlpha, show);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetHDR<T>(this UxmlItem<T> uxmlItem, bool hdr) where T : ColorField
        {
            if (uxmlItem.element is not ColorField colorField) return uxmlItem;
            colorField.hdr = hdr;
            uxmlItem.SetAttribute(Editor.ColorFieldAttribute.hdr, hdr);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetCurve<T>(this UxmlItem<T> uxmlItem, AnimationCurve curve) where T : CurveField
        {
            if (uxmlItem.element is not CurveField curveField) return uxmlItem;
            curveField.value = curve;
            
            return uxmlItem;
        }
            
        public static UxmlItem<T> SetTag<T>(this UxmlItem<T> uxmlItem, string tag) where T : TagField
        {
            if (uxmlItem.element is not TagField tagField) return uxmlItem;
            tagField.value = tag;
            uxmlItem.SetAttribute(BindableLabelWithValueElementAttribute.value, tag);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetLayer<T>(this UxmlItem<T> uxmlItem, int layer) where T : LayerField
        {
            if (uxmlItem.element is not LayerField layerField) return uxmlItem;
            layerField.value = layer;
            uxmlItem.SetAttribute(BindableLabelWithValueElementAttribute.value, layer);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetMask<T>(this UxmlItem<T> uxmlItem, int mask) where T : MaskField
        {
            if (uxmlItem.element is not MaskField maskField) return uxmlItem;
            maskField.value = mask;
            uxmlItem.SetAttribute(BindableLabelWithValueElementAttribute.value, mask);
            return uxmlItem;
        }
        public static UxmlItem<T> SetMaskChocies<T>(this UxmlItem<T> uxmlItem, List<string> choices) where T : MaskField
        {
            if (uxmlItem.element is not MaskField maskField) return uxmlItem;
            maskField.choices = choices;
            uxmlItem.SetAttribute(Editor.MaskFieldAttribute.choices, choices);
            return uxmlItem;
        }
        public static UxmlItem<T> SetLayerMask<T>(this UxmlItem<T> uxmlItem, LayerMask mask) where T : LayerMaskField
        {
            if (uxmlItem.element is not LayerMaskField maskField) return uxmlItem;
            maskField.value = mask;
            uxmlItem.SetAttribute(BindableLabelWithValueElementAttribute.value, mask);
            return uxmlItem;
        }
        public static UxmlItem<T> SetEnumFlags<T>(this UxmlItem<T> uxmlItem, Enum @enum) where T : EnumFlagsField
        {
            if (uxmlItem.element is not EnumFlagsField enumFlagsField) return uxmlItem;
            enumFlagsField.value = @enum;
            uxmlItem.SetAttribute(BindableLabelWithValueElementAttribute.value, @enum);
            return uxmlItem;
        }
        public static UxmlItem<T> SetToolbarMenuEnableRichText<T>(this UxmlItem<T> uxmlItem, bool enable) where T : ToolbarMenu
        {
            if (uxmlItem.element is not ToolbarMenu toolbarMenu) return uxmlItem;
            toolbarMenu.enableRichText = enable;
            uxmlItem.SetAttribute(Editor.ToolbarMenuAttribute.enableRichText, enable);
            return uxmlItem;
        }
        public static UxmlItem<T> SetToolbarMenuDisplayTooltipWhenElided<T>(this UxmlItem<T> uxmlItem, bool display) where T : ToolbarMenu
        {
            if (uxmlItem.element is not ToolbarMenu toolbarMenu) return uxmlItem;
            toolbarMenu.displayTooltipWhenElided = display;
            uxmlItem.SetAttribute(Editor.ToolbarMenuAttribute.displayTooltipWhenElided, display);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetToolbarToggleText<T>(this UxmlItem<T> uxmlItem, string text) where T : ToolbarToggle
        {
            if (uxmlItem.element is not ToolbarToggle toolbarToggle) return uxmlItem;
            toolbarToggle.text = text;
            uxmlItem.SetAttribute(Editor.ToolbarToggleAttribute.text, text);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetobjectFieldAllowSceneObjects<T>(this UxmlItem<T> uxmlItem, bool allow) where T : ObjectField
        {
            if (uxmlItem.element is not ObjectField objectField) return uxmlItem;
            objectField.allowSceneObjects = allow;
            uxmlItem.SetAttribute(Editor.ObjectFieldAttribute.allowSceneObjects, allow);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetObjectFieldType<T>(this UxmlItem<T> uxmlItem, string type) where T : ObjectField
        {
            if (uxmlItem.element is not ObjectField objectField) return uxmlItem;
            objectField.objectType = Type.GetType(type);
            uxmlItem.SetAttribute(Editor.ObjectFieldAttribute.type, type);
            return uxmlItem;
        }
        
        public static UxmlItem<T> SetObjectFieldType<T>(this UxmlItem<T> uxmlItem, Type type) where T : ObjectField
        {
            if (uxmlItem.element is not ObjectField objectField) return uxmlItem;
            objectField.objectType = type;
            uxmlItem.SetAttribute(Editor.ObjectFieldAttribute.type, type);
            return uxmlItem;
        }
#endif
    }

}