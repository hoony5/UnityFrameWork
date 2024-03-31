using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Writer.Core;
using Writer.Ex;
using Writer.SourceGenerator.Format;

namespace Writer.AssetGenerator.UIElement
{
    public class UXmlWriter : UIDocumentWriterBase
    {
        private Type elementType;
        private int indent;
        private bool haveChildren;
        private StyleMode mode;
        
        // type - attributes - styles
        /// <summary>
        /// attribute name with value. i.e {"bottom", "10px"} 
        /// </summary>
        private Dictionary<string, object> attributePairs;
        
        public UXmlWriter()
        {
            attributePairs = new Dictionary<string, object>();
        }
        
        public override string WriteContent()
        {
            string type = elementType.Name;
            string attributes =  GetAttributeLine(attributePairs.Select(pair => (pair.Key, pair.Value)).ToArray());
            string styles = mode switch
            {
                StyleMode.Inline => GetStyleLine(stylePairs.Select(pair => (pair.Key, pair.Value)).ToArray()),
                StyleMode.Uss => string.Empty,
                _ => string.Empty
            };

            UxmlLineFormatType formatType = haveChildren
                ? UxmlLineFormatType.AnyChild 
                : UxmlLineFormatType.NoChild;
            
            string result = ToUXmlLine(indent, formatType, type, attributes, styles);
            
            return result;
        }
        
        public void SetAttribute<T>(string name, object value)
        {
#if UNITY_EDITOR
            if (typeof(T) == typeof(UnityEditor.UIElements.ColorField))
            {
                Color color = (Color)value;
                value = color.ToRGBA();
            }
#endif
            
            attributePairs.Add(name, value.ToString());
        }
        
        public override void Clear()
        {
            attributePairs.Clear();
            base.Clear();
            
        }
        
        public string ToUXmlLine<T>(int indent, bool haveChildren, StyleMode mode = StyleMode.Inline)
        {
            elementType = typeof(T);
            this.indent = indent;
            this.haveChildren = haveChildren;
            this.mode = mode;
            
            return WriteContent();
        }
        public string ToUXmlLines<T>(int indent, bool haveChildren, List<object> children, StyleMode mode = StyleMode.Inline)
        {
            string current = ToUXmlLine<T>(indent, haveChildren, mode);
            // current
            if (children.Count == 0)
            {
                return current;
            }

            StringBuilder uxmlLines = new StringBuilder();
            uxmlLines.AppendLine(current);
            
            // next
            foreach (var child in children)
            {
                Type childType = child.GetType();
                string childLine = childType.GetMethod(nameof(ToUXmlLine))?.Invoke(child, new object[] { mode }).ToString();
                
                if (!(bool)childType.GetMethod("HaveChildren")?.Invoke(child, null)!)
                {
                    uxmlLines.AppendLine(childLine);
                    continue;
                }
                string grandChildren = childType.GetMethod(nameof(ToUXmlLines))?.Invoke(child, new object[] { mode }).ToString();
                // because this method last return AppendLine. so, it doesn't need new Line just append.
                uxmlLines.Append(grandChildren);
            }
            uxmlLines.AppendLine(ToCloseUXmlLine(indent, elementType.Name));
            return uxmlLines.ToString();
        }

        public bool IsSetUp()
        {
            return attributePairs != null && attributePairs.Count > 0;
        }
        
        private string GetFormat(UxmlLineFormatType type)
        {
            return type == UxmlLineFormatType.AnyChild 
                ? GetFormat(nameof(uxmlFormatStartWithChildren)) 
                : GetFormat(nameof(uxmlFormatStartDefault));
        }

        private string GetAttributeLine(params (string name, object value)[] attributes)
        {
            if(attributes.Length == 0) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach ((string name, object value) in attributes)
            {
                sb.AppendFormat(GetFormat(nameof(uxmlAttributesFormat)), name, value);
            }
            return sb.ToString();
        }

        private string GetStyleLine(params (string name, object value)[] styles)
        {
            if(styles.Length == 0) return string.Empty;
            StringBuilder sb = new StringBuilder();
            for (var index = 0; index < styles.Length; index++)
            {
                (string name, object value) = styles[index];
                sb.AppendFormat(GetFormat(nameof(uxmlStyleItemFormat)), name, value);
                if(index == styles.Length - 1) continue;
                sb.Append(space);
            }
            return string.Format(GetFormat(nameof(uxmlStyleFormat)), sb);
        }

        
        private string Indent(int count)
        {
            return string.Concat(Enumerable.Repeat(tab, count));
        }
        
        private string ToUXmlLine(int level, UxmlLineFormatType format, string type, string attributes, string styles)
        {
            string indent = Indent(level);
            string line = string.Format(GetFormat(format), type, attributes, styles);
            
            return string.Format(GetFormat(nameof(uxmlForamtDefulat)), indent, line);
        }

        private string ToCloseUXmlLine(int level, string type)
        {
            string indent = Indent(level);
            string line = $"</ui:{type}>";
            return string.Format(GetFormat(nameof(uxmlForamtDefulat)), indent, line);
        }
    }
}