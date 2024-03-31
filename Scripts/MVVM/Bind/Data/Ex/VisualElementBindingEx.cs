using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public static class VisualElementBindingEx
{
    public static DataBinding Bind<T>(this TextField element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(TextField.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this TextField element, ViewModel<T> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<T>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(TextField.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this Label element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(Label.text), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this Label element, ViewModel<T> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<T>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(Label.text), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this IntegerField element, ViewModel<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                int.TryParse(value, out int result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref int value) => value.ToString());
        
        element.SetBinding(nameof(IntegerField.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this IntegerField element, ViewModel<int> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<int>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(IntegerField.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this IntegerField element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged) where T : class
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                int.TryParse(value, out int result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref int value) => value.ToString());
        
        element.SetBinding(nameof(IntegerField.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this IntegerField element, ViewModel<float> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<float>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(IntegerField.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this FloatField element, ViewModel<float> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<float>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(FloatField.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this FloatField element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged) where T : class
    {
            DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                float.TryParse(value, out float result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref float value) => value.ToString(CultureInfo.InvariantCulture));
        
        element.SetBinding(nameof(FloatField.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this FloatField element, ViewModel<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                float.TryParse(value, out float result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref float value) => value.ToString(CultureInfo.InvariantCulture));
        
        element.SetBinding(nameof(FloatField.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this FloatField element, ViewModel<int> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<int>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(FloatField.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this Slider element, ViewModel<int> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<int>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(Slider.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this Slider element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged) where T : class
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                float.TryParse(value, out float result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref float value) => value.ToString(CultureInfo.InvariantCulture));
        
        element.SetBinding(nameof(Slider.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this Slider element, ViewModel<float> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<float>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(Slider.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this Slider element, ViewModel<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                float.TryParse(value, out float result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref float value) => value.ToString(CultureInfo.InvariantCulture));
        
        element.SetBinding(nameof(Slider.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this SliderInt element, ViewModel<float> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<float>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(SliderInt.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this SliderInt element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged) where T : class
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                int.TryParse(value, out int result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref int value) => value.ToString());
        
        element.SetBinding(nameof(SliderInt.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this SliderInt element, ViewModel<int> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<int>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(SliderInt.value), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this SliderInt element, ViewModel<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(SliderInt.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this Toggle element, ViewModel<bool> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<bool>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(Toggle.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this Toggle element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged) where T : class
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                bool.TryParse(value, out bool result) && result);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref bool value) => value.ToString());
        element.SetBinding(nameof(Toggle.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this Toggle element, ViewModel<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) => bool.TryParse(value, out bool result) && result);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref bool value) => value.ToString());
        
        element.SetBinding(nameof(Toggle.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this ProgressBar element, ViewModel<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) => float.TryParse(value, out float result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref float value) => value.ToString(CultureInfo.InvariantCulture));
        
        element.SetBinding(nameof(ProgressBar.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this ProgressBar element, ViewModel<float> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<float>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(ProgressBar.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this ProgressBar element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged) where T : class
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                float.TryParse(value, out float result) ? result : 0);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref float value) => value.ToString(CultureInfo.InvariantCulture));
        
        element.SetBinding(nameof(ProgressBar.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this ProgressBar element, ViewModel<int> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<int>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(ProgressBar.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this DropdownField element, ViewModel<IList<string>> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<IList<string>>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(DropdownField.choices), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this DropdownField element, ViewModel<string[]> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string[]>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(DropdownField.choices), dataBinding);
        return dataBinding;
    }
    
    
    public static DataBinding Bind(this DropdownField element, IList<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<IList<string>>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(DropdownField.choices), dataBinding);
        return dataBinding;
    }
    public static DataBinding Bind(this DropdownField element, string[] viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string[]>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(DropdownField.choices), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this DropdownField element, ViewModel<string> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<string>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(DropdownField.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this DropdownField element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(typeof(T).Name == nameof(Int32) 
                ? nameof(DropdownField.index) 
                : nameof(DropdownField.value),
            dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this MinMaxSlider element, ViewModel<Vector2> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<Vector2>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(MinMaxSlider.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this MinMaxSlider element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(MinMaxSlider.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this MinMaxSlider element, ViewModel<Vector2Int> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<Vector2Int>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(MinMaxSlider.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this RadioButton element, ViewModel<bool> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<bool>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(RadioButton.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this RadioButton element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        if(bindingMode is BindingMode.ToTarget or BindingMode.TwoWay)
            dataBinding.sourceToUiConverters.AddConverter((ref string value) =>
                bool.TryParse(value, out bool result) && result);
        
        if(bindingMode is BindingMode.ToSource or BindingMode.TwoWay)
            dataBinding.uiToSourceConverters.AddConverter((ref bool value) => value.ToString());
        element.SetBinding(nameof(RadioButton.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind(this EnumField element, ViewModel<Enum> viewModel,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged)
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(nameof(ViewModel<Enum>.Value)),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        element.SetBinding(nameof(EnumField.value), dataBinding);
        return dataBinding;
    }
    
    public static DataBinding Bind<T>(this EnumField element, T viewModel, string path,
        BindingMode bindingMode = BindingMode.ToTarget,
        BindingUpdateTrigger updateTrigger = BindingUpdateTrigger.OnSourceChanged) where T : class
    {
        DataBinding dataBinding = new DataBinding
        {
            dataSource = viewModel,
            dataSourcePath = new PropertyPath(typeof(T).Name == typeof(ViewModel<>).Name ? $"Value.{path}" : path),
            bindingMode = bindingMode,
            updateTrigger = updateTrigger
        };
        
        element.SetBinding(nameof(EnumField.value), dataBinding);
        return dataBinding;
    }
}
