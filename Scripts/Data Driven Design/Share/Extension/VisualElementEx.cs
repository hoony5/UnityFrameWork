using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;

#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace Share
{
    public static class VisualElementEx
    {
        public static void AddElements(this VisualElement root, params VisualElement[] elements)
        {
            if (elements == null || elements.Length == 0)
                return;
            
            elements.ForEach(element =>
            {
                if(!root.TryAdd(element))
                {
                    UnityEngine.Debug.LogError($"Element {element.name} already exists in {root.name}");
                }
            });
        }
        
        public static void RemoveElements(this VisualElement root, params VisualElement[] elements)
        {
            if (elements == null || elements.Length == 0)
                return;
            
            elements.ForEach(root.SafeRemove);
        }
        public static bool TryAdd(this VisualElement root, VisualElement element)
        {
            if (element == null) return false;
            if (root.Contains(element)) return false;
            
            root.Add(element);
            return true;
        }
        public static void SafeRemove(this VisualElement root, VisualElement element)
        {
            if (element == null) return;
            if (!root.Contains(element)) return;
            
            root.Remove(element);
        }
        public static VisualElement CreateVerticalSpace(this VisualElement root, float value)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetHeight(value);
            space.SetWidth(new StyleLength(new Length(100, LengthUnit.Percent)));
            root.Add(space);
            return space;
        }
        public static VisualElement CreateHorizontalSpace(this VisualElement root, float value)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetHeight(new StyleLength(new Length(100, LengthUnit.Percent)));
            space.SetWidth(value);
            root.Add(space);
            return space;
        }
        public static VisualElement CreateVerticalSpace(this VisualElement root, float value, Color color)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetBackgroundColor(color);
            space.SetHeight(value);
            space.SetWidth(new StyleLength(new Length(100, LengthUnit.Percent)));
            root.Add(space);
            return space;
        }
        public static VisualElement CreateHorizontalSpace(this VisualElement root, float value, Color color)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetHeight(new StyleLength(new Length(100, LengthUnit.Percent)));
            space.SetWidth(value);
            space.SetBackgroundColor(color);
            root.Add(space);
            return space;
        }
        public static VisualElement CreateVerticalBar(this VisualElement root, float thickness)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetHeight(new StyleLength(new Length(100, LengthUnit.Percent)));
            space.SetWidth(thickness);
            root.Add(space);
            return space;
        }
        public static VisualElement CreateHorizontalBar(this VisualElement root, float thickness)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetWidth(new StyleLength(new Length(100, LengthUnit.Percent)));
            space.SetHeight(thickness);
            root.Add(space);
            return space;
        }
        public static VisualElement CreateVerticalBar(this VisualElement root,float thickness, Color color)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetHeight(new StyleLength(new Length(100, LengthUnit.Percent)));
            space.SetWidth(thickness);
            space.SetBackgroundColor(color);
            root.Add(space);
            return space;
        }
        public static VisualElement CreateHorizontalBar(this VisualElement root, float thickness, Color color)
        {
            
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetWidth(new StyleLength(new Length(100, LengthUnit.Percent)));
            space.SetHeight(thickness);
            space.SetBackgroundColor(color);
            root.Add(space);
            return space;
        }

        public static VisualElement CreateSpace(float width, float height, Color color)
        {
            VisualElement space = new VisualElement();
            space.SetFlexGrow(0);
            space.SetWidth(width);
            space.SetHeight(height);
            space.SetBackgroundColor(color);
            return space;
        }
        public static VisualElement CreateSpace(float width, float height)
        {
            return CreateSpace(width, height, Color.clear);
        }

        public static Slider CreateSlider(float value, EventCallback<ChangeEvent<float>> callback = null)
        {
            Slider slider = new Slider
            {
                name = "slider",
                value = value,
                label = string.Empty,
                lowValue = 0,
                highValue = 1,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static Slider CreateSlider(float value, string label, EventCallback<ChangeEvent<float>> callback = null)
        {
            Slider slider = new Slider
            {
                name = "slider",
                value = value,
                label = label,
                lowValue = 0,
                highValue = 1,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static Slider CreateSlider(float value, float high, EventCallback<ChangeEvent<float>> callback = null)
        {
            Slider slider = new Slider
            {
                name = "slider",
                value = value,
                label = string.Empty,
                lowValue = 0,
                highValue = high,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static Slider CreateSlider(float value, float low, float high, EventCallback<ChangeEvent<float>> callback = null)
        {
            Slider slider = new Slider
            {
                name = "slider",
                value = value,
                label = string.Empty,
                lowValue = low,
                highValue = high,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static Slider CreateSlider(float value, string label, float low, float high,  EventCallback<ChangeEvent<float>> callback = null)
        {
            Slider slider = new Slider
            {
                name = "slider",
                value = value,
                label = label,
                lowValue = low,
                highValue = high,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }


        public static SliderInt CreateSliderInt(int value, EventCallback<ChangeEvent<int>> callback = null)
        {
            SliderInt slider = new SliderInt
            {
                name = "slider",
                value = value,
                label = string.Empty,
                lowValue = 0,
                highValue = 100,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static SliderInt CreateSliderInt(int value, string label, EventCallback<ChangeEvent<int>> callback = null)
        {
            SliderInt slider = new SliderInt
            {
                name = "slider",
                value = value,
                label = label,
                lowValue = 0,
                highValue = 100,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static SliderInt CreateSliderInt(int value, int high, EventCallback<ChangeEvent<int>> callback = null)
        {
            SliderInt slider = new SliderInt
            {
                name = "slider",
                value = value,
                label = string.Empty,
                lowValue = 0,
                highValue = high,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static SliderInt CreateSliderInt(int value, int low, int high, EventCallback<ChangeEvent<int>> callback = null)
        {
            SliderInt slider = new SliderInt
            {
                name = "slider",
                value = value,
                label = string.Empty,
                lowValue = low,
                highValue = high,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static SliderInt CreateSliderInt(int value, string label, int low, int high,  EventCallback<ChangeEvent<int>> callback = null)
        {
            SliderInt slider = new SliderInt
            {
                name = "slider",
                value = value,
                label = label,
                lowValue = low,
                highValue = high,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }

        public static ProgressBar CreateProgressBar(float value, EventCallback<ChangeEvent<float>> callback = null)
        {
            ProgressBar slider = new ProgressBar
            {
                name = "progress-bar",
                value = value,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }
        public static MinMaxSlider CreateMinMaxSlider(Vector2 value, float low, float high, EventCallback<ChangeEvent<Vector2>> callback = null)
        {
            MinMaxSlider slider = new MinMaxSlider
            {
                name = "min-max-slider",
                highLimit = high,
                lowLimit = low,
                value = value,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }
        public static MinMaxSlider CreateMinMaxSlider(Vector2 value, float high, EventCallback<ChangeEvent<Vector2>> callback = null)
        {
            MinMaxSlider slider = new MinMaxSlider
            {
                name = "min-max-slider",
                highLimit = high,
                lowLimit = 0,
                value = value,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };
            
            if (callback != null)
                slider.RegisterValueChangedCallback(callback);
            
            return slider;
        }
        public static TextField CreateTextField(string value, EventCallback<ChangeEvent<string>> callback = null)
        {
            TextField textField = new TextField
            {
                name = "text-field",
                value = value,
                label = string.Empty,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                textField.RegisterValueChangedCallback(callback);

            return textField;
        }

        public static TextField CreateTextField(string label, string value, EventCallback<ChangeEvent<string>> callback = null)
        {
            TextField valueField = new TextField
            {
                name = "text-field",
                value = value,
                label = label,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                valueField.RegisterValueChangedCallback(callback);

            return valueField;
        }
        public static TextField CreateTextField(string value,int width, int height, EventCallback<ChangeEvent<string>> callback = null)
        {
            TextField textField = new TextField
            {
                name = "text-field",
                value = value,
                label = string.Empty,
                style = 
                {
                    width = width,
                    height = height,
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                textField.RegisterValueChangedCallback(callback);

            return textField;
        }

        public static TextField CreateTextField(string label, string value, int width, int height, EventCallback<ChangeEvent<string>> callback = null)
        {
            TextField valueField = new TextField
            {
                name = "text-field",
                value = value,
                label = label,
                style =
                {
                    width = width,
                    height = height,
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                valueField.RegisterValueChangedCallback(callback);

            return valueField;
        }

        public static TextField CreateTextField(string value,int width, EventCallback<ChangeEvent<string>> callback = null)
        {
            TextField textField = new TextField
            {
                name = "text-field",
                value = value,
                label = string.Empty,
                style = 
                {
                    width = width,
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                textField.RegisterValueChangedCallback(callback);

            return textField;
        }

        public static TextField CreateTextField(string label, string value,int width, EventCallback<ChangeEvent<string>> callback = null)
        {
            TextField valueField = new TextField
            {
                name = "text-field",
                value = value,
                label = label,
                style =
                {
                    width = width,
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                valueField.RegisterValueChangedCallback(callback);

            return valueField;
        }

        public static IntegerField CreateIntField(int value, EventCallback<ChangeEvent<int>> callback = null)
        {
            IntegerField valueField = new IntegerField()
            {
                name = "int-field",
                value = value,
                label = string.Empty,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                valueField.RegisterValueChangedCallback(callback);

            return valueField;
        }

        public static IntegerField CreateIntField(string label, int value, EventCallback<ChangeEvent<int>> callback = null)
        {
            IntegerField valueField = new IntegerField
            {
                name = "int-field",
                value = value,
                label = label,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                valueField.RegisterValueChangedCallback(callback);

            return valueField;
        }

        public static FloatField CreateFloatField(float value, EventCallback<ChangeEvent<float>> callback = null)
        {
            FloatField valueField = new FloatField
            {
                name = "float-field",
                value = value,
                label = string.Empty,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                valueField.RegisterValueChangedCallback(callback);

            return valueField;
        }

        public static FloatField CreateFloatField(float value, string label, EventCallback<ChangeEvent<float>> callback = null)
        {
            FloatField valueField = new FloatField
            {
                name = "float-field",
                value = value,
                label = label,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                valueField.RegisterValueChangedCallback(callback);

            return valueField;
        }

        public static Label CreateLabel(string value, EventCallback<ChangeEvent<string>> callback = null)
        {
            Label label = new Label
            {
                name = "label",
                text = value,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                },
                enableRichText = true
            };
            
            if (callback != null)
                label.RegisterValueChangedCallback(callback);
            
            return label;
        }
        public static Label CreateLabel(string value, int height , EventCallback<ChangeEvent<string>> callback = null)
        {
            Label label = new Label
            {
                name = "label",
                text = value,
                style = 
                {
                    height = height,
                    flexGrow = 1,
                    flexShrink = 0,
                },
                enableRichText = true
            };
            
            if (callback != null)
                label.RegisterValueChangedCallback(callback);
            
            return label;
        }
        public static TextField CreateTextArea(string value, EventCallback<ChangeEvent<string>> callback = null, int height = 25)
        {
            TextField textArea = CreateTextField(value, callback);

            textArea.multiline = true;
            textArea.style.flexGrow = 1;
            textArea.style.flexShrink = 0;
            textArea.style.height = height;
            return textArea;
        }
        public static TextField CreateReadonlyTextArea(string value, EventCallback<ChangeEvent<string>> callback = null, int height = 25)
        {
            TextField textArea = CreateTextField(value, callback);

            textArea.multiline = true;
            textArea.style.flexGrow = 1;
            textArea.style.flexShrink = 0;
            textArea.isReadOnly = true;
            textArea.style.height = height;
            return textArea;
        }

        public static Foldout CreateFoldout(string title, bool isCollapsed = false)
        {
            Foldout foldout = new Foldout
            {
                name = "foldout",
                text = title,
                value = !isCollapsed
            };
            return foldout;
        }

        public static EnumField CreateEnumField<TEnum>(TEnum value,
            EventCallback<ChangeEvent<Enum>> callback = null) where TEnum : Enum
        {
            EnumField enumField = new EnumField(value)
            {
                name = "enum-field",
                label = string.Empty,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                enumField.RegisterValueChangedCallback(callback);

            return enumField;
        }

        public static EnumField CreateEnumField<TEnum>(TEnum value, string label,
            EventCallback<ChangeEvent<Enum>> callback = null) where TEnum : Enum
        {
            EnumField enumField = new EnumField(value)
            {
                name = "enum-field",
                label = label
            };

            if (callback != null)
                enumField.RegisterValueChangedCallback(callback);

            return enumField;
        }
        public static PopupField<TType> CreatePopupField<TType>(List<TType> options,
            EventCallback<ChangeEvent<TType>> callback = null)
        {
            PopupField<TType> popupField = new PopupField<TType>
            {
                name = "popup-field",
                label = string.Empty,
                index = 0,
                choices = options,
                value = options[0],
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                popupField.RegisterValueChangedCallback(callback);

            return popupField;
        }
        public static PopupField<TType> CreatePopupField<TType>(string label, List<TType> options,
            EventCallback<ChangeEvent<TType>> callback = null)
        {
            PopupField<TType> popupField = new PopupField<TType>
            {
                name = "popup-field",
                label = label,
                choices = options,
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                popupField.RegisterValueChangedCallback(callback);

            return popupField;
        }
        public static DropdownField CreateDropdownField(List<string> options,
            EventCallback<ChangeEvent<string>> callback = null)
        {
            options ??= new List<string>();

            if (options.Count == 0)
                options.Add(string.Empty);

            DropdownField dropdownField = new DropdownField
            {
                name = "dropdown-field",
                label = string.Empty,
                choices = options,
                index = 0,
                value = options[0],
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                dropdownField.RegisterValueChangedCallback(callback);

            return dropdownField;
        }

        public static DropdownField CreateDropdownField(List<string> options, int selected,
            EventCallback<ChangeEvent<string>> callback = null)
        {
            options ??= new List<string>();

            if (options.Count == 0)
                options.Add(string.Empty);

            if (options.Count <= selected)
                selected = 0;

            DropdownField dropdownField = new DropdownField
            {
                name = "dropdown-field",
                label = string.Empty,
                choices = options,
                index = selected,
                value = options[selected],
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                dropdownField.RegisterValueChangedCallback(callback);

            return dropdownField;
        }


        public static DropdownField CreateDropdownField(string label, List<string> options, int selected,
            EventCallback<ChangeEvent<string>> callback = null)
        {
            options ??= new List<string>();

            if (options.Count == 0)
                options.Add(string.Empty);

            if (options.Count <= selected)
                selected = 0;

            DropdownField dropdownField = new DropdownField
            {
                name = "dropdown-field",
                label = label,
                choices = options,
                index = selected,
                value = options[selected],
                style = 
                {
                    flexGrow = 1,
                    flexShrink = 0,
                }
            };

            if (callback != null)
                dropdownField.RegisterValueChangedCallback(callback);

            return dropdownField;
        }

        public static Button CreateButton(Action onClick = null)
        {
            return new Button(onClick)
            {
                name = "button"
            };
        }
        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                name = "button",
                text = text
            };

            return button;
        }

        public static Toggle CreateToggle(bool value, EventCallback<ChangeEvent<bool>> callback = null)
        {
            Toggle toggle = new Toggle
            {
                name = "toggle",
                label = string.Empty,
                value = value
            };

            if (callback != null)
                toggle.RegisterValueChangedCallback(callback);

            return toggle;
        }

        public static Toggle CreateToggle(string label, bool value, EventCallback<ChangeEvent<bool>> callback = null)
        {
            Toggle toggle = new Toggle
            {
                name = "toggle",
                label = label,
                value = value
            };

            if (callback != null)
                toggle.RegisterValueChangedCallback(callback);

            return toggle;
        }

        public static GroupBox CreateGroupBox(string label, IEnumerable<VisualElement> contents,
            FlexDirection direction = FlexDirection.Row)
        {
            GroupBox groupBox = new GroupBox(label)
            {
                name = "group-box"
            };
            foreach (VisualElement element in contents)
            {
                groupBox.Add(element);
            }

            groupBox.style.flexDirection = direction;
            return groupBox;
        }

        public static ListView CreateListView<T>(List<T> items, Func<VisualElement> makeItem,
            Action<VisualElement, int> bindItem)
        {
            ListView listView = new ListView
            {
                name = "list-view",
                makeItem = makeItem,
                bindItem = bindItem,
                itemsSource = items,
                reorderable = true,
            };
            return listView;
        }

        public static void SetDirty(this VisualElement element)
        {
            element.MarkDirtyRepaint();
        }

        public static VisualElement SetGroup(string parentName, params VisualElement[] elements)
        {
            VisualElement group = new VisualElement
            {
                name = parentName
            };
            foreach (VisualElement element in elements)
            {
                group.Add(element);
            }

            return group;
        }

        public static VisualElement[] SetUngroup(VisualElement element)
        {
            if (element.childCount == 0)
                return Array.Empty<VisualElement>();

            VisualElement[] elements = Enumerable
                .Repeat(new VisualElement(), element.childCount)
                .ToArray();
            for (int i = 0; i < element.childCount; i++)
            {
                elements[i] = element[i];
            }

            element.Clear();
            return elements;
        }
        
#if UNITY_EDITOR
        public static ObjectField CreateObjectField<TObject>(TObject value,
            EventCallback<ChangeEvent<UnityEngine.Object>> callback = null) where TObject : UnityEngine.Object
        {
            ObjectField objectField = new ObjectField
            {
                name = "object-field",
                objectType = typeof(TObject),
                value = value,
            };

            if (callback != null)
                objectField.RegisterValueChangedCallback(callback);

            return objectField;
        }
        public static Toolbar CreateToolbar(string title, TextField titleTextField, params (string name, Action action)[] buttons)
        {
            Toolbar toolbar = new Toolbar()
            {
                name = "toolbar"
            };

            if (titleTextField != null)
            {
                titleTextField = CreateTextField(title, "Name: ");
                toolbar.Add(titleTextField);
            }

            if (buttons?.Length > 0)
            {
                foreach ((string name, Action action) button in buttons)
                {
                    Button newButton = CreateButton(button.name, button.action);
                    toolbar.Add(newButton);
                }   
            }
            return toolbar;
        }
#endif
    }
}