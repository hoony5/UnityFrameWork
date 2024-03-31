using System.Collections.Generic;
using System.Text;
using Share;
using UnityEngine;

namespace Writer.AssetGenerator.UIElement
{
    public class UxmlGenerator
    {
        private static string header(bool containsEditorMode = false) => $@"<ui:UXML xmlns:ui=""UnityEngine.UIElements"" xmlns:uie=""UnityEditor.UIElements"" xsi=""http://www.w3.org/2001/XMLSchema-instance"" engine=""UnityEngine.UIElements"" editor=""UnityEditor.UIElements"" noNamespaceSchemaLocation=""../UIElementsSchema/UIElements.xsd"" editor-extension-mode=""{containsEditorMode}"">";
        private const string footer = "</ui:UXML>";
        private static string validatedTypeName => typeof(UxmlItem<>).Name;
        
        // alias, uxmlItems * parent
        private static Dictionary<string, object> uxmlItems;
        
        private static bool IsValidType(object item)
        {
            return item.GetType().Name == validatedTypeName;
        }
        
        public UxmlGenerator()
        {
            uxmlItems = new Dictionary<string, object>();
        }

        public static void SetUp()
        {
            uxmlItems = new Dictionary<string, object>();
        }
        
        public static void AddOrUpdate(string alias, object uxmlItem)
        {
            if (!IsValidType(uxmlItem)) return;
            uxmlItems.AddOrUpdate(alias, uxmlItem);
        }
        
        public static string GetUxmlItem(string alias)
        {
            return uxmlItems.TryGetValue(alias, out var item) ? item.ToString() : string.Empty;
        }
        
        public static void BatchAddOrUpdate(params (string alias, object uxmlItem)[] items)
        {
            foreach ((string alias, object uxmlItem) in items)
            {
                if (!IsValidType(uxmlItem)) continue;
                AddOrUpdate(alias, uxmlItem);
            }
        }
        
        public static string WriteUxmlByKey(string alias, bool containsEditorMode = false)
        {
            if(!uxmlItems.TryGetValue(alias, out object item))
                return $"{alias} is not found";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header(containsEditorMode));
            string contentLine = item.GetType().GetMethod("ToUXmlLines")?.Invoke(item, null).ToString();
            sb.Append(contentLine);
            sb.AppendLine(footer);
            return sb.ToString();
        }
        public static string WriteUxml(string content, bool containsEditorMode = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header(containsEditorMode));
            sb.Append(content);
            sb.AppendLine(footer);
            return sb.ToString();
        }

        public static Dictionary<string, string> BatchToUxmlByKeys(bool containsEditorMode, params string[] aliases)
        {
            if (aliases.Length == 0) return null;
            
            Dictionary<string, string> uxmls = new Dictionary<string, string>();
            foreach (string alias in aliases)
            {
                string uxml = WriteUxmlByKey(alias, containsEditorMode);
                if (uxmls.TryAdd(alias, uxml)) continue;
                
                Debug.LogWarning($"{alias} is already added");
            }
            return uxmls;
        }

        public static List<string> BatchToUxmls(bool containsEditorMode,params string[] contents)
        {
            if (contents.Length == 0) return null;
            
            List<string> uxmls = new List<string>();
            foreach (string content in contents)
            {
                string uxml = WriteUxml(content, containsEditorMode);
                if(uxmls.Contains(uxml))
                {
                    Debug.LogWarning($"{content} is already added");
                    continue;
                }
                uxmls.Add(uxml);
            }
            return uxmls;
        }
        
        public static async void ExportUxmlByKey( string path,string alias, bool containsEditorMode = false)
        {
            string uxml = WriteUxmlByKey(alias, containsEditorMode);
            if (string.IsNullOrEmpty(uxml)) return;
            
            await FileExporter.Write(path, uxml);
        }
        
        public static async void ExportUxml(string path, string content, bool containsEditorMode = false)
        {
            string uxml = WriteUxml(content, containsEditorMode);
            if (string.IsNullOrEmpty(uxml)) return;
            
            await FileExporter.Write(path, uxml);
        }
        
        public static async void ExportBatchUxmlByKeys(string path, bool containsEditorMode = false, params string[] aliases)
        {
            Dictionary<string, string> uxmls = BatchToUxmlByKeys(containsEditorMode, aliases);
            if (uxmls == null) return;
            await FileExporter.BatchWriteAsync(path, uxmls.Values);
        }
        
        public static async void ExportBatchUxmls(string path, bool containsEditorMode = false, params string[] contents)
        {
            List<string> uxmls = BatchToUxmls(containsEditorMode, contents);
            if (uxmls == null) return;
            await FileExporter.BatchWriteAsync(path, uxmls);
        }

        public void Clear()
        {
            uxmlItems.Clear();
        }
        
    }
}