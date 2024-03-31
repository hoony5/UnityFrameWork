using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace Writer.AssetGenerator.UIElement
{
    public class UxmlItem<T> where T : VisualElement
    {
        /// <summary>
        ///   UIDocumentWriter uxml writer.
        /// </summary>
        private UXmlWriter uxmlWriter;
        /// <summary>
        ///   UIDocumentWriter uss writer.
        /// </summary>
        private USSWriter ussWriter;
        
        /// <summary>
        /// VisualElement instance.
        /// </summary>
        public readonly T element;
        
        /// <summary>
        /// children UxmlItems.
        /// </summary>
        private List<object> children; 
        
        /// <summary>
        /// uxml indent.
        /// </summary>
        public int indent = 1;
        
        public void SetAttribute(string name, object value)
        {
            uxmlWriter.SetAttribute<T>(name, value);
        }
        
        public void SetStyle(string name, object value)
        {
            uxmlWriter.SetStyle(name, value);
            ussWriter.SetStyle(name, value);
        }

        public void SetChild(object child)
        {
            children.Add(child);
        }

        public bool HaveChildren()
        {
            return children.Count != 0;
        }

        public bool IsSetUp()
        {
            return uxmlWriter.IsSetUp();
        }

        public void AdjustChildrenIndent()
        {
            if (children.Count == 0) return;
            foreach (object child in children)
            {
                Type childType = child.GetType();
                if (childType.Name != typeof(UxmlItem<>).Name) 
                    continue;
                
                // adjust child's
                childType.GetField("indent").SetValue(child, indent + 1);
            }
        }

        public string ToUXmlLine(StyleMode mode = StyleMode.Inline)
        {
            return uxmlWriter.ToUXmlLine<T>(indent, HaveChildren(), mode);
        }

        public string ToUXmlLines(StyleMode mode = StyleMode.Inline)
        {
            return uxmlWriter.ToUXmlLines<T>(indent, HaveChildren(), children, mode);
        }

        public string ToUssBlocks(string styleName, string selectors = "")
        {
            ussWriter.StyleName = styleName;
            ussWriter.Selectors = selectors;
            return ussWriter.WriteContent();
        }

        public void Clear()
        {
            children.Clear();
            uxmlWriter.Clear();
            ussWriter.Clear();
        }
        
        public UxmlItem(T visualElement)
        {
            element = visualElement;
            uxmlWriter = new UXmlWriter();
            ussWriter = new USSWriter();
            children = new List<object>();
        }
        public UxmlItem()
        {
            T visualElement = new VisualElement() as T;
            element = visualElement;
            uxmlWriter = new UXmlWriter();
            ussWriter = new USSWriter();
            children = new List<object>();
        }
    }
}