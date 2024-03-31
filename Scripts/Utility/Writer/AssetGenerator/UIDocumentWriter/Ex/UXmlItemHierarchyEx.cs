using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Writer.AssetGenerator.UIElement;

namespace Writer.AssetGenerator.UIElement
{
    public static class UXmlItemHierarchyEx
    {
        // only for editor, so no need remove
        public static UxmlItem<T> Add<T>(this UxmlItem<T> parent, params object[] children) where T : VisualElement
        {
            foreach (object child in children)
            {
                Type elementType = child.GetType();

                string compared = typeof(UxmlItem<>).Name;
                bool isUxmlItem = elementType.GetRootType().Name == compared;
                if (!isUxmlItem) continue;
                 
                // set child indent + 1
                elementType.GetField("indent").SetValue(child, parent.indent + 1);
                
                // child set parent in real UIElement hierarchy
                VisualElement childElement = elementType.GetField("element").GetValue(child) as VisualElement;
                parent.element.Add(childElement);
                
                // parent set child UxmlItem
                parent.SetChild(child);
                
                // child set child's children indent if exists
                elementType.GetMethod("AdjustChildrenIndent")?.Invoke(child, null);
            }
            
            return parent;
        }
    }

}