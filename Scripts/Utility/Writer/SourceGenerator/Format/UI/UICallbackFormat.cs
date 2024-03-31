using UnityEngine.UIElements;
using Writer.Core;

namespace Writer.SourceGenerator.Format
{

    public class UICallbackFormat : IWriterFormat
    {
        private readonly string implementLine = "{implementLine}";
        
        private string uiType;
        private string methodName;
        
        public UICallbackFormat(string type, string name)
        {
            uiType = type;
            methodName = char.ToUpper(name[0]) + name[1..];
        }

        public string GetFormat()
        {
            switch (uiType)
            {
                default:
                    return string.Empty;
                case nameof(Button):
                    return $@"private void On{methodName}Clicked() 
    {{
        {implementLine}    
    }}";
                case nameof(ScrollView):
                    return $@"private void On{methodName}VerticalValueChanged(float changedValue)
    {{
        {implementLine}    
    }}
                          
    private void On{methodName}HorizontalValueChanged(float changedValue)
    {{
        {implementLine}    
    }}";
                case nameof(ListView):
                    return $@"private void On{methodName}ItemsAdded(IEnumerable<int> obj)
    {{
        {implementLine}    
    }}

    private void On{methodName}ItemsRemoved(IEnumerable<int> obj)
    {{
        {implementLine}    
    }}

    private void On{methodName}itemsSourceChanged()
    {{
        {implementLine}    
    }}
                          
    private void On{methodName}ItemsChosen(IEnumerable<object> obj)
    {{
        {implementLine}    
    }}    
                          
    private VisualElement On{methodName}MakeItem()
    {{
        {implementLine}    
    }}    
                          
    private void On{methodName}BindItem(VisualElement visualElement, int index)
    {{
        {implementLine}    
    }}
                          
    private void On{methodName}DestroyItem(VisualElement visualElement)
    {{
        {implementLine}    
    }}
                          
    private void On{methodName}UnbindItem(VisualElement visualElement, int index)
    {{
        {implementLine}    
    }}";
                case nameof(TreeView):
                    return $@"private void On{methodName}itemsSourceChanged()
    {{
        {implementLine}    
    }}
                          
    private void On{methodName}ItemsChosen(IEnumerable<object> obj)
    {{
        {implementLine}    
    }}

    private VisualElement On{methodName}MakeItem()
    {{
        {implementLine}    
    }}

    private void On{methodName}BindItem(VisualElement visualElement, int index)
    {{
        {implementLine}    
    }}

    private void On{methodName}DestroyItem(VisualElement visualElement)
    {{
        {implementLine}    
    }}

    private void On{methodName}UnbindItem(VisualElement visualElement, int index)
    {{
        {implementLine}    
    }}";
                case nameof(ProgressBar):
                case nameof(Slider):
                case nameof(Scroller):
                case nameof(FloatField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<float> evt)
    {{
        {implementLine}    
    }}";
                case nameof(RadioButton):
                case nameof(Toggle):
                case nameof(Foldout):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<bool> evt)
    {{
        {implementLine}    
    }}";
                case nameof(TextField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<string> evt)
    {{
        {implementLine}    
    }}";
                case nameof(IntegerField):
                case nameof(DropdownField):
                case nameof(SliderInt):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<int> evt)
    {{
        {implementLine}    
    }}";
                case nameof(Vector2Field):
                case nameof(MinMaxSlider):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Vector2> evt)
    {{
        {implementLine}    
    }}";
                case nameof(EnumField):
                case nameof(RadioButtonGroup):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Enum> evt)
    {{
        {implementLine}    
    }}";
                case nameof(LongField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<long> evt)
    {{
        {implementLine}    
    }}";
                case nameof(DoubleField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<double> evt)
    {{
        {implementLine}    
    }}";
                case nameof(Hash128Field):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Hash128> evt)
    {{
        {implementLine}    
    }}";
                case nameof(Vector3Field):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Vector3> evt)
    {{
        {implementLine}    
    }}";
                case nameof(Vector4Field):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Vector4> evt)
    {{
        {implementLine}    
    }}";
                case nameof(Vector2IntField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Vector2Int> evt)
    {{
        {implementLine}    
    }}";
                case nameof(Vector3IntField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Vector3Int> evt)
    {{
        {implementLine}    
    }}";
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
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<AnimationCurve> evt)
    {{
        {implementLine}    
    }}";
                case nameof(UnityEditor.UIElements.GradientField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<Gradient> evt)
    {{
        {implementLine}    
    }}";
                case nameof(UnityEditor.UIElements.TagField):
                case nameof(UnityEditor.UIElements.ToolbarPopupSearchField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<string> evt)
    {{
        {implementLine}    
    }}";
                case nameof(UnityEditor.UIElements.MaskField):
                case nameof(UnityEditor.UIElements.LayerField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<int> evt)
    {{
        {implementLine}    
    }}";
                case nameof(UnityEditor.UIElements.LayerMaskField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<LayerMask> evt)
    {{
        {implementLine}    
    }}";
                case nameof(UnityEditor.UIElements.EnumFlagsField):
                    return $@"   private void On{methodName}ValueChanged(ChangeEvent<Enum> evt)
    {{
        {implementLine}    
    }}";
                case nameof(UnityEditor.UIElements.ToolbarToggle):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<bool> evt)
    {{
        {implementLine}    
    }}";
                case nameof(UnityEditor.UIElements.ObjectField):
                case nameof(UnityEditor.UIElements.PropertyField):
                    return $@"private void On{methodName}ValueChanged(ChangeEvent<UnityEngine.Object> evt)
    {{
        {implementLine}    
    }}";
#endif
            }
        }
    }
}
