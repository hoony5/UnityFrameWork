using System;

using UIElements;
using UnityEngine.UIElements;
using Writer.Core;

namespace Writer.SourceGenerator.Format
{
    public class UIRegisterCallbackFormat : IWriterFormat
    {
        private string uiType;
        private RegisterType @operator;
        private string operatorString;
        private string elementName;
        private string methodName;

        public UIRegisterCallbackFormat(string type, string name, RegisterType @operator)
        {
            uiType = type;
            elementName = name;
            methodName = char.ToUpper(name[0]) + name[1..];
            this.@operator = @operator;
            operatorString = @operator switch
            {
                RegisterType.Add => "+=",
                RegisterType.Remove => "-=",
                RegisterType.Equals => "=",
                _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
            };
        }

        /// <summary>
        /// {0} : modelVariableName
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GetFormat()
        {
            switch (uiType)
            {
                default:
                    return string.Empty;
                case nameof(Button):
                    return $@"{{0}}.{elementName}.clicked {operatorString} On{methodName}Clicked;";
                case nameof(ScrollView):
                    return
                        $@"{{0}}.{elementName}.verticalScroller.valueChanged {operatorString} On{methodName}VerticalValueChanged;
        {{0}}.{elementName}.horizontalScroller.valueChanged {operatorString} On{methodName}HorizontalValueChanged;";
                case nameof(ListView):
                    return $@"{{0}}.{elementName}.itemsAdded {operatorString} On{methodName}ItemsAdded;
        {{0}}.{elementName}.itemsRemoved {operatorString} On{methodName}ItemsRemoved;
        {{0}}.{elementName}.itemsSourceChanged {operatorString} On{methodName}itemsSourceChanged;
        {{0}}.{elementName}.itemsChosen {operatorString} On{methodName}ItemsChosen;
        {{0}}.{elementName}.makeItem {operatorString} On{methodName}MakeItem;
        {{0}}.{elementName}.bindItem {operatorString} On{methodName}BindItem;
        {{0}}.{elementName}.destroyItem {operatorString} On{methodName}DestroyItem;
        {{0}}.{elementName}.unbindItem {operatorString} On{methodName}UnbindItem;";
                case nameof(TreeView):
                    return
                        $@"{{0}}.{elementName}.itemsSourceChanged {operatorString} On{methodName}itemsSourceChanged;
        {{0}}.{elementName}.itemsChosen {operatorString} On{methodName}ItemsChosen;
        {{0}}.{elementName}.makeItem {operatorString} On{methodName}MakeItem;
        {{0}}.{elementName}.bindItem {operatorString} On{methodName}BindItem;
        {{0}}.{elementName}.destroyItem {operatorString} On{methodName}DestroyItem;
        {{0}}.{elementName}.unbindItem {operatorString} On{methodName}UnbindItem;";
                case nameof(Scroller):
                    return $@"{{0}}.{elementName}.valueChanged {operatorString} On{methodName}ValueChanged;";
                case nameof(Toggle):
                case nameof(TextField):
                case nameof(Foldout):
                case nameof(Slider):
                case nameof(SliderInt):
                case nameof(MinMaxSlider):
                case nameof(ProgressBar):
                case nameof(DropdownField):
                case nameof(EnumField):
                case nameof(RadioButton):
                case nameof(RadioButtonGroup):
                case nameof(IntegerField):
                case nameof(FloatField):
                case nameof(LongField):
                case nameof(DoubleField):
                case nameof(Hash128Field):
                case nameof(Vector2Field):
                case nameof(Vector3Field):
                case nameof(Vector4Field):
                case nameof(Vector2IntField):
                case nameof(Vector3IntField):
                    return @operator switch
                    {
                        RegisterType.Add or RegisterType.Equals =>
                            $@"{{0}}.{elementName}.RegisterValueChangedCallback(On{methodName}ValueChanged);",
                        RegisterType.Remove =>
                            $@"{{0}}.{elementName}.UnregisterValueChangedCallback(On{methodName}ValueChanged);",
                        _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
                    };
                // editor-Only
#if UNITY_EDITOR
                case nameof(IMGUIContainer):
                case nameof(UnityEditor.UIElements.ColorField):
                case nameof(UnityEditor.UIElements.Toolbar):
                case nameof(UnityEditor.UIElements.ToolbarMenu):
                case nameof(UnityEditor.UIElements.ToolbarSpacer):
                case nameof(UnityEditor.UIElements.ToolbarBreadcrumbs):
                case nameof(UnityEditor.UIElements.ToolbarSearchField):
                    return string.Empty;
                case nameof(UnityEditor.UIElements.CurveField):
                case nameof(UnityEditor.UIElements.GradientField):
                case nameof(UnityEditor.UIElements.TagField):
                case nameof(UnityEditor.UIElements.MaskField):
                case nameof(UnityEditor.UIElements.LayerField):
                case nameof(UnityEditor.UIElements.LayerMaskField):
                case nameof(UnityEditor.UIElements.EnumFlagsField):
                case nameof(UnityEditor.UIElements.ToolbarToggle):
                case nameof(UnityEditor.UIElements.ToolbarPopupSearchField):
                case nameof(UnityEditor.UIElements.ObjectField):
                case nameof(UnityEditor.UIElements.PropertyField):
                    return @operator switch
                    {
                        RegisterType.Add or RegisterType.Equals =>
                            $@"{{0}}.{elementName}.RegisterValueChangedCallback(On{methodName}ValueChanged);",
                        RegisterType.Remove =>
                            $@"{{0}}.{elementName}.UnregisterValueChangedCallback(On{methodName}ValueChanged);",
                        _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
                    };
#endif
            }
        }
    }
}
